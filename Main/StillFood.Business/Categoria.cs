using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class Categoria
    {
        public List<Entities.Categoria> ObtenerCategorias()
        {
            DAL.Categorias wCategoriasDAL = new DAL.Categorias();
            return wCategoriasDAL.ObtenerCategorias();
        }

        public Entities.Categoria ObtenerCategoria(int pId)
        {
            DAL.Categorias wCategoriasDAL = new DAL.Categorias();
            return wCategoriasDAL.ObtenerCategoria(pId);
        }

        public int Guardar(Entities.Categoria pCategoria)
        {
            DAL.Categorias wCategoriasDAL = new DAL.Categorias();
            int wId = 0;

            if (pCategoria.Id == 0)
            {
                wId = wCategoriasDAL.Agregar(pCategoria);
            }
            else
            {
                wId = wCategoriasDAL.Editar(pCategoria);
            }

            return wId;
        }

        public void Eliminar(int pIdCategoria)
        {
            DAL.Categorias wCategoriasDAL = new DAL.Categorias();
            wCategoriasDAL.Eliminar(pIdCategoria);
        }
    }
}
