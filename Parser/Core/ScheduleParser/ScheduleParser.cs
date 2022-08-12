using OfficeOpenXml;

namespace Parser.Core.ScheduleParser;

public class ScheduleParser : IParser
{
    public string[] Parse(ExcelPackage package)
    {
        int wsCount = package.Workbook.Worksheets.Count;
        Console.WriteLine("Worksheets ammount: " + wsCount);
        // TODO
        return Array.Empty<string>();
    }
}
