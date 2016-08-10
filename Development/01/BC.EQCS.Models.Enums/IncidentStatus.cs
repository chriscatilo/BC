using System.Runtime.Serialization;

namespace BC.EQCS.Models.Enums
{
    public enum IncidentStatus
    {
        None = 0,

        Draft = 1,

        [EnumMember(Value = "Submitted")]
        Submitted = 2,

        [EnumMember(Value = "In Progress")]
        InProgress = 3,

        Rejected = 4,

        Closed = 5
    }
}