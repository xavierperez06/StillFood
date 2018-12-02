using StillFood.WEB.Atributtes;
using System.Web.Mvc;

namespace StillFood.WEB.Controllers
{
    [Session]
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

        [HttpGet]
        public ActionResult Contact()
        {
            Models.Contacto wContacto = new Models.Contacto();
            wContacto.Enviado = false;
            return View(wContacto);
        }

        [HttpPost]
        public ActionResult Contact(Models.Contacto pContacto)
        {
            Models.Contacto wContacto = new Models.Contacto();

            Common.ServicioEmail wServicio = new Common.ServicioEmail();
            string wAsunto = $"Nueva Consulta - {System.DateTime.Today.ToShortDateString()}";
            string wMensaje = string.Format("<div> {0}.  <br/> <strong>Nombre:</strong> {1}  <br/> <strong>Email:</strong> {2}  <br/> <strong>Teléfono:</strong> {3}</div>", pContacto.Mensaje, pContacto.Nombre, pContacto.Email, pContacto.Telefono);

            Common.Enums.eResultadoEnvio wResultado = wServicio.Enviar("uai.stillfood@gmail.com", wAsunto, wMensaje, true);

            wContacto = pContacto;
            wContacto.Enviado = true;

            return View(wContacto);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}