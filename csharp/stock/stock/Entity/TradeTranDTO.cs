using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public class TradeTranDTO:StockEntity
    {
        public string Detail { get; set; }
        private List<TradeItem> trans { get; set; }

        public List<TradeItem> Trans
        {
            get
            {
                if(trans == null
                    && !string.IsNullOrWhiteSpace(Detail))
                {
                    trans = SerializeHelper.CsvDeserialize<List<TradeItem>>(Detail);
                }
                return trans;
            }
        }
    }

    public class TradeItem
    {
        public string Time { get; set; }
        public decimal Price { get; set; }
        public decimal PriceChange { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public string Vector { get; set; }
    }
}
