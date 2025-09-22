using ParallelWebScraper.Models;

namespace ParallelWebScraper.Services;

public class WebScraper
{
    private readonly HttpClient _httpClient;
    private readonly HtmlParser _parser;

    public WebScraper(HttpClient httpClient, HtmlParser parser)
    {
        _httpClient = httpClient;
        _parser = parser;
    }

    public async Task<ScrapeResult> ScrapeAsync(string url)
    {
        try
        {
            var html = await _httpClient.GetStringAsync(url);
            var title = _parser.GetTitle(html);

            return new ScrapeResult
            {
                Url = url,
                Title = title,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new ScrapeResult
            {
                Url = url,
                Success = false,
                Error = ex.Message
            };
        }
    }
}
