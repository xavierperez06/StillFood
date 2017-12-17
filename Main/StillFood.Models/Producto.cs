using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public int IdComercio { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Stock { get; set; }
        public decimal? Precio { get; set; }
        public decimal? PrecioDescuento { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Imagen { get; set; }
        public int? Cantidad { get; set; }
        public Categoria Categoria { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
}
