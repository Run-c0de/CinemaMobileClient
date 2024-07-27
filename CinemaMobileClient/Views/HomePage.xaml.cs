using System.Collections.ObjectModel;
using System.Windows.Input;
using CinemaMobileClient.Models;
using CinemaMobileClient.Servicios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace CinemaMobileClient.Views;

public partial class HomePage : ContentPage
{
    //*************************Linea 13,14,15 Sin uso*********************************************
    public ObservableCollection<ImagenPredeterminada> ImagenPredeterminada { get; set; }
    public ObservableCollection<CarteleraImage> CarteleraImage { get; set; }
    public ICommand ItemSelectedCommand { get; private set; }

    private readonly IPeliculasService _peliculasService;


    public HomePage(IPeliculasService peliculasService)
    {
        InitializeComponent();
        loading.IsVisible = true;

        //Quita la barra de navegación
        NavigationPage.SetHasNavigationBar(this, false);

        _peliculasService = peliculasService;
        ImagenPredeterminada = new ObservableCollection<ImagenPredeterminada>
        {
            new ImagenPredeterminada{ foto="transparente.png"},
        };
       
        collectionEstrenos.ItemsSource= ImagenPredeterminada;
        collectionCartelera.ItemsSource= ImagenPredeterminada;
        collectionPreventa.ItemsSource = ImagenPredeterminada;
        InicializarPeliculas();
        //InicializarImagenes();
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void InicializarPeliculas()
    {
        var peliculas = await _peliculasService.ObtenerPeliculas();
        //BindingContext = peliculas;

        if (peliculas != null)
        {
            var estrenos = peliculas.data
                .Where(p => p.tipoPelicula.tipoPeliculaId == 1)
                .ToList();
            var cartelera = peliculas.data
                .Where(p => p.tipoPelicula.tipoPeliculaId == 2)
                .ToList();
            var preventa = peliculas.data
                .Where(p => p.tipoPelicula.tipoPeliculaId == 3)
                .ToList();

            collectionEstrenos.ItemsSource = estrenos;
            collectionCartelera.ItemsSource = cartelera;
            collectionPreventa.ItemsSource = preventa;
        }
        loading.IsVisible = false;

    }

    private async void OnCollectionView(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            var cinesService = Servicios.ServiceProvider.GetService<ICinesService>();
            var tipoProyeccionService = Servicios.ServiceProvider.GetService<ITipoProyeccionService>();
            var horarioService = Servicios.ServiceProvider.GetService<IHorarioService>();
            await Navigation.PushModalAsync(new ReservacionPage(e.CurrentSelection, cinesService, tipoProyeccionService, horarioService));
            collectionView.SelectionChanged -= OnCollectionView;
            collectionView.SelectedItem = null;
            collectionView.SelectionChanged += OnCollectionView;
        }
    }




   







//**********************************Código sin uso, solo para primeras pruebas******************************

private void InicializarImagenes()
    {
        ImagenPredeterminada = new ObservableCollection<ImagenPredeterminada>
        {
            new ImagenPredeterminada{ foto="planetasimios.png"},
            new ImagenPredeterminada{ foto="garfield.png"},
            new ImagenPredeterminada{ foto="intensamente.png"},
            new ImagenPredeterminada{ foto="badboys.jpg"}
        };

        CarteleraImage = new ObservableCollection<CarteleraImage>
        {
            new CarteleraImage{ foto="cartelerauno.png"},
            new CarteleraImage{ foto="cartelerados.png"},
            new CarteleraImage{ foto="carteleratres.png"}
        };

        collectionEstrenos.ItemsSource = ImagenPredeterminada;
        collectionCartelera.ItemsSource = CarteleraImage;
        collectionPreventa.ItemsSource = ImagenPredeterminada;
    }

    //private async void OnCollectionViewSelectionChangedCartelera(object sender, SelectionChangedEventArgs e)
    //{
    //    if (sender is CollectionView collectionView)
    //    {
    //        // Deshabilitar el evento temporalmente para evitar loops
    //        collectionView.SelectionChanged -= OnCollectionViewSelectionChangedCartelera;

    //        try
    //        {
    //            // Navegación a la nueva página
    //            var cinesService = Servicios.ServiceProvider.GetService<ICinesService>();
    //            await Navigation.PushModalAsync(new ReservacionPage(e.CurrentSelection, "Cartelera", cinesService));

    //            // Deseleccionar el elemento cambiando el SelectionMode
    //            collectionView.SelectedItem = null;
    //        }
    //        catch (Exception ex)
    //        {
    //            await Application.Current.MainPage.DisplayAlert("Error", $"Error al navegar: {ex.Message}", "OK");
    //        }
    //        finally
    //        {
    //            // Volver a habilitar el evento
    //            collectionView.SelectionChanged += OnCollectionViewSelectionChangedCartelera;
    //        }
    //    }
    //    else
    //    {
    //        await Application.Current.MainPage.DisplayAlert("Error", "CollectionView no es válido.", "OK");
    //    }
    //}

    //private async void OnCollectionViewSelectionChangedEstrenos(object sender, SelectionChangedEventArgs e)
    //{
    //    if (sender is CollectionView collectionView)
    //    {
    //        // Deshabilitar el evento temporalmente para evitar loops
    //        collectionView.SelectionChanged -= OnCollectionViewSelectionChangedEstrenos;

    //        try
    //        {
    //            // Navegación a la nueva página
    //            var cinesService = Servicios.ServiceProvider.GetService<ICinesService>();
    //            await Navigation.PushModalAsync(new ReservacionPage(e.CurrentSelection, "Estrenos",cinesService));

    //            // Deseleccionar el elemento cambiando el SelectionMode
    //            collectionView.SelectedItem = null;
    //        }
    //        catch (Exception ex)
    //        {
    //            await Application.Current.MainPage.DisplayAlert("Error", $"Error al navegar: {ex.Message}", "OK");
    //        }
    //        finally
    //        {
    //            // Volver a habilitar el evento
    //            collectionView.SelectionChanged += OnCollectionViewSelectionChangedEstrenos;
    //        }
    //    }
    //    else
    //    {
    //        await Application.Current.MainPage.DisplayAlert("Error", "CollectionView no es válido.", "OK");
    //    }
    //}

}