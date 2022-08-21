using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Parser.Core;

class HtmlParser
{
    readonly string url;
    readonly Uri baseUri;

    public HtmlParser(IParserSettings settings)
    {
        url = settings.Url;
        baseUri = new Uri(url);
    }
    public string[] Parse(HtmlDocument htmlDocument)
    {
        List<string> list = new();

        HtmlNodeCollection linkNodes = htmlDocument.DocumentNode.SelectNodes("//h4/a");

        foreach (var link in linkNodes)
        {
            string href = link.Attributes["href"].Value;
            string _href = href.ToLower();

            if (!_href.Contains("ekzamenov")
                && !_href.Contains("mag")
                && _href.Contains(".xlsx")
                && !_href.Contains("tszopb")
                && !_href.Contains("tszopm")
                && Regex.Match(_href, @"2\D*kurs").Success
                && Regex.Match(_href, @"\d+\.\d+\.\d+").Success)
            {
                list.Add(new Uri(baseUri, href).AbsoluteUri);
            }
        }

        return list.ToArray();
    }
}
