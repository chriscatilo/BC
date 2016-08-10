using System;
using System.ComponentModel;

namespace BC.EQCS.ActivityLog.Logger.AttributeTemplates
{
    // TODO: Bryan: refacter this a common read model implementation when possible
    public class ActionAttributeTemplate
    {
        [DisplayName(@"Assigned On")]
        public String AssignedOn { get; set; }

        [DisplayName(@"Action Description")]
        public String ActionDescription { get; set; }

        [DisplayName(@"Action Response")]
        public String ActionResponse { get; set; }

        [DisplayName(@"Assigned To Test Centre")]
        public String AssignedToTestCentre { get; set; }

        [DisplayName(@"Assigned By")]
        public String AssignedBy { get; set; }

        public String Status { get; set; }

        [DisplayName(@"Assigned To")]
        public String AssignedTo { get; set; }

        [DisplayName(@"Documents")]
        public String DocumentList { get; set; }
    }
}
