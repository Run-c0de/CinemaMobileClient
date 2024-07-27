using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using CinemaMobileClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
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
                //HttpResponseMessage response = await _client.PostAsJsonAsync(Endpoints.Endpoints.GetAsientosOcupados, requestBody);

                //if (response.IsSuccessStatusCode)
                //{
                    string content = "[\r\n    {\r\n      \"asiento\": \"A1\"\r\n    },\r\n    {\r\n      \"asiento\": \"A3\"\r\n    },\r\n    {\r\n      \"asiento\": \"B1\"\r\n    },\r\n    {\r\n      \"asiento\": \"A5\"\r\n    },\r\n    {\r\n      \"asiento\": \"C1\"\r\n    },\r\n    {\r\n      \"asiento\": \"C5\"\r\n    }\r\n  ]";

                    var result = JsonSerializer.Deserialize<List<AsientosOcupados>>(content, _serializerOptions);

                   
                //}
                return result;
            }
            catch (Exception ex)
            {
                return new List<AsientosOcupados>();
            }
        }
    }
}
