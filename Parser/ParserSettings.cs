namespace Parser;

public class ParserSettings : IParserSettings
{
    public string Url { get; set; } = "https://mtuci.ru/time-table/";

    public bool IgnoreErrors { get; set; } = true;

    public int Delay { get; set; } = 84000;
}
