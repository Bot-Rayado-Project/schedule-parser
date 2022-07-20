namespace Parser;

public interface IParserWorker
{
    ParserStates State { get; }
    void RunForever();
    void RunOnce();
}

