using System.Web.Http;
using BC.EQCS.Contracts;
using BC.EQCS.Models;
using BC.EQCS.Models.Extensions;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.API
{
    public class IncidentClassController : ApiController
    {
        private readonly IUserContext _userContext;

        public IncidentClassController(
            IUserContext userContext)
        {
            _userContext = userContext;
        }

        [Route(ApiRoutes.IncidentClass.Route, Name = ApiRoutes.IncidentClass.Name)]
        public IncidentClassModel Get()
        {
            var value = _userContext.CurrentUser.AvailableIncidentClasses;

            return value.FindByCodeActiveOnly(value.Code);
        }
    }
}