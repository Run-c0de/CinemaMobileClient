namespace CinemaMobileClient.Views;

using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;

public partial class DetallePage : ContentPage
{
    PeliculaHorario pelicula = new PeliculaHorario();
    public DetallePage(IReadOnlyList<Peliculas.Datum> datos)
    {
        InitializeComponent();
        // Realiza una conversión explícita a IList<object>
        IList<Peliculas.Datum> itemList = datos.ToList();
        pelicula.pelicula = datos.First();
        pelicula.horarioId = 1; //Cambiar valor por horario seleccionado

        // Suponiendo que los elementos son de un tipo que tiene una propiedad Image

        //if (TipoDePelicula == "Cartelera")
        //{
        //    if (itemList[0] is CarteleraImage item)
        //    {
        //        imagen.Source = item.foto; // Asigna la imagen al control Image
        //    }
        //}
        //else
        //{
        //    if (itemList[0] is Estrenos item)
        //    {
        //        imagen.Source = item.foto; // Asigna la imagen al control Image
        //    }
        //}
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
        var horarioid = 1;
        var salasService = Servicios.ServiceProvider.GetService<ISalasServices>();
        await Navigation.PushModalAsync(new AsientosPages(salasService, pelicula));
    }
}