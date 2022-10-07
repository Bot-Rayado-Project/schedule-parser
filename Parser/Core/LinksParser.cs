using System.Text.RegularExpressions;
using Parser.Core.Models;

namespace Parser.Core;

class LinksParser
{
    readonly string[] faculties = new string[] { "it", "siss", "kiib", "rit", "tseimk" };
    readonly Dictionary<string, string[]> streamMatchesFaculties = new()
    {
        {"it", new string[] {"bfi", "bvt", "bst", "bei"} },
        {"kiib", new string[] {"bap", "bpm", "but", "zrs", "bib"} },
        {"rit", new string[] {"brt", "bik"} },
        {"tseimk", new string[] {"bee", "ber", "bbi"} },
        {"siss", new string[] {"bin"} }
    };
    readonly Regex gradeRx = new Regex(@"\d\D*kurs",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);
    readonly Regex streamRx = new Regex(@"\d+\.\d+\.\d+",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public Dictionary<string, TableInfo> Parse(string[] links)
    {
        Dictionary<string, TableInfo> dictionary = new();

        foreach (var link in links)
        {
            var linkInfo = GetLinkInfo(link);
            dictionary.Add(link, linkInfo);
        }

        return dictionary;
    }

    private TableInfo GetLinkInfo(string url)
    {
        TableInfo tableInfo = new TableInfo()
        {
            Grade = GetGrade(url),
            Faculty = GetFaculty(url),
            Stream = GetStream(url)
        };
        return tableInfo;
    }

    private int GetGrade(string url)
    {
        MatchCollection matches = gradeRx.Matches(url);
        if (matches.Count != 1) throw new Exception($"Found more than 1 grade patterns in link {url}");
        return Convert.ToInt32(matches[0].Value.Substring(0, 1));
    }

    private string GetFaculty(string url)
    {
        string _url = url.ToLower();
        foreach (string faculty in faculties)
        {
            if (_url.Contains(faculty)) return faculty;
        }
        throw new Exception($"Faculty not found in link {url}");
    }

    private string GetStream(string url)
    {
        MatchCollection matches = streamRx.Matches(url);
        if (matches.Count != 1) throw new Exception($"Found more than 1 stream patterns in link {url}\n{matches.Count}");
        return matches[0].Value;
    }

}
