using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    [Table("Permisos")]
    public class Permiso
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(200)]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<Rol> Roles { get; set; }
    }
}
