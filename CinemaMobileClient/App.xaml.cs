using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Servicios;
using CinemaMobileClient.Views;

namespace CinemaMobileClient;

public partial class App : Application
{
    public App()
    {

        InitializeComponent();
        var loginService = Servicios.ServiceProvider.GetService<ILoginServices>();
        var userId = Preferences.Get("userId", "");
        if (userId != "")
        {
            MainPage = new NavigationPage(new MenuPage());
        }
        else
        {
            //MainPage = new NavigationPage(new loginPage(loginService));
            MainPage = new NavigationPage(new PerfilPage());
        }
    }
}
