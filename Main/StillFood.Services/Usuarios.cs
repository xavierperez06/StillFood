using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class Usuarios
    {
        public Common.Enums.eResultadoRegistro Agregar(Models.Usuario pUsuario,string pURL)
        {
            Business.Usuario wUsuario = new Business.Usuario();

            Common.Enums.eResultadoRegistro wAgregado = wUsuario.Agregar(ORM.UsuarioModelToEntitie(pUsuario),pURL);

            return wAgregado;
        }

        public Common.Enums.eResultadoRegistro ReenviarActivacion(Models.Usuario pUsuario, string pURL)
        {
            Business.Usuario wUsuario = new Business.Usuario();

            Common.Enums.eResultadoRegistro wAgregado = wUsuario.ReenviarActivacion(ORM.UsuarioModelToEntitie(pUsuario), pURL);

            return wAgregado;
        }

        public int Guardar(Models.Usuario pUsuario)
        {
            Business.Usuario wUsuario = new Business.Usuario();

            return wUsuario.Guardar(ORM.UsuarioModelToEntitie(pUsuario));
        }

        public Models.Usuario ObtenerUsuarioPorIdConfirmacion(Guid pIdConfirmacion)
        {
            Business.Usuario wUsuario = new Business.Usuario();

            return ORM.UsuarioEntitieToModel(wUsuario.ObtenerUsuarioPorIdConfirmacion(pIdConfirmacion));
        }

        public Models.Usuario ObtenerUsuarioPorIdRecuperoContraseña(Guid pIdRecuperoContraseña)
        {
            Business.Usuario wUsuario = new Business.Usuario();

            return ORM.UsuarioEntitieToModel(wUsuario.ObtenerUsuarioPorIdRecuperoContraseña(pIdRecuperoContraseña));
        }

        public  Models.Usuario ObtenerUsuarioPorEmail(string pEmail)
        {
            Business.Usuario wUsuario = new Business.Usuario();

            return ORM.UsuarioEntitieToModel(wUsuario.ObtenerUsuarioPorEmail(pEmail));
        }

        public Models.Usuario ObtenerUsuario(int pId)
        {
            Business.Usuario wUsuario = new Business.Usuario();

            return ORM.UsuarioEntitieToModel(wUsuario.ObtenerUsuario(pId));
        }

        public List<Models.Usuario> ObtenerUsuarios()
        {
            Business.Usuario wUsuario = new Business.Usuario();

            return ORM.ListaUsuariosEntitieAModel(wUsuario.ObtenerUsuarios());
        }

        public Common.Enums.eResultadoCambioContraseña SolicitaCambioContraseña(string pEmail, string pURL)
        {
            Business.Usuario wUsuario = new Business.Usuario();

            return wUsuario.SolicitaCambioContraseña(pEmail, pURL);
        }

        public void Eliminar(int pIdUsuario)
        {
            Business.Usuario wUsuario = new Business.Usuario();
            wUsuario.Eliminar(pIdUsuario);
        }

        public void ModificarRoles(Models.Usuario pUsuario)
        {
            Business.Usuario wUsuario = new Business.Usuario();
            wUsuario.ModificarRoles(ORM.UsuarioModelToEntitie(pUsuario));
        }
    }
}
