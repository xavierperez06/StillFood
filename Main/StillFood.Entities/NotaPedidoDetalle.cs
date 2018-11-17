using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    [Table("NotasPedidosDetalle")]
    public class NotaPedidoDetalle
    {
        public int Id { get; set; }
        [Required]
        public int IdNotaPedido { get; set; }
        [Required]
        public int IdProducto { get; set; }
        [StringLength(1000)]
        public string Comentario { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [ForeignKey("IdNotaPedido")]
        public NotaPedido NotaPedido { get; set; }
        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }
    }
}
