using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public class CategoryStockDTO:StockEntity
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
    }
}
