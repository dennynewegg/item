using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public class ArticleDTO
    {  
        public uint? ArticleID { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime? InDate { get; set; }
        public string ArticleType { get; set; }
    }
}
