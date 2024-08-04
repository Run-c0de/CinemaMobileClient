using CinemaMobileClient.Controllers;
using System.Net.NetworkInformation;
using SkiaSharp;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using CinemaMobileClient.Models;

using System;
using System.Text;
using Newtonsoft.Json;

namespace CinemaMobileClient.Views;

public partial class pantalla_perfil : ContentPage
{
    private static readonly HttpClient httpClient = new HttpClient();
    private ApiService _apiService;
    private string base64Foto;
    private string nameFoto;
    private string username;
    byte[] imageToSave;
    private int idItem;
    private string userId;

    public pantalla_perfil()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _apiService = new ApiService();
        userId = Preferences.Get("userId", "");
        InitializePage();
    }


    private async void InitializePage()
    {
        Console.WriteLine($"userId: " + userId);

        var client = new HttpClient();
        var response = await client.GetAsync("https://cinepolisapipm2.azurewebsites.net/api/Usuarios/" + userId);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var userData = JsonConvert.DeserializeObject<UserResponse>(jsonResponse);

            username = userData.Data.Usuario;
            entryNombres.Text = userData.Data.Nombres;
            entryApellidos.Text = userData.Data.Apellidos;
            entryCorreo.Text = userData.Data.Correo;
            entryTelefono.Text = userData.Data.Telefono;
            //base64Foto = userData.Data.ImgBase64;
            string userimageUrl = userData.Data.Foto;


            if (!string.IsNullOrEmpty(base64Foto))
            {
                var imageBytes = Convert.FromBase64String(base64Foto);
                img.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            else
            {
                string imageUrl = userData.Data.Foto;
                img.Source = ImageSource.FromUri(new Uri(imageUrl));
            }
        }
        else
        {
            await DisplayAlert("Error", "No se pudieron cargar los datos del usuario", "OK");
        }
        // Deshabilitar los campos inicialmente
        SetEntriesEnabled(false);
    }

    private async void btnfoto_Clicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                imageToSave = null;
                string localizacion = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using (Stream sourceStream = await photo.OpenReadAsync())
                {
                    var resizedStream = ResizeImage(sourceStream, 200, 200);
                    base64Foto = ConvertToBase64(resizedStream);

                    using (FileStream imagenLocal = File.OpenWrite(localizacion))
                    {
                        await resizedStream.CopyToAsync(imagenLocal);
                    }

                    resizedStream.Position = 0;

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await resizedStream.CopyToAsync(memoryStream);
                        imageToSave = memoryStream.ToArray();
                    }

                    img.Source = ImageSource.FromStream(() => new MemoryStream(imageToSave));
                }

                nameFoto = photo.FileName;
                entryNombres.Focus();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Se ha generado el siguiente error al agregar la imagen: " + ex.Message, "Aceptar");
        }
    }

    private string ConvertToBase64(Stream stream)
    {
        try
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();

                if (imageBytes.Length == 0)
                {
                    throw new Exception("La imagen est� vac�a.");
                }

                return Convert.ToBase64String(imageBytes);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al convertir la imagen a Base64: {ex.Message}");
            throw;
        }
    }

    private Stream ResizeImage(Stream inputStream, int width, int height)
    {
        using (var original = SKBitmap.Decode(inputStream))
        {
            var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.Medium);

            if (resized == null)
            {
                throw new Exception("Error al redimensionar la imagen.");
            }

            var image = SKImage.FromBitmap(resized);
            var data = image.Encode(SKEncodedImageFormat.Jpeg, 80);

            var resizedStream = new MemoryStream();
            data.SaveTo(resizedStream);
            resizedStream.Seek(0, SeekOrigin.Begin);

            return resizedStream;
        }
    }


    private async Task UpdateUserAsync()
    {
        var usuario = new
        {
            usuarioId = userId,
            usuario = username,
            nombres = entryNombres.Text,
            apellidos = entryApellidos.Text,
            telefono = entryTelefono.Text,
            correo = entryCorreo.Text,
            esAdministrador = false,
            activo= true,
            imgBase64 = base64Foto,
        };

        string json = JsonConvert.SerializeObject(usuario);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await httpClient.PutAsync("https://cinepolisapipm2.azurewebsites.net/api/Usuarios", content);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            UpdateUserResponse jsonResponse = JsonConvert.DeserializeObject<UpdateUserResponse>(responseBody);

            if (jsonResponse.Data)
            {
                await DisplayAlert("Éxito", "Datos actualizados correctamente.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Datos no actualizados.", "OK");
            }
        }
        catch (HttpRequestException e)
        {
            await DisplayAlert("Error", $"No se pudo actualizar el usuario: {e.Message}", "OK");
        }
    }

    public class UpdateUserResponse
    {
        public bool Data { get; set; }
    }



    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {

        ShowLoadingDialog();

        if (string.IsNullOrEmpty(base64Foto))
        {
            await DisplayAlert("Alerta", "Por favor agregar una foto.", "OK");
            HideLoadingDialog();
            return;
        }


        if (string.IsNullOrEmpty(entryNombres.Text))
        {
            await DisplayAlert("Alerta", "Por favor ingrese el nombre", "OK");
            HideLoadingDialog();
            return;
        }

        await UpdateUserAsync();
        SetEntriesEnabled(false);

    }

    private void ShowLoadingDialog()
    {
        btnActualizar.IsEnabled = false;
    }

    private void HideLoadingDialog()
    {
        btnActualizar.IsEnabled = true;
    }

    //private async void btnCerrar_Clicked(object sender, EventArgs e)
    //{
    //    await Navigation.PopModalAsync();
    //}

    private void OnEditSwitchToggled(object sender, ToggledEventArgs e)
    {
        SetEntriesEnabled(e.Value);
    }
    private void SetEntriesEnabled(bool isEnabled)
    {
        entryNombres.IsEnabled = isEnabled;
        entryApellidos.IsEnabled = isEnabled;
        entryCorreo.IsEnabled = isEnabled;
        entryTelefono.IsEnabled = isEnabled;
        img.IsEnabled = isEnabled;
        btnActualizar.IsEnabled = isEnabled;
    }

}