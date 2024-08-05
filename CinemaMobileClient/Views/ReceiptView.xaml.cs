using CinemaMobileClient.ViewModels;
using QRCoder;

namespace CinemaMobileClient.Views;

public partial class ReceiptView : ContentPage
{

    private string getQR(string id) => $"https://ksoriano55.github.io/CinepolisWebApp/#/tickets/{id}";
    
    public ReceiptView(int ventaId)
    {
        InitializeComponent();
        OnGenerateClicked(ventaId.ToString());
        Comprador.Text = "Comprado";
        TotalCharge.Text = $"Total Charge: L. 100";
        // Quita la barra de navegación
        NavigationPage.SetHasNavigationBar(this, false);
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