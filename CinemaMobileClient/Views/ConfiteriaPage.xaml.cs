using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using CinemaMobileClient.Models;

namespace CinemaMobileClient.Views
{
    public partial class ConfiteriaPage : ContentPage
    {
        private const string ApiUrl = "https://cinepolisapipm2.azurewebsites.net/api/Producto";
        private readonly Dictionary<int, int> _productQuantities = new Dictionary<int, int>();
        private readonly Dictionary<int, Label> _quantityLabels = new Dictionary<int, Label>();
        private readonly Dictionary<int, Producto> _productos = new Dictionary<int, Producto>();
        private Label _totalLabel;

        public ConfiteriaPage()
        {
            InitializeComponent();
            LoadProductos();
            //InitializeTotalLabel();
        }

        private async void LoadProductos()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(ApiUrl);
                var productoResponse = JsonConvert.DeserializeObject<ProductoResponse>(response);
                var productos = productoResponse?.Data ?? new List<Producto>();
                PopulateProductos(productos);
            }
        }

        private void PopulateProductos(List<Producto> productos)
        {
            foreach (var producto in productos)
            {
                if (!producto.Activo) continue;

                _productQuantities[producto.ProductoId] = 0;
                _productos[producto.ProductoId] = producto;

                var frame = new Frame
                {
                    BackgroundColor = Colors.White,
                    CornerRadius = 10,
                    Padding = 10,
                    Margin = new Thickness(10, 0, 10, 10)
                };

                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Margin = new Thickness(10, 0)
                };

                var innerStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(10, 0)
                };

                var image = new Image
                {
                    Source = ImageSource.FromUri(new Uri(producto.Foto)),
                    WidthRequest = 80,
                    HeightRequest = 80,
                    BackgroundColor = Colors.White
                };

                var detailsStackLayout = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(10, 0)
                };

                var nameLabel = new Label
                {
                    Text = producto.Descripcion,
                    FontSize = 18,
                    TextColor = Colors.Black
                };

                var priceLabel = new Label
                {
                    Text = $"L {producto.Precio:F2}",
                    FontSize = 16,
                    TextColor = Colors.Gray
                };

                var quantityStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 10, 0)
                };

                var minusButton = new Button
                {
                    Text = "-",
                    WidthRequest = 40,
                    BackgroundColor = Colors.Purple,
                    TextColor = Colors.White
                };
                minusButton.Clicked += (sender, e) => AdjustQuantity(producto.ProductoId, -1);

                var quantityLabel = new Label
                {
                    Text = "0",
                    WidthRequest = 40,
                    TextColor = Colors.Black,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                var plusButton = new Button
                {
                    Text = "+",
                    WidthRequest = 40,
                    BackgroundColor = Colors.Purple,
                    TextColor = Colors.White
                };
                plusButton.Clicked += (sender, e) => AdjustQuantity(producto.ProductoId, 1);

                quantityStackLayout.Children.Add(minusButton);
                quantityStackLayout.Children.Add(quantityLabel);
                quantityStackLayout.Children.Add(plusButton);

                detailsStackLayout.Children.Add(nameLabel);
                detailsStackLayout.Children.Add(priceLabel);

                innerStackLayout.Children.Add(image);
                innerStackLayout.Children.Add(detailsStackLayout);
                innerStackLayout.Children.Add(quantityStackLayout);

                stackLayout.Children.Add(innerStackLayout);

                var separator = new BoxView
                {
                    HeightRequest = 1,
                    BackgroundColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(0, 10)
                };

                stackLayout.Children.Add(separator);

                frame.Content = stackLayout;

                MainStackLayout.Children.Add(frame);

                _quantityLabels[producto.ProductoId] = quantityLabel;
            }
        }

        private void AdjustQuantity(int productoId, int change)
        {
            if (_productQuantities.ContainsKey(productoId))
            {
                var newQuantity = _productQuantities[productoId] + change;
                _productQuantities[productoId] = Math.Max(0, newQuantity);
                UpdateQuantityLabel(productoId);
                UpdateTotal();
            }
        }

        private void UpdateQuantityLabel(int productoId)
        {
            if (_quantityLabels.TryGetValue(productoId, out var quantityLabel))
            {
                quantityLabel.Text = _productQuantities[productoId].ToString();
            }
        }
        /*
        private void InitializeTotalLabel()
        {
            _totalLabel = new Label
            {
                Text = "0.00 LPS",
                FontSize = 24,
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            MainStackLayout.Children.Add(_totalLabel);
        }*/

        private void UpdateTotal()
        {
            decimal total = 0.0m;

            foreach (var kvp in _productQuantities)
            {
                var productoId = kvp.Key;
                var cantidad = kvp.Value;

                if (_productos.TryGetValue(productoId, out var producto))
                {
                    var precio = producto.Precio;
                    total += cantidad * precio;
                }
            }

            // _totalLabel.Text = $" {total:F2} LPS";
            TotalCompraLabel.Text = $" {total:F2} LPS";
        }

        private void OnContinueButtonClicked(object sender, EventArgs e)
        {
            var selectedProducts = new List<SelectedProduct>();
            var selectedEntradas = new List<selectedEntrada>();

            foreach (var kvp in _productQuantities)
            {
                var productoId = kvp.Key;
                var cantidad = kvp.Value;

                if (cantidad > 0 && _productos.TryGetValue(productoId, out var producto))
                {
                    selectedProducts.Add(new SelectedProduct
                    {
                        ProductoId = producto.ProductoId,
                        Descripcion = producto.Descripcion,
                        Precio = producto.Precio,
                        Cantidad = cantidad,
                        Categoria = "Confiteria"
                    });
                    selectedEntradas.Add(new selectedEntrada
                    {//informacion de la pelicula
                        pelicula = "El planeta de los simios",
                        duracion = "2 h 25 min",
                        formato = "2D Doblada",
                        fecha = "12 Jun 3 Jun",
                        clasificacion = "A",
                        Categoria = "Entradas"
                    });
                }
            }

            var detalleCompraPage = new DetalleCompraPage(selectedProducts);
            Navigation.PushAsync(detalleCompraPage);
        }
    }

    public class SelectedProduct
    {
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Categoria { get; set; }
    }
    public class selectedEntrada
    {
        public string pelicula { get; set; }
        public string duracion { get; set; }
        public string formato { get; set; }
        public string fecha { get; set; }
        public string clasificacion { get; set; }
        public string Categoria { get; set; }

    }
}
