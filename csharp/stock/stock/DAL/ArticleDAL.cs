using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class ArticleDAL
    {
        public static void Insert(List<ArticleDTO> articles)
        {
            if (articles.IsEmpty()) { return; }

            if (articles.Count > 10)
            {
                ListHelper.ForRange(articles, Insert, 10);
                return;
            }

            var sql = @"INSERT INTO `svc`.`article`
(`title`,
`Url`,
`InDate`,
`ArticleType`,
`Content`)
VALUES
(@title,
@Url,
@InDate,
@ArticleType,
@Content); ";

            SqlHelper.ExecuteNonQuery(sql, articles);

        }

        public static ArticleDTO GetByUrl(string url)
        {
            var sql = @"SELECT `article`.`RowID`,
    `article`.`title`,
    `article`.`Url`,
    `article`.`InDate`,
    `article`.`ArticleType`
FROM `svc`.`article`
where url = @url
limit 1; ";
            return SqlHelper.GetEntity<ArticleDTO>(sql, 
                SqlHelper.CreateParameter("@url",url));
        }

        public static DateTime? GetMaxDate(string articleType)
        {
            var sql = string.Format("select max(indate) from svc.article where articleType like '{0}%' ;", articleType);
            return SqlHelper.GetEntity<DateTime?>(sql);
        }

        public static List<ArticleDTO> GetArticleMoreThanDate(DateTime date)
        {
            var sql = @"SELECT `article`.`ArticleID`,
    `article`.`title`,
    `article`.`Url`,
    `article`.`InDate`,
    `article`.`ArticleType`
FROM `svc`.`article`
where indate>=@date ";
            return SqlHelper.GetList<ArticleDTO>(sql,
                SqlHelper.CreateParameter("@date", date));
        }
        
    }
}
