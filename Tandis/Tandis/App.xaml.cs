using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tandis
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            if (_savedStateWebView != null)
            {
                WebView appWebview = MainPage.FindByName<WebView>("MyWebView");
                appWebview = _savedStateWebView;
                _savedStateWebView = null;
            }
            if (Preferences.Get("savedSession", "") != "")
            {
                DependencyService.Get<IBaseCookie>().Set(Preferences.Get("savedSession", ""));
            }
        }

        private WebView _savedStateWebView;
        protected override void OnSleep()
        {
            _savedStateWebView = MainPage.FindByName<WebView>("MyWebView");
        }

        protected override void OnResume()
        {
            if (_savedStateWebView != null)
            {
                WebView appWebview = MainPage.FindByName<WebView>("MyWebView");
                appWebview = _savedStateWebView;
                _savedStateWebView = null;
            }
            else
            {
                WebView appWebview = MainPage.FindByName<WebView>("MyWebView");
                if (DependencyService.Get<IIntent>().HasExtra("state") &&
                    DependencyService.Get<IIntent>().GetExtra("state") == "ok")
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        appWebview.Source = "http://tandisapp.azarnezam.ir/#/reservelist";
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        appWebview.Source = "http://tandisapp.azarnezam.ir/#/paylist";
                    });
                }
            }
        }
    }
}
