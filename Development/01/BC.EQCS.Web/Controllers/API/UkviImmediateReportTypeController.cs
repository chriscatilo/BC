using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using System.Collections.Generic;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class UkviImmediateReportTypeController : ApiController
    {
        private readonly IRepository<UkviImmediateReportTypeModel> _repository;

        public UkviImmediateReportTypeController(IRepository<UkviImmediateReportTypeModel> repository)
        {
            _repository = repository;
        }

        [Route(ApiRoutes.UkviImmediateReportType.Route, Name = ApiRoutes.UkviImmediateReportType.Name)]
        public IEnumerable<UkviImmediateReportTypeModel> Get()
        {
            return _repository.GetAll();
        }

    }
}