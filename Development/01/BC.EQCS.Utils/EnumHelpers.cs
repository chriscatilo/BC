using System;
using System.Linq;

namespace BC.EQCS.Utils
{
    public static class EnumHelpers
    {
        public static bool IsEnumType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            return property.PropertyType.IsEnum;
        }

        public static Type GetEnumType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            var enumType = property.PropertyType;

            return enumType;
        }

        public static Enum ConvertTo(Type enumType, string literalValue)
        {
            var value = Enum.GetValues(enumType).Cast<Enum>().First(@enum => Enum.GetName(enumType, @enum).EqualsCaseInsensitive(literalValue));

            return value;
        }

        public static bool IsNullableEnumType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            var u = Nullable.GetUnderlyingType(property.PropertyType);

            return (u != null) && u.IsEnum;
        }
    }
}