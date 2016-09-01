using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


namespace stock
{
    [TestFixture]
    public static class TestSqlHelper
    {
        [Test]
        public static void TestCreateParameter()
        {
            string sql = "insert into svc.stock(stockcode,stockname) value(@stockcode,@stockname);";
            sql = sql.ToUpper();
            SqlHelper.ExecuteNonQuery(sql
                , new {StockCode="020501",StockName="中文支持"}
                , new { StockCode = "020501", StockName = "中文支持" }
                , new { StockCode = "020501", StockName = "中文支持" }
                , new { StockCode = "020501", StockName = "中文支持" }
                , new { StockCode = "020501", StockName = "中文支持" });

        }
    }
}
