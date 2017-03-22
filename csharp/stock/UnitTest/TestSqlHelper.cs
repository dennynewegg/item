using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StockBiz;
using StockBiz;


namespace stock
{
    [TestFixture]
    public static class TestSqlHelper
    {
        [Test]
        public static void TestCreateParameter()
        {
            string sql = "insert into svc.stock(stockcode,stockname,publicdate) value(@stockcode,@stockname,@publicdate);";
            var list = new List<StockCodeEntity>(1000);
            for (int i = 0; i < list.Capacity; i++)
            {
                list.Add(new StockCodeEntity()
                {
                    StockCode = (200+i).ToString().PadLeft(6,'0'),
                    StockName = "中文"+i,
                    Publicdate = DateTime.Now
                });
            }
            SqlHelper.ExecuteNonQuery(sql, list);
        }


        [Test]
        public static void TestGetString()
        {
            var sql = "select stockcode,stockname from svc.stock;";
            var list = SqlHelper.GetList<string>(sql);
            Console.WriteLine(list.Count);
        }

        [Test]
        public static void TestGetEntity()
        {
            var sql = "select * from svc.stock;";
            var list = SqlHelper.GetList<StockCodeEntity>(sql);
            Console.WriteLine(list.Count);
        }

        public class StockCodeEntity
        {
            public string StockCode { get; set; }
            public string StockName { get; set; }
            public DateTime Publicdate { get; set; }
        }

        [Test]
        public static void TestUpdate()
        {
            var sql = @"
update stock
set publicdate=now();";
            SqlHelper.ExecuteNonQuery(sql);
        }

        [Test]
        public static void TestGetHQFrom163()
        {
            var list = WYStockBiz.GetTradeList();
            if (list.Count > 0)
            {
               DailyDAL.Insert(list);
            }
            Console.WriteLine(list.Count);
        }

        [Test]
        public static void TestPlanFinance()
        {
            var list = WYStockBiz.GetPlanFinance(DateTime.Now.AddDays(-10));
            Console.WriteLine(list.Count);
        }

        [Test]
        public static void Testsina()
        {
            var list = SinaBiz.GetTradeList(2);
            if (list.Count>0)
            {
                DailyDAL.Insert(list);
            }
           
            Console.WriteLine(list.Count);
        }

        [Test]
        public static void TestHistoryDate()
        {
            var stocklist  = WYStockBiz.GetTradeList();
            foreach (var stockEntity in stocklist)
            {
                var list = WYStockBiz.HistoryTradeList(stockEntity.StockCode, DateTime.Now.AddYears(-1), DateTime.Now);
                if (list.Count > 0)
                {
                    DailyDAL.Insert(list);
                }
                Console.WriteLine(list.Count);
            }
        }

        [Test]
        public static void TestReport()
        {
            var stockList = 
            ReportDAL.QueryHQ(new List<string>() { "600015" });
        }

    }
}
