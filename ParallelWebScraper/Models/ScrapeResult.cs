using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelWebScraper.Models
{
    public class ScrapeResult
    {
        public string Url { get; set; } = string.Empty;
        public string? Title { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
