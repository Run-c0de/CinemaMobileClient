using Microsoft.Extensions.Logging;
using CinemaMobileClient.Servicios;
using CinemaMobileClient.Views;
using CinemaMobileClient.Interfaces;//Para poder usar los servicios en las páginas

namespace CinemaMobileClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

		builder.Services.AddSingleton<ICinesService, CinesService>();//Para registrar el servicio (Creando un instancia de nuestro servicio, agregamos la interfaz y la clase del servicio).
		builder.Services.AddTransient<Servicios.Prueba>(); //Para registrar la página que usa el servicio.
        builder.Services.AddSingleton<IPeliculasService, PeliculasService>();
        builder.Services.AddSingleton<ITipoProyeccionService, TipoProyeccionService>();
        builder.Services.AddSingleton<IHorarioService, HorarioService>();
        builder.Services.AddSingleton<ILoginServices, LoginServices>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ReservacionPage>();
        builder.Services.AddTransient<loginPage>();
        builder.Services.AddSingleton<IPreciosService, PreciosService>();
        builder.Services.AddTransient<DetallePage>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Inicializar el ServiceProvider estático
        Servicios.ServiceProvider.Initialize(app.Services);

        return app;
        //return builder.Build();
    }
}
