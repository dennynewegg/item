using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.DAL
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

        public static void InsertFinance(List<StockFinanceEntity> list)
        {
            var sql = @"
INSERT INTO `svc`.`finance`
(
`stockcode`,
`stockname`,
`indate`,
`reportdate`,
`eps`,
`NetAssPerShare`,
`CashPerShare`,
`MainIncome`,
`MainProfit`,
`TotalAssets`,
`NetAssets`)
VALUES
(
@stockcode,
@stockname,
@indate,
@reportdate,
@eps,
@NetAssPerShare,
@CashPerShare,
@MainIncome,
@MainProfit,
@TotalAssets,
@NetAssets);

";

            SqlHelper.ExecuteNonQuery(sql,list);

        }


    }
}
