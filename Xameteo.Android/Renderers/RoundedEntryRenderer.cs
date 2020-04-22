using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xameteo;
using Xameteo.Droid;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace Xameteo.Droid
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        public RoundedEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.SetBackgroundResource(Resource.Layout.rounded_shape);
            }
        }
    }
}