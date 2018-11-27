using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class Comercio
    {
        public List<Entities.Comercio> ObtenerComercios()
        {
            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            return wComerciosDAL.ObtenerComercios();
        }

        public List<Entities.Comercio> FiltrarComercios(string pBusqueda)
        {
            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            return wComerciosDAL.FiltrarComercios(pBusqueda);
        }

        public Entities.Comercio ObtenerComercioConProductos(int pIdComercio)
        {
            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            Entities.Comercio wComercio = wComerciosDAL.ObtenerComercio(pIdComercio);

            if (wComercio == null)
            {
                return null;
            }
            else
            {
                DAL.Productos wProductosDAL = new DAL.Productos();
                wComercio.Productos = wProductosDAL.ObtenerProductosPorIdComercio(wComercio.Id);
                return wComercio;
            }
        }

        public Entities.Comercio ObtenerComercioConProductosFiltrado(int pIdComercio, string pBusqueda)
        {
            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            Entities.Comercio wComercio = wComerciosDAL.ObtenerComercio(pIdComercio);

            if (wComercio == null)
            {
                return null;
            }
            else
            {
                DAL.Productos wProductosDAL = new DAL.Productos();
                wComercio.Productos = wProductosDAL.ObtenerProductosPorIdComercioFiltrado(wComercio.Id, pBusqueda);

                return wComercio;
            }
        }

        public Entities.Comercio ObtenerComercio(int pIdComercio)
        {
            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            Entities.Comercio wComercio = wComerciosDAL.ObtenerComercio(pIdComercio);

            return wComercio;
        }

        public int Guardar(Entities.Comercio pComercio)
        {
            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            int wId = 0;

            if (pComercio.Id == 0)
            {
                wId = wComerciosDAL.Agregar(pComercio);
            }
            else
            {
                wId = wComerciosDAL.Editar(pComercio);
            }

            return wId;
        }

        public void Eliminar(int pIdComercio)
        {
            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            wComerciosDAL.Eliminar(pIdComercio);
        }

        public void ModificarFormasPago(Entities.Comercio pComercio)
        {
            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            wComerciosDAL.ModificarFormasPago(pComercio);
        }

        public List<Entities.NotaPedido> ReporteIngresosDiarios(int pIdComercio)
        {
            DAL.NotasPedido wNotasPedidoDAL = new DAL.NotasPedido();
            return wNotasPedidoDAL.ReporteIngresosDiarios(pIdComercio);
        }

        public List<Entities.NotaPedido> FiltrarNotasPedido(int? pIdEstado, string pUsuario, int pIdComercio)
        {
            DAL.NotasPedido wNotasPedidoDAL = new DAL.NotasPedido();
            return wNotasPedidoDAL.FiltrarNotasPedido(pIdEstado,pUsuario,pIdComercio);
        }

        public List<Entities.NotaPedido> ReporteIngresosMensuales(int pIdComercio)
        {
            DAL.NotasPedido wNotasPedidoDAL = new DAL.NotasPedido();
            return wNotasPedidoDAL.ReporteIngresosMensuales(pIdComercio);
        }

        public List<Entities.NotaPedido> ReporteProductosVendidos(int pIdComercio)
        {
            DAL.NotasPedido wNotasPedidoDAL = new DAL.NotasPedido();
            return wNotasPedidoDAL.ReporteProductosVendidos(pIdComercio);
        }

        public Common.Enums.eResultadoAccion EnviarNotificaciones(int pIdComercio, string pNotificacion)
        {
            DAL.UsuariosFavoritos wUsuariosFavoritosDAL = new DAL.UsuariosFavoritos();
            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            string wEmails = string.Empty;
            //Primero obtengo todos los favoritos de acuerdo al Id de Comercio
            List<Entities.UsuarioFavorito> wFavoritos = wUsuariosFavoritosDAL.ObtenerFavoritosPorIdComercio(pIdComercio);
            //Recorro todos los favoritos, y voy obteniendo los emails de los usuarios
            foreach(var wFav in wFavoritos)
            {
                Entities.Usuario wUsuario = wUsuariosDAL.ObtenerUsuario(wFav.IdUsuario);

                if(wUsuario != null)
                {
                    wEmails += wUsuario.Email + ";";
                }
            }
            //Envio los emials
            Common.ServicioEmail wServicio = new Common.ServicioEmail();
            string wAsunto = "Novedades";
            string wMensaje = pNotificacion;
            
            Common.Enums.eResultadoEnvio wResultado = wServicio.Enviar(wEmails, wAsunto, wMensaje, true);

            //TODO : Una vez enviados los emails, guardo la notificaciones para mostrarlas en el menu del usuario

            if (wResultado.Equals(Convert.ToInt32(Common.Enums.eResultadoEnvio.Ok)))
                return Common.Enums.eResultadoAccion.Ok;
            else
                return Common.Enums.eResultadoAccion.Error;
        }
    }
}
