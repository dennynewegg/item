using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class MathHelper
    {
        public static List<T> RangeBefore<T>(this List<T> list, int length)
        {
            return RangeBefore(list, length, list.Count);
        }
        public static List<T> RangeBefore<T>(this List<T> list,int length, int startIndex)
        {
            if(list.IsEmpty()
                || length<1)
            {
                return new List<T>();
            }

            if(startIndex>=list.Count)
            {
                startIndex = list.Count - 1;
            }

            var start = Math.Max(0, startIndex - length + 1);
            return  list.GetRange(start, startIndex- start + 1);
        }
    }
}
