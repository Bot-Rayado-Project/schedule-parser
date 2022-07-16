using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Parser.Core;

public class HtmlParser : IHtmlParser
{
    private static readonly HttpClient httpClient = new HttpClient();
    public async Task<List<string>> GetTablesLinksAsync(string url)
    {
        return await ParseHtml(url);
    }
    private async Task<string> GetHtmlAsync(string url)
    {
        var res = await httpClient.GetStringAsync(url);
        return res;
    }
    private async Task<List<string>> ParseHtml(string url)
    {
        List<string> tablesList = new List<string>();
        HtmlDocument htmlDoc = new HtmlDocument();
        Uri baseUri = new Uri(url);

        string html = await GetHtmlAsync(url);
        htmlDoc.LoadHtml(html);
        HtmlNodeCollection linkNodes = htmlDoc.DocumentNode.SelectNodes("//h4/a");
        foreach (var link in linkNodes)
        {
            string href = link.Attributes["href"].Value;
            string _href = href.ToLower();
            if (!_href.Contains("ekzamenov")
                && !_href.Contains("mag")
                && _href.Contains(".xlsx")
                && !_href.Contains("tszopb")
                && !_href.Contains("tszopm")
                && Regex.Match(_href, @"\d*\D*kurs").Success
                && Regex.Match(_href, @"\d*\.\d*\.\d*").Success) tablesList.Add(new Uri(baseUri, href).AbsoluteUri);
        }
        return tablesList;
    }
}
