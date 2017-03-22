using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;


namespace StockBiz
{
    public static class DailyDAL
    {

        public static void Insert(List<StockEntity> list)
        {
            if (list.IsNullOrEmpty())
            {
                return;
            }

            var dailyList = new List<StockEntity>(list.Count);
            var realTime = new List<StockEntity>(list.Count);
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (item.InDate.Value.Hour == 0
                    || String.Compare(item.InDate.Value.ToString("HH:mm"),"14:58")>0)
                {
                    dailyList.Add(item);
                }
                else
                {
                    realTime.Add(item);
                }
            }
            if (!dailyList.IsNullOrEmpty())
            {
                ZhubiBiz.BuildUpLimit(dailyList);
                InsertDaily(dailyList);
            }
            if (!realTime.IsNullOrEmpty())
            {
                InsertRealTime(realTime);
            }

        }


        public static void InsertLimitSell(List<StockEntity> list)
        {
            if (list.IsNullOrEmpty())
            {
                return;
            }
            var dbList = GetLimitSellList(list.Min(item => item.InDate.Value), list.Max(item => item.InDate.Value));
            if (!dbList.IsNullOrEmpty())
            {
                list.RemoveAll(item => 
                    dbList.Exists(dbItem => 
                    dbItem.StockCode.IsEqual(item.StockCode)
                    && dbItem.InDate==item.InDate));
            }
            if (list.IsNullOrEmpty())
            {
                return;
            }


            var sql = @"INSERT INTO `svc`.`limitsell`
(
`stockcode`,
`stockname`,
`InDate`,
`Volume`)
VALUES
(
@stockcode,
@stockname,
@InDate,
@Volume);
            ";
            SqlHelper.ExecuteNonQuery(sql, list);
        }


        public static List<StockEntity> GetLimitSellList(DateTime from,DateTime to)
        {
            var sql = @"SELECT `limitsell`.`rowid`,
    `limitsell`.`stockcode`,
    `limitsell`.`stockname`,
    `limitsell`.`InDate`,
    `limitsell`.`Volume`
FROM `svc`.`limitsell`
wHere indate>@from and indate<@to
";
            return SqlHelper.GetList<StockEntity>(sql
                , SqlHelper.CreateParameter("@from",from)
                ,SqlHelper.CreateParameter("@to",to));
        }



        private static void InsertDaily(List<StockEntity> list)
        {
            var sql = @"INSERT INTO `svc`.`daily`
(
`stockcode`,
`InDate`,
`open`,
`high`,
`low`,
`close`,
`Volume`,
`Turnover`,
Percent,
Amount,
`datesort`,
UpLimitTime
)
VALUES
(
@stockcode,
@InDate,
@open,
@high,
@low,
@close,
@Volume,
@Turnover,
@Percent,
@Amount,
@datesort,
UpLimitTime);";

            foreach (var entity in list)
            {
                entity.InDate = entity.InDate.Value.Date;
            }
            SqlHelper.ExecuteNonQuery(sql,list);
        }


        private static void InsertRealTime(List<StockEntity> list)
        {
            var sql = @"INSERT INTO `svc`.`realTime`
(
`stockcode`,
`InDate`,
`open`,
`high`,
`low`,
`close`,
`Volume`,
`Turnover`,
Percent,
Amount)
VALUES
(
@stockcode,
@InDate,
@open,
@high,
@low,
@close,
@Volume,
@Turnover,
@Percent,
@Amount,
@UpLimitTime);";


            ListHelper.ForRange(list, slist => SqlHelper.ExecuteNonQuery(sql, slist));
        }

        public static List<StockEntity> GetDailyList(string stockCode)
        {
            var sql = @"
SELECT 
    `daily`.`stockcode`,
    `daily`.`InDate`,
    `daily`.`close`,
    `daily`.`open`,
    `daily`.`high`,
    `daily`.`low`,
    `daily`.`Turnover`,
    `daily`.`Percent`,
    `daily`.`Volume`,
    `daily`.`Amount`,
    `daily`.`datesort`
FROM `svc`.`daily`
where stockCode= @stockCode
order by indate
";
            return SqlHelper.GetList<StockEntity>(sql
                , SqlHelper.CreateParameter("stockCode", stockCode));
        }

        public static List<StockEntity> GetDailyList(DateTime day)
        {
            var sql = @"
SELECT 
    `daily`.`stockcode`,
    `daily`.`InDate`,
    `daily`.`close`,
    `daily`.`open`,
    `daily`.`high`,
    `daily`.`low`,
    `daily`.`Turnover`,
    `daily`.`Percent`,
    `daily`.`Volume`,
    `daily`.`Amount`,
    `daily`.`datesort`
FROM `svc`.`daily`
where Indate>= @day";
            return SqlHelper.GetList<StockEntity>(sql, day.Date);
        }

        public static DateTime? GetMaxDaily(string stockCode=null)
        {
            var sql = @"select max(indate) from svc.daily";
            if(!stockCode.IsNullOrEmpty())
            {
                sql = sql + string.Format(" where StockCode = '{0}'", stockCode);
            }
            return SqlHelper.GetEntity<DateTime>(sql);
        }
    }
}
