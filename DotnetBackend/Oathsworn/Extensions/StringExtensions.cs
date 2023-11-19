namespace Oathsworn.Extensions
{
    public static class StringExtensions
    {
        public static string ToEmptyIfNull(this string? variable)
        {
            return variable is null ? string.Empty : variable;
        }

        public static string ToEmptyIfNull<T>(this T variable, string? prefix, string? suffix)
        {
            return variable is null ? string.Empty : prefix.ToEmptyIfNull() + variable.ToString() + suffix.ToEmptyIfNull();
        }
    }
}