using CinemaMobileClient.Interfaces;

namespace CinemaMobileClient.Views;

public partial class restablecer_contrasenaPage : ContentPage
{
    private readonly ILoginServices _loginService;
    public restablecer_contrasenaPage(ILoginServices loginService)
    {
        _loginService = loginService;
        InitializeComponent();
    }

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    private async void btnEnviar_Clicked(object sender, EventArgs e)
    {
        var usuario = txtUsuario.Text?.Trim();

        if (string.IsNullOrEmpty(usuario))
        {
            await DisplayAlert("Error", "Usuario es obligatorio", "OK");
            return;
        }

        // Mostrar el ActivityIndicator dentro del botón
        activityIndicator.IsVisible = true;
        activityIndicator.IsRunning = true;
        btnEnviar.IsVisible = false;

        try
        {
            var result = await _loginService.enviarClaveTemporal(usuario);
            if (result != null)
            {
                var loginService = Servicios.ServiceProvider.GetService<ILoginServices>();
                await Navigation.PushModalAsync(new Nueva_contrasenaPage(result, loginService));
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;
            btnEnviar.IsVisible = true;
        }


    }
}