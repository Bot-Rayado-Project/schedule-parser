namespace Parser.Core.HtmlParser;

internal interface IHtmlParser
{
    Task<List<string>> GetTablesLinksAsync(string url, CancellationToken token);
}
