using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using System.Diagnostics;
using System.Text.Json;

namespace CinemaMobileClient.Servicios
{
    public class UsuarioService : IUsuarioServices
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public UsuarioService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<UsuarioInfo> GetUsuarioById(int id)
        {
            UsuarioInfo result = new UsuarioInfo();
            try
            {
                var url = Endpoints.Endpoints.GetUsuario + $"{id}";
                HttpResponseMessage response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var cinesResponse = JsonSerializer.Deserialize<Usuario>(content, _serializerOptions);

                    result = cinesResponse.data;

                }
                else
                {
                    Debug.WriteLine($"Error: {response.ReasonPhrase}");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }
            return result;
        }
    }
}
