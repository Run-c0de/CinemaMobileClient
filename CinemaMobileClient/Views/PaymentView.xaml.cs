using CinemaMobileClient.Contracts;
using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;
using CinemaMobileClient.Platforms.Android;
using CinemaMobileClient.ViewModels;

namespace CinemaMobileClient.Views;

public partial class PaymentView : ContentPage
{
    private INotificationManagerService notificationManager;
    public string viewName { get; set; }
    private UsuarioInfo _usuario;
    private VentaViewModel _venta;
    public PaymentView(UsuarioInfo usuario, List<SelectedProduct> selectedProducts, InfoPelicula dataPelicula)
    {
        InitializeComponent();
        viewName = "Forma de pago";
        BindingContext = this;
        _usuario = usuario;
        Venta(selectedProducts, dataPelicula);

        notificationManager = Application.Current?.MainPage?.Handler?.MauiContext?.Services.GetService<INotificationManagerService>();


    }

    private void Venta(List<SelectedProduct>? _selectedProducts, InfoPelicula? _datosPelicula)
    {
        List<VentaEntrada>? ventaEntradas = new List<VentaEntrada>();
        List<VentaProducto>? ventaProductos = new List<VentaProducto>();

        foreach (var product in _selectedProducts)
        {
            VentaProducto newVentaProducto = new VentaProducto
            {
                ventaDetalleId = 0,
                productoId = product.ProductoId,
                precio = product.Precio,
                cantidad = product.Cantidad,
            };
            ventaProductos.Add(newVentaProducto);
        }

        foreach (var entrada in _datosPelicula.detalleEntradas)
        {
            VentaEntrada newVentaEntrada = new VentaEntrada
            {
                ventaDetalleId = 0,
                numeroBoleto = entrada.numeroBoleto,
                precio = (decimal)entrada.precio,
                cantidad = entrada.cantidad,
            };
            ventaEntradas.Add(newVentaEntrada);
        }

        var userId = Convert.ToInt32(Preferences.Get("userId", ""));

        VentaViewModel ventaViewModel = new VentaViewModel
        {
            ventaId = 0,
            usuarioId = userId,
            horarioId = _datosPelicula.horarioId,
            ventaEntrada = ventaEntradas,
            ventaProducto = ventaProductos
        };
        _venta = ventaViewModel;
        return;
    }

    protected async override void OnAppearing()
    {

        Nombre.Text = _usuario.nombres + " " + _usuario.apellidos;
        Telefono.Text = _usuario.telefono;
        Correo.Text = _usuario.correo;

        PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();

        if (status == PermissionStatus.Granted)
        {
            string peliculaName = "Wolverrine";

            notificationManager.SendNotification($"Las pelicula {peliculaName}esta a punto de empezar",
                "Por favor ingresar y presentar el QR.", DateTime.Now.AddSeconds(10)
                );

            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var eventData = (NotificationEventArgs)eventArgs;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Take required action in the app once the notification has been received.
                    string me = eventData.Message; ;
                    DisplayAlert(me, "Executed", "");
                });
            };
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(Nombre.Text) || String.IsNullOrEmpty(Correo.Text) || String.IsNullOrEmpty(Telefono.Text) || String.IsNullOrEmpty(Dni.Text))
        {
            await DisplayAlert("Advertencia", $"Campos requeridos en Mis Datos", "OK");
            return;
        }

        if (String.IsNullOrEmpty(NumeroTarjeta.Text))
        {
            await DisplayAlert("Advertencia", $"Favor digite el número de su tarjeta", "OK");
            return;
        }

        string numeroTarjeta = NumeroTarjeta.Text.ToString().Replace("-", "").Replace(" ", "");

        if (numeroTarjeta.Length != 16)
        {
            await DisplayAlert("Advertencia", $"El número de tarjeta no es válido", "OK");
            return;
        }


        if (String.IsNullOrEmpty(Expiracion.Text))
        {
            await DisplayAlert("Advertencia", $"Favor digite la expiración de su tarjeta", "OK");
            return;
        }

        if (String.IsNullOrEmpty(Cvv.Text))
        {
            await DisplayAlert("Advertencia", $"Favor digite el Cvv de su tarjeta", "OK");
            return;
        }

        string cvv = Cvv.Text.ToString();
        if (cvv.Length != 3)
        {
            await DisplayAlert("Advertencia", $"El Cvv de su tarjeta no es válido", "OK");
            return;

        }

        var ventaServicio = Servicios.ServiceProvider.GetService<IVenta>();
        var venta = await ventaServicio.InsertVenta(_venta);

        await DisplayAlert("Exito", $"Se ha guardado la compra", "OK");
    }
}