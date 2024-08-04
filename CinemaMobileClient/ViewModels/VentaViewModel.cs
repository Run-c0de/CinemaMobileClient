namespace CinemaMobileClient.ViewModels
{
    public class VentaViewModel
    {
        public int ventaId { get; set; }
        public int usuarioId { get; set; }
        public int horarioId { get; set; }
        public List<VentaEntrada> ventaEntrada { get; set; }
        public List<VentaProducto> ventaProducto { get; set; }
    }

    public class VentaEntrada
    {
        public int ventaDetalleId { get; set; }
        public string numeroBoleto { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public decimal cantidad { get; set; }
    }

    public class VentaProducto
    {
        public int ventaDetalleId { get; set; }
        public int productoId { get; set; }
        public decimal precio { get; set; }
        public decimal cantidad { get; set; }
    }
}