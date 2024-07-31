using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class Precios
    {
        public int precioId { get; set; }
        public int categoriaId { get; set; }
        public int tipoProyeccionId { get; set; }
        public double monto { get; set; }
        public bool activo { get; set; }
        public object tipoProyeccion { get; set; }
    }

    public class ListPrecios
    {
        public List<Precios> data { get; set; }
    }

    public class infoCompra
    {
        public double totalAsientos { get; set; }
        public double totalPago { get; set; }
    }
}
