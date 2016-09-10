using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using ServiceStack;
using StockBiz.Helper;


namespace StockBiz
{
    public static class WYStockBiz
    {

        #region HQ

        private const string hq163url =
            "http://quotes.money.163.com/hs/service/diyrank.php?page=0&query=STYPE:EQA&sort=VOLUME&order=desc&count={0}&type=query";

        public static List<StockEntity> GetTradeList(int count = 3500)
        {
            var client = new RestClient();
            var json = client.DownloadString(string.Format(hq163url, count));
            var result = SerializeHelper.JsonDeserialize<hq>(json);
            if (result?.list != null && result.list.Any())
            {
                return result.list
                    //.Where(item => item.VOLUME.GetValueOrDefault() > 0)
                    .Select(item =>
                            new StockEntity
                            {
                                StockCode = item.SYMBOL,
                                Open = item.OPEN,
                                Low = item.LOW,
                                High = item.HIGH,
                                Percent = item.PERCENT.GetValueOrDefault()*100,
                                Close = item.PRICE,
                                StockName = item.SNAME,
                                InDate = DateTime.Parse(item.TIME).Date,
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

        #endregion

        #region  historydata

        private const string historydataUrl = "http://quotes.money.163.com/service/chddata.html?code={0}&start={1}&end={2}&fields=TCLOSE;HIGH;LOW;TOPEN;LCLOSE;PCHG;TURNOVER;VOTURNOVER;VATURNOVER";
        public static List<StockEntity> HistoryTradeList(string stockCode, DateTime from, DateTime to)
        {
            var client = new RestClient();
            var csv = client.DownloadString(String.Format(historydataUrl, StockHelper.WYCode(stockCode)
                , from.ToString("yyyyMMdd")
                , to.ToString("yyyyMMdd")));

            if (!string.IsNullOrWhiteSpace(csv))
            {
                csv = csv.Replace("日期,股票代码,名称,收盘价,最高价,最低价,开盘价,前收盘,涨跌幅,换手率,成交量,成交金额"
                    , "InDate,StockCode,StockName,Close,High,Low,Open,LClose,Percent,Turnover,Volume,Amount");
                csv = csv.Replace("None", "0");
                var list = SerializeHelper.CsvDeserialize<List<StockEntity>>(csv);
                if (list.Count > 0)
                {
                 
                    foreach (var item in list)
                    {
                        item.StockCode = item.StockCode.Replace("'", string.Empty).Trim();
                    }
                    list = list.Where(item => item.Volume.GetValueOrDefault() > 0)
                        .OrderBy(item => item.InDate)
                        .ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].DateSort = i;
                    }
                    return list;
                }
            }
            return new List<StockEntity>();
        }



        #endregion
       
        #region 业绩预告

        private const string yjygUrl = "http://quotes.money.163.com/hs/marketdata/service/yjyg.php?page={0}&sort=REPORTDATE&order=desc&count={1}&req=6916";

        public static List<StockFinanceEntity> GetPlanFinance(DateTime startDate)
        {

            var planList = new List<StockFinanceEntity>(100);
            const int pageCount = 200;
            int pageIndex = 0;
            while (true)
            {
                var url = string.Format(yjygUrl, pageIndex, pageCount);
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
                { break; }
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

            public DateTime? PUBLISHDATE { get; set; }
            /// <summary>
            /// 当前报表每股收益
            /// </summary>
            public string MFRATIO28 { get; set; }

            /// <summary>
            /// 每股净资产       
            /// </summary>
            public string MFRATIO18 { get; set; }

            /// <summary>
            /// 每股经营现金流
            /// </summary>
            public string MFRATIO20 { get; set; }

            /// <summary>
            /// 主营业务收入
            /// </summary>
            public string MFRATIO10 { get; set; }
            /// <summary>
            /// 主营业务利润
            /// </summary>
            public string MFRATIO4 { get; set; }
            /// <summary>
            /// 净利润
            /// </summary>
            public string MFRATIO2 { get; set; }

            /// <summary>
            /// 总资产
            /// </summary>
            public string MFRATIO12 { get; set; }


            /// <summary>
            /// 总负责
            /// </summary>
            public string MFRATIO25 { get; set; }


            /// <summary>
            /// 净资产
            /// </summary>
            public string MFRATIO122 { get; set; }


            /// <summary>
            /// 流动资产
            /// </summary>
            public string MFRATIO23 { get; set; }

            /// <summary>
            /// 流动负债
            /// </summary>
            public string MFRATIO24 { get; set; }


        }
        #endregion

        #region 财报
        private static List<string> reportDates = new List<string>()
        {
            "03-31","06-30","09-30","12-31"
        };

        const string finBaseUrl = "http://quotes.money.163.com/hs/marketdata/service/cwsd.php?page=0&query=date:{0}&sort=MFRATIO28&order=desc&count=5000&type=query&req=01750";

        public static List<StockFinanceEntity> GetFinanceList(DateTime reportDate)
        {
            reportDate = DateTime.Parse(string.Format("{0}-{1}", reportDate.Year-1, reportDates[3]));
            var dateIndex = 3;
            List<StockFinanceEntity> finList = new List<StockFinanceEntity>(10000);

            while (reportDate<DateTime.Now)//.Parse("2016-12-01"))
            {
                finList.AddRange(GetFinanceList(reportDate.ToString("yyyy-MM-dd")));

                dateIndex++;
                if (dateIndex >= 4)
                {
                    dateIndex = 0;
                    reportDate = reportDate.AddYears(1);
                }
                reportDate = DateTime.Parse(string.Format("{0}-{1}", reportDate.Year, reportDates[dateIndex]));
            }
            return finList;
        }

        private static List<StockFinanceEntity> GetFinanceList(string reportDate)
        {
            var client = new RestClient();
            var fin = client.GetJson<yjyg>(string.Format(finBaseUrl, reportDate));
            if (fin != null
                && !fin.list.IsNullOrEmpty())
            {
                var date = DateTime.Parse(reportDate);

                return fin.list.Select(item =>
                {
                    return new StockFinanceEntity
                    {
                        StockCode = item.SYMBOL,
                        StockName = item.SNAME,
                        ReportDate = item.PUBLISHDATE.GetValueOrDefault(date),
                        EPS = item.MFRATIO28.ToDec(),
                        NetAssPerShare = item.MFRATIO18.ToDec(),
                        CashPerShare = item.MFRATIO20.ToDec(),
                        MainIncoming = item.MFRATIO10.ToDec(),
                        NetAssets = item.MFRATIO122.ToDec(),
                        MainpProfit = item.MFRATIO4.ToDec(),
                        TotalAssets = item.MFRATIO12.ToDec()
                    };
                }).ToList();
                
            }
            return new List<StockFinanceEntity>();
        }

       
        #endregion



    }
}
