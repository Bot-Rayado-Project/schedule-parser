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

        foreach (var faculty in streamsMatchesFaculties.Values)
        {
            foreach (var stream in faculty.Keys)
            {
                List<string> tempList = new();
                Dictionary<string, string> tempListFileNames = new();
                foreach (var link in linkNodes)
                {
                    string href = link.Attributes["href"].Value;
                    string _href = href.ToLower();
                    if (_href.Contains(stream + "21"))
                    {
                        string uri = new Uri(baseUri, href).AbsoluteUri;
                        tempList.Add(uri);
                        tempListFileNames.Add(uri, uri.Split('/').ToList<string>().Last<string>());
                        // System.Console.WriteLine(uri.Split('/').ToList<string>().Last<string>());
                    }
                }
                var uniqueValues = tempListFileNames.GroupBy(pair => pair.Value)
                         .Select(group => group.Last())
                         .ToDictionary(pair => pair.Key, pair => pair.Value);
                foreach (var pair in uniqueValues)
                {
                    // System.Console.WriteLine(pair.Key + " " + pair.Value);
                    list.Add(pair.Key);
                }
            }
        }
        return list.ToArray();
    }
}
