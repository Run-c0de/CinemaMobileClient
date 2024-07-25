namespace CinemaMobileClient.Views;
using CinemaMobileClient.Models;

public partial class DetallePage : ContentPage
{
	public DetallePage(IReadOnlyList<object> datos,String TipoDePelicula)
	{
		InitializeComponent();
        // Realiza una conversión explícita a IList<object>
        IList<object> itemList = datos.ToList();
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

    private void MasAdulto(object sender, EventArgs e)
    {
        int num = int.Parse(lblAdulto.Text);
       // DisplayAlert("Hola", (num++).ToString(), "Aceptar");
        lblAdulto.Text = (++num).ToString();
    }

    private void MenosAdulto(object sender, EventArgs e)
    {
        int num = int.Parse(lblAdulto.Text);
        // DisplayAlert("Hola", (num++).ToString(), "Aceptar");
        if (num !=0 )
        {
            lblAdulto.Text = (--num).ToString();
        }
    }

    private async void IrAsientos(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AsientosPages());
    }
}