using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class Rol
    {
        public List<Entities.Rol> ObtenerRoles()
        {
            DAL.Roles wRolesDAL = new DAL.Roles();
            return wRolesDAL.ObtenerRoles();
        }

        public Entities.Rol ObtenerRol(int pId)
        {
            DAL.Roles wRolesDAL = new DAL.Roles();
            return wRolesDAL.ObtenerRol(pId);
        }

        public int Guardar(Entities.Rol pRol)
        {
            DAL.Roles wRolesDAL = new DAL.Roles();
            int wId = 0;

            if (pRol.Id == 0)
            {
                wId = wRolesDAL.Agregar(pRol);
            }
            else
            {
                wId = wRolesDAL.Editar(pRol);
            }

            return wId;
        }

        public void Eliminar(int pIdRol)
        {
            DAL.Roles wRolesDAL = new DAL.Roles();
            wRolesDAL.Eliminar(pIdRol);
        }

        public void ModificarPermisos(Entities.Rol pRol)
        {
            DAL.Roles wRolesDAL = new DAL.Roles();
            wRolesDAL.ModificarPermisos(pRol);
        }

        public bool CheckRolAsociadoAUsuario(int pIdRol)
        {
            DAL.Roles wRolesDAL = new DAL.Roles();
            return wRolesDAL.CheckRolAsociadoAUsuario(pIdRol);
        }

        public List<Entities.Rol> ObtenerRolesRestantes(Entities.Usuario pUsuario)
        {
            DAL.Roles wRolesDAL = new DAL.Roles();
            return wRolesDAL.ObtenerRolesRestantes(pUsuario);
        }
    }
}


