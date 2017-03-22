using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class ZhubiDAL
    {
        public static ZhubiDTO GetZhubi(string code,DateTime date)
        {
            var sql = @"SELECT StockCode,Indate, `Xls`
FROM `svc`.`zhubi`
where stockcode = @StockCode and InDate = @InDate ";

            return SqlHelper.GetEntity<ZhubiDTO>(sql,
                SqlHelper.CreateParameter("@StockCode", StockHelper.GetShortCode(code))
                , SqlHelper.CreateParameter("@InDate", date.ToString("yyyy-MM-dd")));

        }

        public static void Insert(ZhubiDTO dto)
        {
            
            var sql = @"INSERT INTO `svc`.`zhubi`
(`StockCode`,
`InDate`,
`xls`)
VALUES
(@StockCode,
@InDate,
@Xls);";
            SqlHelper.ExecuteNonQuery(sql, dto);
        }
    }
}
