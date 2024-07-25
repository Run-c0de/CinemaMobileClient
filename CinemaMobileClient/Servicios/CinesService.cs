using CinemaMobileClient.Models;
using CinemaMobileClient.Endpoints;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CinemaMobileClient.Servicios
{
    public class CinesService : ICinesService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public List<Cines> Items { get; private set; }

        public CinesService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            Items = new List<Cines>();
        }

        public async Task<List<Cines>> ObtenerCines()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Endpoints.Endpoints.GetCines);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON into a CinesResponse object
                    var cinesResponse = JsonSerializer.Deserialize<CinesResponse>(content, _serializerOptions);

                    // Extract the list of Cines from the response
                    Items = cinesResponse?.Data ?? new List<Cines>();
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

            return Items;
        }
    }
}
