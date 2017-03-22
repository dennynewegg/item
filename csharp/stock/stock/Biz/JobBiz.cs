using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using StockBiz;


using System.Threading;

namespace StockBiz
{
    public static class JobBiz
    {
        public static void SyncHistoryData(DateTime? from,DateTime? to)
        {
           
            if (!to.HasValue)
            {
                to = DateTime.Now.AddDays(1);
            }

            var tradeList = SinaBiz.GetTradeList();
            if(tradeList.IsNullOrEmpty())
            {
                return;
            }
            var stocklist = tradeList;
            for(int i = 0;i<stocklist.Count;i++)
            {
                var fromDate = DailyDAL.GetMaxDaily(stocklist[i].StockCode);
                var list = WYStockBiz.HistoryTradeList(stocklist[i].StockCode
                    , fromDate.GetValueOrDefault(DateTime.Now.AddYears(-10)).AddDays(1)
                    , to.Value);
                if (list.Count > 0)
                {
                    ThreadPool.QueueUserWorkItem(state => {
                        DailyDAL.Insert(list);
                    });
                }
                LogFactory.Instance.Write($"{i}/{stocklist.Count}");
            }
        }

        public static void SyncTradeData()
        {
            var stockList = StockDAL.GetStockList();
            var tradeList = SinaBiz.GetTradeList();
            if (tradeList.IsNullOrEmpty()) return;
            var dbTradeList = DailyDAL.GetDailyList(tradeList.First().InDate.Value);
            if (dbTradeList.IsNullOrEmpty())
            {
                DailyDAL.Insert(tradeList);
            }
            var newStockList = tradeList.Where(trade =>
                        !stockList.Any(stock =>
                        stock.StockCode.IsEqual(trade.StockCode)))
                        .Select(stock=> { stock.IsNew = "1"; return stock; })
                        .ToList();
            if (!newStockList.IsNullOrEmpty())
            {
                StockDAL.InsertStock(newStockList);
            }

        }

        public static void SyncPlanFinance()
        {
            var date = FinanceDAL.GetMaxPlanIndate();
            if (!date.HasValue)
            {
                date = DateTime.Now.AddYears(-2);

            }
            var list = WYStockBiz.GetPlanFinance(date.Value);
            if (!list.IsNullOrEmpty())
            {
                FinanceDAL.InsertPlan(list);
            }
        }

        public static void SyncFinance()
        {
            var list = WYStockBiz.GetFinanceList(DateTime.Now);
            if (!list.IsNullOrEmpty())
            {
                FinanceDAL.InsertFinance(list);
            }
        }

     
        public static void SyncFinanceFromTHS()
        {
            var reportDate = DateTime.Now;
            reportDate = DateTime.Parse(string.Format("{0}-{1}", reportDate.AddMonths(-6).Year - 2, "09-30"));
            while (reportDate < DateTime.Now)
            {
                var endDate = FinanceDAL.GetMaxIndate(reportDate).GetValueOrDefault(reportDate);
                var list = THSBiz.GetFinanceList(reportDate, endDate);
                if(!list.IsNullOrEmpty())
                {
                    FinanceDAL.InsertFinance(list);
                }
                reportDate = reportDate.AddDays(1).AddMonths(3).AddDays(-1);
            }
        }

        public static void DownloadFinanceFromTHS()
        {
            var stockList = FinanceDAL.GetNoFinanceStock();
            foreach(var stock in stockList)
            {
                var list = THSBiz.DownloadFinance(stock);
                if (!list.IsNullOrEmpty())
                {
                    FinanceDAL.InsertFinance(list);
                }
            }

        }


        public static void SyncNewsFromThs()
        {
            var list = THSBiz.LimitUpNews();
            if (list.IsNullOrEmpty()) return;
            list.RemoveAll(item =>
            {
                var dbItem = NewsDAL.GetStockNews(item.StockCode, item.Url);
                return !dbItem.IsNullOrEmpty();
            });
            if (!list.IsNullOrEmpty())
            {
                NewsDAL.Insert(list);
            }
        }

