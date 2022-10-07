namespace Parser.Core.ScheduleParser;

public class ScheduleParserSettings : IParserSettings
{
    public string Url { get; set; }

    public string DownloadPath { get; set; }

    public Dictionary<string, Dictionary<string, int>> StreamsMatchesFaculties { get; set; }

    public ScheduleParserSettings(string url, string downloadPath, Dictionary<string, Dictionary<string, int>> streamsMatchesFaculties)
    {
        Url = url;
        DownloadPath = downloadPath;
        StreamsMatchesFaculties = streamsMatchesFaculties;
    }
}
