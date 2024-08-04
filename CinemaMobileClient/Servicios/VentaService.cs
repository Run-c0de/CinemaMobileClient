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
        public async Task<VentaViewModel> InsertVenta(VentaViewModel venta)
        {
            try
            {
                var jsonString = (object)venta;
                HttpResponseMessage response = await _client.PostAsJsonAsync(Endpoints.Endpoints.Ventas, jsonString);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    //var loginResponse = JsonSerializer.Deserialize<VentaViewModel>(content, _serializerOptions);

                    //if (loginResponse != null)
                    //{
                    //    result = loginResponse.data;
                    //    Preferences.Set("userId", result.userId.ToString());
                    //    Preferences.Set("username", result.username);
                    //}
                }
                else
                {
                    Debug.WriteLine($"Error: {response.ReasonPhrase}");
                }

                return null;
            }
            catch (Exception ex)
            {
                return new VentaViewModel();
            }
        }
    }
}
