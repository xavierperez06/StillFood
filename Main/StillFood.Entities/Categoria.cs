using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
   public class Categoria
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }
        [StringLength(400)]
        public string Imagen { get; set; }
        public bool Activo { get; set; }

    }
}
