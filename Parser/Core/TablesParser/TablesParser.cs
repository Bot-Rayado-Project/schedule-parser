using OfficeOpenXml;

namespace Parser.Core.TablesParser;

internal class TablesParser
{
    private int WorksheetsAmmount { get; set; }

    public async Task OpenTable(string filePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(filePath);
        await package.LoadAsync(filePath);
        WorksheetsAmmount = package.Workbook.Worksheets.Count;
        // print info about workbook
        System.Console.WriteLine("Worksheets ammount: " + WorksheetsAmmount);

        for (var i = 0; i < WorksheetsAmmount; i++)
        {
            var ws = package.Workbook.Worksheets[i];
            System.Console.WriteLine(ws.Name);
            //ws.MergedCells.ToList().ForEach(x => System.Console.WriteLine(x));

            for (int col = 1; col < 6; col++)
            {
                for (int row = 1; row < 62; row++)
                {
                    //if (!string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString())) System.Console.WriteLine($"Row: {row} Col: {col} Value: \n" + ws.Cells[row, col].Value?.ToString());
                }
            }
        }
    }
}
