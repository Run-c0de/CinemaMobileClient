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
                var productLabel = new Label
                {
                    Text = $"{product.Descripcion} - Cantidad: {product.Cantidad} - Precio: L {product.Precio:F2} - Total: L {(product.Cantidad * product.Precio):F2}",
                    FontSize = 18,
                    TextColor = Colors.White
                };

                MainStackLayout.Children.Add(productLabel);
            }
        }

        private void DisplayTotal()
        {
            decimal total = _selectedProducts.Sum(p => p.Cantidad * p.Precio);

            var totalLabel = new Label
            {
                Text = $"Total a Pagar: L {total:F2}",
                FontSize = 24,
                TextColor = Colors.Red,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            MainStackLayout.Children.Add(totalLabel);
        }
    }
}
