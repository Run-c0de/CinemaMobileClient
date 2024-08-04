using CinemaMobileClient.Servicios;
using CinemaMobileClient.Interfaces;
namespace CinemaMobileClient.Views;

public partial class PerfilPage : ContentPage
{
	public PerfilPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void btnRegistro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RegistroPage());
    }

    private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
    {
        var loginService = Servicios.ServiceProvider.GetService<ILoginServices>();
        //await Navigation.PushModalAsync(new loginPage(loginService));
        //MainPage = new NavigationPage(new loginPage(loginService));
        await Navigation.PushAsync(new loginPage(loginService));

    }
}