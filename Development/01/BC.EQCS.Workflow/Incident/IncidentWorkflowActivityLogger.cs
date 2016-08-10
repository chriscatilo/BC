using System;
using BC.EQCS.Contracts;
using BC.EQCS.DataTransfer;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Workflow.Incident
{
    public abstract class IncidentWorkflowActivityLogger
    {
        private readonly IRepository<IncidentActivityLogModel> _activityLogRepository;
        private readonly IModelValidator<IncidentActivityLogModel> _activityLogValidator;
        private readonly IContextResolver _contextResolver;

        protected IncidentWorkflowActivityLogger(
            IRepository<IncidentActivityLogModel> activityLogRepository,
            IModelValidator<IncidentActivityLogModel> activityLogValidator,
            IContextResolver contextResolver)
        {
            _activityLogRepository = activityLogRepository;
            _activityLogValidator = activityLogValidator;
            _contextResolver = contextResolver;
        }

        public abstract IncidentStatus ForIncidentStatus { get; }

        protected void Execute(int modelId, IncidentActivityLogModel logEntry)
        {
            logEntry.IncidentId = modelId;
            logEntry.DateTimeOfActivity = DateTime.UtcNow;
            logEntry.User = Mapper.Map<UserModel>(_contextResolver.CurrentUser);
            _activityLogValidator.ValidateModel(logEntry);
            _activityLogRepository.Create(logEntry);
        }
    }
}