using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class Permisos
    {
        public List<Entities.Permiso> ObtenerPermisos()
        {
            DAL.Permisos wPermisosDAL = new DAL.Permisos();
            return wPermisosDAL.ObtenerPermisos();
        }

        public Entities.Permiso ObtenerPermiso(int pId)
        {
            DAL.Permisos wPermisosDAL = new DAL.Permisos();
            return wPermisosDAL.ObtenerPermiso(pId);
        }

        public int Guardar(Entities.Permiso pPermiso)
        {
            DAL.Permisos wPermisosDAL = new DAL.Permisos();
            int wId = 0;

            if (pPermiso.Id == 0)
            {
                wId = wPermisosDAL.Agregar(pPermiso);
            }
            else
            {
                wId = wPermisosDAL.Editar(pPermiso);
            }

            return wId;
        }

        public void Eliminar(int pIdPermiso)
        {
            DAL.Permisos wPermisosDAL = new DAL.Permisos();
            wPermisosDAL.Eliminar(pIdPermiso);
        }

        public List<Entities.Permiso> ObtenerPermisosRestantes(Entities.Rol pRol)
        {
            DAL.Permisos wPermisosDAL = new DAL.Permisos();
            return wPermisosDAL.ObtenerPermisosRestantes(pRol);
        }
    }
}
