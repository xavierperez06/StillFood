using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class Compra
    {
        public Entities.NotaPedido ObtenerNotaPedido(int pId)
        {
            DAL.NotasPedido wNotaPedidoDAL = new DAL.NotasPedido();
            return wNotaPedidoDAL.ObtenerNotaPedido(pId);
        }

        public List<Entities.NotaPedido> ObtenerNotasPedidoPorIdUsuario(int pId)
        {
            DAL.NotasPedido wNotaPedidoDAL = new DAL.NotasPedido();
            return wNotaPedidoDAL.ObtenerNotasPedidoPorIdUsuario(pId);
        }

        public int GuardarNotaPedido(Entities.NotaPedido pNotaPedido)
        {
            DAL.NotasPedido wNotasPedidoDAL = new DAL.NotasPedido();
            int wId = 0;

            pNotaPedido.Numero = wNotasPedidoDAL.ObtenerNumeroSiguiente();

            wId = wNotasPedidoDAL.Agregar(pNotaPedido);

            return wId;
        }

        public bool GuardarDetalles(List<Entities.NotaPedidoDetalle> pDetalles)
        {
            DAL.NotasPedidoDetalle wNotasPedidoDetalleDAL = new DAL.NotasPedidoDetalle();
            DAL.Productos wProductosDAL = new DAL.Productos();

            foreach (var wDetalle in pDetalles)
            {
                int wId = wNotasPedidoDetalleDAL.Agregar(wDetalle);
                //Si falla uno, vuelvo todo atras
                if(wId == 0)
                {
                    //Primero elimino todos los detalles en caso de que alguno se haya guardado
                    wNotasPedidoDetalleDAL.EliminarDetallesPorIdNotaPedido(wDetalle.IdNotaPedido);
                    //Por cada producto del detalle le vuelvo a subir el stock
                    wProductosDAL.AumentarStock(wDetalle.IdProducto, wDetalle.Cantidad);
                    //Luego elimino la nota de pedido 
                    DAL.NotasPedido wNotaPedidoDAL = new DAL.NotasPedido();
                    wNotaPedidoDAL.Eliminar(wDetalle.IdNotaPedido);

                    break;
                }
                else
                {
                    //Si no falló descuento la cantidad del stock del producto
                    wProductosDAL.DescontarStock(wDetalle.IdProducto, wDetalle.Cantidad);
                }
            }

            return true;
        }
    }
}
