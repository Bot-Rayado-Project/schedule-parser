using OfficeOpenXml;
using Parser.Core.Models;

namespace Parser;

public interface IParser
{
    string[] Parse(ExcelPackage package, TableInfo tableInfo);
}