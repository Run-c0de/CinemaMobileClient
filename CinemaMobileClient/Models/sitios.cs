using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public  class sitios
    {
        public int id { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public object audioFile { get; set; }
        public string descripcion { get; set; }
        public object firmaDigital { get; set; }
    }
}
