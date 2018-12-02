using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StillFood.WEB.Controllers
{
    public class ComercioController : Controller
    {
        private readonly Services.Compras mComprasServices;
        private readonly Services.Comercios mComercioServices;

        public ComercioController(Services.Compras pComprasServices, Services.Comercios pComercioServices)
        {
            mComprasServices = pComprasServices;
            mComercioServices = pComercioServices;
        }

        // GET: Comercio
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pedidos()
        {
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();
            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            Models.Pedido wPedido = new Models.Pedido();

            wPedido.NotasPedido = mComprasServices.ObtenerNotasPedidoPorIdComercio(wUsuario.IdComercio.Value);

            return View(wPedido);
        }

        [HttpPost]
        public ActionResult Pedidos(Models.Pedido pPedido)
        {
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();
            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            Models.Pedido wPedido = new Models.Pedido();

            int? wIdEstado = null;

            if (!string.IsNullOrWhiteSpace(pPedido.Estado))
            {
                wIdEstado = Convert.ToInt32(pPedido.Estado);
            }

            wPedido.NotasPedido = mComercioServices.FiltrarNotasPedido(wIdEstado, pPedido.Usuario, wUsuario.IdComercio.Value);

            return View(wPedido);
        }

        [HttpGet]
        public ActionResult DetallePedido(int pIdPedido)
        {
            Models.NotaPedido wPedido = mComprasServices.ObtenerNotaPedido(pIdPedido);

            Patterns.State.Context wcontext = new Patterns.State.Context();
            wcontext.SetearEstado(new Patterns.State.RealizadoState());
            wcontext.EjecutarAccion(wPedido);

            return View(wPedido);
        }

        [HttpPost]
        public ActionResult AdministrarEstados(int pIdNotaPedido, int pAccion, string pComentario)
        {
            Models.NotaPedido wPedido = mComprasServices.ObtenerNotaPedido(pIdNotaPedido);

            Patterns.State.Context wContext = new Patterns.State.Context();

            switch(pAccion)
            {
                case 1:
                    wContext.SetearEstado(new Patterns.State.EnPreparacionState());
                    break;
                case 2:
                    wContext.SetearEstado(new Patterns.State.PreparadoState());
                    break;
                case 3:
                    wContext.SetearEstado(new Patterns.State.EntregadoState());
                    break;
                case 4:
                    wPedido.MotivoCancelacion = pComentario;
                    wContext.SetearEstado(new Patterns.State.CanceladoState());
                    break;
            }

            wContext.EjecutarAccion(wPedido);

            return View();
        }

        public ActionResult Reportes()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ObtenerReporte(int pCommand)
        {
            Patterns.Command.Reporte wReporte = new Patterns.Command.Reporte();
            Patterns.Command.Command wCommand = null;
            Patterns.Command.ReportInvoker wInvoker = new Patterns.Command.ReportInvoker();

            switch(pCommand)
            {
                case 1:
                    wCommand = new Patterns.Command.IngresosDiariosCommand(wReporte);
                    break;
                case 2:
                    wCommand = new Patterns.Command.IngresosMensualesCommand(wReporte);
                    break;
                case 3:
                    wCommand = new Patterns.Command.ProductosVendidosCommand(wReporte);
                    break;
                case 4:
                    wCommand = new Patterns.Command.StockCommand(wReporte);
                    break;

            }


            wInvoker.SetearCommand(wCommand);
            wInvoker.EjecutarComando();

            List<object> wListaResultado = wInvoker.Resultado;


            return Json(wListaResultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EnviarNotificaciones()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EnviarNotificaciones(string pHTML)
        {
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            if(wUsuario != null)
            {
                 Common.Enums.eResultadoAccion wResultado = mComercioServices.EnviarNotificaciones(wUsuario.IdComercio.Value, pHTML);

                if(wResultado.Equals(Common.Enums.eResultadoAccion.Ok))
                    ModelState.AddModelError("", "Notificaciones enviadas satisfactoriamente.");
                else
                    ModelState.AddModelError("", "Ocurrió un error al enviar las notificaciones. Vuelva a intentarlo más tarde.");
            }           

            return View();
        }
    }
}