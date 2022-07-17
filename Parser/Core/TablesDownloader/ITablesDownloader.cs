namespace Parser.Core.TablesDownloader;

internal interface ITablesDownloader
{
    Task DownloadTables(Dictionary<string, List<string>> linksInfo);
}
