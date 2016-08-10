using System;
using System.Net;
using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Models.Enums;
using BC.EQCS.Security.Constants;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Web.Controllers.API
{
    public class WorkNoteController : ApiController
    {
        private readonly IRepository<IncidentActivityLogModel> _activityLogRepository;
        private readonly IAssetAuthoriser _authoriser;
        private readonly IUserContext _userContext;

        public WorkNoteController(IRepository<IncidentActivityLogModel> activityLogRepository, IAssetAuthoriser authoriser, IUserContext userContext)
        {
            _activityLogRepository = activityLogRepository;
            _authoriser = authoriser;
            _userContext = userContext;
        }


        public IHttpActionResult Post([FromBody]WorkNoteDto workNote)
        {
            var isNotAuthorised = !_authoriser.IsAuthorised(AssetType.IncidentActivityAddWorkNote, workNote.IncidentId);

            if (isNotAuthorised)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            Save(workNote);

            return Ok();
        }

        private void Save(WorkNoteDto workNote)
            {
                var logModel = new IncidentActivityLogModel
                {
                    DateTimeOfActivity = DateTime.Now.ToUniversalTime(),
                    Payload = workNote.Content,
                    IncidentId = workNote.IncidentId,
                    LogType = IncidentActivityLogType.WorkNote,
                    User = _userContext.CurrentUser
                };

                _activityLogRepository.Create(logModel);
            }


        public class WorkNoteDto
        {
            public Int32 Id { get; set; }
            public Int32 IncidentId { get; set; }
            public String Content { get; set; }
        }
    }
}
