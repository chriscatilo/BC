using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using BC.EQCS.Entities.Models;
using BC.EQCS.Models;
using BC.EQCS.Notifications;
using BC.EQCS.Web.Models;
using BC.EQCS.Web.Utils;
using BC.Security.Internal.Contracts;
using BC.StructureMap.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.DataProtection;
using Newtonsoft.Json;

namespace BC.EQCS.Web.Controllers.UI
{
    public class AccountController : Controller
    {
        public ApplicationUserManager UserManager { get; private set; }
        
        public AccountController()
        {
            
            //TODO (Ravi): Use DI and ValidateAntiForgeryToken on all POSTs
            UserManager = ApplicationUserManager.Create(new IdentityFactoryOptions<ApplicationUserManager>());
        }

        [AllowAnonymous]
        [Route(MvcRoutes.AccountLoginView.Route, Name = MvcRoutes.AccountLoginView.Name)]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [AllowAnonymous]
        [Route(MvcRoutes.AccountLoginAutoView.Route, Name = MvcRoutes.AccountLoginAutoView.Name)]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult LoginAuto(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(MvcRoutes.AccountLogin.Route, Name = MvcRoutes.AccountLogin.Name)]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user);
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }
        
        [HttpPost]
        [Route(MvcRoutes.AccountLogOff.Route, Name = MvcRoutes.AccountLogOff.Name)]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
        
        [AllowAnonymous]
       
        [HttpPost]
        public async Task<ActionResult> WindowsLogin(string userName, string returnUrl)
        {
       
            // If IIS could not Windows Authenticate User then redirect to login page
            if (!Request.LogonUserIdentity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            
            // If the user could not be found in database then throw exception inside GetUserIdentity()
            var scope = Request.GetOwinContext().Get<StructureMapOwinDependencyScope>(StructureMapOwinMiddleware.ContextContainerKey);
            var userRepository = scope.RequestContainer.GetInstance<ISecurityUserRepository>();
            var user = await userRepository.GetUserIdentity(Request.LogonUserIdentity);

            // If the user not enabled then redirect to login page
            if (!user.Enabled)
            {
                return RedirectToAction("Login");
            }

            // Create a basic Identity for the Windows Authenticated User
            var identity = new ClaimsIdentity(
            DefaultAuthenticationTypes.ApplicationCookie,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

            // These claims MUST be set otherwise MVC Pipeline will not regard user as logged in and 302 redirect to Login page.
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimTypes.NameIdentifier, DefaultAuthenticationTypes.ApplicationCookie));
            identity.AddClaim(new Claim(ClaimTypes.Name, Request.LogonUserIdentity.Name, ClaimTypes.Name, DefaultAuthenticationTypes.ApplicationCookie));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, Request.LogonUserIdentity.Name, ClaimTypes.WindowsUserClaim, DefaultAuthenticationTypes.ApplicationCookie));
            //identity.AddClaim(new Claim(AntiForgeryConfig.UniqueClaimTypeIdentifier, user.Id.ToString(), AntiForgeryConfig.UniqueClaimTypeIdentifier, DefaultAuthenticationTypes.ApplicationCookie));

            var applicationUser = await UserManager.FindByIdAsync(user.Id);
            identity.AddClaim(new Claim(Constants.DefaultSecurityStampClaimType, applicationUser.SecurityStamp));

            identity.AddClaim(new Claim(ClaimTypes.Role, Guid.NewGuid().ToString()));

            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Main", "Incident");
            }

            return Redirect(returnUrl);
        }
        
        [HttpPost]
        public void WindowsLogOff()
        {
         
           AuthenticationManager.SignOut();
            
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
        
        
        [AllowAnonymous]
        [Route(MvcRoutes.AccountForgorPasswordView.Route, Name = MvcRoutes.AccountForgorPasswordView.Name)]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(MvcRoutes.AccountForgorPassword.Route, Name = MvcRoutes.AccountForgorPassword.Name)]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist 
                    return View("ForgotPasswordConfirmation");
                }

                var provider = new DpapiDataProtectionProvider("BC.EQCS");
                UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(
                    provider.Create("EmailConfirmation"));
                
                //await UserManager.UpdateAsync(user);
                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: WebAppSettings.GetCurrentHttpProtocol);
              //  await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");

                bool sent;
                NotificationSender sender = new NotificationSender();
                sender.SendEmail(new NotificationMessageModel
                {
                    Body = "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>",
                    Error = "",
                    EventId = null,
                    Id = 1,
                    Recipient = model.Email,
                    Subject = "BC ECQS Password Reset Request",
                    Succeed = true

                }, out sent);



                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        [Route(MvcRoutes.AccountForgorPasswordConfirmationView.Route, Name = MvcRoutes.AccountForgorPasswordConfirmationView.Name)]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        
        
        [AllowAnonymous]
        [Route(MvcRoutes.AccountResetPasswordView.Route, Name = MvcRoutes.AccountResetPasswordView.Name)]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(MvcRoutes.AccountResetPassword.Route, Name = MvcRoutes.AccountResetPassword.Name)]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var provider = new DpapiDataProtectionProvider("BC.EQCS");
            UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(
                provider.Create("EmailConfirmation"));

            var user = await UserManager.FindByEmailAsync(model.Email);

         
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        [AllowAnonymous]
        [Route(MvcRoutes.AccountResetPasswordConfirmationView.Route, Name = MvcRoutes.AccountResetPasswordConfirmationView.Name)]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #region Helpers
        
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            //var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            // Create a basic Identity for the Windows Authenticated User
            var identity = new ClaimsIdentity(
            DefaultAuthenticationTypes.ApplicationCookie,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

            // These claims MUST be set otherwise MVC Pipeline will not regard user as logged in and 302 redirect to Login page.
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimTypes.NameIdentifier, DefaultAuthenticationTypes.ApplicationCookie));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Login ?? user.UserName, ClaimTypes.Name, DefaultAuthenticationTypes.ApplicationCookie));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login ?? user.UserName, ClaimTypes.UserData, DefaultAuthenticationTypes.ApplicationCookie));
            //identity.AddClaim(new Claim(AntiForgeryConfig.UniqueClaimTypeIdentifier, user.Id.ToString(), AntiForgeryConfig.UniqueClaimTypeIdentifier, DefaultAuthenticationTypes.ApplicationCookie));

            identity.AddClaim(new Claim(Constants.DefaultSecurityStampClaimType, user.SecurityStamp));

            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
      
        #endregion
        
    }
}