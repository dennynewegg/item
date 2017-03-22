using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dhyana
{

    public class KEndPoint:KLineBase
    {

        public KEndPoint(KLineBase inner) : base(inner)
        {
            
        }

        protected override KlineEntity Core(KlineEntity kline)
        {
            var lines = kline.Lines;
            if (lines.Count < 3)
            {
                return kline;
            }
            var current = lines[1];
            var previous = lines[0];
            if (current.High > previous.High
                && current.Low > previous.Low)
            {
                current.Vector = NodeVector.Up;
            }
            else
            {
                current.Vector = NodeVector.Down;
            }

            var turnStack = new Stack<int>();

            turnStack.Push(0);

            for (int i = 2; i < lines.Count; i++)
            {
                var preTurn = -1;
                if (turnStack.Count > 0)
                {
                  preTurn = turnStack.Peek();
                }
                var preTurnNode = current;
                if (preTurn > -1)
                {
                    preTurnNode = lines[preTurn];
                }
                current = lines[i];
                previous = lines[i - 1];
                var vector = current.High >= previous.High?NodeVector.Up:NodeVector.Down;

                //Debug.WriteLine(string.Format("{3} befor current: {0} Vector:{1},previous Vector:{2}", i, vector, previous.Vector,current.InDate));
                Debug.WriteLine("{0} pre({3},{4}),cur:({1},{2}),vector:{5}"
                    ,current.InDate.ToShortDateString()
                    ,current.Low
                    ,current.High
                    ,preTurnNode.Low
                    ,preTurnNode.High
                    ,vector);

                if (previous.Vector == vector)
                {
                    current.Vector = vector;
                }
                else if (vector == NodeVector.Up)
                {
#region Up
                    if (previous.Vector == NodeVector.Down)
                    {
                        current.Vector = NodeVector.PreUp;
                        turnStack.Push(i-1);
                    }
                    else if (previous.Vector == NodeVector.PreUp)
                    {
                        current.Vector = NodeVector.PreUp;
                        if (i - preTurn > 3
                            && preTurnNode.High > 0
                            && current.Low>preTurnNode.High
                            && current.High>= lines.GetRange(preTurn,i-preTurn).Max(item=>item.High)
                            )
                        {
                            //preTurnNode.Position = NodePosition.Bottom;
                            preTurnNode.Vector = NodeVector.Down;
                            for (var j = preTurn + 1; j < i + 1; j++)
                            {
                                lines[j].Vector = NodeVector.Up;
                            }
                        }
                    }
                    else
                    {
                        if (preTurnNode.High < current.High
                            && preTurn > -1)
                        {
                            for (int j = preTurn+1; j < i + 1; j++)
                            {
                                lines[j].Vector = NodeVector.Up;
                            }
                            turnStack.Pop();
                        }
                        else
                        {
                            current.Vector = NodeVector.PreDwon;
                        }
                        

                    }
#endregion
                }
                else if(vector == NodeVector.Down) //vector == down
                {
                    if (previous.Vector == NodeVector.Up)
                    {
                        current.Vector = NodeVector.PreDwon;
                        turnStack.Push(i - 1);
                    }
                    else if (previous.Vector == NodeVector.PreDwon)
                    {
                        current.Vector = NodeVector.PreDwon;
                        if (i - preTurn > 3
                            && current.Low > 0
                            &&  preTurnNode.Low>current.High
                            && current.Low <= lines.GetRange(preTurn, i - preTurn).Min(item => item.Low)
                            )
                        {
                            Debug.WriteLine(string.Format("Top:curr:{0},pretu:{1}", i, preTurn));
                            preTurnNode.Vector = NodeVector.Up;
                            for (int j = preTurn + 1; j < i + 1; j++)
                            {
                                lines[j].Vector = NodeVector.Down;
                            }
                        }
                    }
                    else if (previous.Vector == NodeVector.PreUp)
                    {
                        if (preTurnNode.Low > current.Low
                            && preTurn > -1)
                        {
                            for (int j = preTurn + 1; j < i + 1; j++)
                            {
                                lines[j].Vector = NodeVector.Down;
                            }
                            turnStack.Pop();

                        }
                        else
                        {
                            current.Vector = NodeVector.PreUp;
                        }
                    }
                }
            }

            kline.EndPoints = new List<EndPoint>(turnStack.Count);
            foreach (var index in turnStack)
            {
                var node = lines[index];
                if (node.Vector == NodeVector.Down)
                {
                    node.Position = NodePosition.Bottom;
                }
                else if (node.Vector == NodeVector.Up)
                {
                    node.Position = NodePosition.Top;
                }
                kline.EndPoints.Add(new EndPoint()
                {
                    Index = index,
                    Point = node,
                    PointType = node.Vector,
                    Value = node.Vector== NodeVector.Up?node.High:node.Low
                });
            }

            kline.EndPoints.Reverse();
            return kline;
        }
    }
}
