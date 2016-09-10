using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using StockBiz.DAL;
using StockBiz.Helper;

namespace StockBiz
{
    public static class JobBiz
    {
        public static void SyncHistoryData(DateTime from,DateTime to)
        {
            var stocklist = StockDAL.GetStockList();
            for(int i = 0;i<stocklist.Count;i++)
            {
                var stockEntity = stocklist[i];
                var list = WYStockBiz.HistoryTradeList(stockEntity.StockCode, from,to);
                if (list.Count > 0)
                {
                    DailyDAL.Insert(list);
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
                        !stockList.Any(stock => stock.StockCode.IsEqual(trade.StockCode)))
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


    }
}
