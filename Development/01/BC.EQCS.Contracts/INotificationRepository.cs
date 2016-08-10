
using System.Collections.Generic;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Contracts
{
    public interface INotificationRepository<TNotificationModel, in TModel> : IRepository<TNotificationModel>
    {
   
        TNotificationModel GetPopulatedFyiTemplate(TModel value, int incidentId,IEnumerable<SecurityUserModel> recipients, string url);
        TNotificationModel GetPopulatedWorkNoteTemplate(TModel value, int id, string url);
        TNotificationModel GetPopulatedRaisedIncidentTemplate(TModel value, int id, string url);
        TNotificationModel GetPopulatedAcceptedIncidentTemplate(TModel value, int id, string url);
        TNotificationModel GetPopulatedCloseIncidentTemplate(TModel value, int id, string url, string user, string role);
        TNotificationModel GetPopulatedRejectIncidentTemplate(TModel value, int id, string url);
        TNotificationModel GetPopulatedEditActionTemplate(TModel value, int incidentId, int actionId, string url);
        TNotificationModel GetPopulatedCloseActionTemplate(TModel value, int incidentId, string url, int actionId);
    }
} 