using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace StillFood.WEB.Controllers
{
    public class StillFoodControllerFactory : IControllerFactory
    {

        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            string controllername = requestContext.RouteData.Values["controller"].ToString();     
            
            Type controllerType = Type.GetType(string.Format("StillFood.WEB.Controllers.{0}Controller", controllername));
          
            IController controller;
            //Creo un Custom IControllerFactory para realizar la inyección de dependencia que corresponda a los controladores
            if (controllerType == typeof(ComprasController))
            {
                Services.Comercios wComerciosServices = new Services.Comercios();
                Services.Productos wProductosServices = new Services.Productos();
                Services.UsuariosDirecciones wUsuariosDireccionesServices = new Services.UsuariosDirecciones();
                Services.FormasEntregas wFormasEntregasServices = new Services.FormasEntregas();
                Services.Compras wComprasServices = new Services.Compras();
                controller = new ComprasController(wComerciosServices,wProductosServices, wUsuariosDireccionesServices, wFormasEntregasServices,wComprasServices);
            }
            else if (controllerType == typeof(AdminController))
            {
                Services.Comercios wComerciosServices = new Services.Comercios();
                Services.Productos wProductosServices = new Services.Productos();
                Services.Categorias wCategoriasServices = new Services.Categorias();
                Services.Permisos wPermisosServices = new Services.Permisos();
                Services.Roles wRolesServices = new Services.Roles();
                Services.Usuarios wUsuariosServices = new Services.Usuarios();
                Services.FormasEntregas wFormasEntregaServices = new Services.FormasEntregas();
                Services.FormasPago wFormasPagoServices = new Services.FormasPago();
                controller = new AdminController(wComerciosServices, wProductosServices, wCategoriasServices,wPermisosServices,wRolesServices, wUsuariosServices, wFormasEntregaServices, wFormasPagoServices);
            }
            else if(controllerType == typeof(UsuarioController))
            {
                Services.Usuarios wUsuariosServices = new Services.Usuarios();
                Services.UsuariosDirecciones wUsuariosDireccionesServices = new Services.UsuariosDirecciones();
                Services.Logs wLogsServices = new Services.Logs();
                Services.Compras wComprasServices = new Services.Compras();
                controller = new UsuarioController(wUsuariosServices,wUsuariosDireccionesServices,wLogsServices,wComprasServices);
            }
            else
            {
                controller = Activator.CreateInstance(controllerType) as IController;
            }
            
            return controller;
        }

        public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(
       System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}