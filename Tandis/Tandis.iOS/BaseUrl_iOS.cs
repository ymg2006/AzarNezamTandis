using Xamarin.Forms;
using Tandis.iOS;
using Foundation;

[assembly: Dependency (typeof (BaseUrl_iOS))]

namespace Tandis.iOS
{
    public class BaseUrl_iOS : IBaseUrl
    {
        public string Get () 
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}