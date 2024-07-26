using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Endpoints
{
     public class Endpoints
    {
        //public static string GetCines = "https://retoolapi.dev/pcRXrO/sitios";
        public static string GetCines = "https://cinepolisapipm2.azurewebsites.net/api/Cines";
        public static string GetPeliculas = "https://cinepolisapipm2.azurewebsites.net/api/pelicula";
        public static string Login = "https://cinepolisapipm2.azurewebsites.net/api/Autenticacion/Login";
        public static string VerificarUsuario = "https://cinepolisapipm2.azurewebsites.net/api/Autenticacion/VerificarUsuario";
        public static string claveTemporal = "https://cinepolisapipm2.azurewebsites.net/api/Autenticacion/EnviarClaveTemporal";
        public static string reestablecerPwd = "https://cinepolisapipm2.azurewebsites.net/api/Autenticacion/ReestablecerPassword";
    }
}
