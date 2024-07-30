using CinemaMobileClient.Models;

namespace CinemaMobileClient.Servicios
{
    public interface IPreciosService
    {
        public Task<List<Precios>> ObtenerPrecios();
    }
}
