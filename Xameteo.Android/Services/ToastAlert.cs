using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(Xameteo.Droid.Services.ToastAlert))]
namespace Xameteo.Droid.Services
{
    public class ToastAlert : IToastAlert
    {
        public void DisplayAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}