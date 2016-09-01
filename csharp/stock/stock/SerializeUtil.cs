using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace stock
{
    public static class SerializeUtil
    {
        public static T JsvDeserialize<T>(string str)
            where T:class 
        {
            var ser = new JsvStringSerializer();
            return ser.DeserializeFromString<T>(str);
        }
    }
}
