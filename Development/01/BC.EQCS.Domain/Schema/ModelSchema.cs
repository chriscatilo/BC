using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BC.EQCS.Utils;

namespace BC.EQCS.Domain.Schema
{
    public class ModelSchema<TModel> : IEnumerable<MemberSchema>
    {
        private readonly IDictionary<PropertyInfo, MemberSchema> _propertySchemata =
            new Dictionary<PropertyInfo, MemberSchema>();

        public IEnumerator<MemberSchema> GetEnumerator()
        {
            var values = _propertySchemata.Values.OrderBy(item => item.ModelProperty.Name);
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ModelSchema<TModel> BuildFor<TProperty>(
            Expression<Func<TModel, TProperty>> expr,
            string label = null,
            ValueConstraint constraint = ValueConstraint.NotApplicable,
            int? maxLength = null)
        {
            var propInfo = TypeHelpers.GetPropertyByExpression(expr);

            if (_propertySchemata.ContainsKey(propInfo))
            {
                var msg = string.Format("Property '{0}' for model '{1}' already mapped to model schema", propInfo.Name,
                    typeof (TModel).FullName);

                throw new ApplicationException(msg);
            }

            var propSchema = new MemberSchema(propInfo, label, constraint, maxLength);

            _propertySchemata.Add(propInfo, propSchema);

            return this;
        }

        public ModelSchema<TModel> Merge(ModelSchema<TModel> input)
        {
            var newSchema = new ModelSchema<TModel>();

            // add field in input that are not here
            input._propertySchemata
                .Where(item => !this._propertySchemata.ContainsKey(item.Key))
                .ToList()
                .ForEach(item => newSchema._propertySchemata.Add(item));

            // merge fields from input to this
            foreach (var source in _propertySchemata)
            {
                MemberSchema overrideSchema = null;
                var propertySchema = !input._propertySchemata.TryGetValue(source.Key, out overrideSchema)
                    ? source.Value.Copy()
                    : source.Value.Merge(overrideSchema);

                newSchema._propertySchemata.Add(source.Key, propertySchema);
            }

            return newSchema;
        }
    }
}