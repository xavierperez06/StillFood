using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    [Table("FormasPago")]
    public class FormaPago
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<Comercio> Comercios { get; set; }
    }
}
