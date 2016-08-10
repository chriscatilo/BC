using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BC.EQCS.Utils;

namespace BC.EQCS.Entities.Utils
{
    public static class EntityExtensions
    {
        /// <summary>
        /// Get the property value of an entity (e.g. Language Code). Returns default if entity is null
        /// </summary>
        public static TValue GetValueOf<TEntity, TValue>(this TEntity value, Expression<Func<TEntity, TValue>> expression)
            where TEntity : class
        {
            if (value == null)
            {
                return default(TValue);
            }

            Func<TEntity, TValue> getCode = expression.Compile();

            var code = getCode(value);

            return code;
        }

        /// <summary>
        /// Get the entity by its identifier  (e.g. Language by Language Code or Id). Returns null if identifier is null
        /// </summary>
        public static TEntity GetByIdentifier<TEntity, TIdentifier>(this IQueryable<TEntity> values, TIdentifier identifier, Func<IQueryable<TEntity>, TIdentifier, TEntity> getValue)
        {
            if (identifier == null)
            {
                return default(TEntity);
            }

            var value = getValue(values, identifier);

            return value;
        }

        /// <summary>
        /// Set the reference property of an entity and its matching identifier when null (e.g. if bp.Language = null then bp.LanguageId = null). 
        /// </summary>
        public static void SetValue<TValue, TEntity>(this TValue value, TEntity entity, Expression<Func<TEntity, TValue>> getValueExpression)
            where TEntity : class
            where TValue : class
        {
            PropertyInfo propertyInfo = TypeHelpers.GetPropertyByExpression(getValueExpression);

            var pairs = EntityHelpers.GetIdReferencePairsOf<TEntity>();

            var idRefPair = pairs.First(pair => pair.ReferrenceProperty == propertyInfo.Name);

            var typeAccessor = EntityHelpers.GetTypeAccessor<TEntity>();

            typeAccessor[entity, idRefPair.ReferrenceProperty] = value;

            if (value == null)
            {

                typeAccessor[entity, idRefPair.IdentifierProperty] = null;
            }
        }


        /// <summary>
        /// Include all properties when loading an entity
        /// </summary>
        public static IQueryable<TEntity> IncludeAllNavigationProperties<TEntity>(this IQueryable<TEntity> query, DbContext context) where TEntity : class
        {
            var objectSet = ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<TEntity>();

            var navigationProperties = objectSet.EntitySet.ElementType.NavigationProperties;

            return navigationProperties.Aggregate(query, (current, prop) => current.Include(prop.Name));
        }

        public static IQueryable<TEntity> SetWithNavigationProperties<TEntity>(this DbContext context) where TEntity : class
        {
            return context.Set<TEntity>().IncludeAllNavigationProperties(context);
        }
            
    }
}
