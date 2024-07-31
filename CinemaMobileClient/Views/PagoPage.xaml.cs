using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace CinemaMobileClient.Views
{
    public partial class PagoPage : ContentPage
    {
        public PagoPage(List<ProductoParaPago> productos, decimal total)
        {
            InitializeComponent(); // Este método debe estar definido
            ProductosListView.ItemsSource = productos;
            TotalLabel.Text = $"L {total:F2}";
        }
    }
}
