using System.Linq;
using System.Text;

namespace BC.EQCS.Models.Extensions
{
    public static class AddressExtensions
    {
        public static string ToSingleLineFormat(this IAddress address)
        {
            var sb = new StringBuilder(address.AddressLine1);

            var addressLines = new[]
            {
                address.AddressLine2,
                address.Town,
                address.State,
                address.PostCode
            }.Where(line => !string.IsNullOrWhiteSpace(line));

            foreach (var line in addressLines)
            {
                sb.AppendFormat("{0},", line.Trim());
            }

            return sb.ToString();
        }

        public static string ToLineBreakFormat(this IAddress address, string lineBreak)
        {
            var sb = new StringBuilder(address.AddressLine1).Append(lineBreak);

            var addressLines = new[]
            {
                address.AddressLine2,
                address.Town,
                address.State,
                address.PostCode
            }.Where(line => !string.IsNullOrWhiteSpace(line));

            foreach (var line in addressLines)
            {
                sb.AppendFormat("{0}{1}", line.Trim(), lineBreak);
            }

            return sb.ToString();
        }
    }
}