using System.Text.RegularExpressions;

namespace Parser.Core;

class LinksParser
{
    readonly string[] faculties = new string[] { "it", "siss", "kiib", "rit", "tseimk" };
    readonly Regex gradeRx = new Regex(@"\d\D*kurs",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);
    readonly Regex streamRx = new Regex(@"\d+\.\d+\.\d+",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public Dictionary<string, string[]> Parse(string[] links)
    {
        Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();

        foreach (var link in links)
        {
            var linkInfo = GetLinkInfo(link);
            dictionary.Add(link, linkInfo);
        }

        return dictionary;
    }

    private string[] GetLinkInfo(string url)
    {
        var grade = GetGrade(url);
        var faculty = GetFaculty(url);
        var stream = GetStream(url);
        return new string[] { grade, faculty, stream };
    }

    private string GetGrade(string url)
    {
        MatchCollection matches = gradeRx.Matches(url);
        if (matches.Count != 1) throw new Exception($"Found more than 1 grade patterns in link {url}");
        return matches[0].Value.Substring(0, 1);
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
