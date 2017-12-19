using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    if(pNotaPedido.IdDireccion == 0)
                    {
                        pNotaPedido.IdDireccion = null;
                    }
                    wContext.NotasPedidos.Add(pNotaPedido);
                    wContext.SaveChanges();
                    wId = pNotaPedido.Id;
                }
                catch(Exception ex)
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
                return wContext.NotasPedidos.FirstOrDefault(np => np.Id == pId);
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
                return wContext.NotasPedidos.Where(np => np.IdComercio == pId).ToList();
            }
        }
    }
}
