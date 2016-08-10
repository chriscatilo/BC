using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BC.EQCS.Web.Infrastructure.Authentication
{
    /// <summary>
    /// Managed handler for windows authentication.
    /// The code is heavily commented and purposely not refactored for clear Understanding and Learning.
    /// </summary>
    public class WindowsLoginHandler :
        HttpTaskAsyncHandler, System.Web.SessionState.IRequiresSessionState
    {
        public HttpContext Context { get; set; }

        /// <summary>
        /// ProcessRequestAsync runs when User POSTS to /Windows/Login (clicks the Windows Login Button)
        /// See path registration in web.config and HTML Form action containing Windows Login Button
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ProcessRequestAsync(HttpContext context)
        {
            this.Context = context;

            // we are aat the beginning of the response pipeline. 
            // let the remaining pipeline know User wants to use Windows Authentication.
            // Global.asax.cs (next in pipeline) will pick this up and send 401 to User to negotiate

            // Scenario 1: the User might be already Authenticated so first Sign out
            if (context.User.Identity.IsAuthenticated)
            {
                // Sign out already Authenticated in User
                RouteData routeData = new RouteData();
                routeData.RouteHandler = new MvcRouteHandler();
                routeData.Values.Add("controller", "Account");
                routeData.Values.Add("action", "WindowsLogoff");

                HttpContextWrapper wrapper = new HttpContextWrapper(context);
                MvcHandler handler = new MvcHandler(new RequestContext(wrapper, routeData));

                IHttpAsyncHandler asyncHandler = ((IHttpAsyncHandler)handler);

                await Task.Factory.FromAsync(
                    asyncHandler.BeginProcessRequest,
                    asyncHandler.EndProcessRequest,
                    context,
                    null);

                // Now add custom header so the pipeline knows User wants to use Windows Authentication
                // By sending a 401 back to User's browser, we are telling IIS to authenticate
                // The value can be anything so here I've chosed "WindowsAuthRequested"
                context.Response.AddHeader("WindowsAuthRequested", "true");
            }

           // Scenario 2: Negotiation has not happened yet and User is not Authenticated 
            else if (!context.Request.LogonUserIdentity.IsAuthenticated)
            {
                // Add custom header so the pipeline knows User wants to use Windows Authentication
                // By sending a 401 back to User's browser, we are telling IIS to authenticate
                // The value can be anything so here I've chosed "WindowsAuthRequested"
                context.Response.AddHeader("WindowsAuthRequested", "true");
            }
            else
            {
                // after negotiation and IIS Windows Authentication, 
                // login via the configured authentication mechanism
                // in this case Cookie Authentication
                RouteData routeData = new RouteData();
                routeData.RouteHandler = new MvcRouteHandler();
                routeData.Values.Add("controller", "Account");
                routeData.Values.Add("action", "WindowsLogin");
                routeData.Values.Add("returnUrl", context.Request.Form["returnUrl"]);
                routeData.Values.Add("userName", context.Request.Form["UserName"]);

                HttpContextWrapper wrapper = new HttpContextWrapper(context);
                MvcHandler handler = new MvcHandler(new RequestContext(wrapper, routeData));

                IHttpAsyncHandler asyncHandler = ((IHttpAsyncHandler)handler);

                await Task.Factory.FromAsync(
                    asyncHandler.BeginProcessRequest,
                    asyncHandler.EndProcessRequest,
                    context,
                    null);

            }
        }
    }
}
