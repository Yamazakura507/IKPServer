using System.Globalization;

namespace IKP.Infrastructure.Services.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string lower = input.ToLowerInvariant();
            string withSpaces = lower.Replace('_', ' ');
            string titleCased = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(withSpaces);

            return titleCased.Replace(" ", string.Empty);
        }
    }
}
