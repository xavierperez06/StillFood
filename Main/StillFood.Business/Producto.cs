using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class Producto
    {
        public List<Entities.Producto> ObtenerProductosPorIdComercio(int pIdComercio)
        {
            DAL.Productos wProductosDAL = new DAL.Productos();
            return wProductosDAL.ObtenerProductosPorIdComercio(pIdComercio);
        }

        public Entities.Producto ObtenerProducto(int pId)
        {
            DAL.Productos wProductosDAL = new DAL.Productos();
            return wProductosDAL.ObtenerProducto(pId);
        }

        public int Guardar(Entities.Producto pProducto)
        {
            DAL.Productos wProductosDAL = new DAL.Productos();
            int wId = 0;

            if (pProducto.Id == 0)
            {
                wId = wProductosDAL.Agregar(pProducto);
            }
            else
            {
                wId = wProductosDAL.Editar(pProducto);
            }

            return wId;
        }

        public void Eliminar(int pIdProducto)
        {
            DAL.Productos wProductosDAL = new DAL.Productos();
            wProductosDAL.Eliminar(pIdProducto);
        }

        public List<Entities.Producto> ReporteStock(int pIdComercio)
        {
            DAL.Productos wProductosDAL = new DAL.Productos();
            return wProductosDAL.ReporteStock(pIdComercio);
        }
    }
}
