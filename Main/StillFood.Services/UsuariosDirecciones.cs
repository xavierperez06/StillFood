using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Services
{
    public class UsuariosDirecciones
    {
        public List<Models.UsuarioDireccion> ObtenerDireccionesPorIdUsuario(int pIdUsuario)
        {
            Business.UsuarioDireccion wDireccion = new Business.UsuarioDireccion();

            List<Models.UsuarioDireccion> wDireccionesModels = new List<Models.UsuarioDireccion>();

            return ORM.ListaUsuariosDireccionesEntitieAModel(wDireccion.ObtenerDireccionesPorIdUsuario(pIdUsuario));
        }

        public Models.UsuarioDireccion ObtenerDireccion(int pId)
        {
            Business.UsuarioDireccion wDireccion = new Business.UsuarioDireccion();

            return ORM.UsuarioDireccionEntitieToModel(wDireccion.ObtenerDireccion(pId));
        }

        public int Guardar(Models.UsuarioDireccion pDireccion)
        {
            Business.UsuarioDireccion wDireccion = new Business.UsuarioDireccion();

            return wDireccion.Guardar(ORM.UsuarioDireccionModelToEntitie(pDireccion));
        }

        public void Eliminar(int pIdUsuario, int pIdDireccion)
        {
            Business.UsuarioDireccion wDireccion = new Business.UsuarioDireccion();
            wDireccion.Eliminar(pIdUsuario, pIdDireccion);
        }
    }
}
