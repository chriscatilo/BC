using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class RiskRatingController : ApiController
    {
        private readonly IRepository<RiskRatingModel> _repository;

        public RiskRatingController(IRepository<RiskRatingModel> repository)
        {
            _repository = repository;
        }

        [Route(ApiRoutes.IncidentRiskRating.Route, Name = ApiRoutes.IncidentRiskRating.Name)]
        public IEnumerable<RiskRatingModel> Get()
        {
            var values = _repository.GetAll().Where(item => item.IsActive);

            return values;
        }
    }
}