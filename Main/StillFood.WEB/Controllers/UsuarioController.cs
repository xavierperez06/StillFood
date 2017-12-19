using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StillFood.WEB.Atributtes;
using System.Web.Security;

namespace StillFood.WEB.Controllers
{
    [SessionAttribute]
    public class UsuarioController : Controller
    {
        private readonly Services.Usuarios mUsuariosServices;
        private readonly Services.UsuariosDirecciones mUsuariosDireccionesServices;
        private readonly Services.Logs mLogsServices;
        private readonly Services.Compras mComprasServices;

        public UsuarioController(Services.Usuarios pUsuariosServices,Services.UsuariosDirecciones pUsuariosDireccionesServices, Services.Logs pLogsServices, Services.Compras pComprasServices)
        {
            mUsuariosServices = pUsuariosServices;
            mUsuariosDireccionesServices = pUsuariosDireccionesServices;
            mLogsServices = pLogsServices;
            mComprasServices = pComprasServices;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracion(Models.Usuario pUsuario)
        {
            if (!ModelState.IsValid)
            {
                return View(pUsuario);
            }
            else
            {
                //Armo la URL que se le va a enviar al Usuario para la verifiacion
                string wURL = Url.Action("Verificacion", "Usuario", new { pIdConfirmacion = "ReemplazarId" }, this.Request.Url.Scheme);
                pUsuario.IdTipoUsuario = Convert.ToInt32(Common.Enums.eTiposUsuarios.Consumidor);

                Common.Enums.eResultadoRegistro wGuardado = mUsuariosServices.Agregar(pUsuario,wURL);

                if (wGuardado == Common.Enums.eResultadoRegistro.ExisteUsuario)
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un usuario con el correo electrónico ingresado.");
                    return View(pUsuario);
                }
                else if (wGuardado == Common.Enums.eResultadoRegistro.FalloMail)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al realizar la operación. Por favor vuelva a intentarlo más tarde.");
                    return View(pUsuario);
                }
            }

            //Si todo fue exitoso muestro un mensaje informandole al usuario, pero antes le agrego el Rol de Consumidor
            TempData["Mensaje"] = "La registración se ha realizado satisfactoriamente. Le hemos envíado un correo electrónico para finalizar el proceso.<br/> Muchas gracias por sumarse a StillFood.";
            return RedirectToAction("Mensaje", "Home");
        }

