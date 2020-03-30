using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xameteo
{
    public class CitiesViewModel : BaseViewModel
    {
        public ICommand AddCityCommand => new Command(AddCity);

        void AddCity()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.DisplayAlert("A venir...", "Rechercher une ville.", "OK");
            });
        }
    }
}
