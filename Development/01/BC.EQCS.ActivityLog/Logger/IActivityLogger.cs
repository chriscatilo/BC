using System;
using BC.EQCS.Models.Enums;

namespace BC.EQCS.ActivityLog.Logger
{
    public interface IActivityLogger<TModel, TModelAttributes>
    {
        void CreateCustomActivityLogEntry(Int32 incidentId, String payload, IncidentActivityLogType activityLogType);
        void LogAllModelValues(Int32 instanceId, Int32 masterId, IncidentActivityLogType activityLogType);
        void LogAllModelValues(int instanceId, IncidentActivityLogType activityLogType);
        void OpenDifferenceLoggingProcess(int updatedItemId, Int32 masterId);
        void OpenDifferenceLoggingProcess(int updatedItemId);
        void CompleteDifferenceLoggingProcess(IncidentActivityLogType activityLogType);
    }
}