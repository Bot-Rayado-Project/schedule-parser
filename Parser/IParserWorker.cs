namespace Parser;

public interface IParserWorker
{
    IParserSettings Settings { get; set; }
    void Start();
    Task StartAsync();
    void Stop();
}

