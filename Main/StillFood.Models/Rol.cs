using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
   public class Rol
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPermisoTemporal { get; set; }
        public List<Permiso> Permisos { get; set; }
    }
}
