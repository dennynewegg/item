using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.Entity
{
    [Serializable]
    public  class ReportDTO:StockEntity
    {
        public decimal? NextPercent { get; set; }
    }
}
