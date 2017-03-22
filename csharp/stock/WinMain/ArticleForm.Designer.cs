namespace WinMain
{
    partial class ArticleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpTotal = new System.Windows.Forms.TableLayoutPanel();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnSection = new System.Windows.Forms.Button();
            this.rtbOrgArticle = new WinMain.UIArticle();
            this.hqList = new WinMain.UIHQ();
            this.tlpTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTotal
            // 
            this.tlpTotal.ColumnCount = 5;
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTotal.Controls.Add(this.rtbOrgArticle, 0, 0);
            this.tlpTotal.Controls.Add(this.btnCategory, 1, 1);
            this.tlpTotal.Controls.Add(this.btnSection, 3, 1);
            this.tlpTotal.Controls.Add(this.hqList, 3, 0);
            this.tlpTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTotal.Location = new System.Drawing.Point(0, 0);
            this.tlpTotal.Name = "tlpTotal";
            this.tlpTotal.RowCount = 2;
            this.tlpTotal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTotal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpTotal.Size = new System.Drawing.Size(982, 494);
            this.tlpTotal.TabIndex = 0;
            // 
            // btnCategory
            // 
            this.btnCategory.Location = new System.Drawing.Point(404, 467);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(74, 23);
            this.btnCategory.TabIndex = 1;
            this.btnCategory.Text = "Category";
            this.btnCategory.UseVisualStyleBackColor = true;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // btnSection
            // 
            this.btnSection.Location = new System.Drawing.Point(504, 467);
            this.btnSection.Name = "btnSection";
            this.btnSection.Size = new System.Drawing.Size(74, 23);
            this.btnSection.TabIndex = 2;
            this.btnSection.Text = "Section";
            this.btnSection.UseVisualStyleBackColor = true;
            this.btnSection.Click += new System.EventHandler(this.btnSection_Click);
            // 
            // rtbOrgArticle
            // 
            this.tlpTotal.SetColumnSpan(this.rtbOrgArticle, 2);
            this.rtbOrgArticle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOrgArticle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbOrgArticle.Location = new System.Drawing.Point(3, 3);
            this.rtbOrgArticle.Name = "rtbOrgArticle";
            this.rtbOrgArticle.Size = new System.Drawing.Size(475, 458);
            this.rtbOrgArticle.TabIndex = 0;
            this.rtbOrgArticle.Text = "Test";
            // 
            // hqList
            // 
            this.tlpTotal.SetColumnSpan(this.hqList, 2);
            this.hqList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hqList.Location = new System.Drawing.Point(504, 3);
            this.hqList.Name = "hqList";
            this.hqList.Size = new System.Drawing.Size(475, 458);
            this.hqList.TabIndex = 3;
            // 
            // ArticleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 494);
            this.Controls.Add(this.tlpTotal);
            this.Name = "ArticleForm";
            this.Text = "ArticleForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ArticleForm_Load);
            this.tlpTotal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpTotal;
        private UIArticle rtbOrgArticle;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Button btnSection;
        private UIHQ hqList;
    }
}