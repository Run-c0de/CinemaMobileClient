namespace CinemaMobileClient.Views;
using CinemaMobileClient.Models;
using System.Collections.ObjectModel;

public partial class ReservacionPage : ContentPage
{
    public ObservableCollection<string> Horario { get; set; }
    public ReservacionPage(IReadOnlyList<object> currentSelection, String TipoDePelicula)
	{
		InitializeComponent();
        // Inicializamos la colección con los nombres de las imágenes
        Horario = new ObservableCollection<string>
            {
                "Dom",
                "Lun",
                "Mar",
                "Mié"
            };
        BindingContext = this;

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

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
