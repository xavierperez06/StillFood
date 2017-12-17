using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class NotaPedido
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int IdEstado { get; set; }
        public int IdComercio { get; set; }
        public int IdUsuario { get; set; }
        public int IdFormaPago { get; set; }
        public int IdFormaEntrega { get; set; }
        public int? IdDireccion { get; set; }
        public DateTime FechaAlta { get; set; }
        public List<NotaPedidoDetalle> Detalles { get; set; }
        public Comercio Comercio { get; set; }
        public string NumeroTarjeta { get; set; }
        public string TitularTarjeta { get; set; }
        public int CVV { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
    }
}
