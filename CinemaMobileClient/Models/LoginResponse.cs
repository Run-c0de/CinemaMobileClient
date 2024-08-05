using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.Models
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            data = new LoginData();
        }
        public LoginData data { get; set; }
    }

    public class LoginData
    {
        public int userId { get; set; }
        public string message { get; set; } = string.Empty;
        public int status { get; set; }
        public string codVerificacion { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string rol { get; set; } = string.Empty;
    }

    public class ClaveTemporalResponse
    {
        public ClaveTemporalResponse()
        {
            data = new ClaveTemporal();
        }
        public ClaveTemporal data { get; set; }
    }

    public class ClaveTemporal
    {

        public int userId { get; set; }
        public string message { get; set; } = string.Empty;
        public string claveTemporal { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
    }

    public class Credentials
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }

}
