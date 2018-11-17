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
    }
}
