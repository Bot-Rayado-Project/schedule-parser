using OfficeOpenXml;
using Parser.Core.Models;
using Newtonsoft.Json;

namespace Parser.Core.ScheduleParser;

public class ScheduleParser : IParser<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<int, string?>>>>>
{
    private Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>>? jsonFile;

    public ScheduleParser()
    {
        jsonFile = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>>>(File.ReadAllText("streamsinfo.json"));
    }

    public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<int, string?>>>> Parse(ExcelPackage package, TableInfo tableInfo)
    {
        Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<int, string?>>>> result = new();
        Dictionary<int, Dictionary<int, string?>?> evenDays = new() {
            {1, null},
            {2, null},
            {3, null},
            {4, null},
            {5, null}
        };
        Dictionary<int, Dictionary<int, string?>?> oddDays = new(){
            {1, null},
            {2, null},
            {3, null},
            {4, null},
            {5, null}
        }; ;
        Dictionary<int, string?> oddPairs = new() {
            {1, null},
            {2, null},
            {3, null},
            {4, null},
            {5, null}
        };
        Dictionary<int, string?> evenPairs = new() {
            {1, null},
            {2, null},
            {3, null},
            {4, null},
            {5, null}
        };

        var listOfWorksheets = jsonFile[Convert.ToString(tableInfo.Grade)][tableInfo.Faculty][tableInfo.Stream];

        foreach (var worksheet in listOfWorksheets)
        {
            // Set active worksheet
            var ws = package.Workbook.Worksheets[Convert.ToInt32(worksheet.Key) - 1];
            var listOfGroups = worksheet.Value;
            foreach (var group in listOfGroups)
            {
                int counter = 0;
                int startRow = Convert.ToInt32(new String(group.Value.Where(Char.IsDigit).ToArray()));
                string col = new String(group.Value.Where(Char.IsLetter).ToArray());
                for (int i = 1; i < 5; i++)
                {
                    for (int row = startRow; row < startRow + 10; row++)
                    {
                        counter++;
                        string cell = col + Convert.ToString(row);
                        var cellData = ws.Cells[cell].Text;
                        Console.WriteLine(cellData);
                        if (counter % 2 != 0)
                        {
                            oddPairs[Convert.ToInt32(Math.Ceiling((double)counter / 2))] = cellData;
                        }
                        else
                        {
                            evenPairs[Convert.ToInt32(Math.Ceiling((double)counter / 2))] = cellData;
                        }
                    }
                    oddDays[i] = oddPairs;
                    evenDays[i] = evenPairs;
                    startRow += 11;
                }
            }
        }

        return result;
    }
}
