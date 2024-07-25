using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class Peliculas
    {
        public class Genero
        {
            public int generoId { get; set; }
            public string descripcion { get; set; }
            public bool activo { get; set; }
        }

        public class TipoPelicula
        {
            public int tipoPeliculaId { get; set; }
            public string tipoPeliculas { get; set; }
            public bool activo { get; set; }
        }

        public class Datum
        {
            public int peliculaId { get; set; }
            public int generoId { get; set; }
            public int tipoPeliculaId { get; set; }
            public string titulo { get; set; }
            public string sinopsis { get; set; }
            public int hora { get; set; }
            public int minutos { get; set; }
            public DateTime fechaLanzamiento { get; set; }
            public string foto { get; set; }
            public bool activo { get; set; }
            public string imgBase64 { get; set; }
            public Genero genero { get; set; }
            public TipoPelicula tipoPelicula { get; set; }
        }

        public class Example
        {
            public IList<Datum> data { get; set; }
        }

    }
}
