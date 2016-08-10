using System.Collections.Generic;
using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class OrganisationTypeController : ApiController
    {
        private readonly IRepository<OrganisationTypeModel> _repository;

        public OrganisationTypeController(IRepository<OrganisationTypeModel> repository)
        {
            _repository = repository;
        }

        [Route(ApiRoutes.IncidentOrgType.Route, Name = ApiRoutes.IncidentOrgType.Name)]
        public IEnumerable<OrganisationTypeModel> Get()
        {
            return _repository.GetAll();
        }
    }
}
