using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace CinemaMobileClient.Views
{
    public partial class DetalleCompraPage : ContentPage
    {
        private readonly List<SelectedProduct> _selectedProducts;

        public DetalleCompraPage(List<SelectedProduct> selectedProducts)
        {
            InitializeComponent();
            _selectedProducts = selectedProducts;
            DisplaySelectedProducts();
            DisplayTotal();
        }

        private void DisplaySelectedProducts()
        {
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

                // Agregar a la sección adecuada
                if (product.Categoria == "Entradas")
                {
                    EntradasStackLayout.Children.Add(grid); // Sección de entradas
                }
                else if (product.Categoria == "Confiteria")
                {
                    ConfiteriaStackLayout.Children.Add(grid); // Sección de confitería
                }
            }
        }

        private void DisplayTotal()
        {
            decimal subtotal = _selectedProducts.Sum(p => p.Cantidad * p.Precio);
            decimal isv = subtotal * 0.15m;
            decimal total = subtotal + isv;

            SubtotalLabel.Text = $"L {subtotal:F2}";
            IsvLabel.Text = $"L {isv:F2}";
            TotalLabel.Text = $"L {total:F2}";
            TotalCompraLabel.Text = $"{total:F2} LPS";
        }

        private async void OnContinuarClicked(object sender, EventArgs e)
        {
            // Crear una lista con los datos que quieres enviar a PagoPage
            var productosParaPago = _selectedProducts.Select(p => new ProductoParaPago
            {
                ProductoId = p.ProductoId,
                Precio = p.Precio,
                Cantidad = p.Cantidad,
                Total = p.Cantidad * p.Precio
            }).ToList();

            // Calcular el total
            decimal total = _selectedProducts.Sum(p => p.Cantidad * p.Precio);

            // Navegar a PagoPage y pasar los datos
            await Navigation.PushAsync(new PagoPage(productosParaPago, total));
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
}
