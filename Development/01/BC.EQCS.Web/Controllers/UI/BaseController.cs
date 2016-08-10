using System.Web.Mvc;

namespace BC.EQCS.Web.Controllers.UI
{
    /*
    Note - This base controller should only be used to extend any of the MVC 
     *      piple lines controller functionality that is to be shared by the controllers
     *      If the logic that you wish to share does not utilise of extend the MVC pipeline then put it in a seperate class
    */
    public class BaseController : Controller
    {
        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }
    }
}