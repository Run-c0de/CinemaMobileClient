using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{

    public class UserResponse
    {
        public UserData Data { get; set; }
    }

    public class UserData
    {
        public int UsuarioId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Foto { get; set; }
        public bool Verificado { get; set; }
        public int RolId { get; set; }
        public bool Activo { get; set; }
        public string ImgBase64 { get; set; }
        public string Rol { get; set; }
    }

}
