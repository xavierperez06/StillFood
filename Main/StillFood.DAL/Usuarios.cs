using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StillFood.DAL
{
    public class Usuarios
    {
        public List<Entities.Usuario> ObtenerUsuarios()
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Usuarios.ToList();
            }
        }

        public Entities.Usuario ObtenerUsuario(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                //Esto es para deshabilitar lazyload que requiere del Context para acceder
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Usuarios.Include(u => u.Roles.Select(r => r.Permisos)).Where(u => u.Id == pId).FirstOrDefault();
            }
        }

        public Entities.Usuario ObtenerUsuarioPorMail(string pMail)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                //Esto es para deshabilitar lazyload que requiere del Context para acceder
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Usuarios.Include(u => u.Roles.Select(r => r.Permisos)).Where(u => u.Email.ToLower().Equals(pMail.ToLower())).FirstOrDefault();
            }
        }

        public int Agregar(Entities.Usuario pUsuario)
        {
            int wId = 0;
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Usuarios.Add(pUsuario);
                wContext.SaveChanges();
                wId = pUsuario.Id;
            }

            return wId;
        }

        public void Eliminar(int pId)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wUsuario = wContext.Usuarios.FirstOrDefault(u => u.Id == pId);
                wContext.Usuarios.Remove(wUsuario);
                wContext.SaveChanges();
            }
        }

        public Entities.Usuario ObtenerUsuarioPorIdConfirmacion(Guid pIdConfirmacion)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return  wContext.Usuarios.FirstOrDefault(u => u.IdConfirmacion == pIdConfirmacion);
            }
        }

        public int Editar(Entities.Usuario pUsuario)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Entry(pUsuario).State = EntityState.Modified;
                wContext.SaveChanges();
            }

            return pUsuario.Id;
        }

        public Entities.Usuario ObtenerUsuarioPorIdRecuperoContraseña(Guid pIdRecuperoContraseña)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                wContext.Configuration.LazyLoadingEnabled = false;

                return wContext.Usuarios.FirstOrDefault(u => u.IdRecuperoContraseña == pIdRecuperoContraseña);
            }
        }

        public void ModificarRoles(Entities.Usuario pUsuario)
        {
            using (StillFoodModel wContext = new StillFoodModel())
            {
                var wUsuario = wContext.Usuarios.Include(u => u.Roles).FirstOrDefault(x => x.Id == pUsuario.Id);

                List<int> wTempList = pUsuario.Roles.Select(r => r.Id).ToList();

                foreach (var wRol in wUsuario.Roles.ToList())
                {
                    // Elimino todos los roles que no se encuentran en la lista de nuevos roles
                    if (!wTempList.Contains(wRol.Id))
                        wUsuario.Roles.Remove(wRol);
                }


                foreach (var wNuevoIdRol in wTempList)
                {
                    // Agrega los roles que no se encuentran en la lista de roles del usuario
                    if (!wUsuario.Roles.Any(r => r.Id == wNuevoIdRol))
                    {
                        var wNuevoRol = new Entities.Rol { Id = wNuevoIdRol };
                        wContext.Roles.Attach(wNuevoRol);
                        wUsuario.Roles.Add(wNuevoRol);
                    }
                }

                wContext.SaveChanges();
            }
        }
    }
}
