using CinemaMobileClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Interfaces
{
    public interface ISalasServices
    {
        Task<List<AsientosOcupados>>AsientosOcupados(int horarioId);
    }
}
