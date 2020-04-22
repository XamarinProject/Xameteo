using Xamarin.Forms;

namespace Xameteo
{
    public partial class App : Application
    {
        public static City SelectedCity;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
