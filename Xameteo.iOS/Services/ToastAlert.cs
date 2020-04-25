using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Xameteo.iOS.Services.ToastAlert))]
namespace Xameteo.iOS.Services
{
    class ToastAlert : IToastAlert
    {
        const double LONG_DELAY = 3.5;

        NSTimer alertDelay;
        UIAlertController alert;

        public void DisplayAlert(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }


        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}