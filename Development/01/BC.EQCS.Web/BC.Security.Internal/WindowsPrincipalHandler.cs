using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using BC.Security.Internal.Contracts;
using BC.Security.Internal.Contracts.Models;
using BC.StructureMap.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace BC.Security.Internal
{

    public class WindowsPrincipalHandler : AuthenticationHandler<WindowsPrincipalAuthenticationOptions>
    {
        public const string UserRequestKey = "BC.ExtendedWindowsAuth.RequestUser";

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var claimsPrincipal = Request.User as WindowsPrincipal;

            var userRepository = ResolveService<ISecurityUserRepository>();

            var user = await userRepository.GetUserForClaimsPrincipal(claimsPrincipal);

            user.Enabled = ValidateUser(user);

            if (!user.Enabled)
            {
                return null;
            }

            Request.Set(UserRequestKey, user);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            return new AuthenticationTicket(user.GetClaimsIdentity(), properties);
        }

        public override async Task<bool> InvokeAsync()
        {
            var ticket = await AuthenticateAsync();

            if (ticket != null)
            {
                Context.Authentication.SignIn(ticket.Properties, ticket.Identity);
                return false;
            }

            Context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            Context.Response.Write("User not authorised");
            return true;
        }

        private T ResolveService<T>()
        {
            var scope = Request.Get<StructureMapOwinDependencyScope>(StructureMapOwinMiddleware.ContextContainerKey);

            // This ensures that the current request context is used - needs more thought
            // TODO: wrap any container error with developer friendly error giving implementation details
            return scope.RequestContainer.GetInstance<T>();

        }

        /// <summary>
        /// In inherited classes this method can be overridden and additional logic inserted to determine whether the user has access to the application.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected virtual bool ValidateUser(UserModel user)
        {
            return user.Enabled;
        }

    }
}

