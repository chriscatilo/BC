using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BC.EQCS.ActivityLog.Logger;
using BC.EQCS.ActivityLog.Logger.AttributeTemplates;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Notifications;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Models;
using BC.EQCS.Security.Service;
using BC.EQCS.Web.Infrastructure.Logging;
using BC.EQCS.Web.Models;
using BC.EQCS.Web.Models.Api;
using Newtonsoft.Json;

namespace BC.EQCS.Web.Controllers.API
{
    public class NotificationController : ApiController
    {
        private readonly INotificationRepository<NotificationMessageModel, NotificationMessageTemplateModel> _notificationRepository;
        private readonly INotificationTemplateRepository<NotificationMessageTemplateModel> _notificationTemplateRepository;
        private readonly IRepository<IncidentViewModel> _incidentViewRepository;
        private readonly IAssetAuthoriser _authoriser;
        private readonly IContextResolver _context;
        private readonly IActivityLogger<IncidentViewModel, IncidentAttributeTemplate> _activityLogger;
        private readonly ILogger _logger;

        public NotificationController(
            INotificationRepository<NotificationMessageModel, 
            NotificationMessageTemplateModel> notificationRepository,
            INotificationTemplateRepository<NotificationMessageTemplateModel> notificationTemplateRepository,
            IRepository<IncidentViewModel> incidentViewRepository,
            IAssetAuthoriser authoriser,
            IContextResolver context,
            IActivityLogger<IncidentViewModel, IncidentAttributeTemplate> activityLogger,
            ILogger logger)
        {
            _notificationRepository = notificationRepository;
            _notificationTemplateRepository = notificationTemplateRepository;
            _incidentViewRepository = incidentViewRepository;
            _authoriser = authoriser;
            _context = context;
            _activityLogger = activityLogger;
            _logger = logger;
        }



        #region WorkNote
        [Route(ApiRoutes.NotificationWorkNote.Route, Name = ApiRoutes.NotificationWorkNote.Name)]
        public void SendWorkNoteNotification(int incidentId)
        {
            //if (SendWorkNote())
            //{

                //need to have the incident id
                var template =
                    _notificationTemplateRepository.GetTemplateBasedOnEventId((int) NotificationEvent.AddWorknote);

                var message = _notificationRepository.GetPopulatedWorkNoteTemplate(template, incidentId, GetIncidentUrl(incidentId));

                //call the notification project to send the notification
                SendEmail(message);
            //}
        }

        private bool SendWorkNote()
        {
            var test =  _context.CurrentUser.ApplicationRoles.Any(x => x.ShortCode == "TCS");
            return test;
        }
        #endregion
        #region FYI
        public class SendFYICommand
        {
            public int IncidentId { get; set; }
            public string Message { get; set; }
            public IEnumerable<SecurityUserModel> Recipients { get; set; }

        }

        [Route(ApiRoutes.NotificationFyiMessage.Route, Name = ApiRoutes.NotificationFyiMessage.Name)]
        public void SendFyiNotification(SendFyiCommand sendFyiCommand)
        {
            // Check User is Authorised to Send FYI Notifications
            if (!_authoriser.IsAuthorised(AssetType.IncidentSendFYI)) return;

            IncidentStatus incidentStatus = _incidentViewRepository.GetById(sendFyiCommand.IncidentId).Status;

            // Check Incident Status permits Sending FYI Notifications
            if (incidentStatus != IncidentStatus.InProgress) return;

            // Retrieve Email template for FYI Notifications
            NotificationMessageTemplateModel templateModel =
                _notificationTemplateRepository.GetTemplateBasedOnEventId((int) NotificationEvent.SendFYI);

            // Populate template with Incident values
            NotificationMessageModel messageModel = 
                _notificationRepository.GetPopulatedFyiTemplate(templateModel, sendFyiCommand.IncidentId, sendFyiCommand.Recipients, GetIncidentUrl(sendFyiCommand.IncidentId));

            // Add incoming Option Message to Email body
            messageModel.Body = string.Format("{0} {1}", messageModel.Body, sendFyiCommand.OptionalMessage);
            
            
            SendEmail(messageModel);
            

            // Log the Notification
            var incident = _incidentViewRepository.GetById(sendFyiCommand.IncidentId);
            string payloadMessage = "An FYI has been sent to you for incident " + incident.FormalId + " at " + incident.TestCentre + ". " + sendFyiCommand.OptionalMessage;
            var payload = new
            {
                Sent =   DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss", new CultureInfo("en-GB")),
                Subject = messageModel.Subject,
                From = Request.GetOwinContext().Request.User.Identity.Name,
                To = messageModel.Recipient,
                Message = payloadMessage
            };

            _activityLogger.CreateCustomActivityLogEntry(
                sendFyiCommand.IncidentId,
                JsonConvert.SerializeObject(payload), 
                IncidentActivityLogType.FYI);
        }

