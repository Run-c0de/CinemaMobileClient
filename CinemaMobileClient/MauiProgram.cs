using CinemaMobileClient.Contracts;
using Microsoft.Extensions.Logging;
using CinemaMobileClient.Servicios;
using CinemaMobileClient.Views;
using CinemaMobileClient.Interfaces;
using CinemaMobileClient.Services;
using CinemaMobileClient.ViewModels;


namespace CinemaMobileClient;

public static class MauiProgram
{
  
    
    public static void Configure(PrismAppBuilder builder)
    {

        builder.RegisterTypes(OnRegisterTypes);

        /*  builder
             // .ConfigureModuleCatalog(OnConfigureModuleCatalog)
              .RegisterTypes(OnRegisterTypes)
              .OnAppStart($"{nameof(NavigationPage)}/{nameof(MainView)}", ex =>
              {
                  System.Diagnostics.Debug.WriteLine($"Error Loading MainView - {ex.Message}");
                  System.Diagnostics.Debugger.Break();
              });

              */

    }
    public static MauiApp CreateMauiApp()
    {
        
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .UsePrism(Configure)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<ICinesService, CinesService>();
		builder.Services.AddTransient<Prueba>(); //Para registrar la página que usa el servicio.
        builder.Services.AddSingleton<IPeliculasService, PeliculasService>();
        builder.Services.AddSingleton<ITipoProyeccionService, TipoProyeccionService>();
        builder.Services.AddSingleton<IHorarioService, HorarioService>();
        builder.Services.AddSingleton<ILoginServices, LoginServices>();
        builder.Services.AddSingleton<ISalasServices, SalasServices>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ReservacionPage>();
        builder.Services.AddTransient<loginPage>();
        builder.Services.AddSingleton<IPreciosService, PreciosService>();
        builder.Services.AddTransient<DetallePage>();
        builder.Services.AddTransient<SalasServices>();
        
#if ANDROID
        builder.Services.AddTransient<INotificationManagerService, CinemaMobileClient.Platforms.Android.NotificationManagerService>();
#elif IOS
            builder.Services.AddTransient<INotificationManagerService, LocalNotificationsDemo.Platforms.iOS.NotificationManagerService>();
#elif MACCATALYST
            builder.Services.AddTransient<INotificationManagerService, LocalNotificationsDemo.Platforms.MacCatalyst.NotificationManagerService>();
#elif WINDOWS
            builder.Services.AddTransient<INotificationManagerService, LocalNotificationsDemo.Platforms.Windows.NotificationManagerService>();          
#endif


#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        Servicios.ServiceProvider.Initialize(app.Services);

        return app;
    }
    
    
    private static void OnRegisterTypes(IContainerRegistry containerRegistry)
    {
        // Services
        containerRegistry.RegisterSingleton<IStoreService, StoreService>();

        // Navigation
        containerRegistry
            .RegisterForNavigation<MenuPage>()
            .RegisterForNavigation<PaymentView, PaymentViewModel>()
            .RegisterForNavigation<ReceiptView, ReceiptViewModel>()
            .RegisterInstance(SemanticScreenReader.Default);
    }
    
    private static void OnConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        // Add custom Module to catalog
        //  moduleCatalog.AddModule<MauiAppModule>();
        //  moduleCatalog.AddModule<MauiTestRegionsModule>();
    }

}
