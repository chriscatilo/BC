using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BC.EQCS.UnitTests.Utils;
using BC.EQCS.Utils;
using FastMember;
using TechTalk.SpecFlow;

namespace BC.EQCS.Integration.Utils
{
    public static class SpecflowTableHelper
    {
        public static void MapToModel<TModel>(this Table source, TModel model, Func<TableRow, bool> rowSelector)
            where TModel : class
        {
            var tableRow = source.Rows.FirstOrDefault(rowSelector);

            if (tableRow == null) return;

            FillModel(model, tableRow);
        }

        public static void MapToModel<TModel>(this Table source, TModel model, Func<TableRows, TableRow> getRow)
            where TModel : class
        {
            var tableRow = getRow(source.Rows);

            FillModel(model, tableRow);
        }

        public static void MapToModel<TModel>(this TableRow row, TModel model)
            where TModel : class
        {
            FillModel(model, row);
        }

        private static readonly Regex valueGeneratorPattern = new Regex(@"GEN.(.*)\((.*)\)", RegexOptions.IgnoreCase);

        public static Table Parse(this Table source)
        {
            var dest = new Table(source.Header.ToArray());

            foreach (var row in source.Rows)
            {
                var destValues = new List<string>();

                foreach (var value in row.Values)
                {
                    if (valueGeneratorPattern.IsMatch(value))
                    {
                        var @type = valueGeneratorPattern.Replace(value, "$1");
                        var @params = valueGeneratorPattern.Replace(value, "$2");
                        var generator = RandomValueGenerators.GetByTypeName(@type);
                        destValues.Add(generator.Generate(@params));
                    }
                    else
                    {
                        destValues.Add(value);
                    }
                }

                dest.AddRow(destValues.ToArray());
            }

            return dest;
        }

        private static void FillModel<TModel>(TModel model, IEnumerable<KeyValuePair<string, string>> tableRow) where TModel : class
        {
            foreach (
                var keyValuePair in
                    tableRow.Where(keyValuePair => TypeHelpers.IsPropertyByNameExists<TModel>(keyValuePair.Key)))
            {
                SetModelPropertyValue(model, keyValuePair.Key, keyValuePair.Value);
            }
        }

        public static void SetModelPropertyValue<TModel>(TModel model, string propertyName, string value)
            where TModel : class
        {
            var accessor = ObjectAccessor.Create(model);

            // if column matches an enum type then map as an enum value
            if (EnumHelpers.IsEnumType(model, propertyName))
            {
                var enumType = EnumHelpers.GetEnumType(model, propertyName);
                accessor[propertyName]
                    = value.EqualsCaseInsensitive("null")
                        ? null
                        : EnumHelpers.ConvertTo(enumType, value);
                return;
            }

            if (EnumHelpers.IsNullableEnumType(model, propertyName))
            {
                var nullableType = EnumHelpers.GetEnumType(model, propertyName);

                var underlyingType = Nullable.GetUnderlyingType(nullableType);

                accessor[propertyName]
                    = value.EqualsCaseInsensitive("null")
                        ? null
                        : EnumHelpers.ConvertTo(underlyingType, value);
                return;
            }

            if (DateTimeHelpers.IsNullableDateTimeType(model, propertyName))
            {
                accessor[propertyName]
                    = value.EqualsCaseInsensitive("null")
                        ? null
                        : (DateTime?) DateTime.Parse(value);
                return;
            }

            if (DateTimeHelpers.IsDateTimeType(model, propertyName))
            {
                accessor[propertyName]
                    = value.EqualsCaseInsensitive("null")
                        ? default(DateTime)
                        : DateTime.Parse(value);
                return;
            }

            if (BooleanHelpers.IsBooleanType(model, propertyName))
            {
                accessor[propertyName]
                    = !value.EqualsCaseInsensitive("null") && bool.Parse(value);
                return;
            }

            if (BooleanHelpers.IsNullableBooleanType(model, propertyName))
            {
                accessor[propertyName]
                    = value.EqualsCaseInsensitive("null")
                        ? default(bool?)
                        : bool.Parse(value);
                return;
            }

            if (IntegerHelpers.IsNullableIntegerType(model, propertyName))
            {
                accessor[propertyName]
                    = value.EqualsCaseInsensitive("null")
                        ? null
                        : (int?) int.Parse(value);
                return;
            }

            if (IntegerHelpers.IsIntegerType(model, propertyName))
            {
                accessor[propertyName]
                    = value.EqualsCaseInsensitive("null")
                        ? default(int)
                        : int.Parse(value);
                return;
            }


            accessor[propertyName]
                = value.EqualsCaseInsensitive("null")
                    ? null
                    : value;
        }
    }
}