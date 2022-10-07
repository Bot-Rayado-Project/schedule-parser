using OfficeOpenXml;
using Parser.Core.Models;
using Newtonsoft.Json;

namespace Parser.Core.ScheduleParser;

public class ScheduleParser : IParser<string[]>
{
    private const int StartRow = 17;
    private const int StartTimeCol = 3;
    private const int StartAudCol = 4;
    private const int StartTypeCol = 5;
    private const int StartLecturerCol = 6;
    private const int StartPairCol = 7;

    public string[] Parse(ExcelPackage package, TableInfo tableInfo)
    {
        List<string> result = new();

        int wsCount = package.Workbook.Worksheets.Count;

        for (int i = 0; i < wsCount; i++)
        {
            // Set active worksheet
            var ws = package.Workbook.Worksheets[i];

            int startRow = StartRow;

            Dictionary<DayOfWeek, Dictionary<int, (string?, string?, string?, string?, string?)>> daysInfo = new();

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
                result.Add(schedule);
                System.Console.WriteLine(schedule);
            }
        }
        return result.ToArray();
    }
}
