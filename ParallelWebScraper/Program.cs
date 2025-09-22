using ParallelWebScraper.Models;
using ParallelWebScraper.Services;

class Program
{
    static async Task Main()
    {
        var urls = new List<string>
        {
            "https://www.microsoft.com",
            "https://www.github.com",
            "https://www.stackoverflow.com",
            "https://dotnet.microsoft.com",
            "https://www.nuget.org"
        };

        urls = Enumerable.Repeat(urls, 10).SelectMany(u => u).Take(50).ToList();

        using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        var scraper = new WebScraper(httpClient, new HtmlParser());

        Console.WriteLine($"Fetching {urls.Count} URLs in parallel...\n");

        var tasks = urls.Select(scraper.ScrapeAsync).ToList();
        var results = await Task.WhenAll(tasks);

        PrintResults(results);
    }

    static void PrintResults(IEnumerable<ScrapeResult> results)
    {
        foreach (var result in results)
        {
            if (result.Success)
                Console.WriteLine($"+ {result.Url} -> {result.Title}");
            else
                Console.WriteLine($"- {result.Url} -> ERROR: {result.Error}");
        }
    }
}
