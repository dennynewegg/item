using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz.DAL
{
   public static class StockDAL
    {
        public static void InsertStock(List<StockEntity> list)
        {
            var sql = "INSERT INTO svc.stock(stockcode,stockname) value(@stockcode,@stockname);";
            SqlHelper.ExecuteNonQuery(sql,list);
        }

        public static List<StockEntity> GetStockList()
        {
            var sql = "select * from svc.stock;";
            return SqlHelper.GetList<StockEntity>(sql);
        }

    }
}
