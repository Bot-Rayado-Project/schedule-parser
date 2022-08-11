namespace Parser.Core;

class HtmlLoader
{
    readonly HttpClient httpClient;
    readonly string url;

    public HtmlLoader(IParserSettings settings)
    {
        httpClient = new HttpClient();
        url = settings.Url;
    }

    public async Task<string> GetHtmlAsync(CancellationToken token)
    {
        return await httpClient.GetStringAsync(url);
    }
}