namespace CinemaMobileClient.Views;
using CinemaMobileClient.Models;
using CinemaMobileClient.Servicios;
using System.Globalization;

public partial class DetallePage : ContentPage
{
    private IReadOnlyList<Peliculas.Datum> datosPelicula;
    private IReadOnlyList<Precios> datosPrecios;
    String foto = "", titulo="", hora="", minutos="", formato = "", fecha = "", dia="";
    DateTime horaPelicula;
    Double totalAsientos=0, totalPago=0, precioAdulto, precioNino, precioDiscapacidad, precioTerceraEdad;

    private readonly IPreciosService _precioService;
    public DetallePage(IReadOnlyList<Peliculas.Datum> datos, String formato, String dia, DateTime hora, IPreciosService servicioPrecios)
	{
		InitializeComponent();
        _precioService = servicioPrecios;
        datosPelicula = datos;
        this.formato = formato;
        this.horaPelicula = hora;
        this.dia = dia;
        this.fecha= fecha;
        llenarDetalle();
    }

    public async void llenarDetalle()
    {
        foreach (var pelicula in datosPelicula)
        {
            foto = pelicula.foto;
            titulo= pelicula.titulo;
            hora = pelicula.hora.ToString();
            minutos = pelicula.minutos.ToString();
        }
        imagen.Source = foto;
        lblTitulo.Text = titulo;
        lblDuracion.Text = "Duración: " + hora + " h " + minutos + " min";
        lblFormato.Text = "Formato: " + formato;
        lblFecha.Text = "Fecha: " + dia ;
        lblClasificacion.Text = "Hora: " + horaPelicula.ToString("hh:mm tt");

        var precios = await _precioService.ObtenerPrecios();
        foreach (var datosPrecios in precios)
        {
            //await DisplayAlert("Hola", datosPrecios.precioId.ToString(), "Aceptar");
            if (datosPrecios.precioId == 1)
            {
                lblPrecioAdulto.Text = datosPrecios.monto.ToString("F2") + " LPS";
                precioAdulto =datosPrecios.monto;
            }
            else if (datosPrecios.precioId == 2)
            {
                lblPrecioNino.Text = datosPrecios.monto.ToString("F2") + " LPS";
                precioNino = datosPrecios.monto;
            }
            else if(datosPrecios.precioId == 3)
            {
                lblPrecioTerceraEdad.Text = datosPrecios.monto.ToString("F2") + " LPS";
                precioTerceraEdad = datosPrecios.monto;
            }
            else if(datosPrecios.precioId == 4)
            {
                lblPrecioDiscapacidad.Text = datosPrecios.monto.ToString("F2") + " LPS";
                precioDiscapacidad = datosPrecios.monto;
            }
        }
    }


    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void MasAdulto(object sender, EventArgs e)
    {
        int num = int.Parse(lblAdulto.Text);
        lblAdulto.Text = (++num).ToString();
        CalcularTotalAsientos(1, "Mas");
        CalcularTotalPago(precioAdulto, "Mas");
    }

    private void MenosAdulto(object sender, EventArgs e)
    {
        int num = int.Parse(lblAdulto.Text);
        if (num != 0)
        {
            lblAdulto.Text = (--num).ToString();
            CalcularTotalAsientos(1, "Menos");
            CalcularTotalPago(precioAdulto, "Menos");
        }
    }

    private void MasNino(object sender, EventArgs e)
    {
        int num = int.Parse(lblNino.Text);
        lblNino.Text = (++num).ToString();
        CalcularTotalAsientos(1, "Mas");
        CalcularTotalPago(precioNino, "Mas");
    }

    private void MenosNino(object sender, EventArgs e)
    {
        int num = int.Parse(lblNino.Text);
        if (num != 0)
        {
            lblNino.Text = (--num).ToString();
            CalcularTotalAsientos(1, "Menos");
            CalcularTotalPago(precioNino, "Menos");
        }
    }

    private void MasTerceraEdad(object sender, EventArgs e)
    {
        int num = int.Parse(lblTerceraEdad.Text);
        lblTerceraEdad.Text = (++num).ToString();
        CalcularTotalAsientos(1, "Mas");
        CalcularTotalPago(precioTerceraEdad, "Mas");
    }

    private void MenosTerceraEdad(object sender, EventArgs e)
    {
        int num = int.Parse(lblTerceraEdad.Text);
        if (num != 0)
        {
            lblTerceraEdad.Text = (--num).ToString();
            CalcularTotalAsientos(1, "Menos");
            CalcularTotalPago(precioTerceraEdad, "Menos");
        }
    }

    private void MasDiscapacidad(object sender, EventArgs e)
    {
        int num = int.Parse(lblDiscapacidad.Text);
        lblDiscapacidad.Text = (++num).ToString();
        CalcularTotalAsientos(1, "Mas");
        CalcularTotalPago(precioDiscapacidad, "Mas");

    }

    private void MenosDiscapacidad(object sender, EventArgs e)
    {
        int num = int.Parse(lblDiscapacidad.Text);
        if (num != 0)
        {
            lblDiscapacidad.Text = (--num).ToString();
            CalcularTotalAsientos(1,"Menos");
            CalcularTotalPago(precioDiscapacidad, "Menos");
        }
    }

    public void CalcularTotalAsientos(Double valor, String signo)
    {
        if (signo == "Menos")
        {
            totalAsientos = totalAsientos - valor;
        }
        else
        {
            totalAsientos = totalAsientos + valor;
        }
        //lblTotal.Text = totalAsientos.ToString("F2")+" LPS";
    }

    public void CalcularTotalPago(Double valor, String signo)
    {
        if (signo == "Menos")
        {
            totalPago = totalPago - valor;
        }
        else
        {
            totalPago = totalPago + valor;
        }
        lblTotal.Text = totalPago.ToString("F2") + " LPS";
    }

    private async void IrAsientos(object sender, EventArgs e)
    {
        if(totalAsientos==0 || totalPago == 0)
        {
            await DisplayAlert("Atención", "Por favor, completa todas las selecciones necesarias para continuar.", "Aceptar");
        }
        else
        {
            await Navigation.PushModalAsync(new AsientosPages());
        }
    }
}