using CinemaMobileClient.Contracts;
using CinemaMobileClient.Platforms.Android;

namespace CinemaMobileClient.Views;

public partial class PaymentView : ContentPage
{
    private INotificationManagerService notificationManager;
    public string viewName { get; set; }
    public PaymentView()
    {
        InitializeComponent();

        viewName = "Forma de pago";
        BindingContext = this;
        
        notificationManager =  Application.Current?.MainPage?.Handler?.MauiContext?.Services.GetService<INotificationManagerService>();
  
           
    }


    protected async override void OnAppearing()
    {
        PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();

        if (status == PermissionStatus.Granted)
        {
            string peliculaName = "Wolverrine";
        
            notificationManager.SendNotification($"Las pelicula {peliculaName}esta a punto de empezar", 
                "Por favor ingresar y presentar el QR.", DateTime.Now.AddSeconds(10)
                );
            
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var eventData = (NotificationEventArgs)eventArgs;

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Take required action in the app once the notification has been received.
                   string me =  eventData.Message;;
                    DisplayAlert(me, "Executed", "");
                });
            };
        }
    }
}