using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class Horario
    {
        public int horarioId { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFinal { get; set; }
        public int peliculaId { get; set; }
        public int salaId { get; set; }
        public int tipoProyeccioId { get; set; }
        public bool activo { get; set; }
        public object pelicula { get; set; }
        public object sala { get; set; }
        public object tipoProyeccion { get; set; }
    }

    public class ListHorario
    {
        public IList<Horario> Data { get; set; }
    }
}
