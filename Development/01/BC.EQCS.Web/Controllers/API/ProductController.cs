using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using System.Collections.Generic;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class ProductController : ApiController
    {
        private readonly IRepository<ProductModel> _repository;

        public ProductController(IRepository<ProductModel> repository)
        {
            _repository = repository;
        }

        [Route(ApiRoutes.IncidentProduct.Route, Name = ApiRoutes.IncidentProduct.Name)]
        public IEnumerable<ProductModel> Get()
        {
            return _repository.GetAll();
        }
    }
}