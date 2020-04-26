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
        private Settings settings;
        public SettingsViewModel()
        {
            settings = LocalStorage.GetSettings();
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
        }
    }
}
