namespace CinemaMobileClient.ViewModels
{
    public class VentaViewModel
    {
        public int ventaId { get; set; }
        public int usuarioId { get; set; }
        public int horarioId { get; set; }
        public List<VentaEntrada> ventaEntrada { get; set; }
        public List<VentaProducto> ventaProducto { get; set; }


        public decimal TotalCharge { get; set; }
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

    public class Venta
    {
        public int VentaId { get; set; }
        public string Pelicula { get; set; }
        public string Portada { get; set; }
        public string Genero { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int BoletosComprados { get; set; }
        public string HoraInicio { get; set; }
        public string Sala { get; set; }
    }

    public class VentasResponse
    {
        public List<Venta> Data { get; set; }
    }
}