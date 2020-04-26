using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xameteo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityPage : ContentPage
    {
        CityViewModel VM;
        public CityPage()
        {
            InitializeComponent();
            BindingContext = VM = new CityViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.LoadWeatherData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            LocalStorage.ResetSelectedCity();
        }
    }
}