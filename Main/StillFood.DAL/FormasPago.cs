using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class FormasPago
    {
        public List<Entities.FormaPago> ObtenerFormasPagoActivas()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.FormasPago.Where(e => e.Activo == true).ToList();
            }
        }

        public List<Entities.FormaPago> ObtenerFormasPago()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.FormasPago.OrderBy(c => c.Nombre).ToList();
            }
        }

        public Entities.FormaPago ObtenerFormaPago(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.FormasPago.FirstOrDefault(c => c.Id == pId);
            }
        }

        public int Agregar(Entities.FormaPago pFormaPago)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.FormasPago.Add(pFormaPago);
                wContext.SaveChanges();
                wId = pFormaPago.Id;
            }

            return wId;
        }

        public int Editar(Entities.FormaPago pFormaPago)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pFormaPago).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return pFormaPago.Id;
        }

        public void Eliminar(int pIdFormaPago)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wFormaPago = wContext.FormasPago.FirstOrDefault(c => c.Id == pIdFormaPago);
                wFormaPago.Activo = false;
                wContext.Entry(wFormaPago).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }
        }

        public List<Entities.FormaPago> ObtenerFormasPagoRestantes(Entities.Comercio pComercio)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                List<int> wTempList = pComercio.FormasPago.Select(fp => fp.Id).ToList();

                return wContext.FormasPago.Where(fp => !wTempList.Contains(fp.Id)).ToList();
            }
        }
    }
}
