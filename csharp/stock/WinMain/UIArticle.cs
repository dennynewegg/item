using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockBiz;


namespace WinMain
{
    public partial class UIArticle : RichTextBox
    {
        public static List<StockEntity> Stocks ;
        public List<StockEntity> ArticleStock = new List<StockEntity>(20);

        public static void Ready()
        {
            if(Stocks == null)
            {
                Stocks = StockDAL.GetStockList();
            }
        }

        //public 

        public void Format()
        {
            ArticleStock = ArticleBiz.ParseArticle(this.Text, Stocks);
            ArticleStock.ForEach(stock =>
            {
                var index = 0;
                do
                {
                    index = this.Text.IndexOf(stock.StockName, index+1);
                    if (index > 0)
                    {
                        this.Select(index, stock.StockName.Length);
                        this.SelectionColor = Color.Red;
                        //ArticleStock.Add(stock);
                    }
                } while (index > 0);

                do
                {
                    index = this.Text.IndexOf(stock.StockCode, index + 1);
                    if (index > 0)
                    {
                        this.Select(index, stock.StockName.Length);
                        this.SelectionColor = Color.Red;
                        //ArticleStock.Add(stock);
                    }
                } while (index > 0);

            });
            if (ArticleStock.Count > 0)
            {
                ArticleStock = ArticleStock.Distinct().ToList();
                this.AppendText(Environment.NewLine);
                this.AppendText(Environment.NewLine);
                this.AppendText(ArticleStock.Select(stock => stock.StockName)
                    .Aggregate((a, b) => a + "," + b));
            }

        }
    }
}
