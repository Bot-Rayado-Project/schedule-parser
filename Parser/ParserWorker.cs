using Parser.Core;
using HtmlAgilityPack;

namespace Parser;

public class ParserWorker
{
    #region Properties

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
        Worker();
    }

    private async Task Worker()
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
    }
}
