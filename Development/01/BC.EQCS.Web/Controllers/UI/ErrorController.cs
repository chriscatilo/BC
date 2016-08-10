using System.Web.Mvc;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.UI
{
    public class ErrorController : Controller
    {
        public dynamic Index()
        {
            var vm = new ViewModel
            {
                NgApp = "eqcs.error",

                BaseRoute = "/error/",

                ActiveMenu = "error",

                HideOverlay = true
            };

            return View("NgView", vm);
        }
    }
}