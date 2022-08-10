using Parser.Core;
using HtmlAgilityPack;

namespace Parser;

public class ParserWorker : IParserWorker
{
    #region Properties

    public ParserStates State { get; private set; } = ParserStates.isStopped;
    public IParser Parser { get; set; }
    private HtmlLoader htmlLoader;
    private HtmlParser htmlParser;
    private LinksParser linksParser;
    private TablesDownloader tablesDownloader;
    private TableLoader tableLoader;
    private IParserSettings settings;
    public IParserSettings Settings
    {
        get
        {
            return settings;
        }
        set
        {
            settings = value;
            htmlLoader = new HtmlLoader(value);
            htmlParser = new HtmlParser(value);
            linksParser = new LinksParser();
            tablesDownloader = new TablesDownloader(value);
            tableLoader = new TableLoader();
        }
    }

    #endregion

    public ParserWorker(IParser parser)
    {
        Parser = parser;
    }

    public ParserWorker(IParser parser, IParserSettings settings) : this(parser)
    {
        Settings = settings;
    }
    public void RunForever()
    {
        _RunForever();
    }

    public async void _RunForever()
    {
        if (State == ParserStates.isStopped)
        {
            State = ParserStates.isRunningForever;

            if (!Settings.IgnoreErrors)
            {
                while (State == ParserStates.isRunningForever)
                {
                    await Worker();
                }
            }

            while (State == ParserStates.isRunningForever)
            {
                try
                {
                    await Worker();
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine("An error occured in event loop: " + ex);
                }
            }
        }
    }
    public void RunOnce()
    {
        if (State == ParserStates.isStopped || State == ParserStates.isSuspended)
        {
            State = ParserStates.isRunningOnce;
            Worker(true);
        }
    }

    public void Stop()
    {
        State = ParserStates.isStopped;
    }

    private async Task Worker(bool isRunOnce = false)
    {
        var source = await htmlLoader.GetHtmlAsync();

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(source);

        var links = htmlParser.Parse(htmlDocument);

        var linksInfo = linksParser.Parse(links);

        var tablesPaths = await tablesDownloader.DownloadTablesAsync(linksInfo);

        foreach (var path in tablesPaths)
        {
            System.Console.WriteLine(path);
            var package = await tableLoader.OpenTable(path);
            Parser.Parse(package);
        }

        if (!isRunOnce)
        {
            State = ParserStates.isSuspended;
            System.Console.WriteLine($"Sleeping for {Settings.Delay} seconds...");
            await Task.Delay(Settings.Delay);
        }
        else State = ParserStates.isStopped;
    }
}
