using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockBiz;
using StockBiz.Helper;

namespace WinMain
{
    public partial class Form1 : Form,ILogWriter
    {
        public Form1()
        {
            InitializeComponent();
            LogFactory.Instance = this;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (ckbHistoryDailyTrade.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    //JobBiz.SyncHistoryData();

                }, null);
            }
            if (chkLastTrade.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Write("开始同步"+chkLastTrade.Text);
                    JobBiz.SyncTradeData();
                    Write("最新行情获取完毕。");
                }, null);
            }

            if (ckbPlanFinance.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Write("开始同步业绩预告");
                    JobBiz.SyncPlanFinance();
                    Write("业绩预告获取完毕。");
                }, null);
            }
            if (ckbFinance.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Write("开始同步财务数据");
                    JobBiz.SyncFinance();
                    Write("财务数据获取完毕。");
                }, null);
            }

        }

        public void Write(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(Write), msg);
                return;
            }
            txbLog.AppendText(msg+Environment.NewLine);
            Application.DoEvents();
        }
    }
}
