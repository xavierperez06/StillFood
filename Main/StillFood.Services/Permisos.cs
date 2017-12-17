using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class Permisos
    {
        public List<Models.Permiso> ObtenerPermisos()
        {
            Business.Permisos wPermisos = new Business.Permisos();

            return ORM.ListaPermisosEntitieAModel(wPermisos.ObtenerPermisos());
        }

        public int Guardar(Models.Permiso pPermiso)
        {
            Business.Permisos wPermiso = new Business.Permisos();

            return wPermiso.Guardar(ORM.PermisoModelToEntitie(pPermiso));
        }

        public Models.Permiso ObtenerPermiso(int pIdPermiso)
        {
            Business.Permisos wPermisos = new Business.Permisos();

            return ORM.PermisoEntitieToModel(wPermisos.ObtenerPermiso(pIdPermiso));
        }

        public void Eliminar(int pIdPermiso)
        {
            Business.Permisos wPermiso = new Business.Permisos();
            wPermiso.Eliminar(pIdPermiso);
        }

        public List<Models.Permiso> ObtenerPermisosRestantes(Models.Rol pRol)
        {
            Business.Permisos wPermiso = new Business.Permisos();
            return ORM.ListaPermisosEntitieAModel(wPermiso.ObtenerPermisosRestantes(ORM.RolModelToEntitie(pRol)));
        }
    }
}
