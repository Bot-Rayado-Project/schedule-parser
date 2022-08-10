namespace Parser;

public interface IParserWorker
{
    public IParserSettings Settings { get; set; }
    public ParserStates State { get; }
    public void RunForever();
    public void RunOnce();
    public void Stop();
}

