using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class LonghuDAL
    {
        public static void Insert(List<LonghuDTO> list)
        {
            var sql = @"INSERT INTO `svc`.`longhu`
(
`stockcode`,
`stockname`,
`comname`,
`buyamount`,
`sellamount`,
`netamount`,
`Indate`)
VALUES
(
@stockcode,
@stockname,
@comname,
@buyamount,
@sellamount,
@netamount,
@Indate);
";
            SqlHelper.ExecuteNonQuery(sql, list);
        }

        public static List<LonghuDTO> GetLonghu(DateTime date)
        {
            var sql = @"SELECT `longhu`.`rowid`,
    `longhu`.`stockcode`,
    `longhu`.`stockname`,
    `longhu`.`comname`,
    `longhu`.`buyamount`,
    `longhu`.`sellamount`,
    `longhu`.`netamount`,
    `longhu`.`Indate`
FROM `svc`.`longhu`
where indate  >= @date
;
";
            return SqlHelper.GetList<LonghuDTO>(sql
                , SqlHelper.CreateParameter("@date", date));

        }

        public static DateTime? GetMaxDate()
        {
            var sql = @"SELECT max(indate)
FROM `svc`.`longhu`;
            ";
            return SqlHelper.GetEntity<DateTime?>(sql);
        }

    }
}
