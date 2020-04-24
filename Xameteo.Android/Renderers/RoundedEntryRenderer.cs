using System;
using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xameteo;
using Xameteo.Droid;

[assembly: ExportRenderer(typeof(Xameteo.RoundedEntry), typeof(Xameteo.Droid.RoundedEntryRenderer))]
namespace Xameteo.Droid
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        public RoundedEntryRenderer(Context context) : base(context)
        {
        }
        protected override void
        OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(60f);
                gradientDrawable.SetStroke(5,
                Android.Graphics.Color.Aqua);
                gradientDrawable.SetColor(Android.Graphics.Color.LightGray);
                Control.SetBackground(gradientDrawable);
                Control.SetPadding(50, Control.PaddingTop,
                Control.PaddingRight,
                Control.PaddingBottom);
            }
        }
    }
}