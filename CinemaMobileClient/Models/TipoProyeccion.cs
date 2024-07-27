using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class TipoProyeccion
    {
        public int tipoProyeccionId { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
    }

    public class ListTipoProyeccion
    {
        public IList<TipoProyeccion> Data { get; set; }
    }
}
