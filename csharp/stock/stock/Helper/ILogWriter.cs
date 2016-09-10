using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.Helper
{
    public interface  ILogWriter
    {
        void Write(string msg);
    }

    class ConsoleLogWriter:ILogWriter
    {
        public void Write(string msg)
        {
            Console.WriteLine(msg);
        }
    }

}
