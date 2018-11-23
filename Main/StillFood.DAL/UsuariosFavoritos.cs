using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.DAL
{
    public class UsuariosFavoritos
    {
        public bool EsFavorito(int pIdComercio, int pIdUsuario)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.UsuariosFavoritos.Where(uf => uf.IdComercio == pIdComercio && uf.IdUsuario == pIdUsuario).Any();
            }
        }

        public List<Entities.UsuarioFavorito> ObtenerFavoritosPorIdUsuario(int pIdUsuario)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.UsuariosFavoritos.Where(u => u.IdUsuario == pIdUsuario).ToList();
            }
        }
    }
}
