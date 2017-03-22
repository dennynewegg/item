using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhyana
{
    public abstract class KLineBase 
    {
        protected KLineBase Inner { get; set; }

        public KLineBase()
        {
            
        }

        public KLineBase(KLineBase inner)
        {
            this.Inner = inner;
        }

        protected abstract KlineEntity Core(KlineEntity kline);

        public KlineEntity Execute(KlineEntity list)
        {
            if (Inner != null)
            {
                list = this.Inner.Core(list);
            }
            return Core(list);
        }
    }
}
