using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class Comercio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Imagen { get; set; }
        public List<Producto> Productos { get; set; }
        public List<FormaPago> FormasPago { get; set; }
        public int IdFormaPagoTemporal { get; set; }
    }
}
