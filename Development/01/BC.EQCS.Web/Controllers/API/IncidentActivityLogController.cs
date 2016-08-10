using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Domain.Exceptions;
using BC.EQCS.Models;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class IncidentActivityLogController : ApiController
    {
        private readonly IAspectRepository<IncidentActivityLogModel, IncidentMasterModel> _activityLogRepository;
        private readonly IRepository<IncidentMasterModel> _modelRepository;

        public IncidentActivityLogController(
            IRepository<IncidentMasterModel> modelRepository,
            IAspectRepository<IncidentActivityLogModel, IncidentMasterModel> activityLogRepository)
        {
            _modelRepository = modelRepository;
            _activityLogRepository = activityLogRepository;
        }

        [HttpGet, Route(ApiRoutes.IncidentByIdActivityLog.Route, Name = ApiRoutes.IncidentByIdActivityLog.Name)]
        public dynamic GetForIncident(int id)
        {
            var model = _modelRepository.GetById(id);

            if (model == null)
            {
                throw new ModelNotFoundException();
            }

            var log = _activityLogRepository.GetFor(model);

            return log;
        }
    }
}