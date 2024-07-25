using CinemaMobileClient.Models;

namespace CinemaMobileClient.Servicios
{
    public interface ICinesService
    {
        public Task<List<Cines>> ObtenerCines();
    }
}
