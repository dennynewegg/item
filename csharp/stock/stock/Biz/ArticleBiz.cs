using StockBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBiz
{
    public static class ArticleBiz
    {
        public static List<StockEntity> ParseArticle(string content,List<StockEntity> stockList=null)
        {
            if(stockList == null)
            {
                stockList = StockDAL.GetStockList();
            }

            return stockList.Where(stock => 
            content.Contains(stock.StockName)
            || content.Contains(stock.StockCode)).ToList();
        }       

        //public static  List<ArticleDTO> ParseArticle(ArticleDTO article,List<StockEntity> stockList)

    }
}
