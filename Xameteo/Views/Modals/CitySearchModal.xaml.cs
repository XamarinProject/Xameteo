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
    public partial class CitySearchModal : ContentPage
    {
        CitySearchViewModel VM;
        public CitySearchModal()
        {
            InitializeComponent();
            BindingContext = VM = new CitySearchViewModel();
        }

        void ResultSelected(object sender, EventArgs e)
        {
            VM.ResultSelected();
        }
    }
}