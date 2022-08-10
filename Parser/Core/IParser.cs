using OfficeOpenXml;

namespace Parser;

public interface IParser
{
    void Parse(ExcelPackage package);
}