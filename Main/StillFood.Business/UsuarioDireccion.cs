using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Business
{
   public class UsuarioDireccion
    {
        public List<Entities.UsuarioDireccion> ObtenerDireccionesPorIdUsuario(int pIdUsuario)
        {
            DAL.UsuariosDirecciones wUsuariosDireccionesDAL = new DAL.UsuariosDirecciones();
            return wUsuariosDireccionesDAL.ObtenerDireccionesPorIdUsuario(pIdUsuario);
        }

        public Entities.UsuarioDireccion ObtenerDireccion(int pId)
        {
            DAL.UsuariosDirecciones wUsuariosDireccionesDAL = new DAL.UsuariosDirecciones();
            return wUsuariosDireccionesDAL.ObtenerDireccion(pId);
        }

        public int Guardar(Entities.UsuarioDireccion pDireccion)
        {
            DAL.UsuariosDirecciones wUsuariosDireccionesDAL = new DAL.UsuariosDirecciones();
            int wId = 0;

            if (pDireccion.Id == 0)
            {
                wId = wUsuariosDireccionesDAL.Agregar(pDireccion);
            }
            else
            {
                wId = wUsuariosDireccionesDAL.Editar(pDireccion);
            }

            return wId;
        }

        public void Eliminar(int pIdUsuario, int pIdDireccion)
        {
            DAL.UsuariosDirecciones wDireccionesDAL = new DAL.UsuariosDirecciones();
            wDireccionesDAL.Eliminar(pIdUsuario, pIdDireccion);
        }
    }
}
