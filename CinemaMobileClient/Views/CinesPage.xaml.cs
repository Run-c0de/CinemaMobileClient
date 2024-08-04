
using CinemaMobileClient.Models;
using CinemaMobileClient.Controllers;

namespace CinemaMobileClient.Views;
public partial class CinesPage : ContentPage
{
    private ApiService _apiService;
    private Cines cine;
    public CinesPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _apiService = new ApiService();

        LoadData();
    }

    private async void LoadData()
    {
        var locations = await _apiService.GetLocationsAsync();
        listacines.ItemsSource = locations;
    }
}