        #endregion
        #region RejectIncident
        [Route(ApiRoutes.NotificationRejectIncident.Route, Name = ApiRoutes.NotificationRejectIncident.Name)]
        public void SendRejectIncidentNotification(int incidentId)
        {
            //need to have the incident id
        
            var template =
                _notificationTemplateRepository.GetTemplateBasedOnEventId((int)NotificationEvent.RejectedIncident);
            
            var message = _notificationRepository.GetPopulatedRejectIncidentTemplate(template, incidentId, GetIncidentUrl(incidentId));

            //call the notification project to send the notification
            SendEmail(message);

        }
        #endregion
        #region CloseIncident
        [Route(ApiRoutes.NotificationCloseIncident.Route, Name = ApiRoutes.NotificationCloseIncident.Name)]
        public void SendCloseIncidentNotification(int incidentId)
        {

            //need to have the incident id
            var template =
                _notificationTemplateRepository.GetTemplateBasedOnEventId((int)NotificationEvent.ClosedIncident);
            string user = _context.CurrentUser.FirstName + " "  + _context.CurrentUser.Surname;
            string role = _context.CurrentUser.ApplicationRoles.FirstOrDefault().Name;
            var message = _notificationRepository.GetPopulatedCloseIncidentTemplate(template, incidentId, GetIncidentUrl(incidentId), user, role);

            //call the notification project to send the notification
            SendEmail(message);
        }
        #endregion
        #region AcceptIncident
        [Route(ApiRoutes.NotificationAcceptedIncident.Route, Name = ApiRoutes.NotificationAcceptedIncident.Name)]
        public void SendAcceptedIncidentNotification(int incidentId)
        {
           
                //need to have the incident id
                var template =
                    _notificationTemplateRepository.GetTemplateBasedOnEventId((int)NotificationEvent.AcceptedIncident);

                var message = _notificationRepository.GetPopulatedAcceptedIncidentTemplate(template, incidentId, GetIncidentUrl(incidentId));

                //call the notification project to send the notification
                SendEmail(message);
           
        }
        #endregion
        #region RaiseIncident
        [Route(ApiRoutes.NotificationRaisedIncident.Route, Name = ApiRoutes.NotificationRaisedIncident.Name)]
        public void SendRaisedIncidentNotification(int incidentId)
        {

            //need to have the incident id
            var template =
                _notificationTemplateRepository.GetTemplateBasedOnEventId((int)NotificationEvent.RaisedIncident);

            var message = _notificationRepository.GetPopulatedRaisedIncidentTemplate(template, incidentId, GetIncidentUrl(incidentId));

                //call the notification project to send the notification
                SendEmail(message);
           
        }
        #endregion



        #region NewAction

        [Route(ApiRoutes.NotificationNewAction.Route, Name = ApiRoutes.NotificationNewAction.Name)]
        public void SendNewActionNotification(int incidentId, int actionId)
        {
            var template = _notificationTemplateRepository.GetTemplateBasedOnActionIdEventId((int)NotificationEvent.NewAction, actionId);
            

            var message = _notificationRepository.GetPopulatedEditActionTemplate(template, incidentId, actionId, GetActionUrl(incidentId, actionId));

            //call the notification project to send the notification
            SendEmail(message);

        }

        #endregion



        #region EditAction 

        [Route(ApiRoutes.NotificationEditAction.Route, Name = ApiRoutes.NotificationEditAction.Name)]
        public void SendEditActionNotification(int incidentId, int actionId)
        {

            //need to have the incident id
            var template =
                _notificationTemplateRepository.GetTemplateBasedOnEventId((int)NotificationEvent.EditAction);

            var message = _notificationRepository.GetPopulatedEditActionTemplate(template, incidentId, actionId, GetActionUrl(incidentId, actionId));

            //call the notification project to send the notification
            SendEmail(message);

        }

        #endregion




        #region CloseAction
        [Route(ApiRoutes.NotificationCloseAction.Route, Name = ApiRoutes.NotificationCloseAction.Name)]
        public void SendCloseActionNotification(int incidentId, int actionId)
        {

            //need to have the incident id
            var template =
                _notificationTemplateRepository.GetTemplateBasedOnEventId((int)NotificationEvent.RespondToAction);
            
            var message = _notificationRepository.GetPopulatedCloseActionTemplate(template, incidentId, GetIncidentUrl(incidentId), actionId);

            //call the notification project to send the notification
            SendEmail(message);

        }
        #endregion

        #region Helpers
        private string GetIncidentUrl(int incidentId)
        {
            string url = "";
            url = ConfigurationManager.AppSettings["IncidentUrl"];
#if DEBUG
            try
            {
                url = "http://" + HttpContext.Current.Request.Url.Authority;
            }
            catch
            {
            }
#endif
            url += "/incident/" + incidentId;
            return url;
        }



        private string GetActionUrl(int incidentId, int actionId)
        {
            string url = "";
            url = ConfigurationManager.AppSettings["IncidentUrl"];
            
            #if DEBUG
            url = "http://" + HttpContext.Current.Request.Url.Authority;
            #endif
            url += "/incident/action/" + incidentId;
            return url;
        }


        #endregion

        private bool SendEmail(NotificationMessageModel model)
        {

           
            bool sent;
            try
            {
                NotificationSender sender = new NotificationSender();
                sender.SendEmail(model, out sent);
                
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

    }

    public enum NotificationEvent
    {
        RaisedIncident = 1,
        AcceptedIncident = 2,
        RejectedIncident = 3,
        AddWorknote = 4,
        SendFYI = 5,
        NewAction = 6,
        EditAction = 7,
        RespondToAction = 8,
        ClosedIncident = 9
    }
}