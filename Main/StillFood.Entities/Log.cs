using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    public class Log
    {
        public int Id { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [StringLength(300)]
        public string EstadoAnterior { get; set; }
        [Required]
        [StringLength(300)]
        public string EstadoPosterior { get; set; }
        [Required]
        [StringLength(30)]
        public string Tabla { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
