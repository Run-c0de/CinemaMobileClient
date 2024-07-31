using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class VentaDetalleEntradas
    {
        public string numeroBoleto { get; set; } = string.Empty;
        public double precio { get; set; }
        public int cantidad { get; set; }
    }
}
