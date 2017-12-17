using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public int? IdComercio { get; set; }
        [Required]
        [StringLength(200)]
        public string NombreApellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        [Required]
        [StringLength(70)]
        public string Email { get; set; }
        [Required]
        [StringLength(150)]
        public string Contraseña { get; set; }
        [Required]
        public int IdEstado { get; set; }
        [Required]
        public int IdTipoUsuario { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        public Guid? IdConfirmacion { get; set; }
        public Guid? IdRecuperoContraseña { get; set; }
        public DateTime? FechaExpConfirmacion { get; set; }
        public virtual ICollection<Rol> Roles { get; set; }
        [ForeignKey("IdComercio")]
        public Comercio Comercio { get; set; }

    }
}
