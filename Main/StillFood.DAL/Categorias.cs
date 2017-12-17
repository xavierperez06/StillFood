using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class Categorias
    {
        public List<Entities.Categoria> ObtenerCategorias()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.Categorias.OrderBy(c => c.Nombre).ToList();
            }
        }

        public Entities.Categoria ObtenerCategoria(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.Categorias.FirstOrDefault(c => c.Id == pId);
            }
        }

        public int Agregar(Entities.Categoria pCategoria)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Categorias.Add(pCategoria);
                wContext.SaveChanges();
                wId = pCategoria.Id;
            }

            return wId;
        }

        public int Editar(Entities.Categoria pCategoria)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pCategoria).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return pCategoria.Id;
        }

        public void Eliminar(int pIdCategoria)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wCategoria = wContext.Categorias.FirstOrDefault(c => c.Id == pIdCategoria);
                wCategoria.Activo = false;
                wContext.Entry(wCategoria).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }
        }
    }
}
