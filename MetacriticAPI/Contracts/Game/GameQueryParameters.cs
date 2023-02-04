namespace MetacriticAPI.Contracts.Game
{
    public record GameQueryParameters(
        string SearchTerm,
        string Platform,
        SortMethod SortMethod);
}
