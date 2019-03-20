using System.Web.Mvc;

namespace Lab08.MVC.Web.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}