        public ActionResult Verificacion(string pIdConfirmacion)
        {
            //Busco el usuario de acuerdo al IdConfirmacion
            Guid wIdConfirmacion = new Guid(pIdConfirmacion);
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuarioPorIdConfirmacion(wIdConfirmacion);

            if (wUsuario == null)
            {
                TempData["Mensaje"] = "Ha ocurrido un error. Por favor vuelva a intentarlo más tarde.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                //Checkeo si la fecha de vencimiento es menor a la fecha actual
                if (wUsuario.FechaExpConfirmacion < DateTime.Now)
                {
                    return RedirectToAction("VerificacionExpira", "Usuario", new { pIdUsuario = wUsuario.Id });
                }
                else
                {
                    //Si pasa todas las validaciones lo redirecciono a la home ya logueado y le cambio el estado al usuario
                    wUsuario.IdEstado = Convert.ToInt32(Common.Enums.eEstadosUsuarios.Activo);
                    mUsuariosServices.Validar(wUsuario);

                    Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();
                    //No le paso la contraseña porque ya viene validado desde el email
                    Common.Enums.eResultadoLogin wResultado = wFacade.Autenticar(wUsuario.Email, false);

                    if (wResultado.Equals(Common.Enums.eResultadoLogin.Logueado))
                    {
                        TempData["Mensaje"] = "La verificación de la cuenta ha sido éxitosa. <br/> <strong>Bievenido a StillFood.</strong>";
                        return RedirectToAction("Mensaje", "Home");
                    }
                    else
                    {
                        TempData["Mensaje"] = "Ha ocurrido un error. Por favor vuelva a intentarlo más tarde.";
                        return RedirectToAction("Mensaje", "Home");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult VerificacionExpira(int pIdUsuario)
        {
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pIdUsuario);

            return View(wUsuario);
        }

        [HttpPost]
        public ActionResult VerificacionExpira(Models.Usuario pUsuario)
        {
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pUsuario.Id);

            string wURL = Url.Action("Verificacion", "Usuario", new { pIdConfirmacion = "ReemplazarId" }, this.Request.Url.Scheme);

            Common.Enums.eResultadoRegistro wResultado = mUsuariosServices.ReenviarActivacion(wUsuario, wURL);

            if (wResultado == Common.Enums.eResultadoRegistro.FalloMail)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al realizar la operación. Por favor vuelva a intentarlo más tarde.");
                return View(wUsuario);
            }
            else
            {
                TempData["Mensaje"] = "Se ha envíado satisfactoriamente un nuevo enlace de activación a su casilla de correo electrónico.";
                return RedirectToAction("Mensaje", "Home");
            }
        }



        [HttpPost]
        public ActionResult LoginMaster(string pEmail, string pContraseña, string pRecuerdame)
        {
            bool wRecuerdame = false;
            if (pRecuerdame != null)
                wRecuerdame = true;

            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            Common.Enums.eResultadoLogin wResultado = wFacade.Autenticar(pEmail, pContraseña, wRecuerdame);

            if (wResultado.Equals(Common.Enums.eResultadoLogin.Logueado))
            {
                //Guardo el Evento en el Log
                Models.Usuario wUsu = (Models.Usuario)this.Session["Usuario"];
                mLogsServices.Agregar(wUsu.Id, "", "Inicio de Sesión", "Usuarios");
                return RedirectToAction("Index", "Home");
            }
            else if (wResultado.Equals(Common.Enums.eResultadoLogin.UsuarioIncorrecto))
            {
                ModelState.AddModelError("", "El usuario ingresado es incorrecto.");
            }
            else if (wResultado.Equals(Common.Enums.eResultadoLogin.ContraseñaIncorrecta))
            {
                ModelState.AddModelError("", "La contraseña ingresada es incorrecta.");
            }
            else if(wResultado.Equals(Common.Enums.eResultadoLogin.Inactivo))
            {
                ModelState.AddModelError("", "Su usuario se encuentra inactivo. Comuniquese con algún administrador.");
            }
            //Si no se logueo correctamente lo redirecciono a la pantalla de login con el mensaje correspondiente
            Models.Usuario wUsuario = new Models.Usuario();
            wUsuario.Email = pEmail;
            wUsuario.Contraseña = pContraseña;
            wUsuario.Recuerdame = wRecuerdame;

            return View("Login", wUsuario);
        }

        [NoLoginAttribute]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Usuario pUsuario)
        {
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            Common.Enums.eResultadoLogin wResultado = wFacade.Autenticar(pUsuario.Email, pUsuario.Contraseña, pUsuario.Recuerdame);

            if (wResultado.Equals(Common.Enums.eResultadoLogin.Logueado))
            {
                Models.Usuario wUsu = (Models.Usuario)this.Session["Usuario"];
                mLogsServices.Agregar(wUsu.Id, "", "Inicio de Sesión", "Usuarios");
                return RedirectToAction("Index", "Home");
            }
            else if (wResultado.Equals(Common.Enums.eResultadoLogin.UsuarioIncorrecto))
            {
                ModelState.AddModelError("", "El usuario ingresado es incorrecto.");
            }
            else if (wResultado.Equals(Common.Enums.eResultadoLogin.ContraseñaIncorrecta))
            {
                ModelState.AddModelError("", "La contraseña ingresada es incorrecta.");
            }
            else if (wResultado.Equals(Common.Enums.eResultadoLogin.Inactivo))
            {
                ModelState.AddModelError("", "Su usuario se encuentra inactivo. Comuniquese con algún administrador.");
            }

            return View(pUsuario);
        }

        public ActionResult LogOut()
        {
            Models.Usuario wUsuario = (Models.Usuario)this.Session["Usuario"];
            mLogsServices.Agregar(wUsuario.Id, "Sesión Activa", "Cierre de Sesión", "Usuarios");
            FormsAuthentication.SignOut();
            this.Session["Usuario"] = null;
            this.Session.Clear();
            this.Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MisDirecciones()
        {
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            int wIdUsuario = wFacade.ObtenerIdUsuario();
            List<Models.UsuarioDireccion> wDirecciones = mUsuariosDireccionesServices.ObtenerDireccionesPorIdUsuario(wIdUsuario);

            return View(wDirecciones);
        }

        [HttpGet]
        public ActionResult Direccion(string pId)
        {
            int wId = Convert.ToInt32(pId);
            Models.UsuarioDireccion wDireccion;

            if (wId == 0)
                wDireccion = new Models.UsuarioDireccion();
            else
                wDireccion = mUsuariosDireccionesServices.ObtenerDireccion(wId);

            return View(wDireccion);
        }

        [HttpPost]
        public ActionResult Direccion(Models.UsuarioDireccion pDireccion)
        {
            if (!ModelState.IsValid)
            {
                return View(pDireccion);
            }
            else
            {
                Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

                pDireccion.IdUsuario = wFacade.ObtenerIdUsuario();

                mUsuariosDireccionesServices.Guardar(pDireccion);

                return RedirectToAction("MisDirecciones", "Usuario");
            }        
        }

        [HttpGet]
        public ActionResult EliminarDireccion(string pIdUsuario, string pIdDireccion)
        {
            int wIdUsuario = Convert.ToInt32(pIdUsuario);
            int wIdDireccion = Convert.ToInt32(pIdDireccion);

            mUsuariosDireccionesServices.Eliminar(wIdUsuario, wIdDireccion);

            return RedirectToAction("MisDirecciones", "Usuario");
        }

        public ActionResult MisDatos()
        {
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            Models.Usuario wUsuario = wFacade.ObtenerUsuario();

            return View(wUsuario);
        }

        [HttpGet]
        public ActionResult RecuperaContraseña()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MisDatos(Models.Usuario pUsuario)
        {
            ModelState["ConfirmarContraseña"].Errors.Clear();

            if(!ModelState.IsValid)
            {
                return View(pUsuario);
            }
            else
            {
                mUsuariosServices.Guardar(pUsuario);
            }

            return View(pUsuario);
        }

        [HttpPost]
        public ActionResult RecuperaContraseña(Models.Usuario pUsuario)
        {
            if (string.IsNullOrWhiteSpace(pUsuario.Email))
            {
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Debe ingresar un correo electrónico.");
                return View(pUsuario);
            }
            else
            {
                string wURL = Url.Action("CambioContraseña", "Usuario", new { pId = "ReemplazarId" }, this.Request.Url.Scheme);

                Common.Enums.eResultadoCambioContraseña wResultado = mUsuariosServices.SolicitaCambioContraseña(pUsuario.Email, wURL);

                if (wResultado == Common.Enums.eResultadoCambioContraseña.MailInexistente)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "El correo electrónico ingresado no corresponde a ningún usuario.");

                    return View(pUsuario);
                }
                else if (wResultado == Common.Enums.eResultadoCambioContraseña.MailInexistente)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "Ha ocurrido un error. Por favor vuelva a intentarlo más tarde.");

