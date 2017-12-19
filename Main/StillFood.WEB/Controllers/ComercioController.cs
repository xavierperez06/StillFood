using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StillFood.WEB.Controllers
{
    public class ComercioController : Controller
    {
        private readonly Services.Compras mComprasServices;

        public ComercioController(Services.Compras pComprasServices)
        {
            mComprasServices = pComprasServices;
        }
        // GET: Comercio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pedidos()
        {
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();
            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            List<Models.NotaPedido> wPedidos = mComprasServices.ObtenerNotasPedidoPorIdComercio(wUsuario.IdComercio.Value);

            return View(wPedidos);
        }
    }
}