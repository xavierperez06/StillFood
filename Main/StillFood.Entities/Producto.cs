using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    [Table("Productos")]
    public class Producto
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public int IdComercio { get; set; }
        [Required]
        [StringLength(50)]
        public string Marca { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(500)]
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioDescuento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        [StringLength(50)]
        public string Imagen { get; set; }
        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }
        [ForeignKey("IdComercio")]
        public Comercio Comercio { get; set; }
    }
}
