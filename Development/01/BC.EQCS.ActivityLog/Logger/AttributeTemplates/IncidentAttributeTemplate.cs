using System.ComponentModel;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.ActivityLog.Logger.AttributeTemplates
{
    // TODO #4034 Replace use of IncidentAttributeTemplate with IncidentAttributes
    public class IncidentAttributeTemplate : IncidentAttributes
    {
        [DisplayName(Constants.IncidentAttributeLabels.Full.ReferringOrganisation)]
        public dynamic ReferringOrgName { get; private set; }
    }
}