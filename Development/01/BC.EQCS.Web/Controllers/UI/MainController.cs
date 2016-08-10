using System.Web.Mvc;

namespace BC.EQCS.Web.Controllers.UI
{
    [Authorize]
    public class MainController : Controller
    {
        public ActionResult Index(string args)
        {
            return View();
        }
    }
}