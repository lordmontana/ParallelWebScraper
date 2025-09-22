using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ParallelWebScraper.Services;

namespace ParallelWebScraper.Benchmarks;

[MemoryDiagnoser]
public class ScraperBenchmarks
{
    private List<string> _urls = new();
    private WebScraper _scraper = null!;

    [GlobalSetup]
    public void Setup()
    {
        _urls = new List<string>
        {
            "https://www.microsoft.com",
            "https://www.github.com",
            "https://www.stackoverflow.com",
            "https://dotnet.microsoft.com",
            "https://www.nuget.org"
        };

        _urls = Enumerable.Repeat(_urls, 20).SelectMany(u => u).ToList();

        var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        _scraper = new WebScraper(httpClient, new HtmlParser());
    }

    [Benchmark]
    public async Task SequentialForeach()
    {
        foreach (var url in _urls)
            await _scraper.ScrapeAsync(url);
    }

    [Benchmark]
    public async Task ParallelWhenAll()
    {
        var tasks = _urls.Select(url => _scraper.ScrapeAsync(url));
        await Task.WhenAll(tasks);
    }

    [Benchmark]
    public async Task ParallelForEachAsync()
    {
        await Parallel.ForEachAsync(_urls, async (url, ct) =>
        {
            await _scraper.ScrapeAsync(url);
        });
    }
}

public class BenchmarkRunnerProgram
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ScraperBenchmarks>();
    }
}
