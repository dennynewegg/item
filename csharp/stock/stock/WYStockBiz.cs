using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Restful.Web.Client.Client;

namespace stock
{
    public static class WYStockBiz
    {
        private const string hq163url =
            "http://quotes.money.163.com/hs/service/diyrank.php?page=0&query=STYPE:EQA&sort=VOLUME&order=desc&count={0}&type=query";

        public static List<StockEntity> GetTradeList(int count = 3500)
        {
            var client = new JsonWebClient(string.Format(hq163url, count));
            
           
            var result = client.Get<hq>(string.Empty);
            if (result != null
                && result.list != null)
            {
                return result.list.Select(item =>
                    new StockEntity
                    {
                        StockCode = item.SYMBOL,
                        Open = item.OPEN,
                        Low = item.LOW,
                        High = item.HIGH,
                        Percent = item.PERCENT,
                        Close = item.PRICE,
                        StockName = item.SNAME,
                        InDate = DateTime.Parse(item.TIME),
                        Volume = item.VOLUME,
                        Turnover = item.TURNOVER
                    }
                ).ToList();
            }
            return new List<StockEntity>();
        }


        class hq
        {
            public List<hqitem> list { get; set; }
        }

        private class hqitem
        {
            public decimal? HIGH { get; set; }
            public decimal? LOW { get; set; }
            public decimal? OPEN { get; set; }
            public decimal? PERCENT { get; set; }
            public decimal? PRICE { get; set; }
            public string SNAME { get; set; }
            public string SYMBOL { get; set; }
            public string TIME { get; set; }
            public decimal? VOLUME { get; set; }
            public decimal? TURNOVER { get; set; }
        }

    }
}
