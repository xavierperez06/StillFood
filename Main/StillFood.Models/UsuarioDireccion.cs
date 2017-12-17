using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class UsuarioDireccion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public int? Piso { get; set; }
        public string Departamento { get; set; }
        public string Telefono { get; set; }
        public string Alias { get; set; }
    }
}
