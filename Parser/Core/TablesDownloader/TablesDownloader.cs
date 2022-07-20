namespace Parser.Core.TablesDownloader;

internal class TablesDownloader : ITablesDownloader
{
    private static readonly HttpClient httpClient = new HttpClient();
    public async Task DownloadTables(Dictionary<string, List<string>> linksInfo)
    {
        if (!Directory.Exists("./tables")) Directory.CreateDirectory("./tables");
        foreach (KeyValuePair<string, List<string>> kvp in linksInfo)
        {
            string filePath = $"./tables/{kvp.Value[0]}_{kvp.Value[1]}_{kvp.Value[2]}.xlsx";
            if (File.Exists(filePath)) File.Delete(filePath);
            using (var stream = await httpClient.GetStreamAsync(kvp.Key))
            {
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await stream.CopyToAsync(fileStream);
                    long length = new FileInfo(filePath).Length;
                    Console.WriteLine($"{kvp.Key}, Grade = {kvp.Value[0]}, Faculty = {kvp.Value[1]}, Stream = {kvp.Value[2]}, File length = {length}");
                }
            }
        }
    }
}
