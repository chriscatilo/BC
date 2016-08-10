using System.Web.Http;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Web.Controllers.API
{
    public class UserController : ApiController
    {
        private readonly IContextResolver _userContext;

        public UserController(IContextResolver userContext)
        {
            _userContext = userContext;
        }

        [HttpGet]
        public dynamic Get()
        {
            var user = _userContext.CurrentUser;

            return Ok(user);
        }
    }
}