using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    [Table("UsuariosDirecciones")]
    public class UsuarioDireccion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(50)]
        public string Calle { get; set; }
        [Required]
        [StringLength(10)]
        public string Numero { get; set; }
        public int? Piso { get; set; }
        [StringLength(10)]
        public string Departamento { get; set; }
        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }
        [StringLength(30)]
        public string Alias { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
