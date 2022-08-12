public class MailLoggerProvider : ILoggerProvider
{
    string adress;
    string password;
    public MailLoggerProvider(string adress, string password)
    {
        this.adress = adress;
        this.password = password;
    }
    public ILogger CreateLogger(string categoryName)
    {
        return new MailLogger(adress, password);
    }

    public void Dispose() { }
}