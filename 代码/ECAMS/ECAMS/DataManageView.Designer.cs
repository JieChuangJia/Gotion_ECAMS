namespace ECAMS
{
    partial class DataManageView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataManageView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_DataDetail = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_endTime = new System.Windows.Forms.DateTimePicker();
            this.dtp_startTime = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscb_dataList = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Query = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_FormatSystem = new System.Windows.Forms.ToolStripButton();
            this.tsb_BackUp = new System.Windows.Forms.ToolStripButton();
            this.tsb_Recover = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Exit = new System.Windows.Forms.ToolStripButton();
            this.gb_ = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataDetail)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.gb_.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgv_DataDetail);
            this.groupBox1.Location = new System.Drawing.Point(0, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 398);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据详细";
            // 
            // dgv_DataDetail
            // 
            this.dgv_DataDetail.AllowUserToAddRows = false;
            this.dgv_DataDetail.AllowUserToDeleteRows = false;
            this.dgv_DataDetail.AllowUserToResizeRows = false;
            this.dgv_DataDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DataDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DataDetail.Location = new System.Drawing.Point(3, 17);
            this.dgv_DataDetail.Name = "dgv_DataDetail";
            this.dgv_DataDetail.RowHeadersVisible = false;
            this.dgv_DataDetail.RowTemplate.Height = 23;
            this.dgv_DataDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DataDetail.Size = new System.Drawing.Size(898, 378);
            this.dgv_DataDetail.TabIndex = 0;
            this.dgv_DataDetail.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DataDetail_CellEndEdit);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "结束时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "开始时间：";
            // 
            // dtp_endTime
            // 
            this.dtp_endTime.Location = new System.Drawing.Point(317, 20);
            this.dtp_endTime.Name = "dtp_endTime";
            this.dtp_endTime.Size = new System.Drawing.Size(155, 21);
            this.dtp_endTime.TabIndex = 5;
            // 
            // dtp_startTime
            // 
            this.dtp_startTime.Location = new System.Drawing.Point(83, 20);
            this.dtp_startTime.Name = "dtp_startTime";
            this.dtp_startTime.Size = new System.Drawing.Size(155, 21);
            this.dtp_startTime.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscb_dataList,
            this.toolStripSeparator1,
            this.tsb_Query,
            this.tsb_Save,
            this.tsb_Delete,
            this.toolStripSeparator2,
            this.tsb_FormatSystem,
            this.tsb_BackUp,
            this.tsb_Recover,
            this.toolStripSeparator3,
            this.tsb_Exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(904, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "数据表：";
            // 
            // tscb_dataList
            // 
            this.tscb_dataList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_dataList.Name = "tscb_dataList";
            this.tscb_dataList.Size = new System.Drawing.Size(121, 25);
            this.tscb_dataList.SelectedIndexChanged += new System.EventHandler(this.tscb_dataList_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_Query
            // 
            this.tsb_Query.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Query.Image")));
            this.tsb_Query.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Query.Name = "tsb_Query";
            this.tsb_Query.Size = new System.Drawing.Size(52, 22);
            this.tsb_Query.Text = "查询";
            this.tsb_Query.Click += new System.EventHandler(this.tsb_Query_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(52, 22);
            this.tsb_Save.Text = "保存";
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // tsb_Delete
            // 
            this.tsb_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Delete.Image")));
            this.tsb_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Delete.Name = "tsb_Delete";
            this.tsb_Delete.Size = new System.Drawing.Size(52, 22);
            this.tsb_Delete.Text = "删除";
            this.tsb_Delete.Click += new System.EventHandler(this.tsb_Delete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_FormatSystem
            // 
            this.tsb_FormatSystem.Image = ((System.Drawing.Image)(resources.GetObject("tsb_FormatSystem.Image")));
            this.tsb_FormatSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_FormatSystem.Name = "tsb_FormatSystem";
            this.tsb_FormatSystem.Size = new System.Drawing.Size(76, 22);
            this.tsb_FormatSystem.Text = "出厂设置";
            this.tsb_FormatSystem.Click += new System.EventHandler(this.tsb_FormatSystem_Click);
            // 
            // tsb_BackUp
            // 
            this.tsb_BackUp.Image = ((System.Drawing.Image)(resources.GetObject("tsb_BackUp.Image")));
            this.tsb_BackUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_BackUp.Name = "tsb_BackUp";
            this.tsb_BackUp.Size = new System.Drawing.Size(88, 22);
            this.tsb_BackUp.Text = "数据库备份";
            this.tsb_BackUp.Click += new System.EventHandler(this.tsb_BackUp_Click);
            // 
            // tsb_Recover
            // 
            this.tsb_Recover.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Recover.Image")));
            this.tsb_Recover.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Recover.Name = "tsb_Recover";
            this.tsb_Recover.Size = new System.Drawing.Size(88, 22);
            this.tsb_Recover.Text = "数据库恢复";
            this.tsb_Recover.Click += new System.EventHandler(this.tsb_Recover_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_Exit
            // 
            this.tsb_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Exit.Image")));
            this.tsb_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Exit.Name = "tsb_Exit";
            this.tsb_Exit.Size = new System.Drawing.Size(52, 22);
            this.tsb_Exit.Text = "退出";
            this.tsb_Exit.Click += new System.EventHandler(this.tsb_Exit_Click);
            // 
            // gb_
            // 
            this.gb_.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_.Controls.Add(this.dtp_endTime);
            this.gb_.Controls.Add(this.dtp_startTime);
            this.gb_.Controls.Add(this.label5);
            this.gb_.Controls.Add(this.label4);
            this.gb_.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gb_.Location = new System.Drawing.Point(2, 28);
            this.gb_.Name = "gb_";
            this.gb_.Size = new System.Drawing.Size(901, 53);
            this.gb_.TabIndex = 2;
            this.gb_.TabStop = false;
            this.gb_.Text = "条件";
            // 
            // DataManageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 486);
            this.Controls.Add(this.gb_);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DataManageView";
            this.Text = "数据管理";
            this.Load += new System.EventHandler(this.DataManageView_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataDetail)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gb_.ResumeLayout(false);
            this.gb_.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_DataDetail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_endTime;
        private System.Windows.Forms.DateTimePicker dtp_startTime;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_Query;
        private System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton tsb_Delete;
        private System.Windows.Forms.ToolStripButton tsb_Exit;
        private System.Windows.Forms.GroupBox gb_;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscb_dataList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_FormatSystem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsb_BackUp;
        private System.Windows.Forms.ToolStripButton tsb_Recover;
    }
}