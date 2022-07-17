using System.Text.RegularExpressions;

namespace Parser.Core.LinkHandler;

public class LinkHandler : ILinkHandler
{
    readonly string[] faculties = new string[] { "it", "siss", "kiib", "rit", "tseimk" };
    readonly Regex gradeRx = new Regex(@"\d\D*kurs",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);
    readonly Regex streamRx = new Regex(@"\d+\.\d+\.\d+",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Method that handles passed link and returns grade, faculty and stream.
    /// </summary>
    /// <param name="url"></param>
    /// <returns>List<string></returns>
    public async Task<List<string>> GetLinkInfo(string url)
    {
        var grade = await Task.Run(() => GetGrade(url));
        var faculty = await Task.Run(() => GetFaculty(url));
        var stream = await Task.Run(() => GetStream(url));
        return new List<string>() { grade, faculty, stream };
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
