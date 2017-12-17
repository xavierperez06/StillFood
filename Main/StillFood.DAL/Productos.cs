using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class Productos
    {
       public List<Entities.Producto> ObtenerProductosPorIdComercio(int pIdComercio)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.Productos.Include("Categoria").Where(p => p.IdComercio == pIdComercio && p.Stock > 0 ).ToList();
            }
        }

        public List<Entities.Producto> ObtenerProductosPorIdComercioFiltrado(int pIdComercio, string pBusqueda)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.Productos.Include("Categoria").Where(p => (p.IdComercio == pIdComercio) && (p.Stock > 0) &&
                                                                                                        (p.Descripcion.Contains(pBusqueda) || p.Nombre.Contains(pBusqueda) || p.Marca.Contains(pBusqueda))).ToList();
            }
        }

        public Entities.Producto ObtenerProducto(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.Productos.FirstOrDefault(p => p.Id == pId);
            }
        }

        public int Agregar(Entities.Producto pProducto)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Productos.Add(pProducto);
                wContext.SaveChanges();
                wId = pProducto.Id;
            }

            return wId;
        }

        public int Editar(Entities.Producto pProducto)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pProducto).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return pProducto.Id;
        }

        public void Eliminar(int pIdProducto)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                // TODO
            }
        }

        public int AumentarStock(int pIdProducto, int pCantidad)
        {
            Entities.Producto wProducto = ObtenerProducto(pIdProducto);
            wProducto.Stock = wProducto.Stock + pCantidad;

            using (StillFoodModel wContext = new StillFoodModel())
            { 
                wContext.Entry(wProducto).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return wProducto.Id;
        }

        public int DescontarStock(int pIdProducto, int pCantidad)
        {
            Entities.Producto wProducto = ObtenerProducto(pIdProducto);
            wProducto.Stock = wProducto.Stock - pCantidad;

            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(wProducto).State = System.Data.Entity.EntityState.Modified;
                wContext.SaveChanges();
            }

            return wProducto.Id;
        }
    }
}
