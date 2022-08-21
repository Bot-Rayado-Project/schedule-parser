using Parser.Core;
using HtmlAgilityPack;

namespace Parser;

public class ParserWorker<T> where T : class
{
    #region Properties

    public IParser<T> Parser { get; set; }
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

    private CancellationTokenSource s_cts;
    public event Action<object, T> OnNewData;

    public ParserWorker(IParser<T> parser)
    {
        s_cts = new CancellationTokenSource();
        Parser = parser;
    }

    public ParserWorker(IParser<T> parser, IParserSettings settings) : this(parser)
    {
        Settings = settings;
    }

    public void Start()
    {
        if (s_cts.IsCancellationRequested)
        {
            s_cts.Dispose();
            s_cts = new CancellationTokenSource();
        }
        Worker(s_cts.Token);
    }

    public async Task StartAsync()
    {
        if (s_cts.IsCancellationRequested)
        {
            s_cts.Dispose();
            s_cts = new CancellationTokenSource();
        }
        await Worker(s_cts.Token);
    }

    public void Stop()
    {
        s_cts.Cancel();
    }

    private async Task Worker(CancellationToken token)
    {
        var source = await htmlLoader.GetHtmlAsync(token);

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(source);

        var links = htmlParser.Parse(htmlDocument);

        var linksInfo = linksParser.Parse(links);

        var tablesPaths = await tablesDownloader.DownloadTablesAsync(linksInfo, token);

        foreach (var kvp in tablesPaths)
        {
            var package = await tableLoader.OpenTableAsync(kvp.Key, token);
            var result = Parser.Parse(package, kvp.Value);
            OnNewData.Invoke(this, result);
        }
    }
}
