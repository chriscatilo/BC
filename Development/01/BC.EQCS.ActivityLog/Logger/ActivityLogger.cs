using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BC.EQCS.ActivityLog.Logger.PayloadModels;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using Newtonsoft.Json;

namespace BC.EQCS.ActivityLog.Logger
{
    // TODO #4034 - Refactor ActivityLogger
    public class ActivityLogger<TModel, TModelAttributes> : IActivityLogger<TModel, TModelAttributes>
    {
        private readonly IRepository<IncidentActivityLogModel> _activityLogRepository;
        private readonly IRepository<TModel> _repositoryInUse;
        private readonly IUserContext _user;
        private TModel _initialModel;
        private int _masterItemId;
        private int _updatedItemId;

        public ActivityLogger(IRepository<IncidentActivityLogModel> activityLogRepository, IUserContext user,
            IRepository<TModel> repositoryInUse)
        {
            _activityLogRepository = activityLogRepository;
            _user = user;
            _repositoryInUse = repositoryInUse;
        }

        public void CreateCustomActivityLogEntry(int incidentId, string payload, IncidentActivityLogType activityLogType)
        {
            _activityLogRepository.Create(new IncidentActivityLogModel
            {
                User = _user.CurrentUser,
                Payload = payload,
                LogType = activityLogType,
                IncidentId = incidentId,
                DateTimeOfActivity = DateTime.UtcNow
            });
        }

        public void LogAllModelValues(int instanceId, int masterId, IncidentActivityLogType activityLogType)
        {
            _activityLogRepository.Create(ModelCreatedPayloadEntry(_repositoryInUse.GetById(instanceId), masterId,
                activityLogType));
        }

        public void LogAllModelValues(int instanceId, IncidentActivityLogType activityLogType)
        {
            _activityLogRepository.Create(ModelCreatedPayloadEntry(_repositoryInUse.GetById(instanceId), instanceId,
                activityLogType));
        }

        public void OpenDifferenceLoggingProcess(int updatedItemId, int masterId)
        {
            _updatedItemId = updatedItemId;
            _masterItemId = masterId;

            _initialModel = _repositoryInUse.GetById(updatedItemId);
        }

        public void OpenDifferenceLoggingProcess(int updatedItemId)
        {
            _updatedItemId = updatedItemId;
            _masterItemId = updatedItemId;

            _initialModel = _repositoryInUse.GetById(_masterItemId);
        }

        public void CompleteDifferenceLoggingProcess(IncidentActivityLogType activityLogType)
        {
            var newModel = _repositoryInUse.GetById(_updatedItemId);
            var changedItemsEntry = ModelChangedPayloadEntry(newModel, _masterItemId, activityLogType);

            if (changedItemsEntry != null)
                _activityLogRepository.Create(changedItemsEntry);
        }

        private IncidentActivityLogModel ModelChangedPayloadEntry(TModel newModel, int masterItemId,
            IncidentActivityLogType activityLogType)
        {
            var type = typeof (TModelAttributes);
            var attribProperties = type.GetProperties();

            var payloads = new List<ChangedPropertyValuesPayload>();

            var newModelProperties = newModel.GetType().GetProperties();

            foreach (var tempAttributeNamesProperty in attribProperties)
            {
                // if attribute is not be tracked then skip
                if (tempAttributeNamesProperty.GetCustomAttributes(typeof (DoNotLogActivityAttribute), true).Any())
                {
                    continue;
                }
                ;

                // if attribute has not matching new model property then skip
                if (!newModelProperties.Any(p => p.Name.Equals(tempAttributeNamesProperty.Name)))
                {
                    continue;
                }

                //Treat any null values as empty strings
                var viewValuesProperty =
                    newModelProperties.First(p => p.Name.Equals(tempAttributeNamesProperty.Name));

                var initialValue = viewValuesProperty.GetValue(_initialModel) == null
                    ? ""
                    : viewValuesProperty.GetValue(_initialModel).ToString();
                var newValue = viewValuesProperty.GetValue(newModel) == null
                    ? ""
                    : viewValuesProperty.GetValue(newModel).ToString();

                var newValueIsACollection =
                    (viewValuesProperty.PropertyType.GetInterface(typeof (IEnumerable<>).FullName) != null ||
                     typeof (IEnumerable).IsAssignableFrom(viewValuesProperty.PropertyType)) &&
                    viewValuesProperty.PropertyType != typeof (string);
                if (newValueIsACollection)
                {
                    initialValue =
                        GetCollectionEntryValueForCollection(
                            viewValuesProperty.GetValue(_initialModel) as ICollection);
                    newValue =
                        GetCollectionEntryValueForCollection(viewValuesProperty.GetValue(newModel) as ICollection);
                }

                if (newValueIsACollection || !initialValue.Equals(newValue))
                {
                    //Get either the display name or if none provided use the property name with added spaces
                    var displayNameAtt =
                        tempAttributeNamesProperty.GetCustomAttributes(typeof (DisplayNameAttribute), true)
                            .SingleOrDefault();
                    var logNameText = tempAttributeNamesProperty.Name;
                    var labelName = tempAttributeNamesProperty.Name;

                    //If the display name has been set, overite the propertyname as the log entry
                    if (displayNameAtt != null)
                        labelName = ((DisplayNameAttribute) displayNameAtt).DisplayName;


                    var payload = new ChangedPropertyValuesPayload
                    {
                        FieldChanged = logNameText,
                        Label = labelName,
                        OriginalValue = initialValue,
                        NewValue = newValue
                    };


                    payloads.Add(payload);
                }
            }


            if (payloads.Any())
                return new IncidentActivityLogModel
                {
                    DateTimeOfActivity = DateTime.Now,
                    IncidentId = masterItemId,
                    LogType = activityLogType,
                    Payload = JsonConvert.SerializeObject(payloads),
                    User = _user.CurrentUser
                };


            return null;
        }

