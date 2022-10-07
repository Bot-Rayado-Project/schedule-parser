using Parser.Core.Models;

namespace Parser;

internal class TablesDownloader
{
    readonly HttpClient httpClient;
    readonly string downloadPath;

    public TablesDownloader(IParserSettings settings)
    {
        httpClient = new HttpClient();
        downloadPath = settings.DownloadPath;
    }

    public async Task<Dictionary<string, TableInfo>> DownloadTablesAsync(Dictionary<string, TableInfo> linksInfo, CancellationToken token)
    {
        var dict = new Dictionary<string, TableInfo>();

        if (!Directory.Exists(downloadPath)) Directory.CreateDirectory(downloadPath);

        foreach (KeyValuePair<string, TableInfo> kvp in linksInfo)
        {
            string filePath = $"{downloadPath}/{kvp.Value.Grade}_{kvp.Value.Faculty}_{kvp.Value.Stream}_{kvp.Value.GroupFrom}_{kvp.Value.GroupTo}.xlsx";
            if (File.Exists(filePath)) File.Delete(filePath);
            using (var stream = await httpClient.GetStreamAsync(kvp.Key))
            {
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await stream.CopyToAsync(fileStream);
                    long length = new FileInfo(filePath).Length;
                    dict.Add(filePath, kvp.Value);
                    Console.WriteLine($"{kvp.Key}, Grade = {kvp.Value.Grade}, Faculty = {kvp.Value.Faculty}, Stream = {kvp.Value.Stream}, File length = {length}");
                }
            }
        }

        return dict;
    }
}
