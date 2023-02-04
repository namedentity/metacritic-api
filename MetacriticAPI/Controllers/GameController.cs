using FluentValidation;
using MetacriticAPI.Contracts.Game;
using MetacriticAPI.Extensions;
using MetacriticAPI.Services;
using MetacriticAPI.Validation;

namespace MetacriticAPI.Controllers
{
    public class GameController : IGameController
    {
        GameService gameService;
        GameSearchParametersValidator gameSearchParametersValidator = new();
        private readonly string baseAddress;

        public GameController(HttpClient httpClient, string baseAddress)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentException($"'{nameof(baseAddress)}' cannot be null or whitespace.", nameof(baseAddress));
            }

            gameService = new GameService(httpClient, baseAddress);
            this.baseAddress = baseAddress;
        }

        public async Task<GameQueryResult> PerformSearchAsync(GameQueryParameters queryParameters)
        {
            gameSearchParametersValidator.ValidateAndThrow(queryParameters);

            return await gameService.SearchGamesAsync(queryParameters);
        }

        public async Task<GameDetails> GetGameDetailsAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"'{nameof(url)}' cannot be null or whitespace.", nameof(url));
            }

            var absoluteUrl = url.IsAbsoluteUrl() ? 
                url : 
                new Uri(new Uri(baseAddress), url).ToString();

            return await gameService.GetDetailsFromGamePageAsync(absoluteUrl);
        }
    }
}
