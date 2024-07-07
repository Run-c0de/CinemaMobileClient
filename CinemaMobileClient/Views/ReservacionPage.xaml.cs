namespace CinemaMobileClient.Views;
using CinemaMobileClient.Models;

public partial class ReservacionPage : ContentPage
{
	public ReservacionPage(IReadOnlyList<object> currentSelection, String TipoDePelicula)
	{
		InitializeComponent();

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
}
