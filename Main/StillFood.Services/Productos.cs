using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
   public class Productos
    {
       public List<Models.Producto> ObtenerProductosPorIdComercio(int pIdComercio)
        {
            Business.Producto wProducto = new Business.Producto();

            List<Models.Producto> wProductosModels = new List<Models.Producto>();

            return ORM.ListaProductosEntitieAModel(wProducto.ObtenerProductosPorIdComercio(pIdComercio));
        }

        public Models.Producto ObtenerProducto(int pIdProducto)
        {
            Business.Producto wProducto = new Business.Producto();

            return ORM.ProductoEntitieToModel(wProducto.ObtenerProducto(pIdProducto));
        }

        public int Guardar(Models.Producto pProducto)
        {
            Business.Producto wProducto = new Business.Producto();

            return wProducto.Guardar(ORM.ProductoModelToEntitie(pProducto));
        }

        public void Eliminar(int pIdProducto)
        {
            Business.Producto wProducto = new Business.Producto();
            wProducto.Eliminar(pIdProducto);
        }
    }
}
