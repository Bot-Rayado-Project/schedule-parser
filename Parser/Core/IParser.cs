using OfficeOpenXml;
using Parser.Core.Models;

namespace Parser;

public interface IParser<T> where T: class
{
    T Parse(ExcelPackage package, TableInfo tableInfo);
}