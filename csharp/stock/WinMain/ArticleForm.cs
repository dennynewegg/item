using StockBiz;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMain
{
    public partial class ArticleForm : Form
    {
        public ArticleDTO ArticleInfo;


        public ArticleForm()
        {
            InitializeComponent();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            rtbOrgArticle.SelectionColor = Color.Red;
        }

        private void btnSection_Click(object sender, EventArgs e)
        {
             //var startIndex  =  rtbOrgArticle.get;
           
        }

        private void ArticleForm_Load(object sender, EventArgs e)
        {
            UIArticle.Ready();

            if (ArticleInfo != null
                &&!string.IsNullOrWhiteSpace(ArticleInfo.Url))
            {
                //object urlList = null; // TaogulaBiz.GetArticleList();
                rtbOrgArticle.Text = TaogulaBiz.GetArticle(ArticleInfo.Url);
                rtbOrgArticle.Format();
                if (rtbOrgArticle.ArticleStock.Count > 0)
                {
                    hqList.QueryHQ(rtbOrgArticle.ArticleStock.Select(stock => stock.StockCode).ToList()
                        ,ArticleInfo.InDate.GetValueOrDefault(DateTime.Now));
                }
            }
        }
    }
}
