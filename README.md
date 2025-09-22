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

| Method               | Mean     | Allocated |
|----------------------|---------:|----------:|
| SequentialForeach    | 23.8 s   | 175.81 MB |
| Task.WhenAll         |  1.3 s   |  28.16 MB |
| Parallel.ForEachAsync|  1.8 s   |  28.39 MB |

---

## Notes
- `SequentialForeach` is slow because it waits for each request before starting the next.  
- `Task.WhenAll` runs all requests concurrently and is the fastest in this case.  
- `Parallel.ForEachAsync` adds a small overhead, but supports `MaxDegreeOfParallelism` to limit concurrency, which is useful when working with rate limits or large workloads.

---

## How to Run

Open Visual Studio, change to Release mode and run 

