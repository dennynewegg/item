using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace stock
{
    public static class SerializeHelper
    {
        public static T JsonDeserialize<T>(string str)
        {
            var ser = new JsonSerializer<T>();
            return ser.DeserializeFromString(str);
        }

        public static T JsvDeserialize<T>(string str)
        {
            var ser = new JsvStringSerializer();
            return ser.DeserializeFromString<T>(str);
        }

    }
}
