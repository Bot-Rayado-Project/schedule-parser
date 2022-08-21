namespace Parser.Core.ScheduleParser;

public class ScheduleParserSettings : IParserSettings
{
    public string Url { get; set; }

    public string DownloadPath { get; set; }

    public ScheduleParserSettings(string url, string downloadPath)
    {
        Url = url;
        DownloadPath = downloadPath;
    }
}
