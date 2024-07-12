﻿namespace CinemaMobileClient.Views;

public partial class Nueva_contrasenaPage : ContentPage
{
    private bool isPasswordVisibleT = false;
    private bool isPasswordVisibleNC = false;
    private bool isPasswordVisibleVC = false;
    public Nueva_contrasenaPage()
	{
		InitializeComponent();
	}
    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    private void TogglePasswordVisibilityIconT_Clicked(object sender, EventArgs e)
    {
        isPasswordVisibleT = !isPasswordVisibleT;

        // Cambia la visibilidad del texto de la contrase�a
        PasswordEntryT.IsPassword = !isPasswordVisibleT;

        // Cambia el icono del bot�n para reflejar la visibilidad actual de la contrase�a
        if (isPasswordVisibleT)
        {
            TogglePasswordVisibilityIconT.Source = "clave.png";
        }
        else
        {
            TogglePasswordVisibilityIconT.Source = "noclave.png";
        }
    }

    private void TogglePasswordVisibilityIconNC_Clicked(object sender, EventArgs e)
    {
        isPasswordVisibleNC = !isPasswordVisibleNC;

        // Cambia la visibilidad del texto de la contrase�a
        PasswordEntryNC.IsPassword = !isPasswordVisibleNC;

        // Cambia el icono del bot�n para reflejar la visibilidad actual de la contrase�a
        if (isPasswordVisibleNC)
        {
            TogglePasswordVisibilityIconNC.Source = "clave.png";
        }
        else
        {
            TogglePasswordVisibilityIconNC.Source = "noclave.png";
        }
    }

    private void TogglePasswordVisibilityIconVC_Clicked(object sender, EventArgs e)
    {
        isPasswordVisibleVC = !isPasswordVisibleVC;

        // Cambia la visibilidad del texto de la contrase�a
        PasswordEntryVC.IsPassword = !isPasswordVisibleVC;

        // Cambia el icono del bot�n para reflejar la visibilidad actual de la contrase�a
        if (isPasswordVisibleVC)
        {
            TogglePasswordVisibilityIconVC.Source = "clave.png";
        }
        else
        {
            TogglePasswordVisibilityIconVC.Source = "noclave.png";
        }
    }

    private async void btnRestablecer_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new pantalla_inicio());
    }
}