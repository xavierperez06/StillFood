using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class UsuarioDireccion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Calle { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Numero { get; set; }
        public int? Piso { get; set; }
        public string Departamento { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Telefono { get; set; }
        public string Alias { get; set; }
    }
}
