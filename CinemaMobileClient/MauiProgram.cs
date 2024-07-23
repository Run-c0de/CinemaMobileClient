using Microsoft.Extensions.Logging;
using CinemaMobileClient.Servicios;//Para poder usar los servicios en las páginas
	
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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
