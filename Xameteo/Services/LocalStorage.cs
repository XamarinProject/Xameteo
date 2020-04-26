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

        internal static void ResetSelectedCity()
        {
            try
            {
                if(Preferences.ContainsKey("city"))
                {
                    Preferences.Remove("city");
                }
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
                 if(cities.Count == 0)
                 {
                     city.IsFavorite = true;
                 } 
                 else
                 {
                     city.IsFavorite = false;
                 }
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

        internal static void UpdateFavoriteCity(City city)
        {
            if (city == null) return;

            try
            {
                Preferences.Set("favoriteCity", JsonConvert.SerializeObject(city));
                ObservableCollection<City> cities = GetCities();
                foreach(City item in cities)
                {
                    if (item.WikiDataId.Equals(city.WikiDataId))
                    {
                        item.IsFavorite = true;
                    } else
                    {
                        item.IsFavorite = false;
                    }
                }
                Preferences.Set("cities", JsonConvert.SerializeObject(cities));
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
                });
            }
        }

        internal static City GetFavoriteCity()
        {
            try
            {
                var city = JsonConvert.DeserializeObject<City>(Preferences.Get("favoriteCity", String.Empty));
                // Default value
                if(city == null || city.Name == null)
                {
                    city = new City();
                    city.Name = "Paris";
                }
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

        internal static void ResetFavoriteCity()
        {
            try
            {
                if (Preferences.ContainsKey("favoriteCity"))
                {
                    Preferences.Remove("favoriteCity");
                }
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
                if(city.IsFavorite)
                {
                    if(cities.Count > 0)
                    {
                        cities[0].IsFavorite = true;
                        UpdateFavoriteCity(cities[0]);
                    } else
                    {
                        ResetFavoriteCity();
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
