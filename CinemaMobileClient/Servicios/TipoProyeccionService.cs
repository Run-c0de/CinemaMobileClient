using CinemaMobileClient.Models;
using System.Diagnostics;
using System.Text.Json;

namespace CinemaMobileClient.Servicios
{
    public class TipoProyeccionService : ITipoProyeccionService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        public List<TipoProyeccion> ListTipoProyeccion { get; private set; }

        public TipoProyeccionService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            ListTipoProyeccion = new List<TipoProyeccion>();
        }

        public async Task<List<TipoProyeccion>> ObtenerTipoProyeccion()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Endpoints.Endpoints.GetTipoProyeccion);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserializar el JSON en un objeto de tipo ListTipoProyeccion
                    var tipoProyeccionResponse = JsonSerializer.Deserialize<ListTipoProyeccion>(content, _serializerOptions);

                    // Asignar la lista deserializada a la propiedad ListTipoProyeccion
                    ListTipoProyeccion = tipoProyeccionResponse?.Data?.ToList() ?? new List<TipoProyeccion>();
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

            return ListTipoProyeccion;
        }
    }
}
