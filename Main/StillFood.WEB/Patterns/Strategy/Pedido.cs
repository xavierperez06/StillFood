using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Strategy
{
    public class Pedido : Models.NotaPedido
    {
        List<Models.Producto> mListaProductos = new List<Models.Producto>();

        private FormaPagoStrategy mFormaPagoStrategy;

        public void EstablecerEstrategia(FormaPagoStrategy pFormaPagoStrategy)
        {
            this.mFormaPagoStrategy = pFormaPagoStrategy;
        }

        public void Agregar(Models.Producto pProducto)
        {
            mListaProductos.Add(pProducto);
        }

        public bool Pagar()
        {
            decimal wMonto = Common.Utils.ObtenerMontoTotal(mListaProductos);

            return mFormaPagoStrategy.Pagar(wMonto);
        }

        public List<Models.Producto> Productos
        {
            get
            {
                return mListaProductos;
            }
        }
    }
}