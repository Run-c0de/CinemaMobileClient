using System.Collections.ObjectModel;
using System.Windows.Input;
using CinemaMobileClient.Models;

namespace CinemaMobileClient.Views;

public partial class HomePage : ContentPage
{

    public ObservableCollection<Estrenos> Estrenos { get; set; }
    public ObservableCollection<CarteleraImage> CarteleraImage { get; set; }
    public ICommand ItemSelectedCommand { get; private set; }
    public HomePage()
	{
		InitializeComponent();
        InicializarImagenes();
        NavigationPage.SetHasNavigationBar(this, false);
        //ItemSelectedCommand = new Command<CarteleraImage>(OnItemSelected);
        BindingContext = this;
    }

    private void InicializarImagenes()
    {
        Estrenos = new ObservableCollection<Estrenos>
        {
            new Estrenos{ Image="planetasimios.png"},
            new Estrenos{ Image="garfield.png"},
            new Estrenos{ Image="intensamente.png"},
            new Estrenos{ Image="badboys.jpg"}
        };

        CarteleraImage = new ObservableCollection<CarteleraImage>
        {
            new CarteleraImage{ Image="cartelerauno.png"},
            new CarteleraImage{ Image="cartelerados.png"},
            new CarteleraImage{ Image="carteleratres.png"}
        };
    }

    private async void OnCollectionViewSelectionChangedCartelera(object sender, SelectionChangedEventArgs e)
    {
        await Navigation.PushModalAsync(new ReservacionPage(e.CurrentSelection, "Cartelera"));
        //Application.Current.MainPage = new Reservacion();
        //await Navigation.PushAsync(new Reservacion());
    }

    private async void OnCollectionViewSelectionChangedEstrenos(object sender, SelectionChangedEventArgs e)
    {
        await Navigation.PushModalAsync(new ReservacionPage(e.CurrentSelection, "Estrenos"));
        //Application.Current.MainPage = new Reservacion();
        //await Navigation.PushAsync(new Reservacion());
    }
}