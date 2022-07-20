namespace Parser;

public class ParserSettings
{
    public string MtuciUrl { get; set; } = "https://mtuci.ru/time-table/";
    public bool IgnoreErrors { get; set; } = true;
    public int Delay { get; set; } = 84000;
}
