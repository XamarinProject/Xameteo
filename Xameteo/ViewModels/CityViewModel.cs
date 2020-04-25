﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xameteo.Models;

namespace Xameteo
{
    public class CityViewModel : BaseViewModel
    {
        public CityViewModel()
        {
            this.LoadWeatherData();
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
                    int actualTime = DateTime.Now.Hour;
                    if ( actualTime > 9 && actualTime < 18) {
                        return "day";
                    } else if (actualTime > 21 && actualTime < 6) {
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

        public string Time
        {
            get { return DateTime.Now.ToShortTimeString(); }
        }

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
                await Shell.Current.DisplayAlert("Aucune ville séléctionnée", "Veuillez séléctionner une ville", "OK");
                await Shell.Current.GoToAsync("//CitiesPage");
                return;
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
                for(int i = 1; i < completeWeather.List.Length; i++)
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
