namespace CinemaMobileClient;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //MainPage = new NavigationPage( new AppShell());
        MainPage = new NavigationPage(new Views.MenuPage());
    }
}
