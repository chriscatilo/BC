using System;

namespace BC.EQCS.ActivityLog.Logger.PayloadModels
{
    public class ChangedPropertyValuesPayload : ActivityLogEntryPayload
    {
        public String FieldChanged { get; set; }
        public String Label { get; set; }
        public String OriginalValue { get; set; }
        public String NewValue { get; set; }
    }
}