using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.Helper
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
    }
}
