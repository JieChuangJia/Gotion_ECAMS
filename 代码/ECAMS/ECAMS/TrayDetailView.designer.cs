namespace ECAMS
{
    partial class TrayDetailView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_CoreDetail = new System.Windows.Forms.DataGridView();
            this.trayCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patchType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coreCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CoreDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_CoreDetail);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(591, 327);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电芯详情";
            // 
            // dgv_CoreDetail
            // 
            this.dgv_CoreDetail.AllowUserToAddRows = false;
            this.dgv_CoreDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CoreDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trayCode,
            this.patch,
            this.patchType,
            this.coreCode});
            this.dgv_CoreDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_CoreDetail.Location = new System.Drawing.Point(3, 17);
            this.dgv_CoreDetail.MultiSelect = false;
            this.dgv_CoreDetail.Name = "dgv_CoreDetail";
            this.dgv_CoreDetail.RowHeadersVisible = false;
            this.dgv_CoreDetail.RowTemplate.Height = 23;
            this.dgv_CoreDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_CoreDetail.Size = new System.Drawing.Size(585, 307);
            this.dgv_CoreDetail.TabIndex = 0;
            // 
            // trayCode
            // 
            this.trayCode.HeaderText = "料框条码";
            this.trayCode.Name = "trayCode";
            // 
            // patch
            // 
            this.patch.HeaderText = "批次";
            this.patch.Name = "patch";
            // 
            // patchType
            // 
            this.patchType.HeaderText = "批次类型";
            this.patchType.Name = "patchType";
            // 
            // coreCode
            // 
            this.coreCode.HeaderText = "电芯条码";
            this.coreCode.Name = "coreCode";
            // 
            // TrayDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 327);
            this.Controls.Add(this.groupBox1);
            this.Name = "TrayDetailView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "料框详细";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CoreDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_CoreDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn trayCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn patch;
        private System.Windows.Forms.DataGridViewTextBoxColumn patchType;
        private System.Windows.Forms.DataGridViewTextBoxColumn coreCode;
    }
}