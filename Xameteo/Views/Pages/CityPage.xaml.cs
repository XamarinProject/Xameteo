using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xameteo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityPage : ContentPage
    {
        CityViewModel VM;
        public CityPage()//(City city)
        {
            InitializeComponent();
            City testCity = new City();
            testCity.Name = "Nice";
            BindingContext = VM = new CityViewModel(testCity);
        }
    }
}