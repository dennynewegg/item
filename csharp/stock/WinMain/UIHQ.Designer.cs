namespace WinMain
{
    partial class UIHQ
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvHQ = new System.Windows.Forms.DataGridView();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrentPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNextPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHQ)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHQ
            // 
            this.dgvHQ.AllowUserToAddRows = false;
            this.dgvHQ.AllowUserToDeleteRows = false;
            this.dgvHQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHQ.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCode,
            this.colName,
            this.colDate,
            this.colCurrentPrice,
            this.colPercent,
            this.colNextPercent});
            this.dgvHQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHQ.Location = new System.Drawing.Point(0, 0);
            this.dgvHQ.Name = "dgvHQ";
            this.dgvHQ.RowHeadersWidth = 20;
            this.dgvHQ.RowTemplate.Height = 23;
            this.dgvHQ.Size = new System.Drawing.Size(873, 472);
            this.dgvHQ.TabIndex = 0;
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "StockCode";
            this.colCode.HeaderText = "code";
            this.colCode.Name = "colCode";
            // 
            // colName
            // 
            this.colName.DataPropertyName = "StockName";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            // 
            // colDate
            // 
            this.colDate.DataPropertyName = "InDate";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd";
            this.colDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            // 
            // colCurrentPrice
            // 
            this.colCurrentPrice.DataPropertyName = "Close";
            this.colCurrentPrice.HeaderText = "Close";
            this.colCurrentPrice.Name = "colCurrentPrice";
            this.colCurrentPrice.Width = 50;
            // 
            // colPercent
            // 
            this.colPercent.DataPropertyName = "Percent";
            this.colPercent.HeaderText = "Current%";
            this.colPercent.Name = "colPercent";
            this.colPercent.Width = 60;
            // 
            // colNextPercent
            // 
            this.colNextPercent.DataPropertyName = "NextPercent";
            this.colNextPercent.HeaderText = "Next%";
            this.colNextPercent.Name = "colNextPercent";
            this.colNextPercent.Width = 50;
            // 
            // UIHQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvHQ);
            this.Name = "UIHQ";
            this.Size = new System.Drawing.Size(873, 472);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHQ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrentPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNextPercent;
    }
}
