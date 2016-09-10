using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceStack;
using ServiceStack.Text;
using ServiceStack.Text.Jsv;
using StockBiz.Helper;

namespace StockBiz
{
    public static class SinaBiz
    {
        private const string hq163url =
           "http://vip.stock.finance.sina.com.cn/quotes_service/api/json_v2.php/Market_Center.getHQNodeData?page=0&num=3500&sort=changepercent&asc=0&node=hs_a";

        public static List<StockEntity> GetTradeList(int count = 3500)
        {
            var client = new RestClient();
            var str = client.DownloadString(hq163url);

            var result = SerializeHelper.JsvDeserialize<List<hqitem>>(str);
            if (result != null
                && result.Count()>0)
            {
                string firstCode = result.First().code;
                var stockDate = GetRealTime(new List<string>() { firstCode }).InDate;
            

            return result
                    .Where(item=>item.volume.GetValueOrDefault()>0)
                    .Select(item =>
                    new StockEntity
                    {
                        StockCode = item.code,
                        Open = item.open,
                        Low = item.low,
                        High = item.high,
                        Percent = item.changepercent,
                        Close = item.trade,
                        StockName = item.name,
                        InDate = stockDate,
                        Volume = item.volume,
                        Amount = item.amount,
                        Turnover =Math.Round( item.turnoverratio.Value)
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
            public decimal? turnoverratio { get; set; }
        }


        public static StockEntity GetRealTime(List<string> stockCodeList)
        {
            string codes =  stockCodeList.Select(str => StockHelper.GetLongCode(str))
                .Join(",");
            string url = "http://hq.sinajs.cn/list=" + codes;
            var client = new RestClient();
            var dataStr = client.DownloadString(url);
            var stockStrAry = dataStr.Split(';');
            //var list 

            //foreach (var stockStr in stockStrAry)
            //{
            var stockStr = stockStrAry.First();
            var eqIndex = stockStr.IndexOf("=");
            var stockData = stockStr.Substring(eqIndex + 2);
            stockData = stockData.Substring(0, stockData.Length - 1);
            var stockDatas = stockData.Split(',');

            return new StockEntity
            {
                InDate = DateTime.Parse( stockDatas[30]+" "+stockDatas[31])
            };
            //}
        }

    }
}
