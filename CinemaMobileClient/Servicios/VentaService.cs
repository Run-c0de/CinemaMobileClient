using CinemaMobileClient.Interfaces;
using CinemaMobileClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CinemaMobileClient.Servicios
{
    public class VentaService : IVenta
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        public VentaService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<Venta> InsertVenta(VentaViewModel venta)
        {
            try
            {
                var jsonString = (object)venta;
                HttpResponseMessage response = await _client.PostAsJsonAsync(Endpoints.Endpoints.Ventas, jsonString);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var Response = JsonSerializer.Deserialize<Venta>(content, _serializerOptions);
                    return Response;
                  
                }
                else
                {
                    Debug.WriteLine($"Error: {response.ReasonPhrase}");
                }

                return null;
            }
            catch (Exception ex)
            {
                return new Venta();
            }
        }
    }
}
