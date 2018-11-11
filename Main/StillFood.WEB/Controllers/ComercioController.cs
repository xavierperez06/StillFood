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
            }


            wInvoker.SetearCommand(wCommand);
            wInvoker.EjecutarComando();

            List<object> wListaResultado = wInvoker.Resultado;


            return Json(wListaResultado, JsonRequestBehavior.AllowGet);
        }
    }
}