using StillFood.WEB.Atributtes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StillFood.WEB.Controllers
{
    [Session]
    public class ComprasController : Controller
    {
        private readonly Services.Comercios mComerciosServices;
        private readonly Services.Productos mProductosServices;
        private readonly Services.UsuariosDirecciones mUsuariosDireccionesServices;
        private readonly Services.FormasEntregas mFormasEntregasServices;
        private readonly Services.Compras mComprasServices;
        private readonly Services.UsuariosFavoritos mUsuariosFavoritosServices;

        public ComprasController(Services.Comercios pComerciosServices,Services.Productos pProductosServices, Services.UsuariosDirecciones pUsuariosDireccionesServices,Services.FormasEntregas pFormasEntregasServices, Services.Compras pComprasServices, Services.UsuariosFavoritos pUsuariosFavoritosServices)
        {
            mComerciosServices = pComerciosServices;
            mProductosServices = pProductosServices;
            mUsuariosDireccionesServices = pUsuariosDireccionesServices;
            mFormasEntregasServices = pFormasEntregasServices;
            mComprasServices = pComprasServices;
            mUsuariosFavoritosServices = pUsuariosFavoritosServices;
        }

        // GET: Compras
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Comercios(string pBusqueda)
        {
            List<Models.Comercio> wComercios;

            if (pBusqueda == null)
            {
                wComercios = mComerciosServices.ObtenerComercios();
            }
            else
            {
                wComercios = mComerciosServices.FiltrarComercios(pBusqueda);
            }

            return View(wComercios);
        }


        public ActionResult ComerciosFiltro(string pBusqueda)
        {
            List<Models.Comercio> wComercios;

            if (pBusqueda == null)
            {
                wComercios = mComerciosServices.ObtenerComercios();
            }
            else
            {
                wComercios = mComerciosServices.FiltrarComercios(pBusqueda);
            }

            return new JsonResult { Data = wComercios, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ObtenerFavoritos()
        {
            List<Models.UsuarioFavorito> wMostrarFav;

            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            int wIdusuario = wFacade.ObtenerIdUsuario();

            wMostrarFav = mUsuariosFavoritosServices.ObtenerFavoritosPorIdUsuario(wIdusuario);

            return new JsonResult { Data = wMostrarFav, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
       

        public ActionResult AgregarQuitarFavorito(int pIdComercio, int pAgrega)
        {
            bool wAgrega;

            List<Models.UsuarioFavorito> wFavoritos;

            if (pAgrega == 1)
                wAgrega = true;
            else
                wAgrega = false;

            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            int wIdusuario = wFacade.ObtenerIdUsuario();

            wFavoritos = mUsuariosFavoritosServices.AgregarQuitarFavorito(wIdusuario, pIdComercio, wAgrega);

            return new JsonResult { Data = wFavoritos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        public ActionResult Productos(int pIdComercio, string pBusqueda)
        {
            List<Models.Producto> wProductosEnLista = (List<Models.Producto>)Session["Lista"];
            //Si la variable de sesion de los productos en lista, no es cero, entonces checkeo si el comercio de esos productos corresponden al que se ingreso
            if (wProductosEnLista != null)
            {
                if(wProductosEnLista.Count > 0)
                {
                    if (wProductosEnLista[0].IdComercio != (int)pIdComercio)
                    {
                        Session["Lista"] = null;
                    }
                }
            }

            Models.Comercio wComercio;

            if (pBusqueda == null)
            {
                wComercio = mComerciosServices.ObtenerComercioConProductos(pIdComercio);
            }
            else
            {
                wComercio = mComerciosServices.ObtenerComercioConProductosFiltrado(pIdComercio, pBusqueda);
            }

            return View(wComercio);
        }

        public ActionResult DetalleProducto(int pIdProducto)
        {
            Models.Producto wProducto = mProductosServices.ObtenerProducto(pIdProducto);

            return View(wProducto);
        }

        public string AgregarALista(int pIdProducto)
        {
            Models.Producto wProducto = mProductosServices.ObtenerProducto(pIdProducto);
            StringBuilder wHTML = new StringBuilder();
            wHTML.Append("<div class='row'>");
            wHTML.Append("<div class='form-group col-lg-12'>");
            wHTML.Append(string.Format("<div>{0}</div>",wProducto.Nombre));
            wHTML.Append("</div>");    
            wHTML.Append(string.Format("<input type='hidden' name='pIdProducto' id='pIdProducto' value='{0}'>", wProducto.Id));          
            wHTML.Append("<div class='form-group col-lg-6'>");
            wHTML.Append("<input name='pCantidad' id='pCantidad' class='form-control glowing-border' placeholder='Cantidad' type='number' onkeydown='return FiltrarEntrada(event)' required>");
            wHTML.Append("<div class='col-lg-12 hide' id='txtErrorCantidad'>");
            wHTML.Append("Debe ingresar una cantidad");
            wHTML.Append("</div>");
            wHTML.Append("</div>");
            wHTML.Append("<div class='form-group col-lg-12'>");
            wHTML.Append("<textarea name='pObservaciones' id='pObservaciones' class='form-control glowing-border' placeholder='Observaciones'></textarea>");
            wHTML.Append("</div>");
            wHTML.Append("</div>");
            return wHTML.ToString();
        }

        public ActionResult EliminarDeLista(int pIdProducto)
        {
            if ((List<Models.Producto>)Session["Lista"] == null)
            {
                TempData["Mensaje"] = "Su sesión de compra expiró.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                List<Models.Producto> wProductos = (List<Models.Producto>)Session["Lista"];

                wProductos.RemoveAll(p => p.Id == pIdProducto);

                if(wProductos.Count == 0)
                {
                    Session["Lista"] = null;
                }
                else
                {
                    Session["Lista"] = wProductos;
                }         

                return PartialView("~/Views/Compras/MiLista.cshtml");
            }
        }

        [HttpGet]
        public ActionResult MiLista(int pIdProducto, string pCantidad, string pObservaciones)
        {
            int wCantidad = Convert.ToInt32(pCantidad);
            List<Models.Producto> wProductos;
            Models.Producto wProducto = mProductosServices.ObtenerProducto(pIdProducto);


            //Primero checkeo que exista en stock la cantidad que ingresó
            if (wProducto.Stock < wCantidad)
            {
                //Retorno mensaje de error
                return new HttpStatusCodeResult(400, "No hay suficiente stock para la cantidad ingresada.");
            }
            else
            {
                wProducto.Cantidad = Convert.ToInt32(pCantidad);

                if (Session["Lista"] == null)
                {
                    //Guardo la observacion en la descripcion para despues agregarlo al pedido
                    wProducto.Descripcion = pObservaciones;
                    wProductos = new List<Models.Producto>();
                    wProductos.Add(wProducto);
                }
                else
                {
                    wProductos = (List<Models.Producto>)Session["Lista"];
                    if (wProductos.Any(item => item.Id == wProducto.Id))
                    {
                        //Si ya existe en la lista le sumo uno a la cantidad de ese producto
                        wProductos.Find(item => item.Id == wProducto.Id).Cantidad += wProducto.Cantidad;
                        //Y le concateno la observacion si se agregó
                        if(!string.IsNullOrWhiteSpace(pObservaciones))
                        {
                            wProductos.Find(item => item.Id == wProducto.Id).Descripcion += Environment.NewLine + pObservaciones;
                        }
                    }
                    else
                    {
                        wProductos.Add(wProducto);
                    }
                }

                Session["Lista"] = wProductos;

                return PartialView("~/Views/Compras/MiLista.cshtml");
            }        
        }

        public ActionResult Confirmar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Compra()
        {
            if((List<Models.Producto>)Session["Lista"] == null)
            {
                TempData["Mensaje"] = "Su sesión de compra expiró.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

                int wIdusuario = wFacade.ObtenerIdUsuario();

                Models.Compra wCompra = new Models.Compra();

                wCompra.Direcciones = mUsuariosDireccionesServices.ObtenerDireccionesPorIdUsuario(wIdusuario);
                wCompra.FormasEntregas = mFormasEntregasServices.ObtenerFormasEntregasActivas();
                wCompra.Productos = (List<Models.Producto>)Session["Lista"];
                wCompra.IdUsuario = wIdusuario;
                wCompra.IdComercio = wCompra.Productos[0].IdComercio;
                wCompra.Comercio = mComerciosServices.ObtenerComercio(wCompra.IdComercio);

                return View(wCompra);
            }
        }

        [HttpPost]
        public ActionResult Compra(Models.Compra pCompra)
        {
            //Armo el pedido
            Strategy.Pedido wPedido = new Strategy.Pedido();

            wPedido.IdDireccion = pCompra.IdDireccion;
            wPedido.IdEstado = Convert.ToInt32(Common.Enums.eEstadosNotasPedido.Realizado);
            wPedido.IdFormaEntrega = pCompra.IdFormaEntrega;
            wPedido.IdFormaPago = pCompra.IdFormaPago;
            wPedido.IdUsuario = pCompra.IdUsuario;
            wPedido.FechaAlta = DateTime.Now;
            wPedido.IdComercio = pCompra.IdComercio;

            switch ((Common.Enums.eFormasPago)wPedido.IdFormaPago)
            {
                case Common.Enums.eFormasPago.Efectivo:
                    return RedirectToAction("Efectivo", "Compras", wPedido);
                case Common.Enums.eFormasPago.TarjetaCredito:
                    return RedirectToAction("TarjetaCredito", "Compras", wPedido);
            }     

            return View(pCompra);
        }


        public ActionResult Efectivo(Strategy.Pedido pPedido)
        {
            if ((List<Models.Producto>)Session["Lista"] == null)
            {
                TempData["Mensaje"] = "Su sesión de compra expiró.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                //Agrego los productos al pedido
                foreach (Models.Producto wProducto in (List<Models.Producto>)Session["Lista"])
                {
                    pPedido.Agregar(wProducto);
                }

                pPedido.Comercio = mComerciosServices.ObtenerComercio(pPedido.IdComercio);

                return View(pPedido);
            }
        }

        public ActionResult TarjetaCredito(Strategy.Pedido pPedido)
        {
            ModelState.Clear();

            if (TempData["ModelState"] != null)
                ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);

            if (!ModelState.IsValid)
            {
                return View(pPedido);
            }

            if ((List<Models.Producto>)Session["Lista"] == null)
            {
                TempData["Mensaje"] = "Su sesión de compra expiró.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                //Agrego los productos al pedido
                foreach (Models.Producto wProducto in (List<Models.Producto>)Session["Lista"])
                {
                    pPedido.Agregar(wProducto);
                }

                pPedido.Comercio = mComerciosServices.ObtenerComercio(pPedido.IdComercio);

                return View(pPedido);
            }   
        }


        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ConfirmarTarjeta")]
        public ActionResult ConfirmarPagoTarjeta(Strategy.Pedido pPedido)
        {
            if ((List<Models.Producto>)Session["Lista"] == null)
            {
                TempData["Mensaje"] = "Su sesión de compra expiró.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                //Agrego los productos al pedido
                foreach (Models.Producto wProducto in (List<Models.Producto>)Session["Lista"])
                {
                    pPedido.Agregar(wProducto);
                }

                string wVencimiento = string.Concat(pPedido.Mes, "/", pPedido.Año);
                pPedido.EstablecerEstrategia(new Strategy.TarjetaCreditoStrategy(pPedido.TitularTarjeta, pPedido.NumeroTarjeta, pPedido.CVV.ToString(), wVencimiento));

                if (pPedido.Pagar())
                {
                    //Creo la nota de pedido a guardar
                    Models.NotaPedido wNotaPedido = new Models.NotaPedido();
                    wNotaPedido.IdComercio = pPedido.IdComercio;
                    wNotaPedido.IdDireccion = pPedido.IdDireccion;
                    wNotaPedido.IdEstado = Convert.ToInt32(Common.Enums.eEstadosNotasPedido.Realizado);
                    wNotaPedido.IdFormaEntrega = pPedido.IdFormaEntrega;
                    wNotaPedido.IdFormaPago = pPedido.IdFormaPago;
                    wNotaPedido.IdUsuario = pPedido.IdUsuario;
                    wNotaPedido.FechaAlta = DateTime.Now;

                    //La guardo para obtener le Id y procedo a armar el detalle de la nota de pedido
                    int wId = mComprasServices.GuardarNotaPedido(wNotaPedido);

                    if (wId == 0)
                    {
                        TempData["Mensaje"] = "Ocurrió un error al realizar la compra. Por favor vuelva a intentarlo más tarde. <br/> Si el problema persiste comuniquese con algún administrador.";
                        return RedirectToAction("Mensaje", "Home");
                    }
                    else
                    {
                        List<Models.NotaPedidoDetalle> wNotasDetalle = new List<Models.NotaPedidoDetalle>();

                        foreach (Models.Producto wProducto in (List<Models.Producto>)Session["Lista"])
                        {
                            Models.NotaPedidoDetalle wDetalle = new Models.NotaPedidoDetalle();

                            wDetalle.Cantidad = wProducto.Cantidad.Value;
                            wDetalle.IdNotaPedido = wId;
                            wDetalle.IdProducto = wProducto.Id;
                            wDetalle.Precio = wProducto.PrecioDescuento.Value;
                            wDetalle.Comentario = wProducto.Descripcion;

                            wNotasDetalle.Add(wDetalle);
                        }
                        //Si el guardado de los detalles devuelve false, elimino la nota de pedido y muestro un mensaje el usuario
                        if (!mComprasServices.GuardarDetalles(wNotasDetalle,wNotaPedido.IdComercio))
                        {
                            TempData["Mensaje"] = "Ocurrió un error al realizar la compra. Por favor vuelva a intentarlo más tarde. <br/> Si el problema persiste comuniquese con algún administrador.";
                            return RedirectToAction("Mensaje", "Home");
                        }
                        else
                        {
                            //Muestro mensaje compra efectuada
                            Models.NotaPedido wNotaPedidoGuardada = mComprasServices.ObtenerNotaPedido(wId);
                            //Limpio la sesion de los productos
                            Session["Lista"] = null;
                            TempData["Mensaje"] = string.Format("Su pedido ha sido solicitado correctamente. Ante cualquier incoveniente, o cuando su pedido este listo, recibirá una notificación informandole de la situación. <br/><br/> Su número de pedido es <strong>{0}</strong> y puede mirar en mayor detalle desde su menú de pedidos. <br/> <br/> Muchas gracias por utilizar StillFood", wNotaPedidoGuardada.Numero);
                            return RedirectToAction("Mensaje", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al efectuar el pago. Por favor verifique los datos ingresados.");
                    TempData["ModelState"] = ModelState;
                    return RedirectToAction("TarjetaCredito", "Compras", pPedido);
                }
            }  
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Confirmar")]
        public ActionResult ConfirmarCompra(Strategy.Pedido pPedido)
        {
            if ((List<Models.Producto>)Session["Lista"] == null)
            {
                TempData["Mensaje"] = "Su sesión de compra expiró.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                decimal wMonto = 0;

                //Agrego los productos al pedido
                foreach (Models.Producto wProducto in (List<Models.Producto>)Session["Lista"])
                {
                    wMonto += (decimal)(wProducto.PrecioDescuento * wProducto.Cantidad);
                    pPedido.Agregar(wProducto);
                }
                pPedido.EstablecerEstrategia(new Strategy.EfectivoStrategy(wMonto));

                if (pPedido.Pagar())
                {
                    //Creo la nota de pedido a guardar
                    Models.NotaPedido wNotaPedido = new Models.NotaPedido();
                    wNotaPedido.IdComercio = pPedido.IdComercio;
                    wNotaPedido.IdDireccion = pPedido.IdDireccion;
                    wNotaPedido.IdEstado = Convert.ToInt32(Common.Enums.eEstadosNotasPedido.Realizado);
                    wNotaPedido.IdFormaEntrega = pPedido.IdFormaEntrega;
                    wNotaPedido.IdFormaPago = pPedido.IdFormaPago;
                    wNotaPedido.IdUsuario = pPedido.IdUsuario;
                    wNotaPedido.FechaAlta = DateTime.Now;

                    //La guardo para obtener el Id y procedo a armar el detalle de la nota de pedido
                    int wId = mComprasServices.GuardarNotaPedido(wNotaPedido);

                    if (wId == 0)
                    {
                        TempData["Mensaje"] = "Ocurrió un error al realizar la compra. Por favor vuelva a intentarlo más tarde. <br/> Si el problema persiste comuniquese con algún administrador.";
                        return RedirectToAction("Mensaje", "Home");
                    }
                    else
                    {
                        List<Models.NotaPedidoDetalle> wNotasDetalle = new List<Models.NotaPedidoDetalle>();

                        foreach (Models.Producto wProducto in (List<Models.Producto>)Session["Lista"])
                        {
                            Models.NotaPedidoDetalle wDetalle = new Models.NotaPedidoDetalle();

                            wDetalle.Cantidad = wProducto.Cantidad.Value;
                            wDetalle.IdNotaPedido = wId;
                            wDetalle.IdProducto = wProducto.Id;
                            wDetalle.Precio = wProducto.PrecioDescuento.Value;
                            wDetalle.Comentario = wProducto.Descripcion;

                            wNotasDetalle.Add(wDetalle);
                        }
                        //Si el guardado de los detalles devuelve false, elimino la nota de pedido y muestro un mensaje el usuario
                        if (!mComprasServices.GuardarDetalles(wNotasDetalle,wNotaPedido.IdComercio))
                        {
                            TempData["Mensaje"] = "Ocurrió un error al realizar la compra. Por favor vuelva a intentarlo más tarde. <br/> Si el problema persiste comuniquese con algún administrador.";
                            return RedirectToAction("Mensaje", "Home");
                        }
                        else
                        {
                            //Muestro mensaje compra efectuada
                            Models.NotaPedido wNotaPedidoGuardada = mComprasServices.ObtenerNotaPedido(wId);
                            //Limpio la sesion de los productos
                            Session["Lista"] = null;
                            TempData["Mensaje"] = string.Format("Su pedido ha sido solicitado correctamente. Ante cualquier incoveniente, o cuando su pedido este listo, recibirá una notificación informandole de la situación. <br/><br/> Su número de pedido es <strong>{0}</strong> y puede mirar en mayor detalle desde su menú de pedidos. <br/> <br/> Muchas gracias por utilizar StillFood", wNotaPedidoGuardada.Numero);
                            return RedirectToAction("Mensaje", "Home");
                        }
                    }
                }
                else
                {
                    TempData["Mensaje"] = "Ocurrió un error al realizar la compra. Por favor vuelva a intentarlo más tarde. <br/> Si el problema persiste comuniquese con algún administrador.";
                    return RedirectToAction("Mensaje", "Home");
                }
            }
        
        }

        [HttpGet]
        public ActionResult DireccionPartial(int pIdDireccion)
        {
            Models.UsuarioDireccion wDireccion = mUsuariosDireccionesServices.ObtenerDireccion(pIdDireccion);

            return PartialView("~/Views/Compras/DireccionPartial.cshtml",wDireccion);
        }

        public ActionResult Volver()
        {
            List<Models.Producto> wProductos = (List<Models.Producto>)Session["Lista"]; 

            if (wProductos == null)
            {
                TempData["Mensaje"] = "Su sesión de compra expiró.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                if(wProductos.Count > 0)
                {
                    int wIdComercio = wProductos[0].IdComercio;

                    return RedirectToAction("Productos", "Compras", new { pIdComercio = wIdComercio, pBusqueda = string.Empty});
                }
                else
                {
                    TempData["Mensaje"] = "Su sesión de compra expiró.";
                    return RedirectToAction("Mensaje", "Home");
                }
            }
        }
    }


}