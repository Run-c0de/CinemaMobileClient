using CinemaMobileClient.Models;
using System.Diagnostics;
using System.Text.Json;

namespace CinemaMobileClient.Servicios
{
    public class HorarioService : IHorarioService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        public List<Horario> ListHorario { get; private set; }

        public HorarioService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            ListHorario = new List<Horario>();
        }

        public async Task<List<Horario>> ObtenerHorario(int horarioId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Endpoints.Endpoints.GetHorario + "?peliculaId=" + horarioId);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserializar el JSON en un objeto de tipo ListTipoProyeccion
                    var horarioResponce = JsonSerializer.Deserialize<ListHorario>(content, _serializerOptions);

                    // Asignar la lista deserializada a la propiedad ListTipoProyeccion
                    ListHorario = horarioResponce?.Data?.ToList() ?? new List<Horario>();
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

            return ListHorario;
        }
    }
}
