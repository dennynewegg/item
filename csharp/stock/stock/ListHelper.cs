using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock
{
    class ListHelper
    {
        public static void ForRange<T>(List<T> list,Action<List<T>> action ,int count=50)
        {
            var index = 0;
            while (true)
            {
                var len = count;
                if (index + len > list.Count())
                {
                    len = list.Count() - index;
                }

                if (len > 0)
                {
                    var sublist = list.GetRange(index, len);
                    action(sublist);
                }
                index += len;



                if(len<count)
                { break;}



            }

            
        }
    }
}
