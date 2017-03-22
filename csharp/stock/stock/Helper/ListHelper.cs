using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
   public static  class ListHelper
    {
        public static void ForRange<T>(List<T> list,Action<List<T>> action ,int count=200)
        {
            var index = 0;
            while (true)
            {
                var len = count;
                if (index + len > list.Count)
                {
                    len = list.Count - index;
                }

                if (len > 0)
                {
                    var sublist =  list.GetRange(index, len);
                    action(sublist);
                }
                index += len;



                if(len<count)
                { break;}



            }

            
        }

        public static bool IsEmpty(this ICollection able)
        {
            return able == null
                || able.Count == 0;

        }

    }
}
