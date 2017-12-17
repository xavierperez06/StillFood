using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class Roles
    {
        public List<Models.Rol> ObtenerRoles()
        {
            Business.Rol wRol = new Business.Rol();

            return ORM.ListaRolesEntitieAModel(wRol.ObtenerRoles());
        }

        public void Eliminar(int pIdRol)
        {
            Business.Rol wRol = new Business.Rol();
            wRol.Eliminar(pIdRol);
        }

        public int Guardar(Models.Rol pRol)
        {
            Business.Rol wRol = new Business.Rol();

            return wRol.Guardar(ORM.RolModelToEntitie(pRol));
        }

        public Models.Rol ObtenerRol(int pIdRol)
        {
            Business.Rol wRoles = new Business.Rol();

            return ORM.RolEntitieToModel(wRoles.ObtenerRol(pIdRol));
        }

        public void ModificarPermisos(Models.Rol pRol)
        {
            Business.Rol wRoles = new Business.Rol();
            wRoles.ModificarPermisos(ORM.RolModelToEntitie(pRol));
        }

        public bool CheckRolAsociadoAUsuario(int pIdRol)
        {
            Business.Rol wRol = new Business.Rol();
            return wRol.CheckRolAsociadoAUsuario(pIdRol);
        }

        public List<Models.Rol> ObtenerRolesRestantes(Models.Usuario pUsuario)
        {
            Business.Rol wRol = new Business.Rol();
            return ORM.ListaRolesEntitieAModel(wRol.ObtenerRolesRestantes(ORM.UsuarioModelToEntitie(pUsuario)));
        }
    }
}
