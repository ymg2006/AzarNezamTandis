using Tandis.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl))]
namespace Tandis.UWP
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "ms-appx-web:///";
        }
    }
}
