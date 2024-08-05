using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;


namespace CinemaMobileClient.Views;

public partial class verificar_contrasenaPage : ContentPage
{
    private readonly ILoginServices _loginService;
    LoginData _loginData;
    private Credentials _credentials;
    public verificar_contrasenaPage(LoginData data, Credentials credentials, ILoginServices loginService)
    {
        _loginService = loginService;
        _loginData = data;
        _credentials = credentials;
        InitializeComponent();

        //Quita la barra de navegación
        NavigationPage.SetHasNavigationBar(this, false);
    }
    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        var loginService = Servicios.ServiceProvider.GetService<ILoginServices>();
        await Navigation.PushAsync(new loginPage(loginService));
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        var response = await _loginService.Login(_credentials.username, _credentials.password);
        if (response != null)
        {
            _loginData = response;
            await DisplayAlert("Exito!", "¡Codigo Reenviado Exitosamente!", "OK");
        }
    }

    private async void btnVerificar_Clicked(object sender, EventArgs e)
    {
        var codigo = txtCodVerificacion.Text?.Trim();
        if (codigo == _loginData.codVerificacion)
        {
            var result = await _loginService.verificarUsuario(_loginData.userId);
            if (result)
            {
                await DisplayAlert("Exito", "Usuario verificado con exito", "OK");
                Preferences.Set("userId", _loginData.userId.ToString());
                Preferences.Set("username", _loginData.username);
                await Navigation.PushAsync(new MenuPage());
            }
        }
        else
        {
            await DisplayAlert("Error", "¡Codigo Incorrecto!", "OK");
            return;
        }
    }
}