using Parser.Core.HtmlParser;
using Parser.Core.TablesDownloader;
using Parser.Core.LinkHandler;
using Parser.Core.TablesParser;

namespace Parser;

public class ParserWorker : IParserWorker
{
    private readonly IHtmlParser htmlParser = new HtmlParser();
    private readonly ITablesDownloader tablesDownloader = new TablesDownloader();
    private readonly ILinkHandler linkHandler = new LinkHandler();
    private readonly TablesParser tablesParser = new TablesParser();
    private bool FirstStart { get; set; } = true;
    private string MtuciUrl { get; set; }
    private bool IgnoreErrors { get; set; }
    private int Delay { get; set; }
    private CancellationTokenSource s_cts = new CancellationTokenSource();
    public ParserStates State { get; internal set; } = ParserStates.isStopped;

    /// <summary>
    /// Class constructor. Need to pass ParserSettings objects to initialize parser.
    /// </summary>
    /// <param name="parserSettings"></param>
    public ParserWorker(ParserSettings parserSettings)
    {
        MtuciUrl = parserSettings.MtuciUrl;
        IgnoreErrors = parserSettings.IgnoreErrors;
        Delay = parserSettings.Delay;
    }

    /// <summary>
    /// Main method that forces parser to run forever.
    /// The method itself is not awaited and is creating a task to run on background over main flow.
    /// </summary>
    public void RunForever() => RunForeverAsync();

    /// <summary>
    /// Starts parser for only one iteration
    /// </summary>
    public void RunOnce() => RunOnceAsync();

    /// <summary>
    /// Stops running tasks with cancelation token
    /// </summary>
    public void Stop() { System.Console.WriteLine("Stop command detected. Cancelling task..."); s_cts.Cancel(); State = ParserStates.isStopped; }

    /// <summary>
    /// Async version of RunForever
    /// </summary>
    private async Task RunForeverAsync()
    {
        if (s_cts.IsCancellationRequested) { s_cts.Dispose(); s_cts = new CancellationTokenSource(); }
        FirstStart = true;
        State = ParserStates.isRunningForever;
        if (IgnoreErrors)
        {
            while (true)
            {
                try { await _RunForeverAsync(s_cts.Token); }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in event loop {ex}");
                    await Task.Delay(330);
                }
            }
        }
        else
        {
            while (true) await _RunForeverAsync(s_cts.Token);
        }
    }

    /// <summary>
    /// Low level method of RunForeverAsync
    /// </summary>
    private async Task _RunForeverAsync(CancellationToken token)
    {
        if (FirstStart)
        {
            FirstStart = false;
            await Run(token);
        }
        else
        {
            System.Console.WriteLine($"Another start detected, waiting for {Delay} seconds...");
            State = ParserStates.isSuspended;
            await Task.Delay(Delay, token);
            while (State == ParserStates.isRunningOnce) { System.Console.WriteLine("Cannot start another pass since is running once"); await Task.Delay(10000, token); }
            await Run(token);
        }
    }

    /// <summary>
    /// Async version of RunOnce
    /// </summary>
    private async Task RunOnceAsync()
    {
        if (s_cts.IsCancellationRequested) { s_cts.Dispose(); s_cts = new CancellationTokenSource(); }
        State = ParserStates.isRunningOnce;
        if (IgnoreErrors)
        {
            try
            {
                await _RunOnceAsync(s_cts.Token);
            }
            catch (TaskCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in running once {ex}");
                await Task.Delay(330, s_cts.Token);
            }
            finally
            {
                State = ParserStates.isStopped;
            }
        }
    }

    /// <summary>
    /// Low level method of RunOnceAsync
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task _RunOnceAsync(CancellationToken token)
    {
        await Run(token);
    }

    /// <summary>
    /// Main run method implementing parser logic that is being used in _RunForeverAsync and _RunOnceAsync
    /// </summary>
    private async Task Run(CancellationToken token)
    {
        List<string> links = await htmlParser.GetTablesLinksAsync("https://mtuci.ru/time-table/", token);
        Dictionary<string, List<string>> linksInfo = new Dictionary<string, List<string>>();
        foreach (var link in links)
        {
            linksInfo.Add(link, await linkHandler.GetLinkInfo(link, token));
        }
        await tablesDownloader.DownloadTables(linksInfo, token);
        //await tablesParser.OpenTable("tables/1_it_02.03.02.xlsx");
    }
}
