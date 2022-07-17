namespace Parser.Core.LinkHandler;

internal interface ILinkHandler
{
    Task<List<string>> GetLinkInfo(string url);
}
