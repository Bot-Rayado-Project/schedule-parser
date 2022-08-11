using Parser.Core.ScheduleParser;
using Parser;

IParserWorker parser = new ParserWorker(new ScheduleParser(), new ScheduleParserSettings());

parser.RunForever();

Console.ReadLine();
