using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using System.Collections.Generic;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class CountryController : ApiController
    {
        private readonly IRepository<CountryModel> _countryRepository;

        public CountryController(IRepository<CountryModel> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [Route(ApiRoutes.Country.Route, Name = ApiRoutes.Country.Name)]
        public IEnumerable<CountryModel> Get()
        {
            return _countryRepository.GetAll();
        }

    }
}