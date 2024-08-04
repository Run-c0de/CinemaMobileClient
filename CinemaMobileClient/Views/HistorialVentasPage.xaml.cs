
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

using CinemaMobileClient.Models;
using CinemaMobileClient.Controllers;

namespace CinemaMobileClient.Views;
public partial class HistorialVentasPage : ContentPage
{
    private string varUserId;
    public HistorialVentasPage()
	{
		InitializeComponent();
        varUserId = Preferences.Get("userId", "");
        NavigationPage.SetHasNavigationBar(this, false);
        LoadVentasAsync(userId: varUserId);
    }

    private async Task LoadVentasAsync(string userId)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync($"http://cinepolisapipm2.azurewebsites.net/api/Ventas/resumen?userId={userId}");
                var ventasResponse = JsonConvert.DeserializeObject<VentasResponse>(response);
                VentasListView.ItemsSource = ventasResponse.Data;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudieron cargar los datos: " + ex.Message, "Aceptar");
        }
    }

    public class Venta
    {
        public int VentaId { get; set; }
        public string Pelicula { get; set; }
        public string Portada { get; set; }
        public string Genero { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int BoletosComprados { get; set; }
        public string HoraInicio { get; set; }
        public string Sala { get; set; }
    }

    public class VentasResponse
    {
        public List<Venta> Data { get; set; }
    }

}