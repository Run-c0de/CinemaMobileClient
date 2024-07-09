namespace CinemaMobileClient.Views;

public partial class verificar_contrasenaPage : ContentPage
{
	public verificar_contrasenaPage()
	{
		InitializeComponent();
	}
    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void OnForgotPasswordTapped(object sender, EventArgs e)
    {

    }

    private async void btnVerificar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Nueva_contrasenaPage());
    }
}