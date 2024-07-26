using CinemaMobileClient.Models;
using CinemaMobileClient.ViewsModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace CinemaMobileClient.Views;

public partial class loginPage : ContentPage
{
    private bool isPasswordVisible = false;
    public loginPage()
    {
        InitializeComponent();
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new restablecer_contrasenaPage()); // Replace ForgotPasswordPage with your actual page name
    }

    private async void btnCrearU_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new pantalla_registro());
    }

    private async void btnRestablecer_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new restablecer_contrasenaPage());
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
        using var client = new HttpClient();
        var requestBody = new
        {
            userName = username,
            password = password
        };
        var response = await client.PostAsJsonAsync("https://cinepolisapipm2.azurewebsites.net/api/Autenticacion/Login", requestBody);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (responseData?.data?.status == 200)
            {
                await DisplayAlert("Inicio de sesión exitoso", $"¡Bienvenido, {responseData.data.username}!", "OK");

                // Guarda la sesión y redirige
                SaveSession(responseData.data.userId, responseData.data.username);
                //await Navigation.PushModalAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error de inicio de sesión", responseData?.data.message, "OK");
            }
        }
        else
        {
            var responseData = await response.Content.ReadFromJsonAsync<LoginData>();
            if (responseData != null)
            {
                await DisplayAlert("Error de inicio de sesión", responseData?.message, "OK");
            }
            else
            {
                await DisplayAlert("Error", "Error en la conexión. Intenta nuevamente.", "OK");
            }

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