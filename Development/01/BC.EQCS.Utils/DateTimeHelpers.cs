using System;
using System.Globalization;

namespace BC.EQCS.Utils
{
    public static class DateTimeHelpers
    {
        public static bool IsNullableDateTimeType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            return property.PropertyType == typeof (DateTime?);
        }

        public static bool IsDateTimeType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            return property.PropertyType == typeof(DateTime);
        }

        public static string ToStringStandardFormat(this DateTime? dateTime)
        {
            return dateTime == null ? string.Empty : dateTime.Value.ToString("dd MMMM yyyy", new CultureInfo("en-GB"));
        }

        public static string ToStringStandardFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMMM yyyy", new CultureInfo("en-GB"));
        }
    }
}