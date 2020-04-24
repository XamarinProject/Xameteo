using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
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

        bool showTrash;
        public bool ShowTrash
        {
            get { return showTrash; }
            set { SetProperty(ref showTrash, value); }
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
                    //ShowTrash = true;
                    App.SelectedCity = value;
                    Shell.Current.Navigation.PushAsync(new CityPage(selectedCity));
                }
            }
        }

        public ICommand AddCityCommand => new Command(AddCity);
        public ICommand DeleteCityCommand => new Command(DeleteCity);

        public CitiesViewModel()
        {
            Init();
        }

        public void Init()
        {
            ShowTrash = false;
            Cities = LocalStorage.GetCities();
            HasCities = !Cities.Any();
        }

        async void AddCity()
        {
            await Shell.Current.Navigation.PushModalAsync(new CitySearchModal());
        }
        void DeleteCity()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Supprimer", "Etes-vous sûr de vouloir supprimer cette ville ?", "Oui", "Annuler");
                if (answer)
                    LocalStorage.RemoveCity(SelectedCity);
                Init();
            });
        }
    }
}
