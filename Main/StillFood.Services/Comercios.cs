using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class Comercios
    {
        public List<Models.Comercio> ObtenerComercios()
        {
            Business.Comercio wComercio = new Business.Comercio();

            return ORM.ListaComercioEntitieAModel(wComercio.ObtenerComercios());
        }

        public List<Models.Comercio> FiltrarComercios(string pBusqueda)
        {
            Business.Comercio wComercio = new Business.Comercio();

            List<Models.Comercio> wComerciosModels = new List<Models.Comercio>();

            return ORM.ListaComercioEntitieAModel(wComercio.FiltrarComercios(pBusqueda));
        }

        public Models.Comercio ObtenerComercioConProductos(int pIdComercio)
        {
            Business.Comercio wComercio = new Business.Comercio();

            return ORM.ComercioEntitieToModel(wComercio.ObtenerComercioConProductos(pIdComercio));
        }

        public Models.Comercio ObtenerComercioConProductosFiltrado(int pIdComercio, string pBusqueda)
        {
            Business.Comercio wComercio = new Business.Comercio();

            return ORM.ComercioEntitieToModel(wComercio.ObtenerComercioConProductosFiltrado(pIdComercio,pBusqueda));
        }

        public Models.Comercio ObtenerComercio(int pIdComercio)
        {
            Business.Comercio wComercio = new Business.Comercio();

            return ORM.ComercioEntitieToModel(wComercio.ObtenerComercio(pIdComercio));
        }

        public int Guardar(Models.Comercio pComercio)
        {
            Business.Comercio wComercio = new Business.Comercio();

            return wComercio.Guardar(ORM.ComercioModelToEntitie(pComercio));
        }

        public void Eliminar(int pIdComercio)
        {
            Business.Comercio wComercio = new Business.Comercio();
            wComercio.Eliminar(pIdComercio);
        }

        public void ModificarFormasPago(Models.Comercio pComercio)
        {
            Business.Comercio wComercio = new Business.Comercio();
            wComercio.ModificarFormasPago(ORM.ComercioModelToEntitie(pComercio));
        }

        public List<Models.NotaPedido> FiltrarNotasPedido(int? pIdEstado, string pUsuario, int pIdComercio)
        {
            Business.Comercio wComercio = new Business.Comercio();

            return ORM.ListaNotaPedidoEntitieAModel(wComercio.FiltrarNotasPedido(pIdEstado,pUsuario,pIdComercio));
        }

        public List<Models.NotaPedido> ReporteIngresosDiarios(int pIdComercios)
        {
            Business.Comercio wComercio = new Business.Comercio();

            return ORM.ListaNotaPedidoEntitieAModel(wComercio.ReporteIngresosDiarios(pIdComercios));
        }

        public List<Models.NotaPedido> ReporteIngresosMensuales(int pIdComercios)
        {
            Business.Comercio wComercio = new Business.Comercio();

            return ORM.ListaNotaPedidoEntitieAModel(wComercio.ReporteIngresosMensuales(pIdComercios));
        }

        public List<Models.NotaPedido> ReporteProductosVendidos(int pIdComercios)
        {
            Business.Comercio wComercio = new Business.Comercio();

            return ORM.ListaNotaPedidoEntitieAModel(wComercio.ReporteProductosVendidos(pIdComercios));
        }
    }
}
