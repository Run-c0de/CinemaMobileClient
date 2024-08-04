using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Models;

namespace CinemaMobileClient.Views
{
    public partial class DetalleCompraPage : ContentPage
    {
        private readonly List<SelectedProduct> _selectedProducts;
        private readonly List<selectedEntrada> _selectedEntradas;
        private readonly List<Entradas> _entradasDetalle;
        private readonly decimal _totalPago;
        private bool _dataLoaded = false;
        private readonly IUsuarioServices _usuarioServices;
        public InfoPelicula? DatosPelicula { get; set; }

        public DetalleCompraPage(List<SelectedProduct> selectedProducts, List<selectedEntrada> selectedEntradas, List<Entradas> entradasDetalle, decimal totalPago)
        {
            InitializeComponent();
            _selectedProducts = selectedProducts ?? new List<SelectedProduct>();
            _selectedEntradas = selectedEntradas ?? new List<selectedEntrada>();
            _entradasDetalle = entradasDetalle ?? new List<Entradas>();
            _totalPago = totalPago;

            if (!_dataLoaded)
            {
                DisplaySelectedEntradas();
                DisplayEntradasDetalle();
                DisplaySelectedProducts();
                DisplayTotal();
                _dataLoaded = true;
            }
        }

        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void DisplaySelectedEntradas()
        {
            if (_selectedEntradas.Any())
            {
                var entrada = _selectedEntradas.First();
                PeliculaImage.Source = entrada.imagen;
                PeliculaLabel.Text = $"Película: {entrada.pelicula}";
                DuracionLabel.Text = $"Duración: {entrada.duracion}";
                FormatoLabel.Text = $"Formato: {entrada.formato}";
                FechaLabel.Text = $"Fecha: {entrada.fecha}";
                ClasificacionLabel.Text = $"Clasificación: {entrada.clasificacion}";
                PeliculaSection.IsVisible = true; // Mostrar sección de la película si hay datos
            }
            else
            {
                PeliculaSection.IsVisible = false; // Ocultar sección de la película si no hay datos
            }
        }

        private void DisplayEntradasDetalle()
        {
            if (_entradasDetalle.Any())
            {
                EntradasStackLayout.Children.Clear();

                foreach (var entrada in _entradasDetalle)
                {
                    var grid = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = GridLength.Auto }
                        }
                    };

                    var boletoLabel = new Label
                    {
                        Text = $" {entrada.numeroboleto}",
                        FontSize = 18,
                        TextColor = Colors.Black,
                        VerticalOptions = LayoutOptions.Center
                    };

                    var precioLabel = new Label
                    {
                        Text = $"{entrada.precioboleto:F2} LPS",
                        FontSize = 18,
                        TextColor = Colors.Black,
                        VerticalOptions = LayoutOptions.Center
                    };

                    grid.Children.Add(boletoLabel);
                    Grid.SetColumn(boletoLabel, 0);
                    grid.Children.Add(precioLabel);
                    Grid.SetColumn(precioLabel, 1);

                    EntradasStackLayout.Children.Add(grid);
                }

                EntradasSection.IsVisible = true; // Mostrar sección de entradas si hay datos
            }
            else
            {
                EntradasSection.IsVisible = false; // Ocultar sección de entradas si no hay datos
            }
        }

        private void DisplaySelectedProducts()
        {
            ConfiteriaStackLayout.Children.Clear();

            foreach (var product in _selectedProducts)
            {
                var grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                var productDescriptionLabel = new Label
                {
                    Text = product.Descripcion,
                    FontSize = 18,
                    TextColor = Colors.Black,
                    VerticalOptions = LayoutOptions.Center
                };

                var productTotalLabel = new Label
                {
                    Text = $"{product.Cantidad * product.Precio:F2} LPS",
                    FontSize = 18,
                    TextColor = Colors.Black,
                    VerticalOptions = LayoutOptions.Center
                };

                grid.Children.Add(productDescriptionLabel);
                Grid.SetColumn(productDescriptionLabel, 0);
                grid.Children.Add(productTotalLabel);
                Grid.SetColumn(productTotalLabel, 1);

                ConfiteriaStackLayout.Children.Add(grid);
            }
        }

        private void DisplayTotal()
        {
            decimal subtotalProductos = _selectedProducts.Sum(p => p.Cantidad * p.Precio);
            decimal subtotalEntradas = _entradasDetalle.Sum(e => (decimal)e.precioboleto);
            decimal subtotal = subtotalProductos + subtotalEntradas;
            decimal isv = subtotal * 0.15m;
            decimal total = subtotal + isv;

            SubtotalLabel.Text = $"L {subtotal:F2}";
            IsvLabel.Text = $"L {isv:F2}";
            TotalLabel.Text = $"L {total:F2}";
            TotalCompraLabel.Text = $"{total:F2} LPS";
        }
        private async void OnContinuarClicked(object sender, EventArgs e)
        {
            var productosParaPago = _selectedProducts.Select(p => new ProductoParaPago
            {
                ProductoId = p.ProductoId,
                Precio = p.Precio,
                Cantidad = p.Cantidad,
                Total = p.Cantidad * p.Precio
            }).ToList();

            decimal total = _selectedProducts.Sum(p => p.Cantidad * p.Precio);

            var userId = Convert.ToInt32(Preferences.Get("userId", ""));
            var usuarioServices = Servicios.ServiceProvider.GetService<IUsuarioServices>();
            var usuario = await usuarioServices.GetUsuarioById(userId);

            var detalleCompraPage = new PaymentView(usuario, _selectedProducts, DatosPelicula);
            Navigation.PushModalAsync(detalleCompraPage);

            //await Navigation.PushAsync(new PagoPage(productosParaPago, total));
        }
        private async void Button_OnClicked(object? sender, EventArgs e)
        {

            //var userId = Convert.ToInt32(Preferences.Get("userId", ""));
            //var usuarioServices = Servicios.ServiceProvider.GetService<IUsuarioServices>();
            //var usuario = await usuarioServices.GetUsuarioById(userId);

            //var detalleCompraPage = new PaymentView(usuario, _selectedProducts, DatosPelicula);
            //Navigation.PushModalAsync(detalleCompraPage);
        }
    }

    public class SelectedProducts
    {
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
    }

    public class ProductoParaPago
    {
        public int ProductoId { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
    // Modelos de datos
    //public class SelectedProducts
    //{
    //    public string Descripcion { get; set; }
    //    public int Cantidad { get; set; }
    //    public decimal Precio { get; set; }
    //}

    public class selectedEntradas
    {
        public string imagen { get; set; }
        public string pelicula { get; set; }
        public string duracion { get; set; }
        public string formato { get; set; }
        public string fecha { get; set; }
        public string clasificacion { get; set; }
    }

    public class Entrada
    {
        public int numeroboleto { get; set; }
        public decimal precioboleto { get; set; }
    }
}