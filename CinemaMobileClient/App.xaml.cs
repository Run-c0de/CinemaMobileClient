using CinemaMobileClient.Servicios;
using CinemaMobileClient.Views;

namespace CinemaMobileClient;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        //var peliculasService = Servicios.ServiceProvider.GetService<IPeliculasService>();
        MainPage = new NavigationPage( new MenuPage());
    }
}
