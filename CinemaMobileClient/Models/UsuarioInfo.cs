namespace CinemaMobileClient.Models
{
    public class Usuario
    {
        public UsuarioInfo data {  get; set; }
    }

    public class UsuarioInfo
    {
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public bool esAdministrador { get; set; }
        public string imgBase64 { get; set; } = string.Empty;
    }

}
