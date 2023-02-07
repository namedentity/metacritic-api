using HtmlAgilityPack;
using MetacriticAPI.Contracts.Game;
using MetacriticAPI.Extensions;

namespace MetacriticAPI.Services
{
    internal class GameService
    {
        private readonly HttpClient httpClient;
        private readonly string baseAddress;
        private readonly GameSearchHtmlParserService gameSearchHtmlParserService = new();
        private readonly GamePageHtmlParserService gamePageHtmlParserService = new();

        internal GameService(HttpClient httpClient, string baseAddress)
        {
            this.httpClient = httpClient;
            this.baseAddress = baseAddress;
        }

        internal async Task<GameDetails> GetDetailsFromGamePageAsync(string url)
        {
            HtmlDocument htmlDocument = await SendAndLoadHtmlDocumentAsync(new(HttpMethod.Get, url));

            return gamePageHtmlParserService.Parse(htmlDocument);
        }

        internal async Task<GameQueryResult> SearchGamesAsync(GameQueryParameters queryParameters)
        {
            var request = CreateRequestMessage(queryParameters);
            HtmlDocument htmlDocument = await SendAndLoadHtmlDocumentAsync(request);

            return gameSearchHtmlParserService.Parse(htmlDocument);
        }

        private async Task<HtmlDocument> SendAndLoadHtmlDocumentAsync(HttpRequestMessage request)
        {
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);
            return htmlDocument;
        }

        private HttpRequestMessage CreateRequestMessage(GameQueryParameters queryParameters)
        {
            Uri uri = queryParameters.ToUri(baseAddress);

            return new(HttpMethod.Get, uri);
        }
    }
}
