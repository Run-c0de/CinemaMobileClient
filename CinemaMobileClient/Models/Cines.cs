using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class Cines
    {
        public int cineId { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }
        public bool activo { get; set; }
        public string imgBase64 { get; set; }
    }

    // Clase para representar el JSON completo
    public class CinesResponse
    {
        public List<Cines> Data { get; set; }
    }
}
