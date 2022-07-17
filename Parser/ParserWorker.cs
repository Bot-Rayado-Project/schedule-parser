using Parser.Core.HtmlParser;
using Parser.Core.TablesDownloader;
using Parser.Core.LinkHandler;

namespace Parser;

public class ParserWorker : IParserWorker
{
    private readonly IHtmlParser htmlParser = new HtmlParser();
    private readonly ITablesDownloader tablesDownloader = new TablesDownloader();
    private readonly ILinkHandler linkHandler = new LinkHandler();
    public bool IgnoreErrors { get; set; } = true;
    private bool FirstStart { get; set; } = true;
    private bool isRunForever { get; set; } = false;

    public void RunForever()
    {
        _RunForever().Wait();
    }

    private async Task _RunForever()
    {
        FirstStart = true;
        isRunForever = true;
        if (IgnoreErrors)
        {
            while (true)
            {
                try
                {
                    await _Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in event loop {ex}");
                    await Task.Delay(330);
                }
            }
        }
    }
    private async Task _Run()
    {
        if (FirstStart)
        {
            FirstStart = false;
            List<string> links = await htmlParser.GetTablesLinksAsync("https://mtuci.ru/time-table/");
            Dictionary<string, List<string>> linksInfo = new Dictionary<string, List<string>>();
            foreach (var link in links)
            {
                linksInfo.Add(link, await linkHandler.GetLinkInfo(link));
            }
            await tablesDownloader.DownloadTables(linksInfo);
        }
        else
        {
            await Task.Delay(5000);
            List<string> links = await htmlParser.GetTablesLinksAsync("https://mtuci.ru/time-table/");
            Dictionary<string, List<string>> linksInfo = new Dictionary<string, List<string>>();
            foreach (var link in links)
            {
                linksInfo.Add(link, await linkHandler.GetLinkInfo(link));
            }
            await tablesDownloader.DownloadTables(linksInfo);
        }
    }
}
