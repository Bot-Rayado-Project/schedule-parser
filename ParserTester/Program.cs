using Parser.Core.ScheduleParser;
using Parser;

ParserWorker parser = new ParserWorker(new ScheduleParser());

parser.Settings = new ScheduleParserSettings(Environment.GetEnvironmentVariable("EmailAdress"), Environment.GetEnvironmentVariable("EmailPassword"));

parser.RunForever();

Console.ReadLine();
