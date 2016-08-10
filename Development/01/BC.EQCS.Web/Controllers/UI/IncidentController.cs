using System.Web.Mvc;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.UI
{
    [Authorize]
    public class IncidentController : Controller
    {
        public dynamic Index()
        {
            var vm = new ViewModel
            {
                NgApp = "eqcs.incident",

                BaseRoute = "/incident/",

                ActiveMenu = "incident"
            };

            return View("NgView", vm);
        }
    }
}