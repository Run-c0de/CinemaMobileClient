using CinemaMobileClient.ViewModels;
using QRCoder;

namespace CinemaMobileClient.Views;

public partial class ReceiptView : ContentPage
{

    private string getQR(string id) => $"https://ksoriano55.github.io/CinepolisWebApp/#/tickets/{id}";
    
    public ReceiptView(VentaViewModel venta)
    {
        InitializeComponent();
        OnGenerateClicked(venta.ventaId.ToString());
        Comprador.Text = "Comprado";
        TotalCharge.Text =  $"Total Charge: L. {venta.TotalCharge.ToString()}";
    }


    private void OnGenerateClicked(string id)
    {
        
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(getQR(id), QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qRCode.GetGraphic(20);
        QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }
}