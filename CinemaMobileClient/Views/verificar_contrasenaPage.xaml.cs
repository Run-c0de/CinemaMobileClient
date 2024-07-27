using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaMobileClient.Views;

public partial class verificar_contrasenaPage : ContentPage
{
    private readonly ILoginServices _loginService;
    LoginData _loginData;
    public verificar_contrasenaPage(LoginData data, ILoginServices loginService)
    {
        _loginService = loginService;
        _loginData = data;
        InitializeComponent();

        //Quita la barra de navegación
        NavigationPage.SetHasNavigationBar(this, false);
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
        var codigo = txtCodVerificacion.Text?.Trim();
        if (codigo == _loginData.codVerificacion)
        {
            var result = await _loginService.verificarUsuario(_loginData.userId);
            if (result)
            {
                await DisplayAlert("Exito", "Usuario verificado con exito", "OK");
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