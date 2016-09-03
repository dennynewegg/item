using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockBiz
{
    public static class DailyDAL
    {
        public static void Insert(List<StockEntity> list)
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


            ListHelper.ForRange(list,slist => SqlHelper.ExecuteNonQuery(sql, slist));
        }



    }
}
