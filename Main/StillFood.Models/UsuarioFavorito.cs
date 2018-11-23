using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class UsuarioFavorito
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdComercio { get; set; }
    }
}
