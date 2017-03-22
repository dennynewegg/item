using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.DAL
{
    public static class TradeTranDAL
    {
        public static void Insert(List<TradeTranDTO> list)
        {
            var sql = @"INSERT INTO `svc`.`tradetran`
(`stockcode`,
`indate`,
`detail`)
VALUES
(@stockcode,
@indate,
@detail);
";
            SqlHelper.ExecuteNonQuery(sql, list);
        }

        public static List<TradeTranDTO> GetTradeTran(string stockcode, DateTime from,DateTime? to=null)
        {
            to = to.GetValueOrDefault(DateTime.Now).Date;
            var sql = @"SELECT 
                        `tradetran`.`stockcode`,
                        `tradetran`.`indate`,
                        `tradetran`.`detail`
                        FROM   `svc`.`tradetran`;
                        where stockcode = @stockcode
                        and indate>= @from
                        and indate<= @to;";
            return SqlHelper.GetList<TradeTranDTO>(sql
                , SqlHelper.CreateParameter("stockcode", stockcode)
                , SqlHelper.CreateParameter("@from", from)
                , SqlHelper.CreateParameter("@to", to));

        }

    }
}
