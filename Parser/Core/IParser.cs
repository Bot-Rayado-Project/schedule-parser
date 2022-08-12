using OfficeOpenXml;

namespace Parser;

public interface IParser
{
    string[] Parse(ExcelPackage package);
}