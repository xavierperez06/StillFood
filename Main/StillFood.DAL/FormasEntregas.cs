using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class FormasEntregas
    {
        public List<Entities.FormaEntrega> ObtenerFormasEntregasActivas()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.FormasEntregas.Where(e=>e.Activo == true).ToList();
            }
        }

        public Entities.FormaEntrega ObtenerFormaEntrega(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.FormasEntregas.FirstOrDefault(p => p.Id == pId);
            }
        }

        public List<Entities.FormaEntrega> ObtenerFormasEntrega()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.FormasEntregas.OrderBy(c => c.Nombre).ToList();
            }
        }

        public int Agregar(Entities.FormaEntrega pFormaEntrega)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.FormasEntregas.Add(pFormaEntrega);
                wContext.SaveChanges();
                wId = pFormaEntrega.Id;
            }

            return wId;
        }

        public int Editar(Entities.FormaEntrega pFormaEntrega)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pFormaEntrega).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return pFormaEntrega.Id;
        }

        public void Eliminar(int pIdFormaEntrega)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wFormaEntrega = wContext.FormasEntregas.FirstOrDefault(c => c.Id == pIdFormaEntrega);
                wFormaEntrega.Activo = false;
                wContext.Entry(wFormaEntrega).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }
        }
    }
}
