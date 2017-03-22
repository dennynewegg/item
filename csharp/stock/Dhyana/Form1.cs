using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ServiceStack;

namespace Dhyana
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var orgList = KlineNodeImport.GetKlines();

            var process = new KPen( new KEndPoint(new KlineMerge()));

            var kinfo = new KlineEntity() {Lines = orgList.ToList()};


            var klineInfo = process.Execute(kinfo);

            var ser = this.ctFirst.Series["SeriesOriginal"];
            var penSer = ctFirst.Series["Pen"];
            var endSer = this.ctFirst.Series["EndPoint"];
            var dpDataList =
                //newList.Lines
                //.GetRange(0,200);
                //.GetRange(newList.Lines.Count - 201, 200);
                 orgList.GetRange(orgList.Count - 201, 200);
            foreach (var node in dpDataList)
            {
                var dp = new DataPoint()
                {
                    XValue = node.InDate.ToOADate(),
                    YValues = new double[] {(double) node.Low, (double) node.High}
                };
                var color = Color.Gray;
                if (node.Position == NodePosition.Top)
                {
                    color = Color.Red;
                }
                else if (node.Position == NodePosition.Bottom)
                {
                    color = Color.Green;
                }
                else if (node.Vector == NodeVector.PreDwon
                         || node.Vector == NodeVector.PreUp)
                {
                    color = Color.Yellow;
                }
                dp.Color = color;
                ser.Points.Add(dp);

                decimal? endValue = null;
                if (node.Position == NodePosition.Top)
                {
                    endValue = node.High;
                }
                else if (node.Position == NodePosition.Bottom)
                {
                    endValue = node.Low;
                }

                if (endValue.HasValue)
                {

                    endSer.Points.Add(new DataPoint()
                    {
                        XValue = node.InDate.ToOADate(),
                        YValues = endValue.HasValue ? new[] {(double) endValue.Value} : null
                    });
                    penSer.Points.Add(new DataPoint()
                    {
                        XValue = node.InDate.ToOADate(),
                        YValues = endValue.HasValue ? new double[] {(double) endValue.Value} : null
                    });
                }
            }

            ctFirst.ChartAreas[0].AxisY.Maximum = (double)dpDataList.Max(item => item.High) + 1;
            ctFirst.ChartAreas[0].AxisY.Minimum = (double)dpDataList.Min(item => item.Low) - 1;
            ctFirst.ChartAreas[0].AxisY2.Maximum = (double)dpDataList.Max(item => item.High) + 1;
            ctFirst.ChartAreas[0].AxisY2.Minimum = (double)dpDataList.Min(item => item.Low) - 1;
            
        }
    }
}
