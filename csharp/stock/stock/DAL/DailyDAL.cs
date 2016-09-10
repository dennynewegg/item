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
                InsertDaily(dailyList);
            }
            if (!realTime.IsNullOrEmpty())
            {
                InsertRealTime(realTime);
            }

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
`datesort`)
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
@datesort);";

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
@Amount);";


            ListHelper.ForRange(list, slist => SqlHelper.ExecuteNonQuery(sql, slist));
        }

        public static List<StockEntity> GetDailyList(DateTime day)
        {
            var sql = @"
SELECT `daily`.`rowid`,
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

    }
}
