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
    public partial class CitiesPage : ContentPage
    {
        CitiesViewModel VM;
        public CitiesPage()
        {
            InitializeComponent();
            BindingContext = VM = new CitiesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.Init();
        }

        public void OnDelete(object sender, EventArgs args)
        {
            var selected = (MenuItem)sender;
            var cityToDelete = selected.CommandParameter as City;
            var name = cityToDelete.FullName;
        }
    }
}