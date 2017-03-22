using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
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
            var dec = ToDecimal(str);
            if (dec.HasValue)
            {
                return Math.Round(dec.Value, decimals);
            }
            return null;
        }
        private static String getSonString(String a, String b)
        {
            String max = null;
            String min = null;
            String temp = "";
            if (a.Length > b.Length)
            {
                max = a;
                min = b;
            }
            else
            {
                max = b;
                min = a;
            }
            for (int i = 0; i < min.Length; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    String substring = min.Substring(j, min.Length - i + j);//从小的字符串开始，从左向右移位比较
                    if (max.Contains(substring))
                    {
                        if (temp.Length < substring.Length)
                        {
                            temp = substring;
                        }
                    }
                }
            }
            return temp;
        }

        private static decimal? ToDecimal(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            var unit = 1;
            if (str.EndsWith("百万"))
            {
                unit = 100 * 10000;
                str = str.Replace("百万", "");
            }
            else if (str.EndsWith("千万"))
            {
                unit = 1000 * 10000;
                str = str.Replace("千万", "");
            }
            else if (str.EndsWith("万"))
            {
                unit = 10000;
                str = str.Replace("万", "");
            }
            else if (str.EndsWith("亿"))
            {
                unit = 10000 * 10000;
                str = str.Replace("亿", "");
            }
            decimal dec;
            if (decimal.TryParse(str, out dec))
            {
                return dec * unit;
            }
            return null;

        }

        

    }

   
}
