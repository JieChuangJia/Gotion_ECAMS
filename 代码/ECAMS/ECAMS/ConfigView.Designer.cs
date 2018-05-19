namespace ECAMS
{
    partial class ConfigView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigView));
            this.tab_config = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_WorkFlowSet = new System.Windows.Forms.DataGridView();
            this.taskTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.needTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.taskDescribe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_ConfigSet = new System.Windows.Forms.ToolStripButton();
            this.tsb_Exit = new System.Windows.Forms.ToolStripButton();
            this.tab_config.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WorkFlowSet)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_config
            // 
            this.tab_config.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tab_config.Controls.Add(this.tabPage1);
            this.tab_config.Location = new System.Drawing.Point(4, 28);
            this.tab_config.Name = "tab_config";
            this.tab_config.SelectedIndex = 0;
            this.tab_config.Size = new System.Drawing.Size(861, 389);
            this.tab_config.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage1.Controls.Add(this.dgv_WorkFlowSet);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(853, 363);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "出库设置";
            // 
            // dgv_WorkFlowSet
            // 
            this.dgv_WorkFlowSet.AllowUserToAddRows = false;
            this.dgv_WorkFlowSet.AllowUserToDeleteRows = false;
            this.dgv_WorkFlowSet.AllowUserToResizeRows = false;
            this.dgv_WorkFlowSet.ColumnHeadersHeight = 20;
            this.dgv_WorkFlowSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_WorkFlowSet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskTypeID,
            this.taskTypeName,
            this.needTime,
            this.taskMode,
            this.taskDescribe});
            this.dgv_WorkFlowSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_WorkFlowSet.Location = new System.Drawing.Point(3, 3);
            this.dgv_WorkFlowSet.Name = "dgv_WorkFlowSet";
            this.dgv_WorkFlowSet.RowHeadersVisible = false;
            this.dgv_WorkFlowSet.RowTemplate.Height = 23;
            this.dgv_WorkFlowSet.Size = new System.Drawing.Size(847, 357);
            this.dgv_WorkFlowSet.TabIndex = 12;
            this.dgv_WorkFlowSet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_WorkFlow_CellContentClick);
            // 
            // taskTypeID
            // 
            this.taskTypeID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.taskTypeID.DefaultCellStyle = dataGridViewCellStyle1;
            this.taskTypeID.HeaderText = "任务流程ID";
            this.taskTypeID.Name = "taskTypeID";
            this.taskTypeID.ReadOnly = true;
            this.taskTypeID.Width = 80;
            // 
            // taskTypeName
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.taskTypeName.DefaultCellStyle = dataGridViewCellStyle2;
            this.taskTypeName.HeaderText = "任务流程名称";
            this.taskTypeName.Name = "taskTypeName";
            this.taskTypeName.ReadOnly = true;
            this.taskTypeName.Width = 72;
            // 
            // needTime
            // 
            this.needTime.HeaderText = "所需时间";
            this.needTime.Name = "needTime";
            this.needTime.Width = 61;
            // 
            // taskMode
            // 
            this.taskMode.HeaderText = "任务模式";
            this.taskMode.Items.AddRange(new object[] {
            "自动",
            "手动"});
            this.taskMode.Name = "taskMode";
            this.taskMode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.taskMode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.taskMode.Width = 61;
            // 
            // taskDescribe
            // 
            this.taskDescribe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.taskDescribe.DefaultCellStyle = dataGridViewCellStyle3;
            this.taskDescribe.HeaderText = "任务描述";
            this.taskDescribe.Name = "taskDescribe";
            this.taskDescribe.Width = 78;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ConfigSet,
            this.tsb_Exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(868, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_ConfigSet
            // 
            this.tsb_ConfigSet.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ConfigSet.Image")));
            this.tsb_ConfigSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ConfigSet.Name = "tsb_ConfigSet";
            this.tsb_ConfigSet.Size = new System.Drawing.Size(52, 22);
            this.tsb_ConfigSet.Text = "设置";
            this.tsb_ConfigSet.Click += new System.EventHandler(this.bt_ConfigSet_Click);
            // 
            // tsb_Exit
            // 
            this.tsb_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Exit.Image")));
            this.tsb_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Exit.Name = "tsb_Exit";
            this.tsb_Exit.Size = new System.Drawing.Size(52, 22);
            this.tsb_Exit.Text = "退出";
            this.tsb_Exit.Click += new System.EventHandler(this.bt_configExit_Click);
            // 
            // ConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 423);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tab_config);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfigView";
            this.Text = "系统配置";
            this.Load += new System.EventHandler(this.ConfigView_Load);
            this.tab_config.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WorkFlowSet)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tab_config;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_WorkFlowSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn needTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn taskMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskDescribe;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_ConfigSet;
        private System.Windows.Forms.ToolStripButton tsb_Exit;
    }
}