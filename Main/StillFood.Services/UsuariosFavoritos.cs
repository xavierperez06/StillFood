using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class UsuariosFavoritos
    {
        public bool EsFavorito(int pIdComercio, int pIdUsuario)
        {
            Business.UsuarioFavorito wUsuarioFavorito = new Business.UsuarioFavorito();

            return wUsuarioFavorito.EsFavorito(pIdComercio, pIdUsuario);
        }

        public List<Models.UsuarioFavorito> ObtenerFavoritosPorIdUsuario(int pIdUsuario)
        {
            Business.UsuarioFavorito wFavoritos = new Business.UsuarioFavorito();

            return ORM.ListaUsuariosFavoritosEntitieAModel(wFavoritos.ObtenerFavoritosPorIdUsuario(pIdUsuario));
        }
    }
}
