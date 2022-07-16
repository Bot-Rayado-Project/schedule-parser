using Parser.Core;

namespace Parser;

public class ParserWorker : IParserWorker
{
    private readonly IHtmlParser htmlParser = new HtmlParser();
    public bool IgnoreErrors { get; set; } = true;
    private bool FirstStart { get; set; } = true;
    private bool isRunForever { get; set; } = false;

    public void RunForever()
    {
        _RunForever().Wait();
    }

    private async Task _RunForever()
    {
        FirstStart = true;
        isRunForever = true;
        if (IgnoreErrors)
        {
            while (true)
            {
                try
                {
                    await _Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in event loop {ex}");
                    await Task.Delay(330);
                }
            }
        }
    }
    private async Task _Run()
    {
        if (FirstStart)
        {
            System.Console.WriteLine("here");
            FirstStart = false;
            var res = await htmlParser.GetTablesLinksAsync("https://mtuci.ru/time-table/");
            res.ForEach(x => Console.WriteLine(x));
        }
        else
        {
            System.Console.WriteLine("here 2");
            await Task.Delay(5000);
            var res = await htmlParser.GetTablesLinksAsync("https://mtuci.ru/time-table/");
            res.ForEach(x => Console.WriteLine(x));
        }
    }
}
