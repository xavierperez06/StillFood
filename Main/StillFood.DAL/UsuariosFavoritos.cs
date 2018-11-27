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

        public List<Entities.UsuarioFavorito> ObtenerFavoritosPorIdComercio(int pIdComercio)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                return wContext.UsuariosFavoritos.Where(u => u.IdComercio == pIdComercio).ToList();
            }
        }

        public List<Entities.UsuarioFavorito> AgregarQuitarFavorito(int pIdUsuario, int pIdComercio, bool pAgrega)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                if(pAgrega)
                {
                    var wUsuarioFavorito = new Entities.UsuarioFavorito() { IdUsuario = pIdUsuario, IdComercio = pIdComercio };
                    wContext.UsuariosFavoritos.Add(wUsuarioFavorito);
                    wContext.SaveChanges();
                }
                else
                {
                    wContext.UsuariosFavoritos.RemoveRange(wContext.UsuariosFavoritos.Where(uf => uf.IdUsuario == pIdUsuario && uf.IdComercio == pIdComercio));
                    wContext.SaveChanges();
                }

                return wContext.UsuariosFavoritos.Where(u => u.IdUsuario == pIdUsuario).ToList();
            }
        }

        
    }
}
