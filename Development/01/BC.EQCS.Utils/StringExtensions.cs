using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BC.EQCS.Utils
{
    public static class StringExtensions
    {
        public static bool EqualsCaseInsensitive(this string value1, string value2)
        {
            return (value1 ?? string.Empty).Equals(value2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return format == null ? null : string.Format(format, args);
        }

        public static string ToCamelCase(this string value)
        {
            if (value == null)
            {
                return null;
            }

            if (value.Length == 1)
            {
                return value.ToLowerInvariant();
            }

            var regex = new Regex(@"^([A-Z]*)(.*)$");

            var match = regex.Match(value);

            if (!match.Success)
            {
                return value.ToLowerInvariant();
            }

            var prefix = match.Groups[1].Value;

            if (prefix.Length == 1)
            {
                prefix = prefix.ToLowerInvariant();
            }
            else
            {
                prefix = prefix.Substring(0, prefix.Length - 1).ToLowerInvariant() + prefix.Substring(prefix.Length - 1);
            }

            var suffix = match.Groups[2];

            return prefix + suffix;
        }

        public static string ToFileNameSafe(this string value)
        {
            var sb = new StringBuilder(value);

            var charactersNotAllowed = new[]
            {
                @"\", @"/", @":", @"*", @"?", "\"", @"<", @">", @"|"
            }.ToList();

            charactersNotAllowed.ForEach(@char => sb.Replace(@char, string.Empty));

            return sb.ToString();
        }
    }
}
