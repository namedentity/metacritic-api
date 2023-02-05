namespace MetacriticAPI.Contracts.Game
{
    public record GameDetails(
        string Title,
        string Platform,
        string Publisher,
        string ReleaseDate,
        string? Metascore,
        string? Userscore,
        string? Summary,
        string BoxArtUrl,
        string Developer,
        IEnumerable<string> Genres);
}
