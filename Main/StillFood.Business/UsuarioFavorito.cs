using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
    public class UsuarioFavorito
    {
        public bool EsFavorito(int pIdComercio, int pIdUsuario)
        {
            DAL.UsuariosFavoritos wUsuariosFavoritosDAL = new DAL.UsuariosFavoritos();

            return wUsuariosFavoritosDAL.EsFavorito(pIdComercio, pIdUsuario);
        }

        public List<Entities.UsuarioFavorito> ObtenerFavoritosPorIdUsuario(int pIdUsuario)
        {
            DAL.UsuariosFavoritos wUsuariosFavoritosDAL = new DAL.UsuariosFavoritos();
            return wUsuariosFavoritosDAL.ObtenerFavoritosPorIdUsuario(pIdUsuario);
        }
    }
}
