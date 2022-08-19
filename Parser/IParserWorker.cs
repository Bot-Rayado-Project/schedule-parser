namespace Parser;

public interface IParserWorker
{
    IParserSettings Settings { get; set; }
    public event Action<object, Dictionary<string, List<Dictionary<int, Dictionary<int, string?>?>>>> OnNewData;
    void Start();
    Task StartAsync();
    void Stop();
}

