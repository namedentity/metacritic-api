using HtmlAgilityPack;
using MetacriticAPI.Contracts.Game;
using System.Collections.Generic;
using System.Xml;

namespace MetacriticAPI.Services
{
    internal class GameSearchHtmlParserService
    {
        internal GameQueryResult Parse(HtmlDocument htmlDocument) => new(
            htmlDocument.DocumentNode
                .SelectNodes("//li[contains(@class, 'result')]")?
                .Select(NodeToResultItem)
                .ToList() ?? Enumerable.Empty<GameQueryResultItem>());

        private GameQueryResultItem NodeToResultItem(HtmlNode htmlNode) => new(
            GetTitle(htmlNode),
            GetPlatform(htmlNode),
            GetYear(htmlNode),
            GetMetascore(htmlNode),
            GetSummary(htmlNode),
            GetGamePath(htmlNode),
            GetThumbnailUrl(htmlNode));

        private static string GetThumbnailUrl(HtmlNode htmlNode) =>
            htmlNode.SelectSingleNode(".//img").Attributes["src"].Value;

        private static string GetSummary(HtmlNode htmlNode) =>
            htmlNode.SelectSingleNode(".//p[contains(@class, 'deck')]").InnerText;

        private static string GetMetascore(HtmlNode htmlNode) =>
            htmlNode.SelectSingleNode(".//span[contains(@class, 'metascore_w')]").InnerText;

        private static string GetYear(HtmlNode htmlNode) =>
            new string(
                htmlNode.SelectSingleNode(".//span[contains(@class, 'platform')]")
                    .NextSibling.InnerText
                    .Replace("\\n", "")
                    .Trim(' ')
                    .TakeLast(4)
                    .ToArray());

        private static string GetPlatform(HtmlNode htmlNode) =>
            htmlNode.SelectSingleNode(".//span[contains(@class, 'platform')]").InnerText;

        private static string GetGamePath(HtmlNode htmlNode) =>
            htmlNode.SelectSingleNode(".//a").Attributes["href"].Value
                .Replace("\\\"", string.Empty);

        private static string GetTitle(HtmlNode htmlNode) =>
            htmlNode.SelectSingleNode(".//a")
                .InnerText
                .Replace("\n", "")
                .Trim(' ');
    }
}
