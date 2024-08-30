using Android.Content;
using Sooq2Door;
using Sooq2Door.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace Sooq2Door.Droid
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        public CustomWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Settings.JavaScriptEnabled = true; // Enable JavaScript
                Control.Settings.DomStorageEnabled = true; // Enable DOM Storage
                Control.Settings.SetAppCacheEnabled(true); // Enable Caching
                Control.Settings.SetAppCachePath(Context.CacheDir.Path); // Set Cache Path
                Control.Settings.SetAppCacheMaxSize(10 * 1024 * 1024); // Set Max Cache Size
               // Control.Settings.SetDatabaseEnabled(true); // Enable Database
                Control.Settings.SetGeolocationEnabled(true); // Enable Geolocation
                Control.Settings.AllowFileAccessFromFileURLs = true; // Allow File Access
                Control.Settings.AllowUniversalAccessFromFileURLs = true; // Allow Universal Access
                //Control.Settings.SetMixedContentMode(Android.Webkit.MixedContentHandling.CompatibilityMode); // Handle Mixed Content
            }
        }
    }
}
