using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xameteo
{
    public class CitiesViewModel : BaseViewModel
    {
        ObservableCollection<City> cities = new ObservableCollection<City>();
        public ObservableCollection<City> Cities
        {
            get { return cities; }
            set { SetProperty(ref cities, value); }
        }

        bool hasCities;
        public bool HasCities
        {
            get { return hasCities; }
            set { SetProperty(ref hasCities, value); }
        }

        City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set 
            {
                SetProperty(ref selectedCity, value);
                if (value != null)
                {
                    LocalStorage.SaveLastSelectedCity(value);
                    Shell.Current.GoToAsync("//CityPage");
                }
            }
        }

        public ICommand DeleteCityCommand => new Command<City>(async city => DeleteCity(city));

        public ICommand AddCityCommand => new Command(AddCity);

        public ICommand SelectFavoriteCityCommand => new Command<City>(async city => UpdateFavoriteCity(city));

        public CitiesViewModel()
        {
            Init();
        }

        public void Init()
        {
            Cities = LocalStorage.GetCities();
            HasCities = !Cities.Any();
        }

        async void AddCity()
        {
            await Shell.Current.Navigation.PushModalAsync(new CitySearchModal());
        }

        public void DeleteCity(City city)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Supprimer", "Etes-vous sûr de vouloir supprimer cette ville ?", "Oui", "Annuler");
                if (answer)
                {
                    LocalStorage.RemoveCity(city);
                    Init();
                }
            });
        }

        private void UpdateFavoriteCity(City city)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                LocalStorage.UpdateFavoriteCity(city);
                Init();
            });
        }

    }
}
