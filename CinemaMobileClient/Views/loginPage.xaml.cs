using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using CinemaMobileClient.Servicios;
using System.Net.Http.Json;
using System.Text.Json;

namespace CinemaMobileClient.Views;

public partial class loginPage : ContentPage
{
    private readonly ILoginServices _loginService;
    private bool isPasswordVisible = false;
    public loginPage(ILoginServices loginService)
    {
        _loginService = loginService;
        InitializeComponent();

        //Quita la barra de navegación
        NavigationPage.SetHasNavigationBar(this, false);

    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        var loginService = Servicios.ServiceProvider.GetService<ILoginServices>();
        await Navigation.PushModalAsync(new restablecer_contrasenaPage(loginService)); // Replace ForgotPasswordPage with your actual page name
    }

    private async void btnCrearU_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new pantalla_registro());
    }

    private void TogglePasswordVisibility(object sender, EventArgs e)
    {
        isPasswordVisible = !isPasswordVisible;

        // Cambia la visibilidad del texto de la contrase a
        txtContra.IsPassword = !isPasswordVisible;

        // Cambia el icono del bot n para reflejar la visibilidad actual de la contrase a
        if (isPasswordVisible)
        {
            TogglePasswordVisibilityIcon.Source = "clave.png";
        }
        else
        {
            TogglePasswordVisibilityIcon.Source = "noclave.png";
        }
    }

    private async void btnInicio_Clicked(object sender, EventArgs e)
    {
        var username = txtUsuario.Text?.Trim();
        var password = txtContra.Text?.Trim();

        if (string.IsNullOrEmpty(username))
        {
            await DisplayAlert("Error", "Correo es obligatorio", "OK");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Contraseña es obligatoria", "OK");
            return;
        }

        await LoginUsuario(username, password);
    }

    private async Task LoginUsuario(string username, string password)
    {
        var response = await _loginService.Login(username, password);

        if (response.status == 200)
        {
            var loginService = Servicios.ServiceProvider.GetService<ILoginServices>();
            await DisplayAlert("Inicio de sesión exitoso", $"¡Bienvenido, {response.username}!", "OK");
            SaveSession(response.userId, response.username);
            if (!String.IsNullOrEmpty(response.codVerificacion))
            {
                await Navigation.PushAsync(new verificar_contrasenaPage(response, loginService));
            }
            else
            {
                await Navigation.PushAsync(new MenuPage());
            }

        }
        else
        {
            await DisplayAlert("Error de inicio de sesión", response.message, "OK");
        }
    }

    private void SaveSession(int userId, string username)
    {
        // Implementa el guardado de sesión aquí
    }

    private void SaveVerificationCode(string codVerificacion, int userId, string username, string password)
    {
        // Implementa el guardado del código de verificación aquí
    }


}