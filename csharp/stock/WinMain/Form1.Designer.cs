namespace WinMain
{
    partial class Form1
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
            this.chkLastTrade.Location = new System.Drawing.Point(90, 12);
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
            this.ckbPlanFinance.Location = new System.Drawing.Point(168, 12);
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
            this.ckbFinance.Location = new System.Drawing.Point(246, 12);
            this.ckbFinance.Name = "ckbFinance";
            this.ckbFinance.Size = new System.Drawing.Size(72, 16);
            this.ckbFinance.TabIndex = 1;
            this.ckbFinance.Text = "财务数据";
            this.ckbFinance.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 466);
            this.Controls.Add(this.txbLog);
            this.Controls.Add(this.ckbFinance);
            this.Controls.Add(this.ckbPlanFinance);
            this.Controls.Add(this.chkLastTrade);
            this.Controls.Add(this.ckbHistoryDailyTrade);
            this.Controls.Add(this.btnExecute);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

