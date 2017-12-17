using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class NotaPedidoDetalle
    {
        public int Id { get; set; }
        public int IdNotaPedido { get; set; }
        public int IdProducto { get; set; }
        public string Comentario { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
