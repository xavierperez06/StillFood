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

        public int ObtenerNumeroSiguiente()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                int wNumero = wContext.NotasPedidos.Max(np => np.Numero);

                if (wNumero == 0)
                    wNumero = 1;
                else
                    wNumero = wNumero + 1;

                return wNumero;
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
    }
}
