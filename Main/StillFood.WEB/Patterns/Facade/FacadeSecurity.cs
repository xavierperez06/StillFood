using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace StillFood.WEB.Facade
{
    public class FacadeSecurity
    {
        Services.Usuarios mUsuariosServices = new Services.Usuarios();

        public bool ExisteUsuarioEnSesion()
        {
            if(HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //Renuevo la variable de sesion
                if(HttpContext.Current.Session["Usuario"] == null)
                {
                    HttpContext.Current.Session["Usuario"] = ObtenerUsuario();
                }     
            }
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public void DestruirUsuarioEnSesion()
        {
            FormsAuthentication.SignOut();
        }

        public Models.Usuario ObtenerUsuario()
        {
            int wIdUsuario = 0;
            Models.Usuario wUsuario = null;

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket wTicket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (wTicket != null)
                {
                    wIdUsuario = Convert.ToInt32(wTicket.UserData);
                    wUsuario = mUsuariosServices.ObtenerUsuario(wIdUsuario);
                }
            }

            return wUsuario;
        }

        public int ObtenerIdUsuario()
        {
            int wIdUsuario = 0;

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket wTicket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (wTicket != null)
                {
                    wIdUsuario = Convert.ToInt32(wTicket.UserData);
                }
            }

            return wIdUsuario;
        }

        public void AgregarUsuarioASesion(Models.Usuario pUsuario, bool pRecuerdame)
        {
            bool wPersistente = true;
            var wCookie = FormsAuthentication.GetAuthCookie("Usuario", wPersistente);

            wCookie.Name = FormsAuthentication.FormsCookieName;
            if (pRecuerdame)
            {
                wCookie.Expires = DateTime.Now.AddMonths(8);
            }
            else
            {
                wCookie.Expires = DateTime.Now.AddHours(1);
            }

            var wTicket = FormsAuthentication.Decrypt(wCookie.Value);
            var wNewTicket = new FormsAuthenticationTicket(wTicket.Version, wTicket.Name, wTicket.IssueDate, wTicket.Expiration, wTicket.IsPersistent, pUsuario.Id.ToString());

            wCookie.Value = FormsAuthentication.Encrypt(wNewTicket);

            if(HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cookies.Add(wCookie);

                HttpContext.Current.Session["Usuario"] = pUsuario;
            }        
        }

        public Common.Enums.eResultadoLogin Autenticar(string pEmail,string pContraseña,bool pRecuerdame)
        {
            Common.Enums.eResultadoLogin wResultado;
            //Primero checkeo que exista el usuario
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuarioPorEmail(pEmail.Trim());
            if (wUsuario == null)
            {
                wResultado = Common.Enums.eResultadoLogin.UsuarioIncorrecto;
            }
            else
            {
                //Si el usuario existe checkeo si esta activo
                if (!wUsuario.IdEstado.Equals(Convert.ToInt32(Common.Enums.eEstadosUsuarios.Activo)))
                {
                    wResultado = Common.Enums.eResultadoLogin.Inactivo;
                }
                else
                {
                    //Luego checke que la contraseña coincida
                    if (Common.Utils.CheckContraseña(pContraseña,wUsuario.Contraseña))
                    {
                        wResultado = Common.Enums.eResultadoLogin.Logueado;
                        AgregarUsuarioASesion(wUsuario, pRecuerdame);
                    }
                    else
                    {
                        wResultado = Common.Enums.eResultadoLogin.ContraseñaIncorrecta;
                    }
                }             
            }

            return wResultado;
        }

        public Common.Enums.eResultadoLogin Autenticar(string pEmail, bool pRecuerdame)
        {
            Common.Enums.eResultadoLogin wResultado;
            //Primero checkeo que exista el usuario
            Models.Usuario wUsuario = mUsuariosServices.ObtenerUsuarioPorEmail(pEmail.Trim());
            if (wUsuario == null)
            {
                wResultado = Common.Enums.eResultadoLogin.UsuarioIncorrecto;
            }
            else
            {
                //Si el usuario existe checkeo si esta activo
                if (!wUsuario.IdEstado.Equals(Convert.ToInt32(Common.Enums.eEstadosUsuarios.Activo)))
                {
                    wResultado = Common.Enums.eResultadoLogin.Inactivo;
                }
                else
                {
                    wResultado = Common.Enums.eResultadoLogin.Logueado;
                    AgregarUsuarioASesion(wUsuario, pRecuerdame);
                }
            }

            return wResultado;
        }

        public bool TienePermiso(Common.Enums.eRolesPermisos pPermiso)
        {
            Models.Usuario wUsuario = ObtenerUsuario();
            if (wUsuario == null)
                return false;
            else
                return wUsuario.Roles.Any(r => r.Permisos.Any(p => p.Id == Convert.ToInt32(pPermiso) && p.Activo == true));
        }
    }
}