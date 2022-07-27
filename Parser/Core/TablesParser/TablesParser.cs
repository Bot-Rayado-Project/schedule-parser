using OfficeOpenXml;

namespace Parser.Core.TablesParser;

internal class TablesParser
{
    private readonly Dictionary<DayOfWeek, List<string>> AllDaysOfWeekVariations = new() {
        {DayOfWeek.Monday, new List<string> {"п о н е д е л ь н и к", "понедельник", "понеделн", "понедел", "пон", "пн", "понеделн.", "понедел.", "пон.", "пн."}},
        {DayOfWeek.Tuesday, new List<string> {"в т о р н и к", "вторник", "вторн", "втор", "вт", "вторн.", "втор.", "вт."}},
        {DayOfWeek.Wednesday, new List<string> {"с р е д а", "среда", "сред", "ср", "сред.", "ср."}},
        {DayOfWeek.Thursday, new List<string> {"ч е т в е р г", "четверг", "четвер", "четв", "чет", "чт", "четвер.", "четв.", "чет.", "чт."}},
        {DayOfWeek.Friday, new List<string> {"п я т н и ц а", "пятница", "пятниц", "пятн", "пят", "пт", "пятниц.", "пятн.", "пят.", "пт."}},
        {DayOfWeek.Saturday, new List<string> {"с у б б о т а", "суббота", "суббот", "субб", "суб", "сб", "суббот.", "субб.", "суб.", "сб."}},
    };

    private int WsCount { get; set; }

    public async Task Parse(string filePath)
    {
        var package = await OpenTable(filePath);
        WsCount = package.Workbook.Worksheets.Count;
        Console.WriteLine("Worksheets ammount: " + WsCount);

        var DaysOfWeek = await FindDaysOfWeek(package);
    }
    private async Task<List<Dictionary<DayOfWeek, (int, int)>>> FindDaysOfWeek(int wsCount)
    {
        List<Dictionary<DayOfWeek, (int, int)>> DaysOfWeek = new();
        
        for (var i = 0; i < wsCount; i++)
        {
            var ws = package.Workbook.Worksheets[i]; System.Console.WriteLine("Worksheet name: " + ws.Name);
            var mergedCells = ws.MergedCells.ToList();

            // Информация о ячейках дней недели
            Dictionary<DayOfWeek, (int, int)> DaysOfWeekCells = new();

            // Первый проход на поиск дней недели
            // Обеспечивают выход из цикла вовремя
            int outerExitCounter = 0;
            bool isStoppedOnCol = false;
            for (int col = 1; !isStoppedOnCol; col++)
            {
                bool isStoppedOnRow = false;
                int innerOuterExitCounter = 0;
                int innerExitCounter = 0;
                for (int row = 1; !isStoppedOnRow; row++)
                {
                    if (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()))
                    {
                        innerExitCounter++;
                        if (innerExitCounter > 60)
                        {
                            isStoppedOnRow = true;
                        }
                        if (row < 60 + 1 && innerExitCounter != 1)
                        {
                            innerOuterExitCounter++;
                            if (innerOuterExitCounter > 58) outerExitCounter++;
                        }
                        else innerOuterExitCounter = 0;
                        // Вторичная логика
                    }
                    else
                    {
                        innerExitCounter = 0;
                        innerOuterExitCounter = 0;

                        // Основная логика
                        if (ws.Cells[row, col].Value is string)
                        {
                            foreach (KeyValuePair<DayOfWeek, List<string>> keyValuePair in AllDaysOfWeekVariations)
                            {
                                if (keyValuePair.Value.Contains(ws.Cells[row, col].Value.ToString().ToLower()))
                                {
                                    foreach (string item in mergedCells)
                                    {
                                        string[] _item = item.Split(':');
                                        if (_item.Contains(ws.Cells[row, col].Address))
                                        {
                                            DaysOfWeekCells.Add(keyValuePair.Key, (row, col));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (outerExitCounter > 10) isStoppedOnCol = true;
            }
            foreach (KeyValuePair<DayOfWeek, (int, int)> keyValuePair in DaysOfWeekCells)
            {
                System.Console.WriteLine("key = {0}, value = {1}", keyValuePair.Key, keyValuePair.Value);
            }
            DaysOfWeek.Add(DaysOfWeekCells);
        }
        return DaysOfWeek;
    }

    private async Task<ExcelPackage> OpenTable(string filePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var package = new ExcelPackage(filePath);
        await package.LoadAsync(filePath);

        return package;
    }
}
