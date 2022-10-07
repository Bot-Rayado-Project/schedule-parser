using OfficeOpenXml;
using Parser.Core.Models;
using Newtonsoft.Json;

namespace Parser.Core.ScheduleParser;

public class ScheduleParser : IParser<Dictionary<string, Dictionary<int, Dictionary<DayOfWeek, string>>>>
{
    private const int StartRow = 17;
    private const int StartTimeCol = 3;
    private const int StartAudCol = 4;
    private const int StartTypeCol = 5;
    private const int StartLecturerCol = 6;
    private const int StartPairCol = 7;

    public Dictionary<string, Dictionary<int, Dictionary<DayOfWeek, string>>> Parse(ExcelPackage package, TableInfo tableInfo)
    {
        Dictionary<string, Dictionary<int, Dictionary<DayOfWeek, string>>> result = new();

        int wsCount = package.Workbook.Worksheets.Count;

        for (int i = 0; i < wsCount; i++)
        {
            var ws = package.Workbook.Worksheets[i];

            Dictionary<int, Dictionary<DayOfWeek, string>> weeksInfo = new();

            for (int j = 1; j < 2; j++)
            {
                int startRow = StartRow;

                Dictionary<DayOfWeek, Dictionary<int, (string?, string?, string?, string?, string?)>> daysInfo = new();
                Dictionary<DayOfWeek, string> daysCompiled = new();

                foreach (DayOfWeek dow in Enum.GetValues(typeof(DayOfWeek)))
                {
                    if (dow == 0)
                        continue;
                    System.Console.WriteLine(((DayOfWeek)dow).ToString());

                    Dictionary<int, (string?, string?, string?, string?, string?)> pairs = new();
                    int pairCounter = 1;

                    for (int row = startRow; row < startRow + 5; row++, pairCounter++)
                    {
                        string? time = ws.Cells[row, StartTimeCol].Value?.ToString();
                        string? auditory = ws.Cells[row, StartAudCol].Value?.ToString();
                        string? type = ws.Cells[row, StartTypeCol].Value?.ToString();
                        string? lecturer = ws.Cells[row, StartLecturerCol].Value?.ToString();
                        string? pair = ws.Cells[row, StartPairCol].Value?.ToString();

                        pairs.Add(pairCounter, (time, auditory, type, lecturer, pair));
                    }
                    daysInfo.Add((DayOfWeek)dow, pairs);
                    startRow += 6;
                }
                foreach (var day in daysInfo)
                {
                    string schedule = @$"
⸻⸻⸻⸻⸻
Группа: {ws.Name}
День недели: {((DayOfWeek)day.Key).ToString()}
Неделя: Нечетная
⸻⸻⸻⸻⸻
1_PAIR
⸻⸻⸻⸻⸻
2_PAIR
⸻⸻⸻⸻⸻
3_PAIR
⸻⸻⸻⸻⸻
4_PAIR
⸻⸻⸻⸻⸻
5_PAIR
⸻⸻⸻⸻⸻
                                     ";
                    foreach (var pair in day.Value)
                    {
                        string composedPair;

                        int pairNumber = pair.Key;
                        (string?, string?, string?, string?, string?) pairInfo = pair.Value;
                        var _pair = pairInfo.Item5 ?? "";
                        composedPair = _pair == "" ? "Пары нет" : @$"
{pairInfo.Item1}
{pairInfo.Item5}
Преподаватель: {pairInfo.Item4}
Аудитория: {pairInfo.Item2}
Тип пары: {pairInfo.Item3}
                                                                          ";
                        schedule = schedule.Replace(Convert.ToString(pairNumber) + "_PAIR", composedPair);
                    }
                    daysCompiled.Add((DayOfWeek)day.Key, schedule);
                    System.Console.WriteLine(schedule);
                }
                weeksInfo.Add(j, daysCompiled);
            }
            result.Add(ws.Name, weeksInfo);
        }
        return result;
    }
}
