namespace Parser.Core;

public interface IHtmlParser
{
    Task<List<string>> GetTablesLinksAsync(string url);
}
