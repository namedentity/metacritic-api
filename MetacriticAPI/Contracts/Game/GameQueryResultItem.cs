namespace MetacriticAPI.Contracts.Game
{
    public record GameQueryResultItem(
        string Title,
        string Platform,
        string Year,
        string Metascore,
        string Summary,
        string GamePath,
        string ThumbnailUrl);
}
