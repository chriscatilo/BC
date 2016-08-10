using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BC.EQCS.Utils
{
    public static class TypeHelpers
    {
        private static readonly IDictionary<string, IEnumerable<PropertyInfo>> TypePropertiesLookup =
            new ConcurrentDictionary<string, IEnumerable<PropertyInfo>>();

        public static bool IsPropertyByNameExists<TEntity>(string name)
        {
            var value = GetPropertiesOf(typeof (TEntity)).Any(info => info.Name.EqualsCaseInsensitive(name));

            return value;
        }

        public static PropertyInfo GetPropertyByName<TEntity>(string name)
            where TEntity : class
        {
            var type = typeof (TEntity);

            var propertyInfo = GetPropertyByName(type, name);

            return propertyInfo;
        }

        public static PropertyInfo GetPropertyByName(Type type, string name)
        {
            var propertyInfo = GetPropertiesOf(type).FirstOrDefault(info => info.Name.EqualsCaseInsensitive(name));

            return propertyInfo;
        }

        public static IEnumerable<PropertyInfo> GetPropertiesOf(Type type)
        {
            IEnumerable<PropertyInfo> properties;

            if (TypePropertiesLookup.TryGetValue(type.FullName, out properties))
            {
                return properties;
            }

            properties = type.GetProperties();

            TypePropertiesLookup.Add(type.FullName, properties);

            return properties;
        }

        public static PropertyInfo GetPropertyByExpression<TEntity, TProperty>(
            Expression<Func<TEntity, TProperty>> selector)
        {
            Action throwError = () =>
            {
                var msg = string.Format("Unable to get PropertyInfo from Lambda expression {0}", selector);
                throw new ApplicationException(msg);
            };

            Func<Expression, PropertyInfo> getPropertyInfo
                = expr => (PropertyInfo) ((MemberExpression) expr).Member;


            var body = selector.Body;

            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                {
                    var propertyInfo = getPropertyInfo(body);
                    return propertyInfo;
                }
                case ExpressionType.Convert:
                {
                    var operand = ((UnaryExpression) selector.Body).Operand;
                    if (operand is MemberExpression)
                    {
                        var propertyInfo = getPropertyInfo(operand);
                        return propertyInfo;
                    }
                    throwError();
                    break;
                }
            }

            throwError();

            return null;
        }
    }
}