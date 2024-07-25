using CinemaMobileClient.ViewModels;

namespace CinemaMobileClient.Servicios;

public partial class Prueba : ContentPage
{
    private readonly ICinesService _cinesService;

    public Prueba(ICinesService service)
	{
        InitializeComponent();
        _cinesService = service;
        //LoadDataAsync();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var cines = await _cinesService.ObtenerCines();
        BindingContext =cines;
    }
    //private async Task LoadDataAsync()
    //{
    //    var cines = await _cinesService.ObtenerCines();
    //    BindingContext = new CinesViewModel(cines); // Aquí se establece el BindingContext
    //}
}