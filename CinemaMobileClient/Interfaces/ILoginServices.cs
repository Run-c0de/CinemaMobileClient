using CinemaMobileClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Interfaces
{
    public interface ILoginServices
    {
        Task<LoginData> Login(string username, string password);
        Task<bool> verificarUsuario(int usuarioId);
        Task<ClaveTemporal> enviarClaveTemporal(string usuario);
        Task<bool> reestablecerPwd(int usuarioId, string pwd);
    }
}
