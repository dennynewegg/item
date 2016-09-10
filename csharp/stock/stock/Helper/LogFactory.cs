using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.Helper
{
    public  class LogFactory
    {
        public static ILogWriter Instance
        {
            get
            {
                if (_sLogWriter != null)
                {
                    return _sLogWriter;
                }
                return s_NullLogger;
            }
            set { _sLogWriter = value; }
        }

        private static readonly ILogWriter s_NullLogger = new ConsoleLogWriter();
        private static ILogWriter _sLogWriter;
    }
}
