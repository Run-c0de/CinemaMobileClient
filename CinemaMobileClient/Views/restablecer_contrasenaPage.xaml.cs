namespace CinemaMobileClient.Views;

public partial class restablecer_contrasenaPage : ContentPage
{
	public restablecer_contrasenaPage()
	{
		InitializeComponent();
	}

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    private async void btnEnviar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new verificar_contrasenaPage());
    }
}