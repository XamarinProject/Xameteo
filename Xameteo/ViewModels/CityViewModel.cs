using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xameteo.Models;

namespace Xameteo.ViewModels
{
    public class CityViewModel : BaseViewModel
    {
       
        public CityViewModel()
        {
            this.LoadWeatherData();
            this.settings = LocalStorage.GetSettings();
        }

        Settings settings;
        public Settings Settings
        {
            get { return settings; }
            set { SetProperty(ref settings, value); }
        }

        City city;
        public City City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        string windSpeed;
        public string WindSpeed
        {
            get { return windSpeed; }
            set { SetProperty(ref windSpeed, value); }
        }

        string humidity;
        public string Humidity
        {
            get { return humidity; }
            set { SetProperty(ref humidity, value); }
        }

        string pressure;
        public string Pressure
        {
            get { return pressure; }
            set { SetProperty(ref pressure, value); }
        }

        string temperature;
        public string Temperature
        {
            get { return temperature; }
            set { SetProperty(ref temperature, value); }
        }

        string tendency;

        public string Tendency
        {
            get { return tendency; }
            set { SetProperty(ref tendency, value); }
        }

        string dayOrNight;

        public string DayOrNight
        {
            get {
                    int actualTime = DateTime.UtcNow.AddSeconds(timezone).Hour;
                    if ( actualTime > 9 && actualTime < 18) {
                        return "day";
                    } else if (actualTime > 21 || actualTime < 6) {
                        return "night";
                    } else {
                        return "mid";
                    }
                }
            set { SetProperty(ref dayOrNight, value); }
        }

        double wind;

        public double Wind
        {
            get { return wind; }
            set { SetProperty(ref wind, value); }
        }

        List<Prevision> previsions;
        public List<Prevision> Previsions
        {
            get { return previsions; }
            set { SetProperty(ref previsions, value); }
        }

        private int timezone;
        public int Timezone
        {
            get { return timezone; }
            set { timezone = value; }
        }

        public string Time
        {
            get { return DateTime.UtcNow.AddSeconds(timezone).ToShortTimeString(); }
        }

        public string Sunset { get; set; }

        public string Sunrise { get; set; }

        public string MinTemp { get; set; }

        public string MaxTemp { get; set; }

        public ICommand GetCommand => new Command(() => Task.Run(LoadWeatherData));
        public async Task LoadWeatherData()
        {
            if (IsBusy) return;
            IsBusy = true;

            if (Preferences.ContainsKey("city"))
            {
                City = LocalStorage.GetLastSelectedCity();
            }
            else
            {
                City = LocalStorage.GetFavoriteCity();
            }

            CompleteWeather completeWeather = await HttpService.GetWeatherAndForecast(City.Name);
            
            if(completeWeather?.List != null && completeWeather?.List.Length > 0 && completeWeather?.Cod == 200)
            {
                this.Previsions = new List<Prevision>();
                Prevision prev;
                WindSpeed = $"{Math.Round(completeWeather.List[0].Wind.Speed, 1)} km/h";
                Humidity = $"{Math.Round((decimal)completeWeather.List[0].Main.Humidity)}%";
                Pressure = $"{Math.Round((decimal)completeWeather.List[0].Main.Pressure)} hPa";
                Temperature = $"{Math.Round(completeWeather.List[0].Main.Temp - 273.15)}°C";
                Wind = Math.Round(-completeWeather.List[0].Wind.Speed, 1);
                Tendency = this.TendencyAnalyser(completeWeather.List[0].Weather[0].Main.ToString());
                Timezone = (int)completeWeather.City.Timezone;
                Sunrise = DateTime.FromFileTime(completeWeather.City.Sunrise).ToShortTimeString();
                Sunset = DateTime.FromFileTime(completeWeather.City.Sunset).ToShortTimeString();
                MinTemp = $"{Math.Round(completeWeather.List[0].Main.TempMin - 273.15)}°C";
                MaxTemp = $"{Math.Round(completeWeather.List[0].Main.TempMax - 273.15)}°C";

                for (int i = 1; i < completeWeather.List.Length; i++)
                {
                    prev = new Prevision(
                        completeWeather.List[i].DtTxt.ToString().Substring(0, 10),
                        completeWeather.List[i].DtTxt.ToString().Substring(11, 5),
                        this.TendencyAnalyser(completeWeather.List[i].Weather[0].Main.ToString()),
                        $"{Math.Round(completeWeather.List[i].Main.Temp - 273.15)}°C"
                        );
                    Previsions.Add(prev);
                }

            }
            else
            {
                DependencyService.Get<IToastAlert>().DisplayAlert("Récupération de la météo impossible");
            }
            if(Preferences.ContainsKey("city"))
            {
                LocalStorage.ResetSelectedCity();
            }
            
            IsBusy = false;

        }

        private string TendencyAnalyser(string tend)
        {
            if (tend == "Clear")
            {
                return "sunny";
            }
            else if (tend == "Rain")
            {
                return "rainy";
            }
            else if (tend == "Snow")
            {
                return "snowy";
            }
            else if (tend == "Clouds")
            {
                return "cloudy";
            }
            else
            {
                return "none";
            }
        }
    }
}
