using System;
using CoreGraphics;
using projectbase;
using projectbase.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(projectbase.Models.RoundedEntry),
typeof(RoundedEntryRenderer))]
namespace projectbase.iOS
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        protected override void
        OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Layer.CornerRadius = 20;
                Control.Layer.BorderWidth = 3f;
                Control.Layer.BorderColor = Color.DeepPink.ToCGColor();
                Control.Layer.BackgroundColor =
                Color.LightGray.ToCGColor();
                Control.LeftView = new UIKit.UIView(new CGRect(0, 0, 10,
                0));
                Control.LeftViewMode = UIKit.UITextFieldViewMode.Always;
            }
        }
    }
}