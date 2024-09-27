using Foundation;
using Sooq2Door;
using Sooq2Door.iOS;
using System;
using System.Collections.Generic;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace Sooq2Door.iOS
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, WKWebView>
    {
        WKWebView _wkWebView;
        private Stack<string> _navigationStack = new Stack<string>();


        public  void HandleBackNavigation()
        {
            if (_wkWebView.CanGoBack)
            {
                _wkWebView.GoBack();
                _navigationStack.Pop();  // Update the navigation stack
            }
        }
        //protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        //{
        //    base.OnElementChanged(e);

        //    if (e.NewElement != null)
        //    {
        //        var config = new WKWebViewConfiguration
        //        {
        //            Preferences = new WKPreferences
        //            {
        //                JavaScriptEnabled = true // Enable JavaScript
        //            },
        //            WebsiteDataStore = WKWebsiteDataStore.DefaultDataStore // Default Data Store for Cookies
        //        };

        //        // Initialize WKWebView with the configuration
        //        _wkWebView = new WKWebView(Frame, config);

        //        // Set the WKWebView as the native control
        //        SetNativeControl(_wkWebView);
        //    }
        //}
        private void OnGoBackRequested(object sender, EventArgs e)
        {
            if (_wkWebView.CanGoBack)
            {
                _wkWebView.GoBack();
                if (_navigationStack.Count > 1)
                {
                    _navigationStack.Pop(); // Remove the last page only if there's more in the stack
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            // Unsubscribe from events to avoid memory leaks
            if (disposing)
            {
                (Element as CustomWebView).GoBackRequested -= OnGoBackRequested;
            }
            base.Dispose(disposing);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
               

                (Element as CustomWebView).GoBackRequested += OnGoBackRequested;

                var config = new WKWebViewConfiguration
                {
                    Preferences = new WKPreferences
                    {
                        JavaScriptEnabled = true
                    },
                    WebsiteDataStore = WKWebsiteDataStore.DefaultDataStore
                };

                // Initialize WKWebView
                _wkWebView = new WKWebView(Frame, config)
                {
                    NavigationDelegate = new CustomWebViewNavigationDelegate(e.NewElement, _navigationStack)
                };

                // Set the native control to WKWebView
                SetNativeControl(_wkWebView);

                // Load the URL set in the Xamarin.Forms WebView.Source
                if (e.NewElement.Source is UrlWebViewSource urlSource)
                {
                    _wkWebView.LoadRequest(new NSUrlRequest(new NSUrl(urlSource.Url)));
                }
            }

            // Handle URL change if Source changes during runtime
            if (e.NewElement != null)
            {
                e.NewElement.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(WebView.Source))
                    {
                        var webView = sender as WebView;
                        if (webView.Source is UrlWebViewSource urlSource)
                        {
                            _wkWebView.LoadRequest(new NSUrlRequest(new NSUrl(urlSource.Url)));
                        }
                    }
                };
            }
        }
    }
    public class CustomWebViewNavigationDelegate : WKNavigationDelegate
    {
        private readonly WebView _formsWebView;
        private readonly Stack<string> _navigationStack;


        public CustomWebViewNavigationDelegate(WebView formsWebView, Stack<string> navigationStack)
        {
            _formsWebView = formsWebView;
            _navigationStack = navigationStack;

        }

        // This method gets called when navigation starts
        public override void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
        {
            ((IWebViewController)_formsWebView).SendNavigating(new WebNavigatingEventArgs(WebNavigationEvent.NewPage, webView.Url.ToString(), webView.Url.ToString()));
        }

        // This method gets called when navigation finishes
        public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            // ((IWebViewController)_formsWebView).SendNavigated(new WebNavigatedEventArgs(WebNavigationEvent.NewPage, webView.Url.ToString(), webView.Url.ToString(), WebNavigationResult.Success));
            ((IWebViewController)_formsWebView).SendNavigated(new WebNavigatedEventArgs(WebNavigationEvent.NewPage, webView.Url.ToString(), webView.Url.ToString(), WebNavigationResult.Success));

            // Handle the back button visibility based on the stack
            if (_navigationStack.Count > 1 && webView.CanGoBack)
            {
                Console.WriteLine("Can go back");
            }
        }

        // Handle navigation failures
        public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            ((IWebViewController)_formsWebView).SendNavigated(new WebNavigatedEventArgs(WebNavigationEvent.NewPage, webView.Url.ToString(), webView.Url.ToString(), WebNavigationResult.Failure));
        }
    }
}
