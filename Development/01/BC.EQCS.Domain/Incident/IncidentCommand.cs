using System.Runtime.Serialization;

namespace BC.EQCS.Domain.Incident
{
    public enum IncidentCommand
    {
        None = 0,

        [EnumMember(Value = "save")]
        Save,

        [EnumMember(Value = "delete")]
        Delete,

        [EnumMember(Value = "raise")]
        Raise,

        [EnumMember(Value = "accept")]
        Accept,

        [EnumMember(Value = "reject")]
        Reject,

        [EnumMember(Value = "close")]
        Close,

        [EnumMember(Value = "reopen")]
        ReOpen,

        [EnumMember(Value = "addCandidate")]
        AddCandidate,
    }
}