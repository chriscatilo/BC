using BC.EQCS.Domain.Schema;
using BC.EQCS.Utils;
using Newtonsoft.Json;

namespace BC.EQCS.Web.Models
{
    public class FieldAttributes
    {
        private readonly MemberSchema _schema;

        public FieldAttributes(MemberSchema schema)
        {
            _schema = schema;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Label
        {
            get { return _schema.Label; }
        }

        public string Field
        {
            get { return _schema.ModelProperty.Name.ToCamelCase(); }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Required
        {
            get
            {
                return _schema.Constraint == ValueConstraint.Mandatory
                    ? true
                    : _schema.Constraint == ValueConstraint.Optional
                        ? false
                        : (bool?) null;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Readonly
        {
            get
            {
                return _schema.Constraint == ValueConstraint.ViewOnly || _schema.Constraint == ValueConstraint.ServerResolved
                    ? true
                    : _schema.Constraint != ValueConstraint.NotApplicable
                        ? false
                        : (bool?) null;
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxLength
        {
            get { return _schema.MaxLength; }
        }


        public bool NotApplicable
        {
            get
            {
                return _schema.Constraint == ValueConstraint.NotApplicable ||
                       _schema.Constraint == ValueConstraint.Restricted;
            }
        }
    }
}