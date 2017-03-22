using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace StockBiz
{
    public static class ExcelHelper
    {
        public static DataSet ToDataSet(string path,string sheetName)
        {
            var builder = new DataTableBuilder();
            var list =  builder.ReadExcelAllSheets(path);
            //foreach(var mt in list)
            //{
            //    mt.
            //}


            return null;
        }
    }
}
