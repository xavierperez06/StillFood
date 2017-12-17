using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class Compras
    {
        public Models.NotaPedido ObtenerNotaPedido(int pIdNotaPedido)
        {
            Business.Compra wCompras = new Business.Compra();

            return ORM.NotaPedidoEntitieToModel(wCompras.ObtenerNotaPedido(pIdNotaPedido));
        }

        public List<Models.NotaPedido> ObtenerNotasPedidoPorIdUsuario(int pIdUsuario)
        {
            Business.Compra wCompras = new Business.Compra();

            return ORM.ListaNotaPedidoEntitieAModel(wCompras.ObtenerNotasPedidoPorIdUsuario(pIdUsuario));
        }

        public int GuardarNotaPedido(Models.NotaPedido pNotaPedido)
        {
            Business.Compra wCompras = new Business.Compra();

            return wCompras.GuardarNotaPedido(ORM.NotaPedidoModelToEntitie(pNotaPedido));
        }

        public bool GuardarDetalles(List<Models.NotaPedidoDetalle> pListaDetalle)
        {
            Business.Compra wCompras = new Business.Compra();

            return wCompras.GuardarDetalles(ORM.ListaNotaPedidoDetalleModelAEntitie(pListaDetalle));
        }
    }
}
