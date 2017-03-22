//using Lucene.Net.Util;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class FinanceDAL
    {
        public static void InsertPlan(List<StockFinanceEntity> list)
        {
            var sql = @"
INSERT INTO `svc`.`planfinance`
(
`stockcode`,
`stockname`,
`PlanType`,
`PlanMemo`,
`ReportDate`,
`Indate`)
VALUES
(
@stockcode,
@stockname,
@PlanType,
@PlanMemo,
@ReportDate,
@Indate);
            ";
            SqlHelper.ExecuteNonQuery(sql,list);
        }

        public static DateTime? GetMaxPlanIndate()
        {
            var sql = @"select max(indate) from svc.planfinance;";
            return SqlHelper.GetEntity<DateTime?>(sql);
        }

        public static void InsertFinance(List<StockFinanceEntity> list,bool dupByDate=true)
        {
            //if (dupByDate)
            //{
            //    list = RemoveDupByDate(list);
            //}
            //else
            //{
            //    list = RemoveDupByStock(list);
            //}
            if(list.Count == 0)
            {
                return;
            }

            var sql = @"
INSERT INTO `svc`.`finance`
(
`stockcode`,
`stockname`,
`indate`,
`reportdate`,
`eps`,
`CashPerShare`,
`MainIncome`,
`MainProfit`,
`TotalAssets`,
`NetAssets`,
`NetAssPerShare`,
`fund`,
`NetProfit`,
`NetProfitGrowth`,
`UndistributedProfit`,
`GrossMargin`,
`NetIncoming`)
VALUES
(
@stockcode,
@stockname,
@indate,
@reportdate,
@eps,
@CashPerShare,
@MainIncome,
@MainProfit,
@TotalAssets,
@NetAssets,
@NetAssPerShare,
@fund,
@NetProfit,
@NetProfitGrowth,
@UndistributedProfit,
@GrossMargin,
@NetIncoming);

";

            SqlHelper.ExecuteNonQuery(sql, list);

        }

        private static List<StockFinanceEntity> RemoveDupByDate(List<StockFinanceEntity> list)
        {
            var dateList = list.Select(d => d.ReportDate).Distinct();
            var whereSql = "where reportdate in ('@date')";
            whereSql = whereSql.Replace("@date"
                , list.Select(item => item.ReportDate.ToString("yyyy-MM-dd")).Distinct().Aggregate((a, b) => a + "','" + b));
            var dbList = GetFinanceList(whereSql);
            list.RemoveAll(item =>
            dbList.Exists(dbItem =>
            dbItem.ReportDate == item.ReportDate
            && StringHelper.IsEqual(dbItem.StockCode, item.StockCode)));
            return list;
        }

        private static List<StockFinanceEntity> RemoveDupByStock(List<StockFinanceEntity> list)
        {
            var dateList = list.Select(d => d.ReportDate).Distinct();
            var whereSql = "where stockCode in ('@date')";
            whereSql = whereSql.Replace("@date"
                , list.Select(item => item.StockCode).Distinct().Aggregate((a, b) => a + "','" + b));
            var dbList = GetFinanceList(whereSql);
            list.RemoveAll(item =>
            dbList.Exists(dbItem =>
            dbItem.ReportDate == item.ReportDate
            && StringHelper.IsEqual(dbItem.StockCode, item.StockCode)));
            return list;
        }

        public static DateTime? GetMaxIndate(DateTime report)
        {
            var sql = "select max(indate) from svc.finance where reportdate=@reportdate;";
            return SqlHelper.GetEntity<DateTime?>(sql, report);
        }

        private static List<StockFinanceEntity> GetFinanceList(string whereSql)
        {
            var sql = @"SELECT `finance`.`rowid`,
    `finance`.`stockcode`,
    `finance`.`stockname`,
    `finance`.`indate`,
    `finance`.`reportdate`,
    `finance`.`eps`,
    `finance`.`CashPerShare`,
    `finance`.`MainIncome`,
    `finance`.`MainProfit`,
    `finance`.`TotalAssets`,
    `finance`.`NetAssets`,
    `finance`.`NetAssPerShare`,
    `finance`.`fund`,
NetProfit
FROM `svc`.`finance` " + whereSql;
//where reportdate in ('@date')";
//            sql = sql.Replace("@date"
//                , dates.Select(date => date.ToString("yyyy-MM-dd")).Aggregate((a, b) => a + "','" + b));
            return SqlHelper.GetList<StockFinanceEntity>(sql);

        }

        public static List<StockEntity> GetNoFinanceStock()
        {
            var sql = @"select * 
from svc.stock as a
where not exists(select 1
 from svc.finance as b
 where a.stockcode = b.stockcode)";

            return SqlHelper.GetList<StockEntity>(sql);

        }

    }
}
