namespace WinMain
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExecute = new System.Windows.Forms.Button();
            this.ckbHistoryDailyTrade = new System.Windows.Forms.CheckBox();
            this.txbLog = new System.Windows.Forms.TextBox();
            this.chkLastTrade = new System.Windows.Forms.CheckBox();
            this.ckbPlanFinance = new System.Windows.Forms.CheckBox();
            this.ckbFinance = new System.Windows.Forms.CheckBox();
            this.ckNews = new System.Windows.Forms.CheckBox();
            this.ckTaogula = new System.Windows.Forms.CheckBox();
            this.ckLimitSell = new System.Windows.Forms.CheckBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.ckLonghu = new System.Windows.Forms.CheckBox();
            this.ckCategory = new System.Windows.Forms.CheckBox();
            this.ckArticle = new System.Windows.Forms.CheckBox();
            this.btnChart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(12, 34);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "执行同步";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // ckbHistoryDailyTrade
            // 
            this.ckbHistoryDailyTrade.AutoSize = true;
            this.ckbHistoryDailyTrade.Location = new System.Drawing.Point(12, 12);
            this.ckbHistoryDailyTrade.Name = "ckbHistoryDailyTrade";
            this.ckbHistoryDailyTrade.Size = new System.Drawing.Size(72, 16);
            this.ckbHistoryDailyTrade.TabIndex = 1;
            this.ckbHistoryDailyTrade.Text = "历史成交";
            this.ckbHistoryDailyTrade.UseVisualStyleBackColor = true;
            // 
            // txbLog
            // 
            this.txbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbLog.Location = new System.Drawing.Point(12, 63);
            this.txbLog.MaxLength = 76532767;
            this.txbLog.Multiline = true;
            this.txbLog.Name = "txbLog";
            this.txbLog.ReadOnly = true;
            this.txbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbLog.Size = new System.Drawing.Size(830, 391);
            this.txbLog.TabIndex = 2;
            // 
            // chkLastTrade
            // 
            this.chkLastTrade.AutoSize = true;
            this.chkLastTrade.Checked = true;
            this.chkLastTrade.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLastTrade.Location = new System.Drawing.Point(91, 12);
            this.chkLastTrade.Name = "chkLastTrade";
            this.chkLastTrade.Size = new System.Drawing.Size(72, 16);
            this.chkLastTrade.TabIndex = 1;
            this.chkLastTrade.Text = "最新交易";
            this.chkLastTrade.UseVisualStyleBackColor = true;
            // 
            // ckbPlanFinance
            // 
            this.ckbPlanFinance.AutoSize = true;
            this.ckbPlanFinance.Checked = true;
            this.ckbPlanFinance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbPlanFinance.Location = new System.Drawing.Point(340, 12);
            this.ckbPlanFinance.Name = "ckbPlanFinance";
            this.ckbPlanFinance.Size = new System.Drawing.Size(72, 16);
            this.ckbPlanFinance.TabIndex = 1;
            this.ckbPlanFinance.Text = "业绩预告";
            this.ckbPlanFinance.UseVisualStyleBackColor = true;
            // 
            // ckbFinance
            // 
            this.ckbFinance.AutoSize = true;
            this.ckbFinance.Checked = true;
            this.ckbFinance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbFinance.Location = new System.Drawing.Point(419, 12);
            this.ckbFinance.Name = "ckbFinance";
            this.ckbFinance.Size = new System.Drawing.Size(72, 16);
            this.ckbFinance.TabIndex = 1;
            this.ckbFinance.Text = "财务数据";
            this.ckbFinance.UseVisualStyleBackColor = true;
            // 
            // ckNews
            // 
            this.ckNews.AutoSize = true;
            this.ckNews.Checked = true;
            this.ckNews.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckNews.Location = new System.Drawing.Point(170, 12);
            this.ckNews.Name = "ckNews";
            this.ckNews.Size = new System.Drawing.Size(72, 16);
            this.ckNews.TabIndex = 3;
            this.ckNews.Text = "涨停原因";
            this.ckNews.UseVisualStyleBackColor = true;
            // 
            // ckTaogula
            // 
            this.ckTaogula.AutoSize = true;
            this.ckTaogula.Checked = true;
            this.ckTaogula.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckTaogula.Location = new System.Drawing.Point(249, 12);
            this.ckTaogula.Name = "ckTaogula";
            this.ckTaogula.Size = new System.Drawing.Size(84, 16);
            this.ckTaogula.TabIndex = 3;
            this.ckTaogula.Text = "淘股啦复盘";
            this.ckTaogula.UseVisualStyleBackColor = true;
            // 
            // ckLimitSell
            // 
            this.ckLimitSell.AutoSize = true;
            this.ckLimitSell.Location = new System.Drawing.Point(498, 12);
            this.ckLimitSell.Name = "ckLimitSell";
            this.ckLimitSell.Size = new System.Drawing.Size(72, 16);
            this.ckLimitSell.TabIndex = 3;
            this.ckLimitSell.Text = "限售解禁";
            this.ckLimitSell.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(647, 33);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Unit Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // ckLonghu
            // 
            this.ckLonghu.AutoSize = true;
            this.ckLonghu.Checked = true;
            this.ckLonghu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckLonghu.Location = new System.Drawing.Point(249, 37);
            this.ckLonghu.Name = "ckLonghu";
            this.ckLonghu.Size = new System.Drawing.Size(60, 16);
            this.ckLonghu.TabIndex = 3;
            this.ckLonghu.Text = "龙虎榜";
            this.ckLonghu.UseVisualStyleBackColor = true;
            // 
            // ckCategory
            // 
            this.ckCategory.AutoSize = true;
            this.ckCategory.Location = new System.Drawing.Point(340, 37);
            this.ckCategory.Name = "ckCategory";
            this.ckCategory.Size = new System.Drawing.Size(48, 16);
            this.ckCategory.TabIndex = 3;
            this.ckCategory.Text = "概念";
            this.ckCategory.UseVisualStyleBackColor = true;
            // 
            // ckArticle
            // 
            this.ckArticle.AutoSize = true;
            this.ckArticle.Checked = true;
            this.ckArticle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckArticle.Location = new System.Drawing.Point(170, 37);
            this.ckArticle.Name = "ckArticle";
            this.ckArticle.Size = new System.Drawing.Size(48, 16);
            this.ckArticle.TabIndex = 6;
            this.ckArticle.Text = "文章";
            this.ckArticle.UseVisualStyleBackColor = true;
            // 
            // btnChart
            // 
            this.btnChart.Location = new System.Drawing.Point(541, 33);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(75, 23);
            this.btnChart.TabIndex = 7;
            this.btnChart.Text = "Chart";
            this.btnChart.UseVisualStyleBackColor = true;
            this.btnChart.Click += new System.EventHandler(this.btnChart_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 466);
            this.Controls.Add(this.btnChart);
            this.Controls.Add(this.ckArticle);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.ckCategory);
            this.Controls.Add(this.ckLonghu);
            this.Controls.Add(this.ckLimitSell);
            this.Controls.Add(this.ckTaogula);
            this.Controls.Add(this.ckNews);
            this.Controls.Add(this.txbLog);
            this.Controls.Add(this.ckbFinance);
            this.Controls.Add(this.ckbPlanFinance);
            this.Controls.Add(this.chkLastTrade);
            this.Controls.Add(this.ckbHistoryDailyTrade);
            this.Controls.Add(this.btnExecute);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.CheckBox ckbHistoryDailyTrade;
        private System.Windows.Forms.TextBox txbLog;
        private System.Windows.Forms.CheckBox chkLastTrade;
        private System.Windows.Forms.CheckBox ckbPlanFinance;
        private System.Windows.Forms.CheckBox ckbFinance;
        private System.Windows.Forms.CheckBox ckNews;
        private System.Windows.Forms.CheckBox ckTaogula;
        private System.Windows.Forms.CheckBox ckLimitSell;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.CheckBox ckLonghu;
        private System.Windows.Forms.CheckBox ckCategory;
        private System.Windows.Forms.CheckBox ckArticle;
        private System.Windows.Forms.Button btnChart;
    }
}

