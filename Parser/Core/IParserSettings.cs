namespace Parser;

public interface IParserSettings
{
    public string Url { get; set; }
    public bool IgnoreErrors { get; set; }
    public int Delay { get; set; }
    public string DownloadPath { get; set; }

}
