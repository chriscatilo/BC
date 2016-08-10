using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BC.EQCS.Security.Service;

namespace BC.EQCS.Web.Infrastructure.Authorisation
{
    public class UserAuthorisationFilterAttribute : ActionFilterAttribute
    {
        internal string _securityAssetKey;
        private IAssetAuthoriser authoriser;

        internal UserAuthorisationFilterAttribute()
        {
            authoriser = new AuthoriserFactory().Create();
        }

        public UserAuthorisationFilterAttribute(string securityAssetKey) : base()
        {
            _securityAssetKey = securityAssetKey;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            authoriser = new AuthoriserFactory().Create();

            if (authoriser != null && !authoriser.IsAuthorised(_securityAssetKey))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "User is not authorised to perform this action");// A 401 causes the connection to abort
            }
        }
    }
}