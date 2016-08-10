using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using System.Collections.Generic;
using System.Linq;

namespace BC.EQCS.Web.Controllers.API
{
    public class TestLocationController : ApiController
    {
        private readonly IRepository<TestLocationModel> _repository;

        public TestLocationController(IRepository<TestLocationModel> repository)
        {
            _repository = repository;
        }

        [HttpGet, Route("api/testlocation")]
        public IEnumerable<TestLocationModel> Get()
        {
            var things = _repository.GetAll().Where(model => model.IsActive);
            return things;
        }

        [HttpGet, Route("api/testlocation/{code}")]
        public TestLocationModel Get(string code)
        {
            var location = _repository.GetByUniqueCode(code);
            return location;
        }
    }
}