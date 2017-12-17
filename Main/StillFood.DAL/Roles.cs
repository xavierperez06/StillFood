using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StillFood.DAL
{
   public class Roles
    {
        public List<Entities.Rol> ObtenerRoles()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Roles.ToList();
            }
        }

        public bool CheckRolAsociadoAUsuario(int pIdRol)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wExiste = wContext.Usuarios.Any(u => u.Roles.Any(r => r.Id == pIdRol));

                return wExiste;
            }
        }

        public Entities.Rol ObtenerRol(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Roles.Include(r => r.Permisos).FirstOrDefault(p => p.Id == pId);
            }
        }

        public int Agregar(Entities.Rol pRol)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Roles.Add(pRol);
                wContext.SaveChanges();
                wId = pRol.Id;
            }

            return wId;
        }

        public int Editar(Entities.Rol pRol)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pRol).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return pRol.Id;
        }

        public void ModificarPermisos(Entities.Rol pRol)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wRol = wContext.Roles.Include(r => r.Permisos).FirstOrDefault(p => p.Id == pRol.Id);

                List<int> wTempList = pRol.Permisos.Select(p => p.Id).ToList();

                foreach (var wPermiso in wRol.Permisos.ToList())
                {
                    // Elimino todos los permisos que no se encuentran en la lista de nuevos permisos
                    if (!wTempList.Contains(wPermiso.Id))
                        wRol.Permisos.Remove(wPermiso);
                }


                foreach (var wNuevoIdPermioso in wTempList)
                {
                    // Agrega los permisos que no se encuentran en la lista de permisos del rol
                    if (!wRol.Permisos.Any(p => p.Id == wNuevoIdPermioso))
                    {
                        var wNuevoPermiso = new Entities.Permiso { Id = wNuevoIdPermioso };
                        wContext.Permisos.Attach(wNuevoPermiso);
                        wRol.Permisos.Add(wNuevoPermiso);
                    }
                }

                wContext.SaveChanges();
            }
        }

        public void Eliminar(int pIdRol)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wRol = wContext.Roles.SingleOrDefault(r => r.Id == pIdRol); 

                if (wRol != null)
                {
                    wContext.Roles.Remove(wRol);
                    wContext.SaveChanges();
                }
            }
        }

        public List<Entities.Rol> ObtenerRolesRestantes(Entities.Usuario pUsuario)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                List<int> wTempList = pUsuario.Roles.Select(r => r.Id).ToList();

                return wContext.Roles.Where(r => !wTempList.Contains(r.Id)).ToList();
            }
        }
    }
}
