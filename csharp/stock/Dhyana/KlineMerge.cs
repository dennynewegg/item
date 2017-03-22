using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhyana
{
    public class KlineMerge : KLineBase
    {
        private List<KNode> Merge(List<KNode> list)
        {
            var index = list.Count - 1;
            while (index > 0)
            {
                var previous =list[index - 1];
                var curr = list[index];
                if (previous.High > curr.High
                    && previous.Low < curr.Low)
                {
                    list[index].IsRemove = true;
                    list.RemoveAt(index);
                    continue;
                }
                if (previous.High < curr.High
                         && previous.Low > curr.Low)
                {
                    list[index - 1].IsRemove = true;
                    list.RemoveAt(index - 1);
                }
                index--;
            }
            return list;
        }


        protected override KlineEntity Core(KlineEntity kline)
        {
            kline.Lines = Merge(kline.Lines);
            return kline;
        }
    }
}
