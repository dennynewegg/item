using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.Helper
{
    public static class StringHelper
    {
        public static bool IsEqual(this string str1, string str2)
        {
            if (str1 == str2)
            {
                return true;
            }

            str1 = str1 ?? string.Empty;
            str2 = str2 ?? string.Empty;
            return string.Compare(str1.Trim(), str2.Trim(), StringComparison.OrdinalIgnoreCase) == 0;

        }

        public static decimal? ToDec(this string str,int decimals=2)
        {
            decimal dec;
            if (decimal.TryParse(str, out dec))
            {
                return Math.Round(dec, decimals);
            }
            return null;
        }
    }
}
