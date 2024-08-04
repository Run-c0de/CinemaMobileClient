using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using CinemaMobileClient.Models;

namespace CinemaMobileClient.Controllers
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private static string apiURL = "https://cinepolisapipm2.azurewebsites.net";

        public ApiService()
        {
            // Inicializa una nueva instancia de HttpClient
            _httpClient = new HttpClient();  
        }

        // Método para obtener la lista de ubicaciones de cines
        public async Task<List<Cines>> GetLocationsAsync()
        {
            try
            {
                string url = $"{apiURL}/api/Cines";
                var response = await _httpClient.GetStringAsync(url);
                var cinesResponse = JsonSerializer.Deserialize<CinesResponse>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (cinesResponse == null || cinesResponse.Data == null)
                {
                    throw new Exception("Deserialización fallida o datos no disponibles.");
                }

                return cinesResponse.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al deserializar JSON: {ex.Message}");
                throw;
            }
        }


        // Clase para deserializar la respuesta de la API de cines
        public class CinesResponse
        {
            public List<Cines> Data { get; set; }
        }

        // Método para enviar datos a un endpoint específico usando una solicitud POST
        public async Task<bool> PostGlobalSuccessAsync(string endpoint, object data)
        {
            try
            {
                string url = $"{apiURL}{endpoint}";
                var jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response body: {responseBody}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return false;
            }
        }

        // Clase para deserializar la respuesta de datos del usuario
        public class ResponseData
        {
            public UserData data { get; set; }
        }

        // Clase para deserializar la respuesta de datos del usuario
        public class UserData
        {
            public int usuarioId { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public string usuario { get; set; }
            public string password { get; set; }
            public string correo { get; set; }
            public string telefono { get; set; }
            public string foto { get; set; }
            public bool verificado { get; set; }
            public int rolId { get; set; }
            public bool activo { get; set; }
            public string imgBase64 { get; set; }
            public object rol { get; set; }
        }

        // Método para actualizar datos en un endpoint específico usando una solicitud PATCH
        public async Task<bool> UpdateDataAsync(string endpoint, int id, object data)
        {
            try
            {
                // Construye la URL del endpoint con el ID del recurso a actualizar
                string url = $"{apiURL}{endpoint}/{id}";
                var jsonData = JsonSerializer.Serialize(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await PatchAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response body: {responseBody}");
                }
                // Retorna true si la solicitud fue exitosa, de lo contrario false
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return false;
            }
        }

        // Método para enviar una solicitud PATCH
        private async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            };

            return await _httpClient.SendAsync(request);
        }

    }
}
