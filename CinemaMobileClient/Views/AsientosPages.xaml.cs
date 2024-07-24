namespace CinemaMobileClient.Views;
using Microsoft.Maui.Controls;
public partial class AsientosPages : ContentPage
{
    public AsientosPages()
    {
        InitializeComponent();
        CreateSeatsGrid();

    }

    private void CreateSeatsGrid()
    {
        int rows = 5;
        int columns = 6;

        // Define las filas y columnas del Grid
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
                var seatButton = new ImageButton
                {
                    Source = "asiento.png",
                    WidthRequest = 40,
                    HeightRequest = 40,
                    Margin = 0,
                    AutomationId = $"{(char)('A' + row)}{column + 1}"
                };

                seatButton.Clicked += OnSeatClicked;

                // Añadir el botón al Grid y establecer su fila y columna
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
            // Cambia el estado del asiento
            if (button.Source.ToString().Contains("asiento.png"))
            {
                button.Source = "asiento_seleccionado.png"; // Seleccionado
            }
            else if (button.Source.ToString().Contains("asiento_seleccionado.png"))
            {
                button.Source = "asiento.png"; // Disponible
            }
            // Puedes agregar más lógica aquí para manejar asientos ocupados
        }
    }

}