                    return View(pUsuario);
                }
                else
                {
                    TempData["Mensaje"] = "Se ha envíado satisfactoriamente el email a su casilla de correo electrónico para realizar el cambio de Contraseña.";
                    return RedirectToAction("Mensaje", "Home");
                }
            }
          
        }

      
        [HttpGet]
        public ActionResult CambioContraseña(string pId)
        {
            Guid wId = new Guid(pId);
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuarioPorIdRecuperoContraseña(wId);

            //Checkeo si existe algún usuario logueado
            Facade.FacadeSecurity wFacede = new Facade.FacadeSecurity();

            if(wFacede.ExisteUsuarioEnSesion())
            {
                int wIdUsuarioLogueado = wFacede.ObtenerIdUsuario();

                if (wIdUsuarioLogueado != wUsuario.Id)
                {
                    TempData["Mensaje"] = "El usuario logueado no corresponde al usuario que solicita el cambio de contraseña.";
                    return RedirectToAction("Mensaje", "Home");
                }
            }
            

            if (wUsuario == null)
            {
                TempData["Mensaje"] = "Ha ocurrido un error. Por favor vuelva a intentarlo más tarde.";
                return RedirectToAction("Mensaje", "Home");
            }
            else
            {
                wUsuario.Contraseña = string.Empty;
                return View(wUsuario);
            }
        }

        [HttpPost]
        public ActionResult CambioContraseña(Models.Usuario pUsuario)
        {
            ModelState.Remove("FechaNacimiento");
            ModelState.Remove("Email");

            if (!ModelState.IsValid)
            {
                return View(pUsuario);
            }
            else
            {
                Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pUsuario.Id);
                //Aplico el hash y guardo la contraseña
                wUsuario.Contraseña = Common.Utils.EncriptarContraseña(pUsuario.Contraseña);

                mUsuariosServices.Guardar(wUsuario);

                //Una vez guardado el cambio, inicio sesión con dicho usuario
                Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

                Common.Enums.eResultadoLogin wResultado = wFacade.Autenticar(wUsuario.Email, wUsuario.Contraseña, false);

                if (wResultado.Equals(Common.Enums.eResultadoLogin.Logueado))
                {
                    TempData["Mensaje"] = "El cambio de contraseña ha sido exitoso.";
                    mLogsServices.Agregar(wUsuario.Id, "", "Modificación de Contraseña", "Usuarios");
                    return RedirectToAction("Mensaje", "Home");
                }
                else if (wResultado.Equals(Common.Enums.eResultadoLogin.Inactivo))
                {
                    TempData["Mensaje"] = "El cambio de contraseña ha sido exitoso. Su usuario se encuentra inactivo, comuniquese con algún administrador.";
                    mLogsServices.Agregar(wUsuario.Id, "", "Modificación de Contraseña", "Usuarios");
                    return RedirectToAction("Mensaje", "Home");
                }

                TempData["Mensaje"] = "El cambio de contraseña ha sido exitoso.";
                mLogsServices.Agregar(wUsuario.Id, "", "Modificación de Contraseña", "Usuarios");

                return RedirectToAction("Mensaje", "Home");
            }
        }

        public ActionResult MisPedidos()
        {
            Facade.FacadeSecurity wFacade = new Facade.FacadeSecurity();

            int wIdUsuario = wFacade.ObtenerIdUsuario();
            List<Models.NotaPedido> wPedidos = mComprasServices.ObtenerNotasPedidoPorIdUsuario(wIdUsuario);


            return View(wPedidos);
        }
    }
}