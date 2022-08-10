using OfficeOpenXml;

namespace Parser.Core.ScheduleParser;

public class ScheduleParser : IParser
{
    public void Parse(ExcelPackage package)
    {
        int wsCount = package.Workbook.Worksheets.Count;
        Console.WriteLine("Worksheets ammount: " + wsCount);
    }
}
