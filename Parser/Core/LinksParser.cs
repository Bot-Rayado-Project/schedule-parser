using System.Text.RegularExpressions;
using Parser.Core.Models;

namespace Parser.Core;

class LinksParser
{
    // readonly Regex gradeRx = new Regex(@"\d\D*kurs",
    //       RegexOptions.Compiled | RegexOptions.IgnoreCase);
    // readonly Regex streamRx = new Regex(@"\d+\.\d+\.\d+",
    //       RegexOptions.Compiled | RegexOptions.IgnoreCase);
    readonly Dictionary<string, Dictionary<string, int>> streamsMatchesFaculties;

    public LinksParser(IParserSettings settings)
    {
        streamsMatchesFaculties = settings.StreamsMatchesFaculties;
    }

    public Dictionary<string, TableInfo> Parse(string[] links)
    {
        Dictionary<string, TableInfo> dictionary = new();

        Dictionary<string, int> tempDict = new();

        Dictionary<string, int> fileNameToGroupsIds = new();

        foreach (var link in links)
        {
            string fileName = link.Split('/').ToList<string>().Last<string>();
            var ammount = CountGroupsAmmountOnFile(fileName);
            foreach (var kvp in ammount)
            {
                if (tempDict.ContainsKey(kvp.Key))
                    tempDict[kvp.Key] += kvp.Value;
                else
                    tempDict[kvp.Key] = kvp.Value;
            }
        }
        foreach (var item in tempDict)
        {
            System.Console.WriteLine(item.Key + " " + item.Value);
            foreach (var faculty in streamsMatchesFaculties)
            {
                foreach (var stream in faculty.Value)
                {
                    if (stream.Key == item.Key && stream.Value != item.Value)
                        throw new Exception(stream.Key + " = " + item.Value + " does not equal to config value " + stream.Value);
                }
            }
        }
        foreach (var link in links)
        {
            string fileName = link.Split('/').ToList<string>().Last<string>();
            var groupFromAndGroupTo = CountGroupFromAndGroupTo(fileName);
            foreach (var faculty in streamsMatchesFaculties)
            {
                foreach (var stream in faculty.Value)
                {
                    if (stream.Key == fileName.Substring(0, 3).ToLower())
                    {
                        TableInfo tableInfo = new()
                        {
                            Grade = 2,
                            Faculty = faculty.Key,
                            Stream = stream.Key,
                            GroupFrom = groupFromAndGroupTo[0],
                            GroupTo = groupFromAndGroupTo[1]
                        };
                        dictionary.Add(link, tableInfo);
                    }
                }
            }
        }
        return dictionary;
    }

    // private TableInfo GetLinkInfo(string url)
    // {
    //     TableInfo tableInfo = new TableInfo()
    //     {
    //         Grade = GetGrade(url),
    //         Faculty = GetFaculty(url),
    //         Stream = GetStream(url)
    //     };
    //     return tableInfo;
    // }

    // private int GetGrade(string url)
    // {
    //     MatchCollection matches = gradeRx.Matches(url);
    //     if (matches.Count != 1) throw new Exception($"Found more than 1 grade patterns in link {url}");
    //     return Convert.ToInt32(matches[0].Value.Substring(0, 1));
    // }

    // private string GetFaculty(string url)
    // {
    //     string _url = url.ToLower();
    //     // foreach (string faculty in faculties)
    //     // {
    //     //     if (_url.Contains(faculty)) return faculty;
    //     // }
    //     throw new Exception($"Faculty not found in link {url}");
    // }

    // private string GetStream(string url)
    // {
    //     MatchCollection matches = streamRx.Matches(url);
    //     if (matches.Count != 1) throw new Exception($"Found more than 1 stream patterns in link {url}\n{matches.Count}");
    //     return matches[0].Value;
    // }

    private Dictionary<string, int> CountGroupsAmmountOnFile(string fileName)
    {
        var streamName = fileName.Substring(0, 3).ToLower();
        if (!fileName.Contains('_'))
            return new Dictionary<string, int>() { { streamName, 1 } };
        fileName = fileName.Substring(3).Replace(".xlsx", "");
        var _str = fileName.Split('_').ToList<string>();
        for (int i = 0; i < _str.Count; i++)
        {
            if (!_str[i].Contains("21"))
                _str[i] = "21" + _str[i];
        }
        return new Dictionary<string, int>() { { streamName, (Convert.ToInt32(_str[1]) - Convert.ToInt32(_str[0])) + 1 } };
    }
    private int[] CountGroupFromAndGroupTo(string fileName)
    {
        var streamName = fileName.Substring(0, 3).ToLower();
        if (!fileName.Contains('_'))
            return new int[2] { 1, 1 };
        fileName = fileName.Substring(3).Replace(".xlsx", "");
        var _str = fileName.Split('_').ToList<string>();
        for (int i = 0; i < _str.Count; i++)
        {
            if (!_str[i].Contains("21"))
                _str[i] = "21" + _str[i];
        }
        return new int[2] { Convert.ToInt32(_str[0].Substring(_str[0].Length - 3, _str[0].Length - 1).Substring(1)),
                            Convert.ToInt32(_str[1].Substring(_str[1].Length - 3, _str[1].Length - 1).Substring(1)) };
    }
}
