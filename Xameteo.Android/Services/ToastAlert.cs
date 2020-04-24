using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using projectbase.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(projectbase.Droid.Services.ToastAlert))]
namespace projectbase.Droid.Services
{
    class ToastAlert : IToastAlert
    {
        
    public void DisplayAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}