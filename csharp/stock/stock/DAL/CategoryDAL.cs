using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class CategoryDAL
    {
        public static List<CategoryStockDTO> GetCategoryList()
        {
            var sql = "Select * from svc.category;";
            return SqlHelper.GetList<CategoryStockDTO>(sql);

        }

        public static void Insert(List<CategoryStockDTO> list)
        {
            var sql = @"INSERT INTO `svc`.`category`
(
`CategoryName`,
`StockCode`,
`StockName`,
`Source`)
VALUES
(
@CategoryName,
@StockCode,
@StockName,
@Source); ";
            SqlHelper.ExecuteNonQuery(sql, list);
        }

    }
}
