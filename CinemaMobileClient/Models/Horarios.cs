using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class Horarios
    {
        public int horarioId { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFinal { get; set; }
        public int peliculaId { get; set; }
        public int salaId { get; set; }
        public int tipoProyeccioId { get; set; }
        public bool activo { get; set; }
    }

    public class PeliculaHorario : Horario
    {
        public Peliculas.Datum pelicula { get; set; }
        public int boletosComprados { get; set; }
    }
}
