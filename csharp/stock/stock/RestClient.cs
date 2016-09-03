using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace stock
{
    public class RestClient
    {
        public WebClient WebReq { get; private set; }

        public RestClient()
        {
            WebReq = new WebClient();
            WebReq.Proxy = WebProxy.GetDefaultProxy();
            if (WebReq.Proxy != null)
            {
                WebReq.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
        }

        public T GetJson<T>(string url)
        {
            try
            {
                var str = WebReq.DownloadString(url);
                return SerializeHelper.JsonDeserialize<T>(str);
            }
            catch (Exception ex)
            {
                WriteMsg(ex.Message+Environment.NewLine+ex.StackTrace);
                return default(T);
            }
        }

        public T GetJsv<T>(string url)
        {
            try
            {
                var str = WebReq.DownloadString(url);
                return SerializeHelper.JsvDeserialize<T>(str);
            }
            catch (Exception ex)
            {
                WriteMsg(ex.Message + Environment.NewLine + ex.StackTrace);
                return default(T);
            }
        }

        public string DownloadString(string url)
        {
            
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    WriteMsg(string.Format("{0} Get {1}", DateTime.Now.ToShortTimeString(), url));
                    return WebReq.DownloadString(url);
                }
                catch (Exception exception)
                {
                    WriteMsg(exception.Message);
                }
            }
            return string.Empty;
        }

        private void WriteMsg(string msg)
        {
            Console.WriteLine(msg);
        }

    }
}
