namespace CinemaMobileClient.Views;
using CinemaMobileClient.Models;
using System.Collections.ObjectModel;
using CinemaMobileClient.Servicios;


public partial class ReservacionPage : ContentPage
{
    IReadOnlyList<object> datos;
    private IReadOnlyList<Peliculas.Datum> datoss;
    String TipoDePelicula;
    public ObservableCollection<string> Horario { get; set; }
    private readonly ICinesService _cinesService;
    String titulo = "Null", foto = "Null", descripcion="Null", hora="0", minutos="0";
    DateTime hoy;

    public ReservacionPage(IReadOnlyList<object> currentSelection, ICinesService service)
	{
		InitializeComponent();
        _cinesService = service;
        datoss = currentSelection.Cast<Peliculas.Datum>().ToList();
        hoy= DateTime.Now;
        llenarDetalle();
    }

    public async void llenarDetalle()
    {
        foreach (var pelicula in datoss)
        {
            titulo = pelicula.titulo;
            foto = pelicula.foto;
            descripcion = pelicula.sinopsis;
            hora = pelicula.hora.ToString();
            minutos = pelicula.minutos.ToString();
        }
        lblTitulo.Text=titulo;
        imagen.Source = foto;
        lblDuracion.Text="Duración: "+hora+" h "+minutos+" min";
        lblDescripcion.Text=descripcion;
        string nombreDelDia = hoy.ToString("dddd");
        string mañana = (hoy.AddDays(1)).ToString("dddd");
        string fecha = hoy.ToString("dd/MM/yyyy");
        //await DisplayAlert("Hola", nombreDelDia+mañana+fecha, "Aceptar");

        //Horario = new ObservableCollection<string>
        //    {
        //        hoy.ToString("dddd"),
        //        hoy.AddDays(1).ToString("dddd"),
        //        hoy.AddDays(2).ToString("dddd"),
        //        hoy.AddDays(3).ToString("dddd"),
        //    };

        Horario = new ObservableCollection<string>
            {
                "Dom",
                "Lun",
                "Mar",
                "Mié"
            };
        //collectionViewDia.ItemsSource = Horario;
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
