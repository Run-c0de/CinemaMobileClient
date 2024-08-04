using CinemaMobileClient.Models;

namespace CinemaMobileClient.Interfaces
{
    public interface IUsuarioServices
    {
        Task<UsuarioInfo> GetUsuarioById(int id);
    }
}
