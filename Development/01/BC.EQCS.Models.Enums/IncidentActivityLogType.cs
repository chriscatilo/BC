namespace BC.EQCS.Models.Enums
{
    public enum IncidentActivityLogType : int
    {
        None = 0,
        Change = 1,
        ActionUpdated = 2,
        Submission = 3,
        Acceptance = 4,
        Rejection = 5,
        Closure = 6,
        Reopening = 7,
        IncidentCreate = 8,
        WorkNote = 9,
        NewCandidate = 10,
        NewAction = 11,
        FYI = 12,
        IncidentSnapshot = 13
    }
}
