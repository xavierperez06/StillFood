using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoAnterior { get; set; }
        public string EstadoPosterior { get; set; }
        public string Tabla { get; set; }
        public Usuario Usuario { get; set; }
    }
}
