using MetacriticAPI.Contracts;
using MetacriticAPI.Utilities;
using System.Collections.Specialized;

namespace MetacriticAPI.Extensions
{
    internal static class NameValueCollectionExtensions
    {
        internal static void AddSortMethod(this NameValueCollection nameValueCollection, SortMethod sortMethod)
        {
            string sortMethodString = sortMethod switch
            {
                SortMethod.Relevancy => "relevancy",
                SortMethod.Score => "score",
                SortMethod.MostRecent => "recent",
                _ => throw new NotImplementedException(),
            };

            nameValueCollection["sort"] = sortMethodString;
        }

        internal static void AddGamePlatform(this NameValueCollection queryCollection, string gamePlatform)
        {
            string? platformId = GamePlatformUtilities.GetMetacriticPlatformId(gamePlatform);

            if (platformId == null)
            {
                return;
            }

            queryCollection["search_type"] = "advanced";

            queryCollection[$"plats[{platformId}]"] = "1";
        }
    }
}
