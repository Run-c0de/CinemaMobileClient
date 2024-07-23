using CinemaMobileClient.Models;
using CinemaMobileClient.Endpoints;
using System.Text.Json.Nodes;
using System.Text.Json;
namespace CinemaMobileClient.Servicios
{
    public class CinesService : ICinesService
    {
        public async Task<List<Cines>> ObtenerCines()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(Endpoints.Endpoints.GetCines);
            var responseBody= await response.Content.ReadAsStringAsync();

            var cinesDatos= JsonSerializer.Deserialize<List<Cines>>(responseBody.ToString());
            return cinesDatos;
        }
    }
}
