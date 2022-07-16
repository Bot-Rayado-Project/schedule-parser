using HtmlAgilityPack;

namespace parser;

public static class TableDownloader
{
    private static HtmlDocument GetDocument(string url)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(url);
        return doc;
    }

    public static List<string> GetBookLinks(string url)
    {
        var bookLinks = new List<string>();
        HtmlDocument doc = GetDocument(url);
        HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//h4/a");
        var baseUri = new Uri(url);
        foreach (var link in linkNodes)
        {
            string href = link.Attributes["href"].Value;
            bookLinks.Add(new Uri(baseUri, href).AbsoluteUri);
        }
        return bookLinks;
    }
}
