namespace MetacriticAPI.Contracts.Game
{
    public record GameDetails(
        string Title,
        string Platform,
        string Publisher,
        DateTime ReleaseDate,
        string Metascore,
        string Userscore,
        string Summary,
        string BoxArtUrl,
        string Developer,
        IEnumerable<string> Genres);
}