        private IncidentActivityLogModel ModelCreatedPayloadEntry(TModel newModel, int incidentId,
            IncidentActivityLogType activityLogType)
        {
            var type = typeof (TModelAttributes);
            var attribProperties = type.GetProperties();

            //Payload collection
            var payloads = new List<AllPropertyValuesPayload>();

            var newModelProperties = newModel.GetType().GetProperties();

            //Compare each property in the template
            foreach (var tempAttributeNamesProperty in attribProperties)
            {
                // if attribute is not be tracked then skip
                if (tempAttributeNamesProperty.GetCustomAttributes(typeof (DoNotLogActivityAttribute), true).Any())
                {
                    continue;
                };

                // if attribute has not matching new model property then skip
                if (!newModelProperties.Any(p => p.Name.Equals(tempAttributeNamesProperty.Name)))
                {
                    continue;
                }

                //Treat any null values as empty strings

                var viewValuesProperty =
                    newModelProperties.First(p => p.Name.Equals(tempAttributeNamesProperty.Name));
                var newValue = viewValuesProperty.GetValue(newModel) == null
                    ? ""
                    : viewValuesProperty.GetValue(newModel).ToString();


                var newValueIsACollection =
                    (viewValuesProperty.PropertyType.GetInterface(typeof (IEnumerable<>).FullName) != null ||
                     typeof (IEnumerable).IsAssignableFrom(viewValuesProperty.PropertyType)) &&
                    viewValuesProperty.PropertyType != typeof (string);
                if (newValueIsACollection)
                {
                    newValue =
                        GetCollectionEntryValueForCollection(viewValuesProperty.GetValue(newModel) as ICollection);
                }


                //Get either the display name or if none provided use the property name with added spaces
                var displayNameAtt =
                    tempAttributeNamesProperty.GetCustomAttributes(typeof (DisplayNameAttribute), true)
                        .SingleOrDefault();
                var logNameText = tempAttributeNamesProperty.Name;
                var labelName = tempAttributeNamesProperty.Name;

                //If the display name has been set, overwrite the propertyname as the log entry
                if (displayNameAtt != null)
                    labelName = ((DisplayNameAttribute) displayNameAtt).DisplayName;


                var payload = new AllPropertyValuesPayload
                {
                    FieldName = logNameText,
                    Label = labelName,
                    Value = newValue
                };

                if (payload.Value != "")
                    payloads.Add(payload);
            }

            var logEntry = new IncidentActivityLogModel
            {
                DateTimeOfActivity = DateTime.Now,
                IncidentId = incidentId,
                LogType = activityLogType,
                Payload = JsonConvert.SerializeObject(payloads),
                User = _user.CurrentUser
            };

            return logEntry;
        }

        private string GetCollectionEntryValueForCollection(ICollection newValue)
        {
            var stringBuild = new StringBuilder();

            foreach (var tempItem in newValue)
            {
                stringBuild.AppendLine(tempItem.ToString());
            }

            return stringBuild.ToString();
        }
    }
}