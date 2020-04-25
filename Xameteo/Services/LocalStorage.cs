using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xameteo
{ 
    public class LocalStorage
    {
        public static City GetLastSelectedCity()
        {
            try
            {
                var city = JsonConvert.DeserializeObject<City>(Preferences.Get("city", String.Empty));
                return city;
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
                });
                return null;
            }
        }

        public static void SaveLastSelectedCity(City city)
        {
            if (city == null) return;

            try
            {
                Preferences.Set("city", JsonConvert.SerializeObject(city));
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
                });
            }
        }

        public static ObservableCollection<City> GetCities()
        {
            try
            {
                var cities = JsonConvert.DeserializeObject<ObservableCollection<City>>(Preferences.Get("cities", String.Empty));
                return cities == null ? new ObservableCollection<City>() : cities;
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
                });
                return new ObservableCollection<City>();
            }
        }

        public static void SaveCity(City city)
        {
            if (city == null) return;

            try
            {
                ObservableCollection<City> cities = GetCities();
                cities.Add(city);
                Preferences.Set("cities", JsonConvert.SerializeObject(cities));

                DependencyService.Get<IToastAlert>().DisplayAlert("Ville sauvegardée");
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
                });
            }
        }

        public static void RemoveCity(City city)
        {
            if (city == null) return;

            try
            {
                ObservableCollection<City> cities = GetCities();
                foreach (City c in cities)
                {
                    if (c.WikiDataId.Equals(city.WikiDataId))
                    {
                        cities.Remove(c);
                        break;
                    }
                }
                Preferences.Set("cities", JsonConvert.SerializeObject(cities));
                DependencyService.Get<IToastAlert>().DisplayAlert("Ville suprimée");
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
                });
            }
        }
    }
}
