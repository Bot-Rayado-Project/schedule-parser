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

    public async Task<string[]> DownloadTablesAsync(Dictionary<string, string[]> linksInfo)
    {
        var list = new List<string>();

        if (!Directory.Exists(downloadPath)) Directory.CreateDirectory(downloadPath);

        foreach (KeyValuePair<string, string[]> kvp in linksInfo)
        {
            string filePath = $"{downloadPath}/{kvp.Value[0]}_{kvp.Value[1]}_{kvp.Value[2]}.xlsx";
            if (File.Exists(filePath)) File.Delete(filePath);
            using (var stream = await httpClient.GetStreamAsync(kvp.Key))
            {
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await stream.CopyToAsync(fileStream);
                    long length = new FileInfo(filePath).Length;
                    list.Add(filePath);
                    Console.WriteLine($"{kvp.Key}, Grade = {kvp.Value[0]}, Faculty = {kvp.Value[1]}, Stream = {kvp.Value[2]}, File length = {length}");
                }
            }
        }

        return list.ToArray();
    }
}
