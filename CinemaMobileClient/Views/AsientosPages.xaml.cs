namespace CinemaMobileClient.Views;

using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using Microsoft.Maui.Controls;
public partial class AsientosPages : ContentPage
{
    private readonly ISalasServices _salasServices;
    PeliculaHorario _peliculaSelect = new PeliculaHorario();
    public AsientosPages(ISalasServices salasServices, PeliculaHorario peliculaSelect)
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
                button.Source = "asiento_seleccionado.png";
            }
            else if (button.Source.ToString().Contains("asiento_seleccionado.png"))
            {
                button.Source = "asiento.png";
            }
        }
    }

    public async void loadData()
    {
        var pelicula = _peliculaSelect.pelicula;
        lbTitulo.Text = pelicula.titulo;
        lbDuracion.Text ="Duracion: " + pelicula.hora + "h " + pelicula.minutos + "min";
        lbFecha.Text ="Lanzamiento: " + pelicula.fechaLanzamiento.ToShortDateString();
        lbGenero.Text = "Genero: " + pelicula.genero.descripcion;
        imgPortada.Source = pelicula.foto;
    }
        private async void redirectConfiteria(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ConfiteriaPage());
    }
}