namespace Parser.Core.ScheduleParser;

public class ScheduleParserSettings : IParserSettings
{
    public string Url { get; set; } = "https://mtuci.ru/time-table/";

    public bool IgnoreErrors { get; set; } = true;

    public int Delay { get; set; }

    public string DownloadPath { get; set; }

    public string EmailAdress { get; set; }

    public string EmailPassword { get; set; }

    public ScheduleParserSettings(string emailAdress, string emailPassword, bool ignoreErrors = true, int delay = 84000, string downloadPath = "./tables")
    {
        IgnoreErrors = ignoreErrors;
        Delay = delay;
        DownloadPath = downloadPath;
        EmailAdress = emailAdress;
        EmailPassword = emailPassword;
    }
}
