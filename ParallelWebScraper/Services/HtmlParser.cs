
using HtmlAgilityPack;

namespace ParallelWebScraper.Services
{
    public class HtmlParser
    {
        public string? GetTitle(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.SelectSingleNode("//title")?.InnerText.Trim();
        }
    }
}
