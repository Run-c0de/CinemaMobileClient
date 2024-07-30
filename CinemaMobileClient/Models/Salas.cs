using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class Salas
    {
        public int salaId { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public int cineId { get; set; }
        public int capacidad { get; set; }
    }

    public class AsientosOcupados
    {
        public string Asiento { get; set; } = string.Empty;
    }

    public class AsientosOcupadosResponse
    {
        public List<AsientosOcupados> Data { get; set; }
    }
}
