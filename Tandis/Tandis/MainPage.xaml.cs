using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Tandis
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private Timer timer1 = new Timer();

        public MainPage()
        {
            InitializeComponent();
            MyWebView.Source = DependencyService.Get<IBaseUrl>().Get() + "splash.html";
            timer1.Interval = 5000;
            timer1.Enabled = true;
            timer1.Elapsed += LoadPage;
            if (!Internet.CheckConnection("http://azarnezam.ir"))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await DisplayAlert("هشدار", "لطفا اینترنت گوشی را بررسی نمایید", "خروج", "ادامه");
                    if (result)
                    {
                        Process.GetCurrentProcess().CloseMainWindow();
                    }
                });
            }
        }

        private void LoadPage(object sender, ElapsedEventArgs e)
        {
            timer1.Enabled = false;
            Device.BeginInvokeOnMainThread(() => { MyWebView.Source = "http://tandisapp.azarnezam.ir/#/dashboard"; });
        }

        protected override bool OnBackButtonPressed()
        {
            if (MyWebView.CanGoBack)
            {
                try
                {
                    UrlWebViewSource dd = (UrlWebViewSource)MyWebView.Source;
                    if (dd.Url.Contains("login") || dd.Url.Contains("dashboard"))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            var result = await DisplayAlert("هشدار", "آیا از خروج مطمئن هستید ؟", "بله", "خیر");
                            if (result)
                            {
                                Process.GetCurrentProcess().CloseMainWindow();
                            }
                        });
                        return true;
                    }
                    MyWebView.GoBack();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else return base.OnBackButtonPressed();
        }

        private void injectJava()
        {
            string bodyHashScript = @"var myVar = setInterval(handleChange, 10);
                    function handleChange(e) {
                        var ad1 = document.getElementsByTagName('adva')[0];
                        if (typeof(ad1) != 'undefined' && ad1 != null)
                        {
                            let success1 = ad1.remove();
                        }
                        var ad2 = document.getElementsByTagName('advb')[0];
                        if (typeof(ad2) != 'undefined' && ad2 != null)
                        {
                            let success2 = ad2.remove();
                        }
                        var ad3 = document.getElementsByTagName('advc')[0];
                        if (typeof(ad3) != 'undefined' && ad3 != null)
                        {
                            let success3 = ad3.remove();
                        }
                        var ad4 = document.getElementsByTagName('advd')[0];
                        if (typeof(ad4) != 'undefined' && ad4 != null)
                        {
                            let success4 = ad4.remove();
                        }
                    }";
            MyWebView.EvaluateJavaScriptAsync(bodyHashScript);
        }

        protected void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            if (!e.Url.Contains("splash"))
            {
                injectJava();
                if (e.Url.Contains("dashboard") && Preferences.Get("savedSession", "") == "")
                {
                    Preferences.Set("savedSession", DependencyService.Get<IBaseCookie>().Get());
                }
                else if (e.Url.Contains("login") && !DependencyService.Get<IBaseCookie>().hasUserCookie())
                {
                    Preferences.Set("savedSession", "");
                }
                else if (e.Url.Contains(".Pdf"))
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            await Launcher.OpenAsync(e.Url);
                            //Device.OpenUri(new Uri(e.Url));
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    });
                }
            }

            if (!Internet.CheckConnection("http://azarnezam.ir"))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await DisplayAlert("هشدار", "لطفا اینترنت گوشی را بررسی نمایید", "خروج", "ادامه");
                    if (result)
                    {
                        Process.GetCurrentProcess().CloseMainWindow();
                    }
                });
            }
        }
    }
}