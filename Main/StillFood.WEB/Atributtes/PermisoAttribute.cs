using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StillFood.WEB.Atributtes
{
    public class PermisoAttribute : ActionFilterAttribute
    {
        public Common.Enums.eRolesPermisos mPermiso { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Facade.FacadeSecurity wFacadeSecurity = new Facade.FacadeSecurity();

            if (!wFacadeSecurity.TienePermiso(this.mPermiso))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }
}