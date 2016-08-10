namespace BC.EQCS.Utils
{
    public static class BooleanHelpers
    {
        public static bool IsNullableBooleanType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            return property.PropertyType == typeof(bool?);
        }

        public static bool IsBooleanType(object model, string propertyName)
        {
            var property = TypeHelpers.GetPropertyByName(model.GetType(), propertyName);

            return property.PropertyType == typeof(bool);
        }
    }
}