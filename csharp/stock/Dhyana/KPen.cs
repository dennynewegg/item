using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Dhyana
{
    class KPen : KLineBase
    {
        public KPen(KLineBase inner) : base(inner)
        {
            
        }

        protected override KlineEntity Core(KlineEntity kline)
        {
            kline.Pens = new List<Pen>(kline.EndPoints.Count);
            if (kline.EndPoints.IsNullOrEmpty())
            {
                return kline;
            }

            for (int i = 1; i < kline.EndPoints.Count; i++)
            {
                var previous = kline.EndPoints[i - 1];
                var curr = kline.EndPoints[i];
                kline.Pens.Add(new Pen()
                {
                    StartPoint = previous.Point,
                    EndPoint = curr.Point,
                    PenVector = curr.PointType,
                    From = previous.Value,
                    To=curr.Value
                });
            }
            return kline;
        }
    }
}
