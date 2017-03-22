using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockBiz;

namespace WinMain
{
    public partial class UIHQ : UserControl
    {
        public UIHQ()
        {
            InitializeComponent();
            dgvHQ.AutoGenerateColumns = false;
        }

        public void QueryHQ(List<string> stockCodes,DateTime? date)
        {
            if(stockCodes == null)
            {
                return;
            }

            var hqList = ReportDAL.QueryHQ(stockCodes,date);
            dgvHQ.DataSource = hqList;
        }

    }
}
