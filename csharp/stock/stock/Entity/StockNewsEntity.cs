using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.Entity
{
    public class StockNewsEntity:StockEntity
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Memo { get; set; }
        public string Url { get; set; }
        public DateTime PubDate { get; set; }
        public string Title { get; set; }
    }
}
