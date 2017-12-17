using StillFood.WEB.Atributtes;
using System.Web.Mvc;

namespace StillFood.WEB.Controllers
{
    [SessionAttribute]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();       
        }

        public ActionResult Mensaje()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}