        public static void SyncTaogulaArticle()
        {
            var date = ArticleDAL.GetMaxDate("tgl");
            var articleList = TaogulaBiz.GetArticleList(date).OrderByDescending(item => item.InDate).ToList();
            if (!articleList.IsEmpty())
            {
                date = articleList.Min(item => item.InDate);
                var dbArticle =  ArticleDAL.GetArticleMoreThanDate(date.Value);
                articleList.RemoveAll(item => dbArticle.Exists(dbItem => dbItem.Url == item.Url));


                articleList.ForEach(article =>
                {
                    try
                    {
                        article.Content = TaogulaBiz.GetArticle(article.Url);
                    }
                    catch
                    {

                    }
                });
                articleList.RemoveAll(item => item.Content.IsEmpty());
                ArticleDAL.Insert(articleList);
            }

        }


        //public static void SyncTaogula()
        //{
        //    var articleList = TaogulaBiz.GetArticleList().OrderByDescending(item=>item.InDate).ToList();
        //    var stockList = StockDAL.GetStockList();
            
        //    foreach(var article in articleList)
        //    {
        //        var articleStocks = ArticleDAL.GetByUrl(article.Url);
        //        //if(articleStocks.IsNullOrEmpty())
        //        //{
        //        //    var content =   TaogulaBiz.GetArticle(article.Url);
        //        //    articleStocks = ArticleBiz
        //        //        .ParseArticle(content, stockList)
        //        //        .Select(stock => new ArticleDTO() {
        //        //            Url = article.Url,
        //        //            StockCode = stock.StockCode,
        //        //            Title = article.Title,
        //        //            StockName = stock.StockName,
        //        //            InDate = article.InDate.Value.Date})
        //        //        .ToList() ;

        //        //    if(articleStocks.IsNullOrEmpty())
        //        //    {
        //        //        articleStocks.Add(article);
        //        //    }

        //        //    ArticleDAL.Insert(articleStocks);
        //        //}
        //    }

        //}

        public static void SyncLimitSell()
        {
            var list = SinaBiz.GetLimitSellList(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(2));
            DailyDAL.InsertLimitSell(list);
        }

        public static void SyncLonghu()
        {
            var date = LonghuDAL.GetMaxDate().GetValueOrDefault(DateTime.Now.AddDays(-3).Date);
            SyncLonghu(date, DateTime.Now.AddDays(1));
          
        }

        private static void SyncLonghu(DateTime from ,DateTime to)
        {
            var date = from.Date;
            while (date < to)
            {
                var list = SinaBiz.GetLonghuList(date);
                if (!list.IsNullOrEmpty())
                {
                    for (var index = list.Count()-1; index > 0; index--)
                    {
                        for (var j = 0; j < index; j++)
                        {
                            if (list[index].IsEqual(list[j]))
                            {
                                list.RemoveAt(index);
                                break;
                            }
                        }
                    }

                    var dbList = LonghuDAL.GetLonghu(list.First().InDate.Value.AddDays(-5));
                    list.RemoveAll(item => dbList.Exists(dbitem => dbitem.IsEqual(item)));
                }
                if (!list.IsNullOrEmpty())
                {
                    LonghuDAL.Insert(list);
                }

                date = date.AddDays(1);
            }
        }

        public static void SyncCategory()
        {
            var categoryList = THSBiz.GetCategoryList();
            var dbCategoryList = CategoryDAL.GetCategoryList();
            if(dbCategoryList != null)
            {
                categoryList.RemoveAll(item => dbCategoryList.Exists(dbitem =>
                {
                    return item.CategoryCode == dbitem.CategoryCode
                    && item.StockCode == dbitem.StockCode;
                }));
            }

            var stockList = StockDAL.GetStockList();
            if(categoryList != null
                && stockList != null)
            {
                foreach(var cat in categoryList)
                {
                    var stock = stockList.Find(s => s.StockCode == cat.StockCode);
                    if(stock  != null)
                    {
                        cat.StockName = stock.StockName;
                    }
                }
            }
            if (categoryList.Count > 0)
            {
                CategoryDAL.Insert(categoryList);
            }
        }

    }
}
