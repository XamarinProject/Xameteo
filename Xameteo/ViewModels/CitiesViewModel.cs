using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
        public ICommand AddCityCommand => new Command(AddCity);

        public CitiesViewModel()
        {
            Init();
        }

        public void Init()
        {
            Cities = App.SavedCities;
        }

        async void AddCity()
        {
            await Shell.Current.Navigation.PushModalAsync(new CitySearchModal());
        }
    }
}
