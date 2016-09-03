using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Restful.Web.Client.Client;
using Restful.Web.Client.Headers;

namespace stock
{
    public static class WYStockBiz
    {
        private const string hq163url =
            "http://quotes.money.163.com/hs/service/diyrank.php?page=0&query=STYPE:EQA&sort=VOLUME&order=desc&count={0}&type=query";

        public static List<StockEntity> GetTradeList(int count = 3500)
        {

            var client = new WebClient();
            var str = client.DownloadString(string.Format(hq163url, count));

            var hqlist = SerializeHelper.JsonDeserialize<hq>(str);
            if (hqlist != null
                && hqlist.list != null
                && hqlist.list.Any())
            {
                return hqlist.list
                    .Where(item=>item.VOLUME>0)
                    .Select(item =>
                    new StockEntity
                    {
                        StockCode = item.SYMBOL,
                        Open = item.OPEN,
                        Low = item.LOW,
                        High = item.HIGH,
                        Percent = item.PERCENT*100,
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

        class hqitem
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

        //https://github.com/linezero/HtmlAgilityPack.git

        #region 业绩预告

        private const string yjygUrl = "http://quotes.money.163.com/hs/marketdata/service/yjyg.php?page={0}&sort=REPORTDATE&order=desc&count={1}&req=6916";

        public static List<StockFinanceEntity> GetPlanFinance(DateTime startDate)
        {

            var planList = new List<StockFinanceEntity>(100);
            const int pageCount = 200;
            int pageIndex = 1;
            while (true)
            {
                var url = string.Format(yjygUrl, pageIndex,pageCount);
                var restClient = new RestClient();
                var ygResult = restClient.GetJson<yjyg>(url);

                if (ygResult == null
                    || ygResult.list == null)
                {
                    break;
                }

                var ygList = ygResult.list.Where(item => item.REPORTDATE > startDate)
                    .Select(item => new StockFinanceEntity()
                    {
                        StockName = item.SNAME,
                        StockCode = item.SYMBOL,
                        ReportDate = item.YY,
                        InDate = item.REPORTDATE,
                        PlanType = item.EFCT12,
                        PlanMemo = item.EFCT11
                    }).ToList();

                planList.AddRange(ygList);

                if (ygList.Count < pageCount)
                {break;}
                pageIndex++;
            }
            return planList;
        }


        class yjyg
        {
            public List<yjygitem> list { get; set; }
        }
        class yjygitem
        {
            /// <summary>
            /// 预告类型:预增,预亏.
            /// </summary>
            public string EFCT12 { get; set; }
            public DateTime REPORTDATE { get; set; }

            /// <summary>
            /// 报告日期,是固定的3.31/6.30/9.30/12.31
            /// </summary>
            public DateTime YY { get; set; }
            /// <summary>
            /// 预告详细说明
            /// </summary>
            public string EFCT11 { get; set; }
            public string SYMBOL { get; set; }
            public string SNAME { get; set; }

            /// <summary>
            /// 上年同期每股收益
            /// </summary>
            public decimal? MFRATIO14 { get; set; }
        }
        #endregion

    }
}
