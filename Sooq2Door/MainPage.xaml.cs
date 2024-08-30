using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sooq2Door
{
    public class CustomWebView : WebView
    {
    }
    public partial class MainPage : ContentPage
    {
        private Stack<string> _navigationStack = new Stack<string>(); // Custom navigation stack
        private bool _isNavigating; // Flag to prevent navigation loop

        private string allProductsUrl;
        private string vegetablesUrl;
        private string fruitsUrl;
        private string cartUrl;
        private string HomeUrl;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false); // Hides the default navigation bar

            // Add WebView navigation handlers

            webView.Navigating += OnWebViewNavigating;
            webView.Navigated += OnWebViewNavigated;

            CheckAndLoadWebsite();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateToolbarLanguage(); // Update toolbar language on appearing
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                // Visual feedback by changing the opacity
                button.Opacity = 0.5;
                await Task.Delay(100); // Short delay to simulate a click effect
                button.Opacity = 1;

                // Logic to handle button click based on the button's purpose
                if (button == cartButton)
                {
                    webView.Source = cartUrl;
                }
                else if (button == allProductsButton)
                {
                    webView.Source = allProductsUrl;
                }
                else if (button == vegetablesButton)
                {
                    webView.Source = vegetablesUrl;
                }
                else if (button == fruitsButton)
                {
                    webView.Source = fruitsUrl;
                }
                else if (button == backButton)
                {
                    OnBackButtonClicked(sender, e);
                }
                else if(button== homeButton)
                {
                    webView.Source = HomeUrl;
                }
            }
        }

        private void UpdateToolbarLanguage()
        {
            // Retrieve the current URL from the WebView
            var urlWebViewSource = webView.Source as UrlWebViewSource;
            string currentUrl = urlWebViewSource?.Url;

            // Logic to update the toolbar text based on the current language of the WebView
            if (currentUrl != null && currentUrl.Contains("/ar"))
            {
                // Arabic language
                cartLabel.Text = "عربة التسوق"; // Arabic for "Cart"
                allProductsLabel.Text = "جميع المنتجات"; // Arabic for "All Products"
                vegetablesLabel.Text = "خضروات"; // Arabic for "Vegetables"
                fruitsLabel.Text = "فواكه"; // Arabic for "Fruits"
                backLabel.Text = "رجوع"; // Arabic for "Back"
                homeLabel.Text = "الرئيسيه";
            }
            else
            {
                // Default to English
                cartLabel.Text = "Cart";
                allProductsLabel.Text = "All Products";
                vegetablesLabel.Text = "Vegetables";
                fruitsLabel.Text = "Fruits";
                backLabel.Text = "Back";
                homeLabel.Text = "Home";
            }
        }

        private void OnWebViewNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (_isNavigating)
            {
                return; // If already navigating, do not set the source again
            }

            // Allow any URL within the sooq2door.com domain
            if (!e.Url.Contains("sooq2door.com"))
            {
                e.Cancel = true;
                DisplayAlert("Error", "The requested URL is not allowed.", "OK");
            }
            else
            {
                UpdateButtonVisibility(); // Update button visibility as navigation starts
            }
        }

        private void OnWebViewNavigated(object sender, WebNavigatedEventArgs e)
        {
            _isNavigating = false; // Reset the flag after navigation completes

            if (e.Result == WebNavigationResult.Failure)
            {
                DisplayAlert("Error", "There was a problem loading the page. Sorry for the inconvenience.", "OK");
            }

            // Detect if the URL has changed to Arabic or back to English
            if (e.Url.Contains("/ar"))
            {
                UpdateShopifyUrls("ar"); // Update URLs for Arabic
            }
            else
            {
                UpdateShopifyUrls("en"); // Default to English if no language code is found
            }

            // Push the URL to the custom navigation stack if navigation was successful
            if (e.Result == WebNavigationResult.Success && !_navigationStack.Contains(e.Url))
            {
                _navigationStack.Push(e.Url);
            }

            UpdateButtonVisibility(); // Update button visibility based on custom stack
            UpdateToolbarLanguage();
        }

        private async void CheckAndLoadWebsite()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                _isNavigating = true; // Set the flag before setting the source
                webView.Source = "https://sooq2door.com/ar"; // Start with Arabic version
                _navigationStack.Clear(); // Clear custom stack for initial load
                _navigationStack.Push("https://sooq2door.com/ar"); // Add initial page to stack
                UpdateButtonVisibility();
            }
            else
            {
                await DisplayAlert("Error", "No internet connection available. Please check your connection and try again.", "OK");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (_navigationStack.Count > 1)
            {
                webView.GoBack();
                _navigationStack.Pop(); // Remove the current page from stack after going back
                UpdateButtonVisibility(); // Update after navigation
                return true; // Prevent default back button behavior
            }
            else
            {
                return true; // Do nothing to keep the app running
            }
        }

        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (_navigationStack.Count > 1)
            {
                webView.GoBack();
                _navigationStack.Pop(); // Remove the current page from stack after going back
                UpdateButtonVisibility();
            }
        }

        private void UpdateButtonVisibility()
        {
            // Show or hide the back button based on custom navigation stack
            backButton.IsVisible = _navigationStack.Count > 1;
        }

        private void UpdateShopifyUrls(string languageCode)
        {
            // Set the base URL according to the selected language
            string baseUrl = languageCode == "ar" ? "https://sooq2door.com/ar" : "https://sooq2door.com";

            // Update all relevant URLs with language consideration
            allProductsUrl = $"{baseUrl}/collections/all";
            vegetablesUrl = $"{baseUrl}/collections/veggie";
            fruitsUrl = $"{baseUrl}/collections/fruits";
            cartUrl = $"{baseUrl}/cart";
            HomeUrl = $"{baseUrl}";
        }
        private void OnSwipeRight(object sender, SwipedEventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        private void OnSwipeLeft(object sender, SwipedEventArgs e)
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

    }
}
