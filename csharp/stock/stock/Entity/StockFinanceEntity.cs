﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public class StockFinanceEntity:StockEntity
    {
        public DateTime ReportDate { get; set; }
        public string PlanType { get; set; }
        public string PlanMemo { get; set; }
        public decimal? EPS { get; set; }
        public decimal? NetAssPerShare { get; set; }
        public decimal? CashPerShare { get; set; }
        /// <summary>
        /// 主营业务收入
        /// </summary>
        public decimal? MainIncoming { get; set; }
        /// <summary>
        /// 主营业务利润
        /// </summary>
        public decimal? MainpProfit { get; set; }

        /// <summary>
        /// 总资产
        /// </summary>
        public decimal? TotalAssets { get; set; }
        /// <summary>
        /// 净资产
        /// </summary>
        public decimal? NetAssets { get; set; }
    }
}
