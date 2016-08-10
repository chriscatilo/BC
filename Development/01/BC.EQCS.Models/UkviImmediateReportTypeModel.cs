using Newtonsoft.Json;

namespace BC.EQCS.Models
{
    public class UkviImmediateReportTypeModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string TemplateName { get; set; }

        public bool IsActive { get; set; }
    }
}