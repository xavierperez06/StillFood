using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StillFood.WEB.Atributtes
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Facade.FacadeSecurity wFacadeSecurity = new Facade.FacadeSecurity();
            //Si no está logueado redirecciono al login
            if (!wFacadeSecurity.ExisteUsuarioEnSesion())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Usuario",
                    action = "Login"
                }));
            }
        }
    }
}