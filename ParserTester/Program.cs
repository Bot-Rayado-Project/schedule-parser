using Parser.Core.ScheduleParser;
using Parser;

ParserWorker parser = new ParserWorker(new ScheduleParser());

parser.Settings = new ScheduleParserSettings();

parser.RunForever();

Console.ReadLine();
