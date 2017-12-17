using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class Permisos
    {
        public List<Entities.Permiso> ObtenerPermisos()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.Permisos.ToList();
            }
        }

        public Entities.Permiso ObtenerPermiso(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.Permisos.FirstOrDefault(p => p.Id == pId);
            }
        }

        public int Agregar(Entities.Permiso pPermiso)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Permisos.Add(pPermiso);
                wContext.SaveChanges();
                wId = pPermiso.Id;
            }

            return wId;
        }

        public int Editar(Entities.Permiso pPermiso)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pPermiso).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return pPermiso.Id;
        }

        public void Eliminar(int pIdPermiso)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wPermiso = wContext.Permisos.FirstOrDefault(p => p.Id == pIdPermiso);
                wPermiso.Activo = false;
                wContext.Entry(wPermiso).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }
        }

        public List<Entities.Permiso> ObtenerPermisosRestantes(Entities.Rol pRol)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                List<int> wTempList = pRol.Permisos.Select(p => p.Id).ToList();

                return wContext.Permisos.Where(p => !wTempList.Contains(p.Id)).ToList();
            }
        }
    }
}
