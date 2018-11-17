using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StillFood.DAL
{
    public class NotasPedidoDetalle
    {
        public int Agregar(Entities.NotaPedidoDetalle pDetalle)
        {
            int wId = 0;

            using (StillFoodModel wContext = new StillFoodModel())
            {
                try
                {
                    wContext.NotasPedidosDetalle.Add(pDetalle);
                    wContext.SaveChanges();
                    wId = pDetalle.Id;
                }
                catch
                {
                    wId = 0;
                }

            }

            return wId;
        }

        public void EliminarDetallesPorIdNotaPedido(int pIdNotaPedido)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.NotasPedidosDetalle.RemoveRange(wContext.NotasPedidosDetalle.Where(d => d.IdNotaPedido == pIdNotaPedido));
                wContext.SaveChanges();
            }
        }
    }
}
