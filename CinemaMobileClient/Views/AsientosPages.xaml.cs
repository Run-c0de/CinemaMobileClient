namespace CinemaMobileClient.Views;

using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using Microsoft.Maui.Controls;
public partial class AsientosPages : ContentPage
{
    private readonly ISalasServices _salasServices;
    InfoPelicula _peliculaSelect = new InfoPelicula();
    public AsientosPages(ISalasServices salasServices, InfoPelicula peliculaSelect)
    {
        _salasServices = salasServices;
        _peliculaSelect = peliculaSelect;
        InitializeComponent();
        loadData();
        CreateSeatsGrid();

    }

    private async void CreateSeatsGrid()
    {
        int rows = 5;
        int columns = 6;

        var ocupados = await _salasServices.AsientosOcupados(_peliculaSelect.horarioId);

        for (int i = 0; i < rows; i++)
        {
            SeatsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }
        for (int i = 0; i < columns; i++)
        {
            SeatsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        }

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                var seatButton = new ImageButton();
                var asiento = $"{(char)('A' + row)}{column + 1}";
                var existe = ocupados.FirstOrDefault(x => x.Asiento == asiento);
                if (existe != null)
                {
                    seatButton = new ImageButton
                    {
                        Source = "asiento_ocupado.png",
                        WidthRequest = 40,
                        HeightRequest = 40,
                        Margin = 0,
                        AutomationId = asiento
                    };
                }
                else
                {
                    seatButton = new ImageButton
                    {
                        Source = "asiento.png",
                        WidthRequest = 40,
                        HeightRequest = 40,
                        Margin = 0,
                        AutomationId = asiento
                    };
                }



                seatButton.Clicked += OnSeatClicked;

                SeatsGrid.Children.Add(seatButton);
                Grid.SetRow(seatButton, row);
                Grid.SetColumn(seatButton, column);
            }
        }
    }

    private void OnSeatClicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        if (button != null)
        {
            if (button.Source.ToString().Contains("asiento.png"))
            {
                var disponible = _peliculaSelect.detalleEntradas.FirstOrDefault(x => x.numeroBoleto == "");
                if (disponible != null)
                {
                    button.Source = "asiento_seleccionado.png";
                    disponible.numeroBoleto = button.AutomationId;
                }
                else
                {

                    DisplayAlert("Alerta", "Ha comprado un total de " + _peliculaSelect.totalAsientos.ToString() + " boletos, ya ha seleccionado los asientos disponibles.", "OK");
                }
            }
            else if (button.Source.ToString().Contains("asiento_seleccionado.png"))
            {
                var ocupado = _peliculaSelect.detalleEntradas.FirstOrDefault(x => x.numeroBoleto == button.AutomationId);
                if (ocupado != null)
                {
                    ocupado.numeroBoleto = "";
                }
                button.Source = "asiento.png";
            }
        }
    }

    public async void loadData()
    {
        var pelicula = _peliculaSelect.pelicula;
        lbTitulo.Text = pelicula.titulo;
        lbDuracion.Text = "Duracion: " + pelicula.hora + "h " + pelicula.minutos + "min";
        lbFecha.Text = "Lanzamiento: " + pelicula.fechaLanzamiento.ToShortDateString();
        lbGenero.Text = "Genero: " + pelicula.genero.descripcion;
        lbTotal.Text = _peliculaSelect.totalPago + " LPS";
        imgPortada.Source = pelicula.foto;
    }
    private async void redirectConfiteria(object sender, EventArgs e)
    {
        var disponible = _peliculaSelect.detalleEntradas.FirstOrDefault(x => x.numeroBoleto == "");
        if (disponible != null)
        {
            await DisplayAlert("Alerta", "Ha comprado un total de " + _peliculaSelect.totalAsientos.ToString() + " boletos, debe seleccionar todos los asientos para poder continuar.", "OK");
            return;
        }

        await Navigation.PushModalAsync(new ConfiteriaPage());
    }
}