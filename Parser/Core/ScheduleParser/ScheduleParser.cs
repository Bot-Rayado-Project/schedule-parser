using OfficeOpenXml;
using Parser.Core.Models;

namespace Parser.Core.ScheduleParser;

public class ScheduleParser : IParser<Dictionary<string, Dictionary<int, Dictionary<DayOfWeekRussian, string>>>>
{
    private const int StartRow = 17;
    private const int StartTimeCol = 3;
    private const int StartAudCol = 4;
    private const int StartTypeCol = 5;
    private const int StartLecturerCol = 6;
    private const int StartPairCol = 7;

    public Dictionary<string, Dictionary<int, Dictionary<DayOfWeekRussian, string>>> Parse(ExcelPackage package, TableInfo tableInfo)
    {
        Dictionary<string, Dictionary<int, Dictionary<DayOfWeekRussian, string>>> result = new();

        int wsCount = package.Workbook.Worksheets.Count;

        for (int i = 0; i < wsCount; i++)
        {
            var ws = package.Workbook.Worksheets[i];

            Dictionary<int, Dictionary<DayOfWeekRussian, string>> weeksInfo = new();

            for (int j = 1; j <= 2; j++)
            {
                int startRow = StartRow;
                int startAudCol = j == 2 ? 11 : StartAudCol;
                int startTypeCol = j == 2 ? 10 : StartTypeCol;
                int startLecturerCol = j == 2 ? 9 : StartLecturerCol;
                int startPairCol = j == 2 ? 8 : StartPairCol;

                Dictionary<DayOfWeekRussian, Dictionary<int, (string?, string?, string?, string?, string?)>> daysInfo = new();
                Dictionary<DayOfWeekRussian, string> daysCompiled = new();

                foreach (DayOfWeekRussian dow in Enum.GetValues(typeof(DayOfWeekRussian)))
                {
                    if (dow == 0)
                        continue;
                    // System.Console.WriteLine(((DayOfWeekRussian)dow).ToString());

                    Dictionary<int, (string?, string?, string?, string?, string?)> pairs = new();
                    int pairCounter = 1;

                    for (int row = startRow; row < startRow + 5; row++, pairCounter++)
                    {
                        string? time = ws.Cells[row, StartTimeCol].Value?.ToString();
                        string? auditory = ws.Cells[row, startAudCol].Value?.ToString();
                        string? type = ws.Cells[row, startTypeCol].Value?.ToString();
                        string? lecturer = ws.Cells[row, startLecturerCol].Value?.ToString();
                        string? pair = ws.Cells[row, startPairCol].Value?.ToString();

                        pairs.Add(pairCounter, (time, auditory, type, lecturer, pair));
                    }
                    daysInfo.Add((DayOfWeekRussian)dow, pairs);
                    startRow += 5;
                }
                foreach (var day in daysInfo)
                {
                    string parity = j == 1 ? "Нечетная" : "Четная";
                    string schedule = "1_PAIR\n⸻⸻⸻⸻⸻\n2_PAIR\n⸻⸻⸻⸻⸻\n3_PAIR\n⸻⸻⸻⸻⸻\n4_PAIR\n⸻⸻⸻⸻⸻\n5_PAIR\n⸻⸻⸻⸻⸻\n";
                    foreach (var pair in day.Value)
                    {
                        string composedPair;

                        int pairNumber = pair.Key;
                        (string?, string?, string?, string?, string?) pairInfo = pair.Value;
                        var _pair = pairInfo.Item5 ?? "";
                        composedPair = _pair == "" ? "Пары нет" : $"{pairInfo.Item1}\n{pairInfo.Item5}\nПреподаватель: {pairInfo.Item4}\nАудитория: {pairInfo.Item2}\nТип пары: {pairInfo.Item3}";
                        schedule = schedule.Replace(Convert.ToString(pairNumber) + "_PAIR", composedPair);
                    }
                    daysCompiled.Add((DayOfWeekRussian)day.Key, schedule);
                    // System.Console.WriteLine(schedule);
                }
                weeksInfo.Add(j, daysCompiled);
                // System.Console.WriteLine(j);
            }
            result.Add(ws.Name, weeksInfo);
        }
        return result;
    }
}
