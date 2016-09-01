using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Restful.Web.Client.Client;
using ServiceStack.Text;
using ServiceStack.Text.Jsv;

namespace stock
{
    public static class SinaBiz
    {
        private const string hq163url =
           "http://vip.stock.finance.sina.com.cn/quotes_service/api/json_v2.php/Market_Center.getHQNodeData?page=0&num=3500&sort=changepercent&asc=0&node=hs_a";

        public static List<StockEntity> GetTradeList(int count = 3500)
        {
            var client = new WebClient();
            var str = client.DownloadString(hq163url);

            var result = SerializeUtil.JsvDeserialize<List<hqitem>>(str);
            if (result != null
                && result.Count()>0)
            {
                return result.Select(item =>
                    new StockEntity
                    {
                        StockCode = item.code,
                        Open = item.open,
                        Low = item.low,
                        High = item.high,
                        Percent = item.changepercent,
                        Close = item.trade,
                        StockName = item.name,
                        InDate = DateTime.Parse(DateTime.Now.ToShortDateString()+" "+item.ticktime),
                        Volume = item.volume,
                        Turnover = item.amount
                    }
                ).ToList();
            }
            return new List<StockEntity>();
        }

        private class hqitem
        {
            public decimal? high { get; set; }
            public decimal? low { get; set; }
            public decimal? open { get; set; }
            public decimal? changepercent { get; set; }
            public decimal? trade { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public string ticktime { get; set; }
            public decimal? volume { get; set; }
            public decimal? amount { get; set; }
        }

    }
}
