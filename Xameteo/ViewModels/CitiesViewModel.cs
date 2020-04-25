﻿using Newtonsoft.Json;
using projectbase.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set 
            {
                SetProperty(ref selectedCity, value);
                if (value != null)
                {
                    App.SelectedCity = value;
                    Shell.Current.Navigation.PushAsync(new CityPage(selectedCity));
                }
            }
        }

        public ICommand DeleteCityCommand { get; set; }

        public ICommand AddCityCommand => new Command(AddCity);
    
        public CitiesViewModel()
        {
            Init();
            DeleteCityCommand = new Command(async (Object city) => await DeleteCity(city));
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

        private async Task DeleteCity(Object sender)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Supprimer", "Etes-vous sûr de vouloir supprimer cette ville ?", "Oui", "Annuler");
                if (answer)
                {
                    LocalStorage.RemoveCity((City)sender);
                    Init();
                    DisplayAlert(((City)sender).Name + " a bien été supprimée de vos favoris");
                }  
               
            });
        }

        private void DisplayAlert(string message)
        {
            DependencyService.Get<IToastAlert>()?.DisplayAlert(message);
        }
    }
}
