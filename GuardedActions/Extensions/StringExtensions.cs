namespace GuardedActions.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNull(this string source) => source == null;
        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);
        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);
    }
}
