using CinemaMobileClient.Models;

namespace CinemaMobileClient.Servicios
{
    public interface ITipoProyeccionService
    {
        public Task<List<TipoProyeccion>> ObtenerTipoProyeccion();
    }
}
