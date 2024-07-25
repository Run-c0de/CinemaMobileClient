using CinemaMobileClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CinemaMobileClient.Servicios
{
    public class PeliculasService : IPeliculasService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        public List<Peliculas.Example> Items { get; private set; }

        public PeliculasService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            Items = new List<Peliculas.Example>();
        }

        //public async Task<List<Peliculas.Example>> ObtenerPeliculas()
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await _client.GetAsync(Endpoints.Endpoints.GetPeliculas);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string content = await response.Content.ReadAsStringAsync();
        //            Items = JsonSerializer.Deserialize<List<Peliculas.Example>>(content, _serializerOptions);
        //        }
        //        else
        //        {
        //            Debug.WriteLine($"Error: {response.ReasonPhrase}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"\tERROR {ex.Message}");
        //    }

        //    return Items;
        //}

        public async Task<Peliculas.Example> ObtenerPeliculas()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Endpoints.Endpoints.GetPeliculas);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var example = JsonSerializer.Deserialize<Peliculas.Example>(content, _serializerOptions);
                    return example;
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

            return null;
        }



    }
}
