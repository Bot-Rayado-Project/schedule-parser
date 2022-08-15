using OfficeOpenXml;
using Parser.Core.Models;

namespace Parser.Core.ScheduleParser;

public class ScheduleParser : IParser
{

    public string[] Parse(ExcelPackage package, TableInfo tableInfo)
    {
        int wsCount = package.Workbook.Worksheets.Count;
        Console.WriteLine("Worksheets ammount: " + wsCount);

        if (tableInfo.Grade == 2 && tableInfo.Faculty == "it" && tableInfo.Stream == "02.03.02")
        {
            var sheet = package.Workbook.Worksheets["Лист1"];
            // ЛЕНЬ ДАЛЬШЕ ДЕЛАТЬ 😢😥😓🙁☹️😭😭
        }

        return Array.Empty<string>();
    }
}
