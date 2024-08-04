using CinemaMobileClient.Models;
using System.Diagnostics;
using System.Text.Json;

namespace CinemaMobileClient.Servicios
{
    public class PreciosService : IPreciosService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        public List<Precios> ListPrecios { get; private set; }

        public PreciosService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            ListPrecios = new List<Precios>();
        }

        public async Task<List<Precios>> ObtenerPrecios()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Endpoints.Endpoints.GetPrecios);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Log the JSON content for debugging
                    Debug.WriteLine($"JSON Content: {content}");

                    // Deserialize the JSON into a ListPrecios object
                    var preciosResponse = JsonSerializer.Deserialize<ListPrecios>(content, _serializerOptions);

                    // Extract the list of Precios from the response
                    ListPrecios = preciosResponse?.data ?? new List<Precios>();
                }
                else
                {
                    Debug.WriteLine($"Error: {response.ReasonPhrase}");
                }
            }
            catch (JsonException jsonEx)
            {
                Debug.WriteLine($"\tJSON Error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR {ex.Message}");
            }

            return ListPrecios;
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
