using StockBiz.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class ReportDAL
    {
        public static List<ReportDTO> QueryHQ(List<string> stockCodes,DateTime? date=null)
        {

            var dateParam = SqlHelper.CreateParameter("@date", date.Value);
            var sql = @"SELECT 
            MIN(INDATE)
        FROM
            SVC.DAILY
        WHERE
            INDATE > @date";

            var minDate = SqlHelper.GetEntity<DateTime>(sql, dateParam);


            date = date.GetValueOrDefault(DateTime.Now);
            sql = @"
SELECT a.stockcode,b.stockname,a.Percent, c.Percent as NextPercent,a.Indate,a.Close,c.Open,c.close as NextClose
FROM SVC.Daily as a
left join svc.stock as b
on a.stockcode = b.stockcode 
left join SVC.Daily as c
on a.stockcode = c.stockcode and c.indate = @minDate
where a.InDate = (select max(indate) from svc.daily where INDATE<= @date)
and a.stockcode in ('@stockcodes')
order by percent desc
; ";
            sql = sql.Replace("@stockcodes",stockCodes.Aggregate((a,b)=>a+"','"+b));
            return SqlHelper.GetList<ReportDTO>(sql,
                SqlHelper.CreateParameter("@minDate", minDate),dateParam);
        }
    }
}
