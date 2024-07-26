using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMobileClient.ViewsModels
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            this.data = new LoginData();
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

}
