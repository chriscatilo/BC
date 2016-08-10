using System.Reflection;

namespace BC.EQCS.Domain.Schema
{
    public class MemberSchema
    {
        public MemberSchema(
            PropertyInfo property, string label = null,
            ValueConstraint constraint = ValueConstraint.NotApplicable, int? maxLength = null)
        {
            MaxLength = maxLength;
            ModelProperty = property;
            Constraint = constraint;
            Label = label;

        }

        public string Label { get; private set; }
        public ValueConstraint Constraint { get; private set; }
        public PropertyInfo ModelProperty { get; private set; }
        public int? MaxLength { get; private set; }

        public MemberSchema Copy()
        {
            return new MemberSchema(ModelProperty, Label, Constraint, MaxLength);
        }

        public MemberSchema Merge(MemberSchema input)
        {
            var output = new MemberSchema(
                ModelProperty,
                Label,
                input.Constraint,
                MaxLength);

            return output;
        }

        public override string ToString()
        {
            const string format = "Property: {0}, Constraint: {1}, MaxLength: {2}";

            var value = ModelProperty != null
                ? string.Format(format, ModelProperty.Name, Constraint, MaxLength)
                : "Unable to build string";

            return value;
        }
    }
}