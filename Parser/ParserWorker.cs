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
    public void RunForever()
    {
        RunForeverAsync();
    }

    public void RunOnce()
    {
        //RunOnceAsync();
    }

    /// <summary>
    /// Async version of RunForever
    /// </summary>
    /// <param name="ignoreErrors"></param>
    /// <returns></returns>
    private async Task RunForeverAsync()
    {
        FirstStart = true;
        State = ParserStates.isRunningForever;
        if (IgnoreErrors)
        {
            while (true)
            {
                try { await _RunForeverAsync(); }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in event loop {ex}");
                    await Task.Delay(330);
                }
            }
        }
        else
        {
            while (true) await _RunForeverAsync();
        }
    }

    /// <summary>
    /// Low level method of RunForeverAsync
    /// </summary>
    /// <returns></returns>
    private async Task _RunForeverAsync()
    {
        if (FirstStart)
        {
            FirstStart = false;
            await Run();
        }
        else
        {
            System.Console.WriteLine($"Another start detected, waiting for {Delay} seconds...");
            await Task.Delay(Delay);
            await Run();
        }
    }

    /// <summary>
    /// Main run method implementing parser logic that is being used in _RunForeverAsync and _RunOnceAsync
    /// </summary>
    /// <returns></returns>
    private async Task Run()
    {
        List<string> links = await htmlParser.GetTablesLinksAsync("https://mtuci.ru/time-table/");
        Dictionary<string, List<string>> linksInfo = new Dictionary<string, List<string>>();
        foreach (var link in links)
        {
            linksInfo.Add(link, await linkHandler.GetLinkInfo(link));
        }
        await tablesDownloader.DownloadTables(linksInfo);
        //await tablesParser.OpenTable("tables/1_it_02.03.02.xlsx");
    }
}
