namespace CinemaMobileClient.Views;
using CinemaMobileClient.Models;
using System.Collections.ObjectModel;
using CinemaMobileClient.Servicios;


public partial class ReservacionPage : ContentPage
{
    IReadOnlyList<object> datos;
    String TipoDePelicula;
    public ObservableCollection<string> Horario { get; set; }
    private readonly ICinesService _cinesService;
    public ReservacionPage(IReadOnlyList<object> currentSelection, String TipoDePelicula, ICinesService service)
	{
		InitializeComponent();
        datos=currentSelection;
        this.TipoDePelicula = TipoDePelicula;
        _cinesService=service;
        // Inicializamos la colección con los nombres de las imágenes
        Horario = new ObservableCollection<string>
            {
                "Dom",
                "Lun",
                "Mar",
                "Mié"
            };
        //BindingContext = this;

        // Realiza una conversión explícita a IList<object>
        IList<object> itemList = currentSelection.ToList();
        // Suponiendo que los elementos son de un tipo que tiene una propiedad Image

        if (TipoDePelicula == "Cartelera")
        {
            if (itemList[0] is CarteleraImage item)
            {
                imagen.Source = item.Image; // Asigna la imagen al control Image
            }
        }
        else
        {
            if (itemList[0] is Estrenos item)
            {
                imagen.Source = item.Image; // Asigna la imagen al control Image
            }
        }

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var cines = await _cinesService.ObtenerCines();
        // Configurar el ItemsSource del Picker
        cinemaPicker.ItemsSource = cines;
        cinemaPicker.ItemDisplayBinding = new Binding("descripcion");

    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnDetalle(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new DetallePage(datos,TipoDePelicula));
    }
}
