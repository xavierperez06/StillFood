using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class Pedido
    {
        public List<NotaPedido> NotasPedido { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
    }
}
