using StillFood.WEB.Atributtes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StillFood.WEB.Controllers
{
    [AutenticadoAttribute]
    public class AdminController : Controller
    {
        private readonly Services.Comercios mComerciosServices;
        private readonly Services.Productos mProductosServices;
        private readonly Services.Categorias mCategoriasServices;
        private readonly Services.Permisos mPermisosServices;
        private readonly Services.Roles mRolesServices;
        private readonly Services.Usuarios mUsuariosServices;
        private readonly Services.FormasEntregas mFormasEntregaServices;
        private readonly Services.FormasPago mFormasPagoServices;

        public AdminController(Services.Comercios pComerciosServices, Services.Productos pProductosServices, Services.Categorias pCategoriasServices, 
            Services.Permisos pPermisosServices, Services.Roles pRolesServicies, Services.Usuarios pUsuariosServices, Services.FormasEntregas pFormasEntregaServices, Services.FormasPago pFormaspagoServices)
        {
            mComerciosServices = pComerciosServices;
            mProductosServices = pProductosServices;
            mCategoriasServices = pCategoriasServices;
            mPermisosServices = pPermisosServices;
            mRolesServices = pRolesServicies;
            mUsuariosServices = pUsuariosServices;
            mFormasEntregaServices = pFormasEntregaServices;
            mFormasPagoServices = pFormaspagoServices;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [PermisoAttribute(mPermiso = Common.Enums.eRolesPermisos.VerAdministracion)]
        [HttpGet]
        public ActionResult Administracion()
        {
            return View();
        }

        #region Categorias
        [HttpGet]
        public PartialViewResult Categorias()
        {
            List<Models.Categoria> wCategorias = mCategoriasServices.ObtenerCategorias();
            return PartialView("~/Views/Admin/Categorias.cshtml", wCategorias);
        }

        [HttpGet]
        public ActionResult AgregarEditarCategoria(int pIdCategoria)
        {
            Models.Categoria wCategoria = new Models.Categoria();
            //Si el Id es mayor que 0 significa que es una edición
            if (pIdCategoria > 0)
            {
                wCategoria = mCategoriasServices.ObtenerCategoria(pIdCategoria);
            }

            return PartialView("~/Views/Admin/AgregarEditarCategoria.cshtml",wCategoria);
        }

        [HttpPost]
        public ActionResult AgregarEditarCategoria(HttpPostedFileBase file,Models.Categoria pCategoria)
        {
            if (file !=null)
            {
                string wNombreImagen = System.IO.Path.GetFileName(file.FileName);
                string wPathFisico = Server.MapPath("~/Content/Img/Categorias/" + wNombreImagen);
                file.SaveAs(wPathFisico); 
                pCategoria.Imagen = wNombreImagen;
            }

            mCategoriasServices.Guardar(pCategoria);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarCategoria(int pIdCategoria)
        {
            mCategoriasServices.Eliminar(pIdCategoria);
            return RedirectToAction("Administracion", "Admin");
        }


        #endregion

        #region Productos

        [AutenticadoAttribute]
        public PartialViewResult Productos()
        {
            Models.Usuario wUsuario = (Models.Usuario)this.Session["Usuario"];
            //Obtengo el comercio del usuario logueado
            Models.Comercio wComercio = mComerciosServices.ObtenerComercioConProductos(wUsuario.IdComercio.Value);
            return PartialView("~/Views/Admin/Productos.cshtml", wComercio);
        }

        public ActionResult AgregarEditarProducto(int pIdProducto)
        {
            Models.Producto wProducto = new Models.Producto();
            //Si el Id es mayor que 0 significa que es una edición
            if (pIdProducto > 0)
            {
                wProducto = mProductosServices.ObtenerProducto(pIdProducto);
            }

            wProducto.Categorias = mCategoriasServices.ObtenerCategorias();
            wProducto.Precio = 0;
            wProducto.PrecioDescuento = 0;

            return PartialView("~/Views/Admin/AgregarEditarProducto.cshtml", wProducto);
        }

        [HttpPost]
        public ActionResult AgregarEditarProducto(HttpPostedFileBase file, Models.Producto pProducto)
        {
            if (file != null)
            {
                string wNombreImagen = System.IO.Path.GetFileName(file.FileName);
                string wPathFisico = Server.MapPath("~/Content/Img/Productos/" + wNombreImagen);
                file.SaveAs(wPathFisico);
                pProducto.Imagen = wNombreImagen;
            }

            Models.Usuario wUsuario = (Models.Usuario)this.Session["Usuario"];
            pProducto.IdComercio = (int)wUsuario.IdComercio;

            mProductosServices.Guardar(pProducto);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarProducto(int pIdProducto)
        {
            mProductosServices.Eliminar(pIdProducto);
            return RedirectToAction("Administracion", "Admin");
        }
        #endregion

        #region Permisos
        [HttpGet]
        public PartialViewResult Permisos()
        {
            List<Models.Permiso> wPermisos = mPermisosServices.ObtenerPermisos();
            return PartialView("~/Views/Admin/Permisos.cshtml", wPermisos);
        }

        [HttpGet]
        public ActionResult AgregarEditarPermiso(int pIdPermiso)
        {
            Models.Permiso wPermiso = new Models.Permiso();
            //Si el Id es mayor que 0 significa que es una edición
            if (pIdPermiso > 0)
            {
                wPermiso = mPermisosServices.ObtenerPermiso(pIdPermiso);
            }

            return PartialView("~/Views/Admin/AgregarEditarPermiso.cshtml", wPermiso);
        }

        [HttpPost]
        public ActionResult AgregarEditarPermiso(Models.Permiso pPermiso)
        {
            mPermisosServices.Guardar(pPermiso);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarPermiso(int pIdPermiso)
        {
            mPermisosServices.Eliminar(pIdPermiso);
            return RedirectToAction("Administracion", "Admin");
        }
        #endregion

        #region Roles
        [HttpGet]
        public PartialViewResult Roles()
        {
            List<Models.Rol> wRoles = mRolesServices.ObtenerRoles();
            return PartialView("~/Views/Admin/Roles.cshtml", wRoles);
        }

        [HttpGet]
        public ActionResult AgregarEditarRol(int pIdRol)
        {
            Models.Rol wRol = new Models.Rol();
            //Si el Id es mayor que 0 significa que es una edición
            if (pIdRol > 0)
            {
                wRol = mRolesServices.ObtenerRol(pIdRol);
            }

            return PartialView("~/Views/Admin/AgregarEditarRol.cshtml", wRol);
        }

        [HttpPost]
        public ActionResult AgregarEditarRol(Models.Rol pRol)
        {
            mRolesServices.Guardar(pRol);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarRol(int pIdRol)
        {
            //Verifico que el Rol no este asociado a ningun usuario
            if (mRolesServices.CheckRolAsociadoAUsuario(pIdRol))
            {
                return new HttpStatusCodeResult(400, "No es posible elminar el rol seleccionado. Asegúrece de que no se encuentre asociado a ningún usuario.");
            }
            else
            {
                mRolesServices.Eliminar(pIdRol);
            }
            
            return RedirectToAction("Administracion", "Admin");
        }

        public PartialViewResult PermisosAsociados(int pIdRol)
        {
            Models.Rol wRol = mRolesServices.ObtenerRol(pIdRol);

            return PartialView("~/Views/Admin/PermisosAsociados.cshtml", wRol);

        }

        [HttpGet]
        public PartialViewResult AsociarPermiso(int pIdRol)
        {
            Models.Rol wRol = mRolesServices.ObtenerRol(pIdRol);
            //ObtenerPermisosRestantes trae todos los permisos que no existan ya asociados en el Rol
            wRol.Permisos = mPermisosServices.ObtenerPermisosRestantes(wRol);

            return PartialView("~/Views/Admin/AsociarPermiso.cshtml", wRol);
        }

        [HttpPost]
        public ActionResult AsociarPermiso(Models.Rol pRol)
        {
            //Obtengo el Rol al que hay que asociarle el permiso
            Models.Rol wRol = mRolesServices.ObtenerRol(pRol.Id);
            //Obtengo el permiso a agregar
            Models.Permiso wPermiso = mPermisosServices.ObtenerPermiso(pRol.IdPermisoTemporal);
            //Lo agrego al rol y lo guardo
            wRol.Permisos.Add(wPermiso);
            mRolesServices.ModificarPermisos(wRol);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarPermisoAsociado(int pIdRol, int pIdPermiso)
        {
            //Obtengo el Rol al que hay que asociarle el permiso
            Models.Rol wRol = mRolesServices.ObtenerRol(pIdRol);
            //Lo agrego al rol y lo guardo
            wRol.Permisos.RemoveAll(p => p.Id == pIdPermiso);
            mRolesServices.ModificarPermisos(wRol);

            return RedirectToAction("Administracion", "Admin");
        }
        #endregion

        #region Usuarios
        [HttpGet]
        public PartialViewResult Usuarios()
        {
            List<Models.Usuario> wUsuarios = mUsuariosServices.ObtenerUsuarios();
            return PartialView("~/Views/Admin/Usuarios.cshtml", wUsuarios);
        }

        [HttpGet]
        public ActionResult AgregarEditarUsuario(int pIdUsuario)
        {
            Models.Usuario wUsuario = new Models.Usuario();
            //Si el Id es mayor que 0 significa que es una edición
            if (pIdUsuario > 0)
            {
                wUsuario = mUsuariosServices.ObtenerUsuario(pIdUsuario);
            }

            List<Models.Comercio> wComercios = mComerciosServices.ObtenerComercios();

            wUsuario.Comercios = wComercios;

            return PartialView("~/Views/Admin/AgregarEditarUsuario.cshtml", wUsuario);
        }

        [HttpPost]
        public ActionResult AgregarEditarUsuario(Models.Usuario pUsuario)
        {
            if (!pUsuario.IdTipoUsuario.Equals((int)Common.Enums.eTiposUsuarios.Comerciante))
            {
                pUsuario.IdComercio = null;
            }

            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuarioPorEmail(pUsuario.Email);

            //Si el Id es distinto de cero entonces es una edicion
            if (pUsuario.Id == 0)
            {
                if (wUsuario == null)
                {
                    pUsuario.FechaAlta = DateTime.Now;
                    pUsuario.IdEstado = Convert.ToInt32(Common.Enums.eEstadosUsuarios.Creado);

                    mUsuariosServices.Guardar(pUsuario);

                    return RedirectToAction("Administracion", "Admin");
                }
                else
                {
                    return new HttpStatusCodeResult(400, "El usuario no se ha creado. <strong>Motivo:</strong> El correo electrónico ingresado ya existe.");
                }
            }
            else
            {
                if (pUsuario.IdComercio == null)
                    pUsuario.IdComercio = wUsuario.IdComercio;

                pUsuario.IdEstado = wUsuario.IdEstado;

                if (pUsuario.Contraseña != wUsuario.Contraseña)
                    pUsuario.Contraseña = Common.Utils.EncriptarContraseña(pUsuario.Contraseña);

                mUsuariosServices.Guardar(pUsuario);

                return RedirectToAction("Administracion", "Admin");
            }
          
        }

        [HttpPost]
        public ActionResult EliminarUsuario(int pIdUsuario)
        {
            mUsuariosServices.Eliminar(pIdUsuario);

            return RedirectToAction("Administracion", "Admin");
        }

        public PartialViewResult RolesAsociados(int pIdUsuario)
        {
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pIdUsuario);

            return PartialView("~/Views/Admin/RolesAsociados.cshtml", wUsuario);

        }

        [HttpGet]
        public PartialViewResult AsociarRol(int pIdUsuario)
        {
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pIdUsuario);
            //ObtenerRolesRestantes trae todos los roles que no existan ya asociados al usuario
            wUsuario.Roles = mRolesServices.ObtenerRolesRestantes(wUsuario);

            return PartialView("~/Views/Admin/AsociarRol.cshtml", wUsuario);
        }

        [HttpPost]
        public ActionResult AsociarRol(Models.Usuario pUsuario)
        {
            //Obtengo el Usuario al que hay que asociarle el rol
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pUsuario.Id);
            //Obtengo el rol a agregar
            Models.Rol wRol = mRolesServices.ObtenerRol(pUsuario.IdRolTemporal);
            //Lo agrego al usuario y lo guardo
            wUsuario.Roles.Add(wRol);
            mUsuariosServices.ModificarRoles(wUsuario);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarRolAsociado(int pIdUsuario, int pIdRol)
        {
            //Obtengo el Usuario al que hay que asociarle el rol
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pIdUsuario);
            //Obtengo el rol a agregar
            Models.Rol wRol = mRolesServices.ObtenerRol(pIdRol);
            //Lo quito al usuario y lo guardo
            wUsuario.Roles.RemoveAll(r => r.Id == wRol.Id);

            mUsuariosServices.ModificarRoles(wUsuario);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpGet]
        public PartialViewResult CambiarEstadoUsuario(int pIdUsuario)
        {
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pIdUsuario);

            return PartialView("~/Views/Admin/CambiarEstadoUsuario.cshtml", wUsuario);
        }

        [HttpPost]
        public ActionResult CambiarEstadoUsuario(Models.Usuario pUsuario)
        {
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuario(pUsuario.Id);
            wUsuario.IdEstado = pUsuario.IdEstado;

            mUsuariosServices.Guardar(wUsuario);

            return RedirectToAction("Administracion", "Admin");
        }
        #endregion

        #region Comercios
        [HttpGet]
        public PartialViewResult Comercios()
        {
            List<Models.Comercio> wComercios = mComerciosServices.ObtenerComercios();
            return PartialView("~/Views/Admin/Comercios.cshtml", wComercios);
        }

        [HttpGet]
        public ActionResult AgregarEditarComercio(int pIdComercio)
        {
            Models.Comercio wComercio = new Models.Comercio();
            //Si el Id es mayor que 0 significa que es una edición
            if (pIdComercio > 0)
            {
                wComercio = mComerciosServices.ObtenerComercio(pIdComercio);
            }

            return PartialView("~/Views/Admin/AgregarEditarComercio.cshtml", wComercio);
        }

        [HttpPost]
        public ActionResult AgregarEditarComercio(HttpPostedFileBase file, Models.Comercio pComercio)
        {
            if (file != null)
            {
                string wNombreImagen = System.IO.Path.GetFileName(file.FileName);
                string wPathFisico = Server.MapPath("~/Content/Img/Comercios/" + wNombreImagen);
                file.SaveAs(wPathFisico);
                pComercio.Imagen = wNombreImagen;
            }

            pComercio.FechaAlta = DateTime.Now;
            mComerciosServices.Guardar(pComercio);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarComercio(int pIdComercio)
        {
            mComerciosServices.Eliminar(pIdComercio);
            return RedirectToAction("Administracion", "Admin");
        }
        #endregion

        #region Formas de Pago
        [HttpGet]
        public PartialViewResult FormasPago()
        {
            List<Models.FormaPago> wFormasPago = mFormasPagoServices.ObtenerFormasPago();
            return PartialView("~/Views/Admin/FormasPago.cshtml", wFormasPago);
        }

        [HttpGet]
        public ActionResult AgregarEditarFormaPago(int pIdFormaPago)
        {
            Models.FormaPago wFormaPago = new Models.FormaPago();
            //Si el Id es mayor que 0 significa que es una edición
            if (pIdFormaPago > 0)
            {
                wFormaPago = mFormasPagoServices.ObtenerFormaPago(pIdFormaPago);
            }

            return PartialView("~/Views/Admin/AgregarEditarFormaPago.cshtml", wFormaPago);
        }

        [HttpPost]
        public ActionResult AgregarEditarFormaPago(HttpPostedFileBase file, Models.FormaPago pFormaPago)
        {
            if (file != null)
            {
                string wNombreImagen = System.IO.Path.GetFileName(file.FileName);
                string wPathFisico = Server.MapPath("~/Content/Img/FormasPago/" + wNombreImagen);
                file.SaveAs(wPathFisico);
                pFormaPago.Imagen = wNombreImagen;
            }

            mFormasPagoServices.Guardar(pFormaPago);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarFormaPago(int pIdFormaPago)
        {
            mFormasPagoServices.Eliminar(pIdFormaPago);
            return RedirectToAction("Administracion", "Admin");
        }
        #endregion

        #region Formas de Entrega
        [HttpGet]
        public PartialViewResult FormasEntrega()
        {
            List<Models.FormaEntrega> wFormasEntrega = mFormasEntregaServices.ObtenerFormasEntrega();
            return PartialView("~/Views/Admin/FormasEntrega.cshtml", wFormasEntrega);
        }

        [HttpGet]
        public ActionResult AgregarEditarFormaEntrega(int pIdFormaEntrega)
        {
            Models.FormaEntrega wFormaEntrega = new Models.FormaEntrega();
            //Si el Id es mayor que 0 significa que es una edición
            if (pIdFormaEntrega > 0)
            {
                wFormaEntrega = mFormasEntregaServices.ObtenerFormaEntrega(pIdFormaEntrega);
            }

            return PartialView("~/Views/Admin/AgregarEditarFormaEntrega.cshtml", wFormaEntrega);
        }

        [HttpPost]
        public ActionResult AgregarEditarFormaEntrega(HttpPostedFileBase file, Models.FormaEntrega pFormaEntrega)
        {
            if (file != null)
            {
                string wNombreImagen = System.IO.Path.GetFileName(file.FileName);
                string wPathFisico = Server.MapPath("~/Content/Img/FormasEntregas/" + wNombreImagen);
                file.SaveAs(wPathFisico);
                pFormaEntrega.Imagen = wNombreImagen;
            }

            mFormasEntregaServices.Guardar(pFormaEntrega);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarFormaEntrega(int pIdFormaEntrega)
        {
            mFormasEntregaServices.Eliminar(pIdFormaEntrega);
            return RedirectToAction("Administracion", "Admin");
        }
        #endregion

        #region Formas de Pago Comercio
        [AutenticadoAttribute]
        public PartialViewResult FormasPagoComercio()
        {
            Models.Usuario wUsuario = (Models.Usuario)this.Session["Usuario"];
            //Obtengo el comercio del usuario logueado
            Models.Comercio wComercio = mComerciosServices.ObtenerComercio((int)wUsuario.IdComercio);
            return PartialView("~/Views/Admin/FormasPagoComercio.cshtml", wComercio);
        }

        [HttpGet]
        public PartialViewResult AsociarFormaPago(int pIdComercio)
        {
            Models.Comercio wComercio = mComerciosServices.ObtenerComercio(pIdComercio);
            //ObtenerFormasPagoRestantes trae todos las formas de pago que no existan ya ascociados al comercio
            wComercio.FormasPago = mFormasPagoServices.ObtenerFormasPagoRestantes(wComercio);

            return PartialView("~/Views/Admin/AsociarFormaPago.cshtml", wComercio);
        }

        [HttpPost]
        public ActionResult AsociarFormaPago(Models.Comercio pComercio)
        {
            //Obtengo el Comercio al que hay que asociarle la forma de pago
            Models.Comercio wComercio = mComerciosServices.ObtenerComercio(pComercio.Id);
            //Obtengo la forma de pago a agregar agregar
            Models.FormaPago wFormaPago = mFormasPagoServices.ObtenerFormaPago(pComercio.IdFormaPagoTemporal);
            //Lo agrego a la forma de pago y lo guardo
            wComercio.FormasPago.Add(wFormaPago);
            mComerciosServices.ModificarFormasPago(wComercio);

            return RedirectToAction("Administracion", "Admin");
        }

        [HttpPost]
        public ActionResult EliminarFormaPagoAsociado(int pIdComercio, int pIdFormaPago)
        {
            //Obtengo el Comercio al que hay que desasociarle la forma de pago
            Models.Comercio wComercio = mComerciosServices.ObtenerComercio(pIdComercio);
            //Le quito la forma de pago y lo guardo
            wComercio.FormasPago.RemoveAll(fp => fp.Id == pIdFormaPago);
            mComerciosServices.ModificarFormasPago(wComercio);

            return RedirectToAction("Administracion", "Admin");
        }
        #endregion  
    }
}