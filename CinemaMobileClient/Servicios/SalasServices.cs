using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using System.Text.Json;

namespace CinemaMobileClient.Servicios
{
    public class SalasServices : ISalasServices
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public SalasServices()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

        }
        public async Task<List<AsientosOcupados>> AsientosOcupados(int horarioId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Endpoints.Endpoints.GetAsientosOcupados + "?horarioId=" + horarioId);
                List<AsientosOcupados> result = new List<AsientosOcupados>();
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<AsientosOcupadosResponse>(content, _serializerOptions);
                    if (loginResponse != null)
                    {
                        result = loginResponse.Data;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<AsientosOcupados>();
            }
        }
    }
}
