using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhyana
{
    public class KlineEntity
    {
        public List<KNode> Lines  { get; set; }
        public List<EndPoint>  EndPoints { get; set; }
        public List<Pen> Pens { get; set; }

    }

    public class EndPoint
    {
        public KNode Point { get; set; }
        /// <summary>
        /// Top,Bottom
        /// </summary>
        public NodeVector PointType { get; set; }
        public decimal Value { get; set; }
        public int Index { get; set; }

    }

    public class Pen
    {
        public KNode StartPoint { get; set; }
        public KNode EndPoint { get; set; }

        /// <summary>
        /// Up,Down
        /// </summary>
        public NodeVector PenVector { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }

    }

    public class Segment
    {
        public List<Pen>  PenList { get; set; }
    }

}
