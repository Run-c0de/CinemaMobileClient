using CinemaMobileClient.Models;

namespace CinemaMobileClient.Servicios
{
    public interface IHorarioService
    {
        public Task<List<Horario>> ObtenerHorario(int horarioId);
    }
}
