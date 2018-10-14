using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StillFood.Business
{
    public class Usuario
    {
        public int Guardar(Entities.Usuario pUsuario)
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            int wId = 0;

            if (pUsuario.Id == 0)
            {
                wId = wUsuariosDAL.Agregar(pUsuario);
            }
            else
            {
                wId = wUsuariosDAL.Editar(pUsuario);
            }

            return wId;
        }

        public void Eliminar(int pIdUsuario)
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            wUsuariosDAL.Eliminar(pIdUsuario);
        }

        public Common.Enums.eResultadoRegistro Agregar(Entities.Usuario pUsuario, string pURL)
        {
            pUsuario.FechaAlta = DateTime.Now;
            if (pUsuario.IdTipoUsuario.Equals(Convert.ToInt32(Common.Enums.eTiposUsuarios.Consumidor)))
            {
                return AgregarUsuarioConsumidor(pUsuario, pURL);
            }
            else
            {
                return AgregarUsuarioComerciante(pUsuario);
            }
        }

        private Common.Enums.eResultadoRegistro AgregarUsuarioConsumidor(Entities.Usuario pUsuario, string pURL)
        {
            Common.Enums.eResultadoRegistro wAgregado;

            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            Entities.Usuario wUsuario;
            wUsuario = wUsuariosDAL.ObtenerUsuarioPorMail(pUsuario.Email);
            //Si existe devuelve falso, de lo contrario lo agrega
            if (wUsuario == null)
            {
                pUsuario.IdEstado = Convert.ToInt32(Common.Enums.eEstadosUsuarios.Creado);
                pUsuario.IdConfirmacion = Guid.NewGuid();
                pUsuario.FechaExpConfirmacion = DateTime.Now.AddDays(2);
                //Encripto la contraseñs
                pUsuario.Contraseña = Common.Utils.EncriptarContraseña(pUsuario.Contraseña);
                int wIdUsuario = wUsuariosDAL.Agregar(pUsuario);
                //Envio el correo
                Common.ServicioEmail wServicio = new Common.ServicioEmail();
                string wAsunto = "Registro StillFood";
                //Reemplazo el parametro de la URL con el valor correspondiente al Id de Verificacion guardado
                string wURL = pURL.Replace("ReemplazarId", pUsuario.IdConfirmacion.ToString());
                string wMensaje = "<div style='width: 70%;margin: 0 auto;'><div><img src='' /></div><div style='font-size: 35px;color: #A3BD31;'> Hola, @NOMBRE</div> Con el fin de ayudar a mantener la seguridad de tu cuenta de StillFood, por favor, verifica tu dirección de email haciendo clic en el siguiente enlace: <br> <a href='@ENLACE'> Verificar email</a> </div>";
                wMensaje = wMensaje.Replace("@NOMBRE", pUsuario.NombreApellido);
                wMensaje = wMensaje.Replace("@ENLACE", wURL);
                var wRecurso = new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/Img/Header-Mail.jpg"));
                Common.Enums.eResultadoEnvio wResultado = wServicio.Enviar(pUsuario.Email, wAsunto, wMensaje, true, wRecurso);
                if (wResultado == Common.Enums.eResultadoEnvio.Error)
                {
                    //Si falla el mail volver atras el cambio del usuario
                    wUsuariosDAL.Eliminar(wIdUsuario);
                    wAgregado = Common.Enums.eResultadoRegistro.FalloMail;
                    return wAgregado;
                }
                wAgregado = Common.Enums.eResultadoRegistro.Ok;
            }
            else
            {
                wAgregado = Common.Enums.eResultadoRegistro.ExisteUsuario;
            }

            return wAgregado;
        }

        public Common.Enums.eResultadoRegistro ReenviarActivacion(Entities.Usuario pUsuario, string pURL)
        {
            Common.ServicioEmail wServicio = new Common.ServicioEmail();
            pUsuario.IdConfirmacion = Guid.NewGuid();
            string wAsunto = "Registro StillFood";
            //Reemplazo el parametro de la URL con el valor correspondiente al Id de Verificacion guardado
            string wURL = pURL.Replace("ReemplazarId", pUsuario.IdConfirmacion.ToString());
            string wMensaje = "<div style='width: 70%;margin: 0 auto;'><div><img src='' /></div><div style='font-size: 35px;color: #A3BD31;'> Hola, @NOMBRE</div> Con el fin de ayudar a mantener la seguridad de tu cuenta de StillFood, por favor, verifica tu dirección de email haciendo clic en el siguiente enlace: <br> <a href='@ENLACE'> Verificar email</a> </div>";
            wMensaje = wMensaje.Replace("@NOMBRE", pUsuario.NombreApellido);
            wMensaje = wMensaje.Replace("@ENLACE", wURL);
            var wRecurso = new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/Img/Header-Mail.jpg"));
            Common.Enums.eResultadoEnvio wResultado = wServicio.Enviar(pUsuario.Email, wAsunto, wMensaje, true, wRecurso);

            if (wResultado == Common.Enums.eResultadoEnvio.Ok)
            {
                //Actualizo la fecha de verificacion
                pUsuario.FechaExpConfirmacion = DateTime.Now.AddDays(2);
                DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
                wUsuariosDAL.Editar(pUsuario);

                return Common.Enums.eResultadoRegistro.Ok;
            }
            else
            {
                return Common.Enums.eResultadoRegistro.FalloMail;
            }
        }

        private Common.Enums.eResultadoRegistro AgregarUsuarioComerciante(Entities.Usuario pUsuario)
        {
            Common.Enums.eResultadoRegistro wAgregado;

            return Common.Enums.eResultadoRegistro.Ok;
        }

        public Entities.Usuario ObtenerUsuarioPorIdConfirmacion(Guid pIdConfirmacion)
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            return wUsuariosDAL.ObtenerUsuarioPorIdConfirmacion(pIdConfirmacion);
        }

        public Entities.Usuario ObtenerUsuarioPorIdRecuperoContraseña(Guid pIdRecuperoContraseña)
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            return wUsuariosDAL.ObtenerUsuarioPorIdRecuperoContraseña(pIdRecuperoContraseña);
        }

        public Entities.Usuario ObtenerUsuarioPorEmail(string pEmail)
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();

            return wUsuariosDAL.ObtenerUsuarioPorMail(pEmail);
        }

        public Entities.Usuario ObtenerUsuario(int pId)
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();

            return wUsuariosDAL.ObtenerUsuario(pId);
        }

        public List<Entities.Usuario> ObtenerUsuarios()
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();

            return wUsuariosDAL.ObtenerUsuarios();
        }

        public Common.Enums.eResultadoCambioContraseña SolicitaCambioContraseña(string pEmail, string pURL)
        {
            Common.Enums.eResultadoCambioContraseña wResultado;

            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            Entities.Usuario wUsuario = wUsuariosDAL.ObtenerUsuarioPorMail(pEmail);

            if (wUsuario == null)
            {
                wResultado = Common.Enums.eResultadoCambioContraseña.MailInexistente;
            }
            else
            {
                wUsuario.IdRecuperoContraseña = Guid.NewGuid();
                wUsuariosDAL.Editar(wUsuario);

                Common.ServicioEmail wServicio = new Common.ServicioEmail();

                string wAsunto = "Cambio de Contraseña";
                //Reemplazo el parametro de la URL con el valor correspondiente al Id de Verificacion guardado
                string wURL = pURL.Replace("ReemplazarId", wUsuario.IdRecuperoContraseña.ToString());
                string wMensaje = "<div style='width: 70%;margin: 0 auto;'><div><img src='' /></div><div style='font-size: 35px;color: #A3BD31;'> Hola, </div> Ingrese al siguiente enlace para modificar su contraseña: <br> <a href='@ENLACE'> Modificar Contraseña</a> </div>";
                wMensaje = wMensaje.Replace("@ENLACE", wURL);

                var wRecurso = new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/Img/Header-Mail.jpg"));

                Common.Enums.eResultadoEnvio wResultadoEnvio = wServicio.Enviar(pEmail, wAsunto, wMensaje, true, wRecurso);

                if (wResultadoEnvio == Common.Enums.eResultadoEnvio.Ok)
                {
                    wResultado = Common.Enums.eResultadoCambioContraseña.Ok;
                }
                else
                {
                    wResultado = Common.Enums.eResultadoCambioContraseña.Error;
                }
                
            }

            return wResultado;
        }

        public void ModificarRoles(Entities.Usuario pUsuario)
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            wUsuariosDAL.ModificarRoles(pUsuario);
        }

        public int Validar(Entities.Usuario pUsuario)
        {
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            int wId = 0;

            wId = wUsuariosDAL.Editar(pUsuario);
            //Obtengo el rol a agregar
            DAL.Roles wRolesDAL = new DAL.Roles();
            Entities.Rol wRol = wRolesDAL.ObtenerRol((int)Common.Enums.eRoles.Consumidor);
            pUsuario.Roles.Add(wRol);
            wUsuariosDAL.ModificarRoles(pUsuario);

            return wId;
        }
    }
}
