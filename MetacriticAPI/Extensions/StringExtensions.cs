namespace MetacriticAPI.Extensions
{
    internal static class StringExtensions
    {
        internal static string Strip(this string str) => str
            .Replace("\n", string.Empty)
            .Replace("\t", string.Empty)
            .Trim(' ');

        internal static bool IsAbsoluteUrl(this string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
