using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    public class Comercio
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }
        [StringLength(300)]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(200)]
        public string Direccion { get; set; }
        [StringLength(20)]
        public string Telefono { get; set; }
        [Required]
        [StringLength(70)]
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Imagen { get; set; }
        public List<Producto> Productos { get; set; }
        public virtual ICollection<FormaPago> FormasPago { get; set; }
    }
}
