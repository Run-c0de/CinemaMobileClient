
using Newtonsoft.Json;
using CinemaMobileClient.ViewModels;

namespace CinemaMobileClient.Views;
public partial class HistorialVentasPage : ContentPage
{
    private string varUserId;
    public HistorialVentasPage()
	{
		InitializeComponent();
        varUserId = Preferences.Get("userId", "");
        NavigationPage.SetHasNavigationBar(this, false);
        //LoadVentasAsync(userId: varUserId);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //InitializePage();
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

    private void verQr(object sender, TappedEventArgs e)
    {

    }

    private async void VentasListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item != null)
        {
            var selectedItem = e.Item as Venta; 
            await Navigation.PushModalAsync(new ReceiptView(selectedItem.VentaId));
        }
    }
}