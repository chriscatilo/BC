namespace BC.EQCS.Utils
{
    public static class IntegerHelpers
    {
        public static bool IsNullableIntegerType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            return property.PropertyType == typeof(int?);
        }

        public static bool IsIntegerType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            return property.PropertyType == typeof(int);
        }
    }
}