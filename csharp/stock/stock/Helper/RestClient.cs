using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;


namespace StockBiz
{
    public class RestClient
    {
    

        public WebClient WebReq { get; private set; }

        public RestClient()
        {
            WebReq = new WebClient();// {Proxy = WebProxy.GetDefaultProxy()};
            //if (WebReq.Proxy != null)
            //{
            //    WebReq.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            //}
          
            //WebReq.InitializeLifetimeService();
        }

        public byte[] GetData(string url)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    WriteMsg($"{DateTime.Now.ToShortTimeString()} Get {url}");
                    return WebReq.DownloadData(url);
                }
                catch (Exception exception)
                {
                    WriteMsg(exception.Message);
                }
            }
            return new byte[0];
        }


        public T GetJson<T>(string url)
        {
            try
            {
                var str = GetString(url);
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
                var str =GetString(url);
                return SerializeHelper.JsvDeserialize<T>(str);
            }
            catch (Exception ex)
            {
                WriteMsg(ex.Message + Environment.NewLine + ex.StackTrace);
                return default(T);
            }
        }

        public string GetString(string url)
        {
            
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    WriteMsg($"{DateTime.Now.ToShortTimeString()} Get {url}");
                    return WebReq.DownloadString(url);
                }
                catch (Exception exception)
                {
                    WriteMsg(exception.Message);
                }
            }
            return string.Empty;
        }

        private static void WriteMsg(string msg)
        {
            LogFactory.Instance.Write(msg);
        }

    }
}
