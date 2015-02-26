using System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace Mouztache.Modern
{
    public sealed partial class MainPage : Page
    {
#if WINDOWS_APP
		private const string ServiceUrl = "http://www.mouztache.com/#/device/windows-store";
#elif WINDOWS_PHONE_APP
		private const string ServiceUrl = "http://epsilonprod.mouztache.com/#/device/windows-phone";
#endif

		public MainPage()
        {
            this.InitializeComponent();

#if WINDOWS_PHONE_APP
			// Setup the view & status bar
			StatusBar.GetForCurrentView().BackgroundColor = Colors.White;
			StatusBar.GetForCurrentView().BackgroundOpacity = 0.5;
			StatusBar.GetForCurrentView().ForegroundColor = Colors.Black;
			ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
#endif

			this.PART_WebView.NavigationStarting += PART_WebView_NavigationStarting;
			this.PART_WebView.NavigationCompleted += PART_WebView_NavigationCompleted;
			this.PART_WebView.Source = new Uri(ServiceUrl);
        }

		private async void PART_WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
		{
#if WINDOWS_PHONE_APP
			await StatusBar.GetForCurrentView().ProgressIndicator.ShowAsync();
#endif
		}

		private async void PART_WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
		{
#if WINDOWS_PHONE_APP
			await StatusBar.GetForCurrentView().ProgressIndicator.HideAsync();
#endif

			// Check for errors
			if(args.IsSuccess == false)
			{
				string message = string.Format("Erreur de chargement ({0})", args.WebErrorStatus);
				string title = "Mouztâche";

				await new MessageDialog(message, title).ShowAsync();
			}
		}
    }
}
