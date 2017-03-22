using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace StockBiz
{
    public static class SerializeHelper
    {
        public static T JsvDeserialize<T>(string str)
            //where T:class 
        {
            var ser = new JsvStringSerializer();
            return ser.DeserializeFromString<T>(str);
        }

        public static T JsonDeserialize<T>(string str)
        {
            var ser = new JsonStringSerializer();
            return ser.DeserializeFromString<T>(str);
        }

        public static T CsvDeserialize<T>(string str)
        {
            return CsvSerializer.DeserializeFromString<T>(str);
        }

        public static string JsonSerialize(object obj)
        {
            return JsonSerializer.SerializeToString(obj, obj.GetType());
        }

    }
}
