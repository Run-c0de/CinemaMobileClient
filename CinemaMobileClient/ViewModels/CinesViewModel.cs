using System.ComponentModel;
using CinemaMobileClient.Models;


namespace CinemaMobileClient.ViewModels
{
    public class CinesViewModel : INotifyPropertyChanged
    {
        private IEnumerable<sitios> _cines;

        public IEnumerable<sitios> sitios
        {
            get => _cines;
            set
            {
                _cines = value;
                OnPropertyChanged(nameof(sitios));
            }
        }

        public CinesViewModel(IEnumerable<sitios> cines)
        {
            sitios = cines;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}