namespace ECAMS
{
    partial class LogView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_LikeQuery = new System.Windows.Forms.CheckBox();
            this.tb_LikeQueryTxt = new System.Windows.Forms.TextBox();
            this.bt_ExportTxt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_logType = new System.Windows.Forms.ComboBox();
            this.cb_logCategory = new System.Windows.Forms.ComboBox();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.bt_QueryLog = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_EndTime = new System.Windows.Forms.DateTimePicker();
            this.dtp_StartTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_logDetail = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_logDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.cb_LikeQuery);
            this.panel1.Controls.Add(this.tb_LikeQueryTxt);
            this.panel1.Controls.Add(this.bt_ExportTxt);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cb_logType);
            this.panel1.Controls.Add(this.cb_logCategory);
            this.panel1.Controls.Add(this.bt_Exit);
            this.panel1.Controls.Add(this.bt_QueryLog);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtp_EndTime);
            this.panel1.Controls.Add(this.dtp_StartTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 60);
            this.panel1.TabIndex = 2;
            // 
            // cb_LikeQuery
            // 
            this.cb_LikeQuery.AutoSize = true;
            this.cb_LikeQuery.Location = new System.Drawing.Point(458, 16);
            this.cb_LikeQuery.Name = "cb_LikeQuery";
            this.cb_LikeQuery.Size = new System.Drawing.Size(72, 16);
            this.cb_LikeQuery.TabIndex = 23;
            this.cb_LikeQuery.Text = "模糊查询";
            this.cb_LikeQuery.UseVisualStyleBackColor = true;
            // 
            // tb_LikeQueryTxt
            // 
            this.tb_LikeQueryTxt.Location = new System.Drawing.Point(529, 12);
            this.tb_LikeQueryTxt.Multiline = true;
            this.tb_LikeQueryTxt.Name = "tb_LikeQueryTxt";
            this.tb_LikeQueryTxt.Size = new System.Drawing.Size(130, 43);
            this.tb_LikeQueryTxt.TabIndex = 22;
            // 
            // bt_ExportTxt
            // 
            this.bt_ExportTxt.Location = new System.Drawing.Point(665, 32);
            this.bt_ExportTxt.Name = "bt_ExportTxt";
            this.bt_ExportTxt.Size = new System.Drawing.Size(103, 23);
            this.bt_ExportTxt.TabIndex = 20;
            this.bt_ExportTxt.Text = "导出文本";
            this.bt_ExportTxt.UseVisualStyleBackColor = true;
            this.bt_ExportTxt.Click += new System.EventHandler(this.bt_ExportTxt_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "日志类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "日志类别：";
            // 
            // cb_logType
            // 
            this.cb_logType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_logType.FormattingEnabled = true;
            this.cb_logType.Location = new System.Drawing.Point(329, 34);
            this.cb_logType.Name = "cb_logType";
            this.cb_logType.Size = new System.Drawing.Size(121, 20);
            this.cb_logType.TabIndex = 17;
            // 
            // cb_logCategory
            // 
            this.cb_logCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_logCategory.FormattingEnabled = true;
            this.cb_logCategory.Location = new System.Drawing.Point(329, 11);
            this.cb_logCategory.Name = "cb_logCategory";
            this.cb_logCategory.Size = new System.Drawing.Size(121, 20);
            this.cb_logCategory.TabIndex = 16;
            // 
            // bt_Exit
            // 
            this.bt_Exit.Location = new System.Drawing.Point(774, 10);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(103, 23);
            this.bt_Exit.TabIndex = 15;
            this.bt_Exit.Text = "退出";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // bt_QueryLog
            // 
            this.bt_QueryLog.Location = new System.Drawing.Point(665, 10);
            this.bt_QueryLog.Name = "bt_QueryLog";
            this.bt_QueryLog.Size = new System.Drawing.Size(103, 23);
            this.bt_QueryLog.TabIndex = 14;
            this.bt_QueryLog.Text = "查询";
            this.bt_QueryLog.UseVisualStyleBackColor = true;
            this.bt_QueryLog.Click += new System.EventHandler(this.bt_QueryLog_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "结束时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "开始时间：";
            // 
            // dtp_EndTime
            // 
            this.dtp_EndTime.Location = new System.Drawing.Point(82, 34);
            this.dtp_EndTime.Name = "dtp_EndTime";
            this.dtp_EndTime.Size = new System.Drawing.Size(155, 21);
            this.dtp_EndTime.TabIndex = 9;
            // 
            // dtp_StartTime
            // 
            this.dtp_StartTime.Location = new System.Drawing.Point(82, 11);
            this.dtp_StartTime.Name = "dtp_StartTime";
            this.dtp_StartTime.Size = new System.Drawing.Size(155, 21);
            this.dtp_StartTime.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgv_logDetail);
            this.groupBox1.Location = new System.Drawing.Point(0, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 395);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志详情";
            // 
            // dgv_logDetail
            // 
            this.dgv_logDetail.AllowUserToAddRows = false;
            this.dgv_logDetail.AllowUserToDeleteRows = false;
            this.dgv_logDetail.AllowUserToResizeRows = false;
            this.dgv_logDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_logDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_logDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_logDetail.Location = new System.Drawing.Point(3, 17);
            this.dgv_logDetail.Name = "dgv_logDetail";
            this.dgv_logDetail.RowHeadersVisible = false;
            this.dgv_logDetail.RowTemplate.Height = 23;
            this.dgv_logDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_logDetail.Size = new System.Drawing.Size(880, 375);
            this.dgv_logDetail.TabIndex = 0;
            this.dgv_logDetail.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_logDetail_CellMouseDoubleClick);
            // 
            // LogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 465);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LogView";
            this.Text = "LogView";
            this.Load += new System.EventHandler(this.LogView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_logDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_logDetail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_logType;
        private System.Windows.Forms.ComboBox cb_logCategory;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.Button bt_QueryLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_EndTime;
        private System.Windows.Forms.DateTimePicker dtp_StartTime;
        private System.Windows.Forms.Button bt_ExportTxt;
        private System.Windows.Forms.CheckBox cb_LikeQuery;
        private System.Windows.Forms.TextBox tb_LikeQueryTxt;
    }
}