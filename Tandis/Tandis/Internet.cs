using System.Net;

namespace Tandis
{
    class Internet
    {
        public static bool CheckConnection (string uri)
        {
            try {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create (uri);
                iNetRequest.Timeout = 3500;
                WebResponse iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close ();
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
