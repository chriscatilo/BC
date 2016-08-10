using System.Web.Mvc;
using BC.EQCS.Web.Models;

namespace BC.EQCS.Web.Controllers.UI
{
    public class AuditingController : Controller
    {
        public dynamic Index()
        {
            var vm = new ViewModel
            {
                NgApp = "eqcs.auditing",

                BaseRoute = "/auditing/",

                ActiveMenu = "auditing"
            };

            return View("NgView", vm);
        }
    }
}