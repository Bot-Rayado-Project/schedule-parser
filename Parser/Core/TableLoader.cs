using OfficeOpenXml;

namespace Parser.Core;

class TableLoader
{
    public async Task<ExcelPackage> OpenTable(string filePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var package = new ExcelPackage(filePath);
        await package.LoadAsync(filePath);

        return package;
    }
}