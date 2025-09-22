# Parallel Web Scraper in C#

This project demonstrates scraping multiple websites using three different approaches in C#:

- Sequential with `foreach`
- Parallel with `Task.WhenAll`
- Parallel with `Parallel.ForEachAsync`

Benchmarking is done with [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet).

---

## Websites Scraped
The following sites are used as test targets (repeated to reach 50–100 URLs):

- https://www.microsoft.com  
- https://www.github.com  
- https://www.stackoverflow.com  
- https://dotnet.microsoft.com  
- https://www.nuget.org  

---

## Benchmark Results

Machine: Intel i7-10700K, .NET 9.0.9  
Dataset: 100 URLs

| Method                | Mean     | Allocated |
|-----------------------|---------:|----------:|
| SequentialForeach     | 23.8 s   | 175.81 MB |
| Task.WhenAll          |  1.3 s   |  28.16 MB |
| Parallel.ForEachAsync |  1.8 s   |  28.39 MB |

---

## Explanation

### Sequential (`foreach`)
Runs requests one after another.  
Simple but very slow for I/O-bound work like HTTP requests.  

### Task.WhenAll
Starts **all tasks at once** and waits for them to complete.  
- Fastest in this benchmark because all requests are concurrent.  
- Best for small to medium workloads.  
- Downside: for very large input (thousands of URLs), it may overload the server or your own system since everything is fired simultaneously.

### Parallel.ForEachAsync
Runs tasks in parallel batches.  
- By default, it distributes work across available resources.  
- You can configure the degree of parallelism:

```csharp
await Parallel.ForEachAsync(urls, new ParallelOptions { MaxDegreeOfParallelism = 5 },
    async (url, ct) =>
    {
        await scraper.ScrapeAsync(url);
    });
