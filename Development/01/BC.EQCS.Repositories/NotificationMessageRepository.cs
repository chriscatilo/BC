using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BC.EQCS.Contracts;
using BC.EQCS.Entities;
using BC.EQCS.Entities.Models;
using BC.EQCS.Entities.Utils;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Models;

namespace BC.EQCS.Repositories
{
    public class NotificationMessageRepository : Repository<NotificationMessage, NotificationMessageModel>,
                                            INotificationRepository<NotificationMessageModel, NotificationMessageTemplateModel>
    {
        private readonly ITreeRepository<AdminUnitModel> _adminUnitRepository;
    
        public NotificationMessageRepository(IEntityFactory entityFactory, ITreeRepository<AdminUnitModel> adminUnitRepository) : base(entityFactory)
        {
            _adminUnitRepository = adminUnitRepository;
            KeyValue = notificationMessage => notificationMessage.Id;
        }

        #region WorkNoteNotification
        public NotificationMessageModel GetPopulatedWorkNoteTemplate(NotificationMessageTemplateModel value, int incidentId, string url)
        {
             var incidentEntity = Context
                .Incidents
                .IncludeAllNavigationProperties(Context)
                .FirstOrDefault(incident => incident.Id == incidentId);
            if (incidentEntity == null)
            {
                return null;
            }
            var subject = ReplaceWorkNoteTemplateSubjectLine(value.SubjectLine,incidentEntity.TestCentre.CentreNumber,incidentEntity.FormalId);
            var mainBody = ReplaceWorkNoteTemplateBodyText(value.BodyText,incidentEntity.TestCentre.Name,incidentEntity.TestCentre.CentreNumber,incidentEntity.FormalId, incidentId, url);
            var recipients = GetRecipients(value.Id);

            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = recipients;
            return returnTemplate;
        }

        private string ReplaceWorkNoteTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber, int incidentId, string url)
        {
            //<TCNumber>
            //<INCIDENTNO>
            //<Name>
            //IncidentNo is to be a link

            url = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<INCIDENTNO>", url);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            return bodyText;
        }

        private string ReplaceWorkNoteTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            //<TCNumber>
            //<INCIDENTNO>
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        #endregion WorkNoteNotification
        
        #region SendFYINotification
        public NotificationMessageModel GetPopulatedFyiTemplate(NotificationMessageTemplateModel value, int incidentId, IEnumerable<SecurityUserModel> recipients, string url)
        {
            var incidentEntity = Context
               .Incidents
               .IncludeAllNavigationProperties(Context)
               .FirstOrDefault(incident => incident.Id == incidentId);
            if (incidentEntity == null)
            {
                return null;
            }
            var subject = ReplaceSendFyiTemplateSubjectLine(value.SubjectLine, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId);
            var mainBody = ReplaceSendFyiTemplateBodyText(value.BodyText, incidentEntity.TestCentre.Name, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId, url);
            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = string.Join(",", (from r in recipients select r.EmailAddress).Distinct().ToList());
            return returnTemplate;
        }

