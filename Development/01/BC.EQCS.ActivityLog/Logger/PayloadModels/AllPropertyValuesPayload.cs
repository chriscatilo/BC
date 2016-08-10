using System;

namespace BC.EQCS.ActivityLog.Logger.PayloadModels
{
    public class AllPropertyValuesPayload : ActivityLogEntryPayload
    {
        public String FieldName { get; set; }
        public String Label { get; set; }
        public String Value { get; set; }
    }
}