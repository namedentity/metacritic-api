using HtmlAgilityPack;
using MetacriticAPI.Contracts.Game;
using MetacriticAPI.Extensions;

namespace MetacriticAPI.Services
{
    internal class GamePageHtmlParserService
    {
        internal GameDetails Parse(HtmlDocument htmlDocument)
        {
            var documentNode = htmlDocument.DocumentNode;

            return new(
                GetTitle(documentNode),
                GetPlatform(documentNode),
                GetPublisher(documentNode),
                GetReleaseDate(documentNode),
                GetMetascore(documentNode),
                GetUserscore(documentNode),
                GetSummary(documentNode),
                GetBoxArtUrl(documentNode),
                GetDeveloper(documentNode),
                GetGenres(documentNode));
        }

        private static string GetTitle(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//div[contains(@class, 'product_title')]/a/h1").InnerText.TrimStandardCharacters();

        private static string GetPlatform(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//span[contains(@class, 'platform')]").InnerText.TrimStandardCharacters();

        private static string GetPublisher(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//li[contains(@class, 'summary_detail publisher')]/span/a").InnerText.TrimStandardCharacters();

        private static string GetReleaseDate(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//li[contains(@class, 'summary_detail release_data')]/span[contains(@class, 'data')]").InnerText;

        private static string? GetMetascore(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//div[contains(@class, 'metascore_w')]/span")?.InnerText;

        private static string? GetUserscore(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//div[contains(@class, 'score_summary')]//div[contains(@class, 'metascore_w user')]")?.InnerText;

        private static string? GetSummary(HtmlNode documentNode)
        {
            var summaryDataNode = documentNode.SelectSingleNode("//li[contains(@class, 'summary_detail product_summary')]//span[contains(@class, 'data')]");

            if (summaryDataNode == null)
            {
                return null;
            }

            var expandedBlurbNode = summaryDataNode.SelectSingleNode(".//span[contains(@class, 'blurb blurb_expanded')]");

            var nodeToUse = expandedBlurbNode ?? summaryDataNode;

            return nodeToUse.InnerText
                .TrimStandardCharacters()
                .TrimEnd("&hellip; Expand");
        }

        private static string GetBoxArtUrl(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//img[contains(@class, 'product_image large_image')]").Attributes["src"].Value;

        private static string GetDeveloper(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//li[contains(@class, 'summary_detail developer')]/span[contains(@class, 'data')]/a").InnerText;

        private static IEnumerable<string> GetGenres(HtmlNode documentNode) =>
            documentNode.SelectNodes("//li[contains(@class, 'summary_detail product_genre')]/span[contains(@class, 'data')]").Select(x => x.InnerText);
    }
}
