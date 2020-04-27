using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xameteo.Models;

namespace Xameteo.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            LoadSettings();    
        }

        Settings settings;
        public Settings Settings
        {
            get { return settings; }
            set {
                SetProperty(ref settings, value);
                CityDetails = settings.CityDetails;
                TemperatureDetails = settings.TemperatureDetails;
                SunsetSunriseDetails = settings.SunsetSunriseDetails;
            }
        }

        bool cityDetails;
        public bool CityDetails
        {
            get { return cityDetails; }
            set { SetProperty(ref cityDetails, value); }
        }
        bool sunsetSunriseDetails;
        public bool SunsetSunriseDetails
        {
            get { return sunsetSunriseDetails; }
            set { SetProperty(ref sunsetSunriseDetails, value); }
        }
        bool temperatureDetails;
        public bool TemperatureDetails
        {
            get { return temperatureDetails; }
            set { SetProperty(ref temperatureDetails, value); }
        }

        public void LoadSettings()
        {
            Settings = LocalStorage.GetSettings();
        }


        public ICommand SetSunsetDetailsSettingsCommand => new Command(SetSunsetSunriseDetailsSettings);
        public ICommand SetCityDetailsSettingsCommand => new Command(SetCityDetailsSettings);
        public ICommand SetTemperatureDetailsSettingsCommand => new Command(SetTemperatureDetailsSettings);
        public ICommand ResetAllCommand => new Command(ResetAll);
        public bool GetCityDetailsSettings()
        {
            return LocalStorage.GetSettings().CityDetails;
        }

        public void SetCityDetailsSettings()
        {
            settings.CityDetails = !settings.CityDetails;
            LocalStorage.SetCityDetailsSettings();
        }

        public void SetSunsetSunriseDetailsSettings()
        {
            LocalStorage.SetSunsetSunriseDetailsSettings();
        }

        public void SetTemperatureDetailsSettings()
        {
            LocalStorage.SetTemperatureDetailsSettings();
        }

        public void ResetAll()
        {
            LocalStorage.ResetAll();
            LoadSettings();
        }
    }
}
