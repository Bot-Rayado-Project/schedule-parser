namespace Parser;

public interface IParserWorker
{
    IParserSettings Settings { get; set; }
    public event Action<object, string[]> OnNewData;
    void Start();
    Task StartAsync();
    void Stop();
}

