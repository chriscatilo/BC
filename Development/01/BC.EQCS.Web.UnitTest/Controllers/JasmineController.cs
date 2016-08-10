using System.Web.Mvc;

namespace BC.EQCS.Web.UnitTest.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
