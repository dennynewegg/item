using StockBiz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinMain
{
    public partial class FrmChart : Form
    {
        Series kSeries;

        public FrmChart()
        {
            InitializeComponent();
            kSeries = stockChart.Series[0];
        }

        private void txtStockCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtStockCode.Text)
                && e.KeyCode == Keys.Enter)
            {
                var list = DailyDAL.GetDailyList(txtStockCode.Text);
                foreach(var item in list)
                {
                    kSeries.Points.Add(new DataPoint(item.InDate.Value.ToOADate(),
                         (double)item.Close.Value));
                }
            }
        }
    }
}
