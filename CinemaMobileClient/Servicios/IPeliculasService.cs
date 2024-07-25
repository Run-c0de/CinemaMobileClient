using CinemaMobileClient.Models;

namespace CinemaMobileClient.Servicios
{
    public interface IPeliculasService
    {
        //public Task<List<Peliculas.Example>> ObtenerPeliculas();
        Task<Peliculas.Example> ObtenerPeliculas();
    }
}
