using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class ExceptionHelper
    {
        public static void Handler(Exception ex)
        {
            LogFactory.Instance.Write(GetMessage(ex));
        }

        private static string GetMessage(Exception ex)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(ex.Message);
            builder.AppendLine(ex.StackTrace);
            if(ex.InnerException != null)
            {
                builder.AppendLine(GetMessage(ex.InnerException));
            }
            return builder.ToString();
        }
    }
}
