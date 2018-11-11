using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        public List<Entities.NotaPedido> ObtenerNotasPedidoPorIdComercio(int pId)
        {
            DAL.NotasPedido wNotaPedidoDAL = new DAL.NotasPedido();
            return wNotaPedidoDAL.ObtenerNotasPedidoPorIdComercio(pId);
        }

        public int GuardarNotaPedido(Entities.NotaPedido pNotaPedido)
        {
            DAL.NotasPedido wNotasPedidoDAL = new DAL.NotasPedido();
            int wId = 0;

            if(pNotaPedido.Id == 0)
            {
                pNotaPedido.Numero = wNotasPedidoDAL.ObtenerNumeroSiguiente();

                wId = wNotasPedidoDAL.Agregar(pNotaPedido);
            }
            else
            {
                wId = wNotasPedidoDAL.Editar(pNotaPedido);
            }

            return wId;
        }

        public bool GuardarDetalles(List<Entities.NotaPedidoDetalle> pDetalles, int pIdComercio)
        {
            DAL.NotasPedidoDetalle wNotasPedidoDetalleDAL = new DAL.NotasPedidoDetalle();
            DAL.Productos wProductosDAL = new DAL.Productos();
            DAL.Comercios wComercioDAL = new DAL.Comercios();

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

            try
            {
                //Y le envio un email al comercio indicandole la compra
                Entities.Comercio wComercio = wComercioDAL.ObtenerComercio(pIdComercio);
                Common.ServicioEmail wServicio = new Common.ServicioEmail();
                string wAsunto = "Nuevo Pedido StillFood";
                //Reemplazo el parametro de la URL con el valor correspondiente al Id de Verificacion guardado
                string wMensaje = string.Format("<div style='width: 70%;margin: 0 auto;'><div><img src='' /></div><div style='font-size: 35px;color: #A3BD31;'> Hola, {0} </div> <br/> se ha realizado un nuevo pedido en su local. Puede verlo desde el menú de opciones de StillFood. </div>", wComercio.Nombre);
                var wRecurso = new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/Img/Header-Mail.jpg"));

                Common.Enums.eResultadoEnvio wResultado = wServicio.Enviar(wComercio.Email, wAsunto, wMensaje, true, wRecurso);
            }
            catch
            {

            }

            return true;
        }
    }
}
