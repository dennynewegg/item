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

using System.IO;

namespace WinMain
{
    public partial class MainForm : Form,ILogWriter
    {
        public MainForm()
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
                    JobBiz.SyncHistoryData(null,null);

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
                    JobBiz.SyncFinanceFromTHS();
                    //JobBiz.DownloadFinanceFromTHS();
                    Write("财务数据获取完毕。");
                }, null);
            }
            if (ckNews.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Write("开始同步涨停原因");
                    JobBiz.SyncNewsFromThs();
                    Write("涨停原因获取完毕。");
                });
            }
            //if (ckTaogula.Checked)
            //{
            //    ThreadPool.QueueUserWorkItem(state =>
            //    {
            //        Write("开始同步淘股啦复盘");
            //        JobBiz.SyncTaogula();
            //        Write("结束同步淘股啦复盘");

            //    });
            //}
            if (ckLimitSell.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Write("开始同步限售解禁");
                    JobBiz.SyncLimitSell();
                    Write("结束同步限售解禁");

                });
            }
            if (ckLonghu.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Write("开始同步龙虎榜");
                    JobBiz.SyncLonghu();
                    Write("结束同步龙虎榜");

                });
            }
            if (ckCategory.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Write("开始同步概念");
                    JobBiz.SyncCategory();
                    Write("结束同步概念");

                });
            }

            if (ckArticle.Checked)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Write("开始同步资料");
                    JobBiz.SyncTaogulaArticle();
                    Write("结束同步资料");

                });
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

        private void button1_Click(object sender, EventArgs e)
        {
          
            var frm = new ArticleListForm();
            frm.Show();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var stockList = StockDAL.GetStockList();
            var accList = new List<Account>(stockList.Count);
            decimal totalGain = 0m;
            foreach (var stock in stockList)
            {
                var dailyList = DailyDAL.GetDailyList(stock.StockCode);
                dailyList.Sort((a, b) => a.InDate.Value.CompareTo(b.InDate.Value));
                var account = new Account(10000);
                foreach (var daily in dailyList)
                {
                    account.AddDaily(daily);
                }
                var gain = account.Gain();
                totalGain += gain;
                accList.Add(account);
                LogFactory.Instance.Write(string.Format("{0} gain:{1}", stock.StockName,gain));
            }

            //accList.Sort((b,a) => a.Gain().CompareTo(b.Gain()));
            //foreach(var account in accList)
            //{
            //    LogFactory.Instance.Write(string.Format("{0} gain:{1}", ));
            //}

            LogFactory.Instance.Write("盈亏：" + totalGain.ToString());
        }

        private void fun()
        {
            ////var list = SinaBiz.GetTradeList("002280", DateTime.Parse("2016-10-25"));
            ////Write(list.First().Trans.Count.ToString());
            //var categoryList =
            //    SerializeHelper.JsonDeserialize<List<CategoryStockDTO>>(File.ReadAllText(@"D:\pro\item\csharp\stock\WinMain\bin\Debug\327f0d79-e5d0-4034-8625-6649c832093e.txt"));


            //CategoryDAL.Insert(categoryList);
            foreach (var path in Directory.GetFiles(@"D:\pro\Release\Finance"))
            {
                try
                {
                    var stockList = StockDAL.GetStockList();
                    //string path = @"D:\pro\Release\Finance\300497.xls";
                    var stockcode = Path.GetFileNameWithoutExtension(path);
                    var stock = stockList.Find(item => item.StockCode.IsEqual(stockcode));
                    var sfl = THSBiz.ReadFromExcel(stock, path);
                    FinanceDAL.InsertFinance(sfl);
                }
                catch (Exception ex)
                {
                    LogFactory.Instance.Write(path);
                    ExceptionHelper.Handler(ex);
                }
            }
            LogFactory.Instance.Write("End");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            var frm = new FrmChart();
            frm.Show();
        }
    }
}
