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

        // Quita la barra de navegación
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        var loginService = Servicios.ServiceProvider.GetService<ILoginServices>();
        await Navigation.PushModalAsync(new restablecer_contrasenaPage(loginService)); // Reemplaza ForgotPasswordPage con el nombre de tu página real
    }

    private async void btnCrearU_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new pantalla_registro());
    }

    private void TogglePasswordVisibility(object sender, EventArgs e)
    {
        isPasswordVisible = !isPasswordVisible;

        // Cambia la visibilidad del texto de la contraseña
        txtContra.IsPassword = !isPasswordVisible;

        // Cambia el icono del botón para reflejar la visibilidad actual de la contraseña
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

        bool hasError = false;

        // Validación del usuario
        if (string.IsNullOrEmpty(username))
        {
            errorUserMessage.IsVisible = true;
            errorIconUser.IsVisible = true;
            hasError = true;
        }
        else
        {
            errorUserMessage.IsVisible = false;
            errorIconUser.IsVisible = false;
        }

        // Validación de la contraseña
        if (string.IsNullOrEmpty(password))
        {
            errorPasswordMessage.IsVisible = true;
            errorIconPassword.IsVisible = true;
            hasError = true;
        }
        else
        {
            errorPasswordMessage.IsVisible = false;
            errorIconPassword.IsVisible = false;
        }

        if (hasError)
        {
            return;
        }

        // Mostrar el ActivityIndicator dentro del botón
        activityIndicator.IsVisible = true;
        activityIndicator.IsRunning = true;
        btnInicio.IsVisible = false;

        try
        {
            await LoginUsuario(username, password);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
        finally
        {
            // Ocultar el ActivityIndicator y mostrar el botón de nuevo
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
            btnInicio.IsVisible = true;
        }
    }

    private async Task LoginUsuario(string username, string password)
    {
        var response = await _loginService.Login(username, password);

        if (response.status == 200)
        {
            var loginService = Servicios.ServiceProvider.GetService<ILoginServices>();
            await DisplayAlert("Inicio de sesión exitoso", $"¡Bienvenido, {response.username}!", "OK");
            if (!String.IsNullOrEmpty(response.codVerificacion))
            {
                var credential = new Credentials() { username = username, password = password };
                await Navigation.PushAsync(new verificar_contrasenaPage(response, credential, loginService));
            }
            else
            {
                Preferences.Set("userId", response.userId.ToString());
                Preferences.Set("username", response.username);
                await Navigation.PushAsync(new MenuPage());
            }
        }
        else
        {
            await DisplayAlert("Error de inicio de sesión", response.message, "OK");
        }
    }

    private void SaveVerificationCode(string codVerificacion, int userId, string username, string password)
    {
        // Implementa el guardado del código de verificación aquí
    }
}
