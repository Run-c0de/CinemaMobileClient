using System.Collections.ObjectModel;
using System.Windows.Input;
using CinemaMobileClient.Models;
using CinemaMobileClient.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaMobileClient.Views;

public partial class HomePage : ContentPage
{

    public ObservableCollection<Estrenos> Estrenos { get; set; }
    public ObservableCollection<CarteleraImage> CarteleraImage { get; set; }
    public ICommand ItemSelectedCommand { get; private set; }
    private readonly IPeliculasService _peliculasService;
    public HomePage(IPeliculasService peliculasService)
    {
        InitializeComponent();
        //InicializarImagenes();
        //Quita la barra de navegación
        NavigationPage.SetHasNavigationBar(this, false);

        _peliculasService = peliculasService;
        //BindingContext = this;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var peliculas = await _peliculasService.ObtenerPeliculas();
        //var cartelera =peliculas.Where(p=>p.tipoPelicula.TipoPeliculaId==2).ToList();
        BindingContext = peliculas;
        //var ejemplo = peliculas as Peliculas.Example;

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
    }
    //private void InicializarImagenes()
    //{
    //    Estrenos = new ObservableCollection<Estrenos>
    //    {
    //        new Estrenos{ Image="planetasimios.png"},
    //        new Estrenos{ Image="garfield.png"},
    //        new Estrenos{ Image="intensamente.png"},
    //        new Estrenos{ Image="badboys.jpg"}
    //    };

    //    CarteleraImage = new ObservableCollection<CarteleraImage>
    //    {
    //        new CarteleraImage{ Image="cartelerauno.png"},
    //        new CarteleraImage{ Image="cartelerados.png"},
    //        new CarteleraImage{ Image="carteleratres.png"}
    //    };
    //}

    private async void OnCollectionViewSelectionChangedCartelera(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            // Deshabilitar el evento temporalmente para evitar loops
            collectionView.SelectionChanged -= OnCollectionViewSelectionChangedCartelera;

            try
            {
                // Navegación a la nueva página
                var cinesService = Servicios.ServiceProvider.GetService<ICinesService>();
                await Navigation.PushModalAsync(new ReservacionPage(e.CurrentSelection, "Cartelera", cinesService));

                // Deseleccionar el elemento cambiando el SelectionMode
                collectionView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al navegar: {ex.Message}", "OK");
            }
            finally
            {
                // Volver a habilitar el evento
                collectionView.SelectionChanged += OnCollectionViewSelectionChangedCartelera;
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "CollectionView no es válido.", "OK");
        }
    }

    private async void OnCollectionViewSelectionChangedEstrenos(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            // Deshabilitar el evento temporalmente para evitar loops
            collectionView.SelectionChanged -= OnCollectionViewSelectionChangedEstrenos;

            try
            {
                // Navegación a la nueva página
                var cinesService = Servicios.ServiceProvider.GetService<ICinesService>();
                await Navigation.PushModalAsync(new ReservacionPage(e.CurrentSelection, "Estrenos",cinesService));

                // Deseleccionar el elemento cambiando el SelectionMode
                collectionView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al navegar: {ex.Message}", "OK");
            }
            finally
            {
                // Volver a habilitar el evento
                collectionView.SelectionChanged += OnCollectionViewSelectionChangedEstrenos;
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "CollectionView no es válido.", "OK");
        }
    }

}