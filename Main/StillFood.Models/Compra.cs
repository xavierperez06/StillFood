using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class Compra
    {
        public int IdUsuario { get; set; }
        public Comercio Comercio { get; set; }
        public int IdDireccion { get; set; }
        public int IdFormaEntrega { get; set; }
        public int IdFormaPago { get; set; }
        public int IdComercio { get; set; }
        public List<Producto> Productos { get; set; }
        public List<UsuarioDireccion> Direcciones { get; set; }
        public List<FormaEntrega> FormasEntregas { get; set; }
    }
}
