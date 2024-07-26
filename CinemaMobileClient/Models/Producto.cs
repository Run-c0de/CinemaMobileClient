using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
        public string Foto { get; set; } // URL de la foto
        public bool Activo { get; set; }
        public string ImgBase64 { get; set; } // Campo adicional
    }

    public class ProductoResponse
    {
        public List<Producto> Data { get; set; } = new List<Producto>();
    }

}


