using BC.EQCS.Models;

namespace BC.EQCS.Web.Models
{
    public class BadRequestMessage
    {
        public string FailureType { get; set; }

        public string FailureMessage { get; set; }

        public ValidationResult ValidationResult { get; set; }
    }
}