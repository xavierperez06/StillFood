using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public int? IdComercio { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string NombreApellido { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Email { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoUsuario { get; set; }
        public DateTime FechaAlta { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Contraseña { get; set; }
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContraseña { get; set; }
        public Guid IdConfirmacion { get; set; }
        public Guid? IdRecuperoContraseña { get; set; }
        public bool Recuerdame { get; set; }
        public DateTime? FechaExpConfirmacion { get; set; }
        public List<Rol> Roles { get; set; }
        public List<Comercio> Comercios { get; set; }
        public int IdRolTemporal { get; set; }
    }
}
