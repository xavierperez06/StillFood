using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Entities
{
    [Table("NotasPedidos")]
    public class NotaPedido
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        [Required]
        public int IdEstado { get; set; }
        [Required]
        public int IdComercio { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdFormaPago { get; set; }
        [Required]
        public int IdFormaEntrega { get; set; }
        public int? IdDireccion { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        [ForeignKey("IdDireccion")]
        public UsuarioDireccion Direccion { get; set; }
        [ForeignKey("IdFormaEntrega")]
        public FormaEntrega FormaEntrega { get; set; }
        [ForeignKey("IdFormaPago")]
        public FormaPago FormaPago { get; set; }
        [ForeignKey("IdComercio")]
        public Comercio Comercio { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        public virtual ICollection<NotaPedidoDetalle> Detalles { get; set; }
    }
}
