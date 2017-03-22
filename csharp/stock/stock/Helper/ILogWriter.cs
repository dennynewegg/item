using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
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

    class FileLogWriter : ILogWriter
    {
        private string _path;

        public FileLogWriter(string path)
        {
            _path = path;
        }

        public void Write(string msg)
        {
            File.AppendAllText(_path, msg+Environment.NewLine);
        }
    }

}
