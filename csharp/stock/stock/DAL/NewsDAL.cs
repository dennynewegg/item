using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockBiz.Entity;

namespace StockBiz
{
    public static class NewsDAL
    {
        public static List<StockNewsEntity> GetStockNews(string stockCode, string url)
        {
            var sql = @"select * from svc.news 
where stockcode = @stockcode and url = @url;";
            return SqlHelper.GetList<StockNewsEntity>(sql
                , SqlHelper.CreateParameter("@stockCode",stockCode)
                , SqlHelper.CreateParameter("@url",url));

        }


        public static void Insert(List<StockNewsEntity> paramList)
        {
            var sql = @"INSERT INTO `svc`.`news`
(
`stockcode`,
`stockname`,
`indate`,
`pubDate`,
`CategoryID`,
`CategoryName`,
`Memo`,
`Url`,
`Title`)
VALUES
(
@stockcode,
@stockname,
@indate,
@pubDate,
@CategoryID,
@CategoryName,
@Memo,
@Url,
@Title);
";
            SqlHelper.ExecuteNonQuery(sql,paramList);
        }

    }
}
