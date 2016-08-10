using System.Web.Mvc;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.UI
{
    public class UserAdminController : Controller
    {
        public dynamic Index()
        {
            var vm = new ViewModel
            {
                NgApp = "eqcs.useradmin",

                BaseRoute = "/useradmin/",

                ActiveMenu = "useradmin"
            };

            return View("NgView", vm);
        }
    }
}