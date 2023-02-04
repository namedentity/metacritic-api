namespace MetacriticAPI.Contracts.Game
{
    public record GameQueryResult(
        IEnumerable<GameQueryResultItem> QueryResultItems);
}
