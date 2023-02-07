namespace MetacriticAPI.Extensions
{
    internal static class StringExtensions
    {
        internal static string TrimStandardCharacters(this string str) =>
            str.Trim(' ', '\r', '\n', '\t');
            

        internal static string TrimEnd(this string str, string trimString)
        {
            if (string.IsNullOrEmpty(trimString))
            {
                return str;
            }

            string result = str;

            while (result.EndsWith(trimString))
            {
                result = result.Substring(0, result.Length - trimString.Length);
            }

            return result;
        }

        internal static bool IsAbsoluteUrl(this string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
