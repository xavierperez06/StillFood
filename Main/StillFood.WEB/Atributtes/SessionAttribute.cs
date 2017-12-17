using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StillFood.WEB.Atributtes
{
    public class SessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Facade.FacadeSecurity wFacadeSecurity = new Facade.FacadeSecurity();
            //Si está logueado, checkeo si existe en la variable de sesión
            if (wFacadeSecurity.ExisteUsuarioEnSesion())
            {
                if (HttpContext.Current.Session["Usuario"] == null)
                {
                    HttpContext.Current.Session["Usuario"] = wFacadeSecurity.ObtenerUsuario();
                }
            }
        }
    }
}