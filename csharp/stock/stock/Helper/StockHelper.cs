using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class StockHelper
    {
        public static string WYCode(string code)
        {
            if (code.StartsWith("60"))
            {
                return "0" + code;
            }
            return "1" + code;
        }

        public static string GetLongCode(string code)
        {

            code = code.ToLower();

            if(code.StartsWith("sh")
                ||code.StartsWith("sz"))
            {
                return code;
            }

            if (code.StartsWith("6"))
            {
                return "sh" + code;
            }
            return "sz" + code;
        }

        public static string GetShortCode(string code)
        {
            code = code.ToLower();
            if (code.Length == 8
               && (code.StartsWith("sh")
                || code.StartsWith("sz")))
            {
                return code.Substring(2, 6);
            }
            if (code.Length == 6
                &&( code.StartsWith("6")
                 || code.StartsWith("30")
                 || code.StartsWith("002")
                 || code.StartsWith("000")))
            {
                return code;
            }
           
            return code;
        }
    }
}
