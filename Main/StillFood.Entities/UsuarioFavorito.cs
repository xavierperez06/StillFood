using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StillFood.Entities
{
    [Table("UsuariosFavoritos")]
    public class UsuarioFavorito
    {
        public int Id { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdComercio { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [ForeignKey("IdComercio")]
        public Comercio Comercio { get; set; }
    }
}
