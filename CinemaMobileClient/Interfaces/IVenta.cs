using CinemaMobileClient.ViewModels;

namespace CinemaMobileClient.Interfaces
{
    public interface IVenta
    {
        Task<VentaViewModel> InsertVenta(VentaViewModel venta);
    }
}
