using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CinemaMobileClient.Servicios
{
    public class LoginServices : ILoginServices
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public LoginServices()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

        }
        public async Task<LoginData> Login(string username, string password)
        {
            try
            {
                var requestBody = new
                {
                    userName = username,
                    password = password
                };
                var result = new LoginData();
                HttpResponseMessage response = await _client.PostAsJsonAsync(Endpoints.Endpoints.Login, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content, _serializerOptions);

                    if (loginResponse != null)
                    {
                        result = loginResponse.data;
                        Preferences.Set("userId", result.userId.ToString());
                        Preferences.Set("username", result.username);
                    }
                }
                else
                {
                    var responseData = await response.Content.ReadFromJsonAsync<LoginData>();
                    if (responseData != null)
                    {
                        result = responseData;
                    }
                    else
                    {
                        result.status = 500;
                        result.message = "Error en la conexión. Intenta nuevamente.";
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                return new LoginData();
            }
        }
        public async Task<bool> verificarUsuario(int usuarioId)
        {
            HttpResponseMessage response = await _client.PutAsync(Endpoints.Endpoints.VerificarUsuario + "?usuarioId=" + usuarioId, null);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }
        public async Task<ClaveTemporal> enviarClaveTemporal(string usuario)
        {
            try
            {
                ClaveTemporal result = new ClaveTemporal();
                HttpResponseMessage response = await _client.PostAsync(Endpoints.Endpoints.claveTemporal + "?usuario=" + usuario, null);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var loginResponse = JsonSerializer.Deserialize<ClaveTemporalResponse>(content, _serializerOptions);

                    if (loginResponse != null)
                    {
                        result = loginResponse.data;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new ClaveTemporal();
            }

        }
        public async Task<bool> reestablecerPwd(int usuarioId, string pwd)
        {
            HttpResponseMessage response = await _client.PutAsync(Endpoints.Endpoints.reestablecerPwd + "?password=" + pwd + "&userId=" + usuarioId, null);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }
    }
}
