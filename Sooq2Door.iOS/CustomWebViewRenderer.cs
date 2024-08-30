using Foundation;
using Sooq2Door;
using Sooq2Door.iOS;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace Sooq2Door.iOS
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, WKWebView>
    {
        WKWebView _wkWebView;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var config = new WKWebViewConfiguration
                {
                    Preferences = new WKPreferences
                    {
                        JavaScriptEnabled = true // Enable JavaScript
                    },
                    WebsiteDataStore = WKWebsiteDataStore.DefaultDataStore // Default Data Store for Cookies
                };

                // Initialize WKWebView with the configuration
                _wkWebView = new WKWebView(Frame, config);

                // Set the WKWebView as the native control
                SetNativeControl(_wkWebView);
            }
        }
    }
}
