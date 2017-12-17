using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class UsuariosDirecciones
    {
        public List<Entities.UsuarioDireccion> ObtenerDireccionesPorIdUsuario(int pIdUsuario)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.UsuariosDirecciones.Where(u => u.IdUsuario == pIdUsuario).ToList();
            }
        }

        public Entities.UsuarioDireccion ObtenerDireccion(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.UsuariosDirecciones.FirstOrDefault(d => d.Id == pId);
            }
        }

        public int Agregar(Entities.UsuarioDireccion pDireccion)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.UsuariosDirecciones.Add(pDireccion);
                wContext.SaveChanges();
                wId = pDireccion.Id;
            }

            return wId;
        }

        public int Editar(Entities.UsuarioDireccion pDireccion)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pDireccion).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return pDireccion.Id;
        }

        public void Eliminar(int pIdUsuario, int pIdDireccion)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wDireccion = wContext.UsuariosDirecciones.SingleOrDefault(d => d.IdUsuario == pIdUsuario && d.Id == pIdDireccion);

                if (wDireccion != null)
                {
                    wContext.UsuariosDirecciones.Remove(wDireccion);
                    wContext.SaveChanges();
                }
            }
        }

    }
}
