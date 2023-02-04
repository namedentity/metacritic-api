using HtmlAgilityPack;
using MetacriticAPI.Contracts.Game;
using MetacriticAPI.Extensions;

namespace MetacriticAPI.Services
{
    internal class GamePageHtmlParsingService
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
            documentNode.SelectSingleNode("//div[contains(@class, 'product_title')]/a/h1").InnerText.Strip();

        private static string GetPlatform(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//span[contains(@class, 'platform')]/a").InnerText.Strip();

        private static string GetPublisher(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//li[contains(@class, 'summary_detail publisher')]/span/a").InnerText.Strip();

        private static DateTime GetReleaseDate(HtmlNode documentNode) =>
            DateTime.ParseExact(
                documentNode.SelectSingleNode("//li[contains(@class, 'summary_detail release_data')]/span[contains(@class, 'data')]").InnerText
                    .Replace("  ", " "),
                "MMM d, yyyy",
                null);

        private static string GetMetascore(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//div[contains(@class, 'metascore_w')]/span").InnerText;

        private static string GetUserscore(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//div[contains(@class, 'metascore_w user')]").InnerText;

        private static string GetSummary(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//span[contains(@class, 'blurb blurb_expanded')]").InnerText;

        private static string GetBoxArtUrl(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//img[contains(@class, 'product_image large_image')]").Attributes["src"].Value;

        private static string GetDeveloper(HtmlNode documentNode) =>
            documentNode.SelectSingleNode("//li[contains(@class, 'summary_detail developer')]/span[contains(@class, 'data')]/a").InnerText;

        private static IEnumerable<string> GetGenres(HtmlNode documentNode) =>
            documentNode.SelectNodes("//li[contains(@class, 'summary_detail product_genre')]/span[contains(@class, 'data')]").Select(x => x.InnerText);
    }
}
