using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace BC.EQCS.Utils
{
    public class EntityProperties<TEntity> : IEnumerable<PropertyInfo>
    {
        private readonly List<PropertyInfo> _properties = new List<PropertyInfo>();

        private EntityProperties()
        {
        }

        public IEnumerator<PropertyInfo> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static EntityProperties<TEntity> Create()
        {
            return new EntityProperties<TEntity>();
        }

        public EntityProperties<TEntity> Add<TProperty>(Expression<Func<TEntity, TProperty>> expr)
        {
            var propInfo = TypeHelpers.GetPropertyByExpression(expr);

            if (_properties.Contains(propInfo))
            {
                throw new ApplicationException(string.Format("{0} already exists", propInfo));
            }

            _properties.Add(propInfo);

            return this;
        }
    }
}