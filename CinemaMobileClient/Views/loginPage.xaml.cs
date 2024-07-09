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

        // Cambia la visibilidad del texto de la contrase�a
        PasswordEntry.IsPassword = !isPasswordVisible;

        // Cambia el icono del bot�n para reflejar la visibilidad actual de la contrase�a
        if (isPasswordVisible)
        {
            TogglePasswordVisibilityIcon.Source = "clave.png";
        }
        else
        {
            TogglePasswordVisibilityIcon.Source = "noclave.png";
        }
    }
}