using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Models.Extensions;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class AdminUnitController : ApiController
    {
        private readonly IUserContext _userContext;

        public AdminUnitController(IUserContext userContext)
        {
            _userContext = userContext;
        }

        [Route(ApiRoutes.IncidentAdminUnit.Route, Name = ApiRoutes.IncidentAdminUnit.Name)]
        public AdminUnitModel Get()
        {
            var value = _userContext.CurrentUser.AdminStructure;

            // return tree with only active nodes
            return value.FindByCodeActiveOnly(value.Code);
        }
    }
}
