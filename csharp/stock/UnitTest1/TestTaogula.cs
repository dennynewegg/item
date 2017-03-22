using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockBiz;
using System.Data;

namespace UnitTest
{
    [TestClass]
    public  class TestTaogula
    {
        [TestMethod]
        public void TestAr()
        {
            DataSet ds = ExcelHelper.ToDataSet(@"D:\pro\Release\Finance\000587.xls", "new-finance-data");

            var td = ds.Tables[0];

        }
    }
}
