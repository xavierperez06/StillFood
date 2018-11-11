using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class Comercios
    {
        public List<Entities.Comercio> ObtenerComercios()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Comercios.Include(c => c.FormasPago).ToList();
            }
        }

        public List<Entities.Comercio> FiltrarComercios(string pBusqueda)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Comercios.Where(c => c.Nombre.Contains(pBusqueda)).ToList();
            }
        }

        public Entities.Comercio ObtenerComercio(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Comercios.Include(c => c.FormasPago).Where(c => c.Id == pId).FirstOrDefault();
            }
        }

        public int Agregar(Entities.Comercio pComercio)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Comercios.Add(pComercio);
                wContext.SaveChanges();
                wId = pComercio.Id;
            }

            return wId;
        }

        public int Editar(Entities.Comercio pComercio)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pComercio).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return pComercio.Id;
        }

        public void Eliminar(int pIdComercio)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wComercio = wContext.Comercios.SingleOrDefault(r => r.Id == pIdComercio);

                if (wComercio != null)
                {
                    wContext.Comercios.Remove(wComercio);
                    wContext.SaveChanges();
                }
            }
        }

        public void ModificarFormasPago(Entities.Comercio pComercio)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wComercio = wContext.Comercios.Include(C => C.FormasPago).FirstOrDefault(c => c.Id == pComercio.Id);

                List<int> wTempList = pComercio.FormasPago.Select(p => p.Id).ToList();

                foreach (var wFormaPago in wComercio.FormasPago.ToList())
                {
                    // Elimino todas las formas de pago que no se encuentran en la lista de nuevas formas de pago
                    if (!wTempList.Contains(wFormaPago.Id))
                        wComercio.FormasPago.Remove(wFormaPago);
                }

                foreach (var wNuevoIdFormaPago in wTempList)
                {
                    // Agrega las formas de pago que no se encuentran en la lista de formas de pago del comercio
                    if (!wComercio.FormasPago.Any(p => p.Id == wNuevoIdFormaPago))
                    {
                        var wNuevaFormaPago = new Entities.FormaPago { Id = wNuevoIdFormaPago };
                        wContext.FormasPago.Attach(wNuevaFormaPago);
                        wComercio.FormasPago.Add(wNuevaFormaPago);
                    }
                }

                wContext.SaveChanges();
            }
        }

    }
}