        private string ReplaceSendFyiTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber, string url)
        {
            var link = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<TCName>", testCentreName);
            bodyText = bodyText.Replace("<INCIDENTNO>", link);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            
            return bodyText;
        }

        private string ReplaceSendFyiTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        #endregion SendFYINotification

        #region RejectIncidentNotification

        public NotificationMessageModel GetPopulatedRejectIncidentTemplate(NotificationMessageTemplateModel value,
            int incidentId, string url)
        {
            var incidentEntity = GetIncident(incidentId);

            if (incidentEntity == null)
            {
                return null;
            }
            //this text needs to come from the activity log once the workflow bug is fixed
            string rejectionText = "test";
            var subject = ReplaceRejectIncidentTemplateSubjectLine(value.SubjectLine, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId);
            var mainBody = ReplaceRejectIncidentTemplateBodyText(value.BodyText, incidentEntity.TestCentre.Name, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId, url, rejectionText);


            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = GetRecipients(value.Id);
            return returnTemplate;
        }
        private string ReplaceRejectIncidentTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber, string url, string rejectionText)
        {
            //<TCNumber>
            //<INCIDENTNO>
            //<Name>
            //IncidentNo is to be a link
            //RejectionText
            url = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<INCIDENTNO>", url);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            bodyText = bodyText.Replace("<RejectionText>", rejectionText);

            return bodyText;
        }

        private string ReplaceRejectIncidentTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            //<TCNumber>
            //<INCIDENTNO>
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        #endregion

        #region CloseIncidentNotification

        public NotificationMessageModel GetPopulatedCloseIncidentTemplate(NotificationMessageTemplateModel value,
            int incidentId, string url, string user, string role)
        {
            var incidentEntity = GetIncident(incidentId);

            if (incidentEntity == null)
            {
                return null;
            }
            var subject = ReplaceCloseIncidentTemplateSubjectLine(value.SubjectLine, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId);
            var mainBody = ReplaceCloseIncidentTemplateBodyText(value.BodyText, incidentEntity.TestCentre.Name, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId,url, user, role);
            

            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = GetRecipients(value.Id);
            return returnTemplate;
        }
        private string ReplaceCloseIncidentTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber,string url, string user, string role)
        {
            //<TCNumber>
            //<INCIDENTNO>
            //<Name>
            //IncidentNo is to be a link
            //<date>
            //<user>
            //<role>

            url = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<INCIDENTNO>", url);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            bodyText = bodyText.Replace("<date>", DateTime.Today.ToShortDateString());
            //user
            bodyText = bodyText.Replace("<user>", user);
            bodyText = bodyText.Replace("<role>", role);
            
            return bodyText;
        }

        private string ReplaceCloseIncidentTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            //<TCNumber>
            //<INCIDENTNO>
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        #endregion

        #region AcceptedIncidentNotification
        public NotificationMessageModel GetPopulatedAcceptedIncidentTemplate(NotificationMessageTemplateModel value, int incidentId, string url)
        {
            var incidentEntity = Context
               .Incidents
               .IncludeAllNavigationProperties(Context)
               .FirstOrDefault(incident => incident.Id == incidentId);
            if (incidentEntity == null)
            {
                return null;
            }
            var subject = ReplaceAcceptedIncidentTemplateSubjectLine(value.SubjectLine, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId);
            var mainBody = ReplaceAcceptedIncidentTemplateBodyText(value.BodyText, incidentEntity.TestCentre.Name, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId, incidentId, url);
            var recipients = GetRecipients(value.Id);

            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = recipients;
            return returnTemplate;
        }
        private string ReplaceAcceptedIncidentTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber, int incidentId, string url)
        {
            //<TCNumber>
            //<INCIDENTNO>
            //<Name>
            //IncidentNo is to be a link

            url = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<INCIDENTNO>", url);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            return bodyText;
        }

        private string ReplaceAcceptedIncidentTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            //<TCNumber>
            //<INCIDENTNO>
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        #endregion

        #region RaisedIncidentNotification
        public NotificationMessageModel GetPopulatedRaisedIncidentTemplate(NotificationMessageTemplateModel value, int incidentId, string url)
        {
            var incidentEntity = GetIncident(incidentId);

            var subject = ReplaceRaisedIncidentTemplateSubjectLine(value.SubjectLine, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId);
            var venueName = incidentEntity.TestLocation.Name;
            var mainBody = ReplaceRaisedIncidentTemplateBodyText(value.BodyText, incidentEntity.TestCentre.Name, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId, incidentId, url, venueName);
            var recipients = GetRecipientsForRaisedIncidents(value.Id, incidentEntity);

            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = recipients;
            return returnTemplate;
        }
        private string ReplaceRaisedIncidentTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            //<TCNumber>
            //<INCIDENTNO>
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        private string ReplaceRaisedIncidentTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber, int incidentId, string url, string venueName)
        {
            //<TCNumber>
            //<INCIDENTNO>
            //<Name>
            //IncidentNo is to be a link
            //<TLName>"

            url = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<INCIDENTNO>", url);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            bodyText = bodyText.Replace("<TLName>", venueName);
            return bodyText;
        }
        #endregion

        #region CloseActionNotification

        public NotificationMessageModel GetPopulatedCloseActionTemplate(NotificationMessageTemplateModel value,
            int incidentId, string url, int actionId)
        {
            var incidentEntity = GetIncident(incidentId);
            var action = GetAction(actionId);

            var subject = ReplaceCloseActionTemplateSubjectLine(value.SubjectLine, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId);
            var response = action.ActionResponse;
            var mainBody = ReplaceCloseActionTemplateBodyText(value.BodyText, incidentEntity.TestCentre.Name, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId, incidentId, url, response, action.ActionDescription);
            var recipients = GetRecipients(value.Id);

            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = recipients;
            return returnTemplate;
        }

        private string ReplaceCloseActionTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            //<TCNumber>
            //<INCIDENTNO>
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        private string ReplaceCloseActionTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber, int incidentId, string url, string response, string actionDescription)
        {
            //<action>
            //<TCNumber>
            //<Name>
            //<INCIDENTNO>
            //IncidentNo is to be a link
            //<Response>

            url = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<INCIDENTNO>", url);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            bodyText = bodyText.Replace("<response>", response);
            bodyText = bodyText.Replace("<action>", actionDescription);
            return bodyText;
        }

        #endregion



        #region NewActionNotification

        public NotificationMessageModel GetPopulatedNewActionTemplate(NotificationMessageTemplateModel value, int incidentId, int actionId, string url)
        {
            var incidentEntity = GetIncident(incidentId);
            var action = GetAction(actionId);

            var subject = ReplaceNewActionTemplateSubjectLine(value.SubjectLine, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId);
            var venueName = incidentEntity.TestLocation.Name;
            var mainBody = ReplaceNewActionTemplateBodyText(value.BodyText, incidentEntity.TestCentre.Name, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId, incidentId, url, venueName, action.ActionDescription);
            var recipients = GetRecipientsForEditAction(value.Id, action);

            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = recipients;
            return returnTemplate;
        }


        private string ReplaceNewActionTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            //<TCNumber>
            //<INCIDENTNO>
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        private string ReplaceNewActionTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber, int incidentId, string url, string venueName, string actionDescription)
        {
            //<TCNumber>
            //<INCIDENTNO>
            //<Name>
            //IncidentNo is to be a link

            url = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<INCIDENTNO>", url);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            bodyText = bodyText.Replace("<TLName>", venueName);
            bodyText = bodyText.Replace("<ACTDESC>", actionDescription);
            return bodyText;
        }

        #endregion





        #region EditActionNotification

        public NotificationMessageModel GetPopulatedEditActionTemplate(NotificationMessageTemplateModel value, int incidentId, int actionId, string url)
        {
            var incidentEntity = GetIncident(incidentId);
            var action = GetAction(actionId);

            var subject = ReplaceEditActionTemplateSubjectLine(value.SubjectLine, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId);
            var venueName = incidentEntity.TestLocation.Name;
            var mainBody = ReplaceEditActionTemplateBodyText(value.BodyText, incidentEntity.TestCentre.Name, incidentEntity.TestCentre.CentreNumber, incidentEntity.FormalId, incidentId, url, venueName, action.ActionDescription);
            var recipients = GetRecipientsForEditAction(value.Id, action);

            var returnTemplate = new NotificationMessageModel();
            returnTemplate.Body = mainBody;
            returnTemplate.Subject = subject;
            returnTemplate.Recipient = recipients;
            return returnTemplate;
        }


        private string ReplaceEditActionTemplateSubjectLine(string subjectText, string testCentreNumber, string incidentNumber)
        {
            //<TCNumber>
            //<INCIDENTNO>
            subjectText = subjectText.Replace("<TCNumber>", testCentreNumber);
            subjectText = subjectText.Replace("<INCIDENTNO>", incidentNumber);
            return subjectText;
        }
        private string ReplaceEditActionTemplateBodyText(string bodyText, string testCentreName, string testCentreNumber, string incidentNumber, int incidentId, string url, string venueName, string actionDescription)
        {
            //<TCNumber>
            //<INCIDENTNO>
            //<Name>
            //IncidentNo is to be a link

            url = "<a href='" + url + "'>" + incidentNumber + "</a>";
            bodyText = bodyText.Replace("<TCNumber>", testCentreNumber);
            bodyText = bodyText.Replace("<INCIDENTNO>", url);
            bodyText = bodyText.Replace("<Name>", testCentreName);
            bodyText = bodyText.Replace("<TLName>", venueName);
            bodyText = bodyText.Replace("<ACTDESC>", actionDescription);
            return bodyText;
        }

        #endregion



        #region Helpers
        

        private string GetUsersForTestCentre(IncidentAction action)
        {
            //To get users assigned to the test centre
            //We need to incident
            //From the incident we need the testlocation
            //From the testlocation we need the AdminUnit
            //From the admin unit, we need all users who also have that admin unit in any of their roles
            var incident = Context.Incidents.FirstOrDefault(i => i.Id.Equals(action.IncidentId));
            var testLocation = incident.TestLocation;
            var testCentre = incident.TestCentre;
            var testLocationAdminUnit = testLocation.AdminUnit;
            var testCentreAdminUnit = testCentre.AdminUnit;

            var allUsersFromTestCentre = Context.Users
                .Where(au => au.UserToRoleToAdminUnits.Any(ura => ura.AdminUnit.Code.Equals(testLocationAdminUnit.Code) || ura.AdminUnit.Code.Equals(testCentreAdminUnit.Code)))
                .Where(au => au.Enabled)
                .ToList();

            return String.Join(",", allUsersFromTestCentre.Select(users => users.Email));
        }

        private string GetRecipientsForEditAction(int id, IncidentAction action)
        {
            //If this action is assigned to the test centre then we need to get the addresses for all members of that test centre
            //If this action is assigned to users, then we need to get the addresses of only those users who are assigned to the test centre
            if (action.AssignedToTestCentre)
            {
                return GetUsersForTestCentre(action);
            }
           
             return String.Join(",", action.AssignedTo.Select(users => users.Email));
        }

        private string GetRecipients(int templateId)
        {
            var roles = Context.NotificationMappings.Where(map => map.MessageTemplateId == templateId).Select(role => role.RoleId).ToList();

            var recipents = (from a in Context.Users
                                join b in Context.UserToRoleToAdminUnits on a.Id equals b.ApplicationUserId
                                join c in Context.ApplicationRoles on b.ApplicationRoleId equals c.Id
                                where roles.Contains(b.ApplicationRoleId)
                                where a.Enabled
                                select a.Email).Distinct().ToList();

                    

            return string.Join(",", recipents);
        }

        private string GetRecipientsForRaisedIncidents(int templateId, Incident incident)
        {
            //We have to get the user who raised the incident from the Activity Log
            var aLog = Context.IncidentActivityLogs
                .IncludeAllNavigationProperties(Context)
                .Include(i => i.ApplicationUser.UserToRoleToAdminUnits)
                .Where(al => al.IncidentId.Equals(incident.Id))
                .FirstOrDefault(al => al.LogType == IncidentActivityLogType.Submission);

            var raisdByUserRoleId = aLog.ApplicationUser.UserToRoleToAdminUnits.FirstOrDefault().ApplicationRoleId;

            var roleIdsForCurrentTemplate = Context.NotificationMappings
                .Where(map => map.MessageTemplateId == templateId)
                .Where(m => m.RaisedByRoleId == null || m.RaisedByRoleId.Value.Equals(raisdByUserRoleId))
                .Select(role => role.RoleId).ToList();


            //We need to make sure that we select the users in the role, where the users admin unit is at a higher up the tree or equal to
            //the locations admin unit
            var incidentLocationAdminUnit = incident.TestLocation.AdminUnit;
            var ancestorsOfIncidientLocation =
                _adminUnitRepository.GetAllAncestorsOfNodeByCode(incidentLocationAdminUnit.Code);
            var codesOfIncidentLocationAncestors = ancestorsOfIncidientLocation.Select(a => a.Code);


            //What we need here is any applicationuser which has a role that is in the Notification mapping table entry
            //and also where that users adminunit for that role has the incidents location admin unit either equal or under it's hierarchy

            //This query gives us the application users which match the roles listed in the mapping table
            var usersMappedToEventType = (from a in Context.Users
                join b in Context.UserToRoleToAdminUnits on a.Id equals b.ApplicationUserId
                join c in Context.ApplicationRoles on b.ApplicationRoleId equals c.Id
                where roleIdsForCurrentTemplate.Contains(b.ApplicationRoleId)
                select a);

            //Now we should filter out any users which don't have the incidents admin unit in their hierarchy
            var recipents =
                usersMappedToEventType
                    .Where(
                        au =>
                            codesOfIncidentLocationAncestors.Contains(
                                au.UserToRoleToAdminUnits.FirstOrDefault().AdminUnit.Code))
                    .Where(au => au.Enabled)
                    .Distinct()
                    .Select(u => u.Email)
                    .ToList();

            return string.Join(",", recipents);


        }

        private Incident GetIncident(int incidentId)
        {
            return Context
              .Incidents
              .IncludeAllNavigationProperties(Context)
              .Include(i => i.TestLocation.AdminUnit)
              .Include(i => i.TestCentre.AdminUnit)
              .FirstOrDefault(incident => incident.Id == incidentId);
        }

        private IncidentAction GetAction(int actionId)
        {
            return Context
              .IncidentActions
              .IncludeAllNavigationProperties(Context)
              .FirstOrDefault(action => action.Id == actionId);
        }

        #endregion Helpers
    }
}
