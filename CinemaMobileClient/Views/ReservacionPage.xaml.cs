namespace CinemaMobileClient.Views;
using CinemaMobileClient.Models;
using System.Collections.ObjectModel;
using CinemaMobileClient.Servicios;
using System.Globalization;

public partial class ReservacionPage : ContentPage
{
    //IReadOnlyList<object> datos;
    private IReadOnlyList<Peliculas.Datum> datoss;
    String TipoDePelicula;
    String titulo = "Null", foto = "Null", descripcion = "Null", hora = "0", minutos = "0";
    DateTime hoy;
    String dia1, dia2, dia3, dia4;

    private readonly ICinesService _cinesService;
    private readonly ITipoProyeccionService _tipoProyeccionService;
    private readonly IHorarioService _horarioService;


    String SeleccionDia = "";
    String SeleccionCine = "";
    String SeleccionFormato = "";
    String SeleccionHora = "";
    public ReservacionPage(IReadOnlyList<object> currentSelection, ICinesService service, ITipoProyeccionService serviceTipoProyeccion, IHorarioService serviceHorario)
    {
        InitializeComponent();
        _cinesService = service;
        _tipoProyeccionService = serviceTipoProyeccion;
        _horarioService = serviceHorario;
        datoss = currentSelection.Cast<Peliculas.Datum>().ToList();
        hoy = DateTime.Now;
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
        lblTitulo.Text = titulo;
        imagen.Source = foto;
        lblDuracion.Text = "Duración: " + hora + " h " + minutos + " min";
        lblDescripcion.Text = descripcion;

        // Obtener la cultura actual para el formateo
        var cultureInfo = CultureInfo.CurrentCulture;
        var textInfo = cultureInfo.TextInfo;
        dia1 = textInfo.ToTitleCase(hoy.ToString("dddd"));
        dia2 = textInfo.ToTitleCase(hoy.AddDays(1).ToString("dddd"));
        dia3 = textInfo.ToTitleCase(hoy.AddDays(2).ToString("dddd"));
        dia4 = textInfo.ToTitleCase(hoy.AddDays(3).ToString("dddd"));

        btnDia1.Text = dia1;
        btnDia2.Text = dia2;
        btnDia3.Text = dia3;
        btnDia4.Text = dia4;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var cines = await _cinesService.ObtenerCines();
        // Configurar el ItemsSource del Picker
        cinemaPickerCines.ItemsSource = cines;
        cinemaPickerCines.ItemDisplayBinding = new Binding("descripcion");

        var tipoProyeccion = await _tipoProyeccionService.ObtenerTipoProyeccion();
        cinemaPickerTipoProyeccion.ItemsSource = tipoProyeccion;
        cinemaPickerTipoProyeccion.ItemDisplayBinding = new Binding("descripcion");

        var horario = await _horarioService.ObtenerHorario();
        cinemaPickerHorario.ItemsSource = horario;
        cinemaPickerHorario.ItemDisplayBinding = new Binding("horaInicio", stringFormat: "{0:hh:mm tt}");
    }



    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }



    private async void OnDetalle(object sender, EventArgs e)
    {
        try
        {
            if (cinemaPickerCines.SelectedItem is Cines seleccionado)
            {
                string descripcionSeleccionada = seleccionado.descripcion;
                SeleccionCine = descripcionSeleccionada;
            }

            if (cinemaPickerTipoProyeccion.SelectedItem is TipoProyeccion seleccionado2)
            {
                string descripcionSeleccionada = seleccionado2.descripcion;
                SeleccionFormato = descripcionSeleccionada;
            }

            if (cinemaPickerHorario.SelectedItem is Horario seleccionado3)
            {
                string descripcionSeleccionada = seleccionado3.horaInicio.ToString();
                SeleccionHora = descripcionSeleccionada;
            }
            
            if (string.IsNullOrEmpty(SeleccionDia) || string.IsNullOrEmpty(SeleccionCine) || string.IsNullOrEmpty(SeleccionFormato) || string.IsNullOrEmpty(SeleccionHora))
            {
                await DisplayAlert("Atención", "Por favor, completa todas las selecciones necesarias para continuar.", "Aceptar");
            }
            else
            {
                await Navigation.PushModalAsync(new DetallePage(datoss));
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Mensaje", ex.Message, "Aceptar");
        }

    }

    private void btndia(object sender, EventArgs e)
    {
        var clickedButton = sender as Button;

        DeseleccionarButones();

        if (clickedButton != null)
        {
            clickedButton.BackgroundColor = Color.FromHex("#912D93");
            clickedButton.TextColor = Colors.White;
            String diaElegido = clickedButton.Text;
            if (diaElegido.Equals(dia1))
            {
                SeleccionDia = hoy.ToString("dd/MM/yyyy");
            }
            else if (diaElegido.Equals(dia2))
            {
                SeleccionDia = hoy.AddDays(1).ToString("dd/MM/yyyy");
            }
            else if (diaElegido.Equals(dia3))
            {
                SeleccionDia = hoy.AddDays(2).ToString("dd/MM/yyyy");
            }
            else if (diaElegido.Equals(dia4))
            {
                SeleccionDia = hoy.AddDays(3).ToString("dd/MM/yyyy");
            }
        }
    }

    private void DeseleccionarButones()
    {
        btnDia1.BackgroundColor = Colors.Transparent;
        btnDia2.BackgroundColor = Colors.Transparent;
        btnDia3.BackgroundColor = Colors.Transparent;
        btnDia4.BackgroundColor = Colors.Transparent;

        btnDia1.TextColor = Colors.Black;
        btnDia2.TextColor = Colors.Black;
        btnDia3.TextColor = Colors.Black;
        btnDia4.TextColor = Colors.Black;
    }
}

