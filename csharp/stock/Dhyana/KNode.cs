using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace Dhyana
{
    public class KNode
    {
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public DateTime InDate { get; set; }
        public NodePosition Position { get; set; }
        public NodeVector Vector { get; set; }
        public bool IsRemove { get; set; }
    }

    public enum NodeVector
    {
        Up,
        Down,
        PreUp,
        PreDwon
    }

    public enum NodePosition
    {
        Normal,
        Top,
        Bottom,
        Pre,
        Next

    }

    public class KlineNodeImport
    {
        public static List<KNode> GetKlines()
        {
            return
                CsvSerializer.DeserializeFromStream<List<KNode>>(
                    File.OpenRead(@"C:\Users\dy45\Documents\Visual Studio 2012\Projects\Dhyana\Dhyana\600015.csv"))
                    .Where(item=>item.Low>0&&item.High>0)
                    .OrderBy(item=>item.InDate)
                    .ToList();
        }
    }
}
