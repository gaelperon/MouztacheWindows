using Awesomium.Core;
using System;
using System.Reflection;
using System.Windows;

namespace Mouztache.Desktop
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const string ServiceUrl = "http://www.mouztache.com/?platform=windows-desktop";

		public MainWindow()
		{
			InitializeComponent();

			// Persist web local storage
			var webSession = WebCore.CreateWebSession(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), Awesomium.Core.WebPreferences.Default);
			PART_WebView.WebSession = webSession;

			PART_WebView.Source = new Uri(ServiceUrl);
		}
	}
}
