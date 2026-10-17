using System.Globalization;

namespace IKP.Infrastructure.Services.Extensions
{
    public static class StringExtensions
    {
        public static string ToPascalCase(this string input)
        {
            char[] blockSeparators = { '_', '.' };
            char allowSeparater = ' ';

            if (string.IsNullOrEmpty(input)) return input;

            string lower = input.ToLowerInvariant();
            string withSpaces = new string(lower.Select(c => blockSeparators.Contains(c) ? allowSeparater : c).ToArray());
            string titleCased = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(withSpaces);

            return titleCased.Replace(" ", string.Empty);
        }
    }
}
