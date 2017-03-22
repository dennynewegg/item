using StockBiz;

using System;
using System.Windows.Forms;

namespace WinMain
{
    public partial class ArticleListForm : Form
    {
        public ArticleListForm()
        {
            InitializeComponent();
            dgvArticle.AutoGenerateColumns = false;
        }

        private void ArticleListForm_Load(object sender, EventArgs e)
        {
            //var articleList = TaogulaBiz.GetArticleList();
            //this.dgvArticle.DataSource = articleList;
        }

        private void dgvArticle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                var item =  this.dgvArticle.Rows[e.RowIndex].DataBoundItem as ArticleDTO;
                if(item != null)
                {
                    var frm = new ArticleForm();
                    frm.ArticleInfo = item;
                    frm.Text = item.Title;
                    frm.Show();
                }
            }
        }
    }
}
