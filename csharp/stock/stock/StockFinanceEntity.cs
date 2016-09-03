using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock
{
    public class StockFinanceEntity:StockEntity
    {
        public DateTime ReportDate { get; set; }
        public string PlanType { get; set; }
        public string PlanMemo { get; set; }
    }
}
