using CinemaMobileClient.ViewModels;

namespace CinemaMobileClient.Interfaces
{
    public interface IVenta
    {
        Task<Venta> InsertVenta(VentaViewModel venta);
    }
}
