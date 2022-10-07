using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Parser.Core;

class HtmlParser
{
    readonly string url;
    readonly Uri baseUri;
    readonly Dictionary<string, Dictionary<string, int>> streamsMatchesFaculties;

    public HtmlParser(IParserSettings settings)
    {
        url = settings.Url;
        baseUri = new Uri(url);
        streamsMatchesFaculties = settings.StreamsMatchesFaculties;
    }
    public string[] Parse(HtmlDocument htmlDocument)
    {
        List<string> list = new();

        HtmlNodeCollection linkNodes = htmlDocument.DocumentNode.SelectNodes("//h4/a");

        foreach (var link in linkNodes)
        {
            string href = link.Attributes["href"].Value;
            string _href = href.ToLower();

            foreach (var faculty in streamsMatchesFaculties.Values)
            {
                foreach (var stream in faculty.Keys)
                {
                    if (_href.Contains(stream + "21"))
                        list.Add(new Uri(baseUri, href).AbsoluteUri);
                }
            }
        }

        return list.ToArray();
    }
}
