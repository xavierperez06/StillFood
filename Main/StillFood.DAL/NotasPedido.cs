using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StillFood.DAL
{
    public class NotasPedido
    {
        public int Agregar(Entities.NotaPedido pNotaPedido)
        {
            int wId = 0;

            using (StillFoodModel wContext = new StillFoodModel())
            {
                try
                {
                    if (pNotaPedido.IdDireccion == 0)
                    {
                        pNotaPedido.IdDireccion = null;
                    }
                    wContext.NotasPedidos.Add(pNotaPedido);
                    wContext.SaveChanges();
                    wId = pNotaPedido.Id;
                }
                catch
                {
                    wId = 0;
                }

            }

            return wId;
        }

        public void Eliminar(int pIdNotaPedido)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wNotaPedido = wContext.NotasPedidos.SingleOrDefault(np => np.Id == pIdNotaPedido);

                if (wNotaPedido != null)
                {
                    wContext.NotasPedidos.Remove(wNotaPedido);
                    wContext.SaveChanges();
                }
            }
        }

        public int Editar(Entities.NotaPedido pNotaPedido)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pNotaPedido).State = EntityState.Modified;
                wContext.SaveChanges();
            }

            return pNotaPedido.Id;
        }

        public int ObtenerNumeroSiguiente()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                int? wNumero = wContext.NotasPedidos.Max(np => (int?)np.Numero);

                if (wNumero.HasValue)
                {
                    if (wNumero.Value == 0)
                        wNumero = 1;
                    else
                        wNumero = wNumero + 1;
                }
                else
                {
                    wNumero = 1;
                }
                
                return wNumero.Value;
            }
        }

        public Entities.NotaPedido ObtenerNotaPedido(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.NotasPedidos.Include(np => np.Detalles).Include(np =>np.Detalles.Select(p =>p.Producto)).Include(np => np.Usuario).FirstOrDefault(d => d.Id == pId);
            }
        }

        public List<Entities.NotaPedido> ObtenerNotasPedidoPorIdUsuario(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.NotasPedidos.Where(np => np.IdUsuario == pId).ToList();
            }
        }

        public List<Entities.NotaPedido> ObtenerNotasPedidoPorIdComercio(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;
                return wContext.NotasPedidos.Where(np => np.IdComercio == pId).ToList();
            }
        }

        public List<Entities.NotaPedido> ReporteIngresosDiarios(int pIdComercio)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.NotasPedidos.Include(np => np.Detalles).Include(np => np.Detalles.Select(p => p.Producto))
                                    .Where(np => np.IdComercio == pIdComercio && np.IdEstado != 5).ToList();
            }
        }

        public List<Entities.NotaPedido> ReporteIngresosMensuales(int pIdComercio)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                int wAño = DateTime.Today.Year;
                int wMes = DateTime.Today.Month;
                DateTime wFecha = new DateTime(wAño, wMes, 1);

                return wContext.NotasPedidos.Include(np => np.Detalles).Include(np => np.Detalles.Select(p => p.Producto))
                                    .Where(np => np.IdComercio == pIdComercio && np.IdEstado != 5 && np.FechaAlta >= wFecha).ToList();
            }
        }
    }
}
