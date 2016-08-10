using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using System.Collections.Generic;
using System.Linq;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class ResidualRiskRatingsController : ApiController
    {
        private readonly IRepository<ResidualRiskRatingModel> _repository;

        public ResidualRiskRatingsController(IRepository<ResidualRiskRatingModel> repository)
        {
            _repository = repository;
        }

        [Route(ApiRoutes.IncidentResidualRiskRating.Route, Name = ApiRoutes.IncidentResidualRiskRating.Name)]
        public IEnumerable<ResidualRiskRatingModel> Get()
        {
            var values = _repository.GetAll().Where(item => item.IsActive);

            return values;
        }
    }
}