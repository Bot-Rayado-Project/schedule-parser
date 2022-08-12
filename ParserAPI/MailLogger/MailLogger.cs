using System.Net.Mail;
using System.Net;

public class MailLogger : ILogger, IDisposable
{
    SmtpClient smtpClient;
    string _adress;
    string _password;
    static object _lock = new object();
    public MailLogger(string adress, string password)
    {
        _adress = adress;
        _password = password;
        smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(adress, password),
            EnableSsl = true,
        };
    }
    public IDisposable BeginScope<TState>(TState state)
    {
        return this;
    }

    public void Dispose() { }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId,
                TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_lock)
        {
            smtpClient.Send(_adress, _adress, "Error in Schedule Parser", formatter(state, exception));
        }
    }
}