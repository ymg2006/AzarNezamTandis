using Tandis.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Cookie_Android))]
namespace Tandis.Droid
{
    class Cookie_Android : MainActivity, IBaseCookie
    {
        private static readonly Android.Webkit.CookieManager _cookieManager = Android.Webkit.CookieManager.Instance;

        public string Get()
        {
            try
            {
                return _cookieManager.GetCookie("http://tandisapp.azarnezam.ir");
            }
            catch
            {
                return string.Empty;
            }
        }

        public void Set(string cookies)
        {
            _cookieManager.RemoveAllCookie();
            foreach (string item in cookies.Split(';'))
            {
                _cookieManager.SetCookie("http://tandisapp.azarnezam.ir", item);
            }

            var debug = _cookieManager.GetCookie("http://tandisapp.azarnezam.ir");
        }

        public bool hasUserCookie()
        {
            try
            {
               return _cookieManager.GetCookie("http://tandisapp.azarnezam.ir").Contains("globals");
            }
            catch
            {
                return false;
            }
        }
    }
}