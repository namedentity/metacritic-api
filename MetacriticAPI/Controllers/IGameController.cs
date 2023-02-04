using MetacriticAPI.Contracts.Game;

namespace MetacriticAPI.Controllers
{
    public interface IGameController
    {
        Task<GameQueryResult> PerformSearchAsync(GameQueryParameters queryParameters);
        Task<GameDetails> GetGameDetailsAsync(string url);
    }
}