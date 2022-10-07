namespace Parser;

public interface IParserSettings
{
    public string Url { get; set; }
    public string DownloadPath { get; set; }
    public Dictionary<string, Dictionary<string, int>> StreamsMatchesFaculties { get; set; }
}
