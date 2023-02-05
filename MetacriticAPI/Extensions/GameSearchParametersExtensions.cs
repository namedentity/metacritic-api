using MetacriticAPI.Contracts.Game;
using System.Web;

namespace MetacriticAPI.Extensions
{
    internal static class GameSearchParametersExtensions
    {
        internal static Uri ToUri(this GameQueryParameters searchParameters, string baseAddress)
        {
            var baseAddressUri = new Uri(baseAddress);
            var uriBuilder = new UriBuilder(
                new Uri(baseAddressUri, $"/search/game/{ReplaceInvalidSearchTermCharacters(searchParameters.SearchTerm)}/results"));
            
            uriBuilder.Query = string.Join('&', "");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            
            query.AddSortMethod(searchParameters.SortMethod);
            query.AddGamePlatform(searchParameters.Platform);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        private static string ReplaceInvalidSearchTermCharacters(this string str) => str
            .Replace(":", string.Empty)
            .Replace(";", " ")
            .Replace("/", " ");
    }
}
