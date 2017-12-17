using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class Categorias
    {
        public List<Models.Categoria> ObtenerCategorias()
        {
            Business.Categoria wCategoria = new Business.Categoria();

            return ORM.ListaCategoriaEntitieAModel(wCategoria.ObtenerCategorias());
        }

        public Models.Categoria ObtenerCategoria(int pIdCategoria)
        {
            Business.Categoria wCategoria = new Business.Categoria();

            return ORM.CategoriaEntitieToModel(wCategoria.ObtenerCategoria(pIdCategoria));
        }

        public int Guardar(Models.Categoria pCategoria)
        {
            Business.Categoria wCategoria = new Business.Categoria();

            return wCategoria.Guardar(ORM.CategoriaModelToEntitie(pCategoria));
        }

        public void Eliminar(int pIdCategoria)
        {
            Business.Categoria wCategoria = new Business.Categoria();
            wCategoria.Eliminar(pIdCategoria);
        }
    }
}
