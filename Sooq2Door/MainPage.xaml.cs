using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sooq2Door
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            webView.Source = "https://sooq2door.com/";
            NavigationPage.SetHasNavigationBar(this, false); // Add to the constructor
            CheckAndLoadWebsite();
            webView.Navigated += WebView_Navigated;


        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Result == WebNavigationResult.Failure)
            {
                // Handle the error, display an alert
                DisplayAlert("Error", "There was a problem loading the page. Sorry for the inconvenience.", "OK");
            }
        }

        private async void CheckAndLoadWebsite()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Internet connection is available, attempt to load the website.
                webView.Source = "https://sooq2door.com";
                webView.Navigating += OnNavigating;
            }
            else
            {
                // No internet connection is available.
                await DisplayAlert("Error", "No internet connection available. Please check your connection and try again.", "OK");
            }
        }

        private void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (!e.Url.StartsWith("https://sooq2door.com"))
            {
                e.Cancel = true;
                DisplayAlert("Error", "The requested URL is not allowed.", "OK");
            }
           
        }

    }
}
