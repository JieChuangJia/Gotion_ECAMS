namespace ECAMS
{
    partial class ControlTaskView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlTaskView));
            this.cms_ControlTask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动完成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_TaskCreateMode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_TaskType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_StoreHouse = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_QueryCtrlTask = new System.Windows.Forms.Button();
            this.cb_QueryStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_QueryTaskFlow = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_ControlTask = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbt_autoRefreshTask = new System.Windows.Forms.ToolStripButton();
            this.tsb_cancelTask = new System.Windows.Forms.ToolStripButton();
            this.tsb_completeTaskByHand = new System.Windows.Forms.ToolStripButton();
            this.tsb_RefreshTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscb_TaskTypeName = new System.Windows.Forms.ToolStripComboBox();
            this.tsb_InStoreByHand = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_ExitControlForm = new System.Windows.Forms.ToolStripButton();
            this.taskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.controlTaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.controlTaskCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startStoreArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endStoreArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms_ControlTask.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ControlTask)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cms_ControlTask
            // 
            this.cms_ControlTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除任务ToolStripMenuItem,
            this.手动完成ToolStripMenuItem,
            this.刷新ToolStripMenuItem});
            this.cms_ControlTask.Name = "cms_ControlTask";
            this.cms_ControlTask.Size = new System.Drawing.Size(125, 70);
            // 
            // 删除任务ToolStripMenuItem
            // 
            this.删除任务ToolStripMenuItem.Name = "删除任务ToolStripMenuItem";
            this.删除任务ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除任务ToolStripMenuItem.Text = "删除任务";
            this.删除任务ToolStripMenuItem.Click += new System.EventHandler(this.删除任务ToolStripMenuItem_Click);
            // 
            // 手动完成ToolStripMenuItem
            // 
            this.手动完成ToolStripMenuItem.Name = "手动完成ToolStripMenuItem";
            this.手动完成ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.手动完成ToolStripMenuItem.Text = "手动完成";
            this.手动完成ToolStripMenuItem.Click += new System.EventHandler(this.手动完成ToolStripMenuItem_Click);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cb_TaskCreateMode);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cb_TaskType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cb_StoreHouse);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.bt_QueryCtrlTask);
            this.groupBox2.Controls.Add(this.cb_QueryStatus);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cb_QueryTaskFlow);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1083, 55);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "任务筛选";
            // 
            // cb_TaskCreateMode
            // 
            this.cb_TaskCreateMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TaskCreateMode.DropDownWidth = 121;
            this.cb_TaskCreateMode.FormattingEnabled = true;
            this.cb_TaskCreateMode.Location = new System.Drawing.Point(424, 24);
            this.cb_TaskCreateMode.Name = "cb_TaskCreateMode";
            this.cb_TaskCreateMode.Size = new System.Drawing.Size(100, 20);
            this.cb_TaskCreateMode.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(341, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "任务创建模式：";
            // 
            // cb_TaskType
            // 
            this.cb_TaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TaskType.DropDownWidth = 121;
            this.cb_TaskType.FormattingEnabled = true;
            this.cb_TaskType.Location = new System.Drawing.Point(226, 24);
            this.cb_TaskType.Name = "cb_TaskType";
            this.cb_TaskType.Size = new System.Drawing.Size(100, 20);
            this.cb_TaskType.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "任务类型：";
            // 
            // cb_StoreHouse
            // 
            this.cb_StoreHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_StoreHouse.DropDownWidth = 121;
            this.cb_StoreHouse.FormattingEnabled = true;
            this.cb_StoreHouse.Location = new System.Drawing.Point(54, 24);
            this.cb_StoreHouse.Name = "cb_StoreHouse";
            this.cb_StoreHouse.Size = new System.Drawing.Size(100, 20);
            this.cb_StoreHouse.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "库房：";
            // 
            // bt_QueryCtrlTask
            // 
            this.bt_QueryCtrlTask.Location = new System.Drawing.Point(891, 22);
            this.bt_QueryCtrlTask.Name = "bt_QueryCtrlTask";
            this.bt_QueryCtrlTask.Size = new System.Drawing.Size(75, 23);
            this.bt_QueryCtrlTask.TabIndex = 4;
            this.bt_QueryCtrlTask.Text = "筛选";
            this.bt_QueryCtrlTask.UseVisualStyleBackColor = true;
            this.bt_QueryCtrlTask.Click += new System.EventHandler(this.bt_QueryCtrlTask_Click);
            // 
            // cb_QueryStatus
            // 
            this.cb_QueryStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_QueryStatus.DropDownWidth = 121;
            this.cb_QueryStatus.FormattingEnabled = true;
            this.cb_QueryStatus.Location = new System.Drawing.Point(761, 24);
            this.cb_QueryStatus.Name = "cb_QueryStatus";
            this.cb_QueryStatus.Size = new System.Drawing.Size(100, 20);
            this.cb_QueryStatus.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(701, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "任务状态：";
            // 
            // cb_QueryTaskFlow
            // 
            this.cb_QueryTaskFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_QueryTaskFlow.DropDownWidth = 121;
            this.cb_QueryTaskFlow.FormattingEnabled = true;
            this.cb_QueryTaskFlow.Location = new System.Drawing.Point(591, 24);
            this.cb_QueryTaskFlow.Name = "cb_QueryTaskFlow";
            this.cb_QueryTaskFlow.Size = new System.Drawing.Size(100, 20);
            this.cb_QueryTaskFlow.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(534, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务流程：";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgv_ControlTask);
            this.groupBox1.Location = new System.Drawing.Point(0, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1089, 389);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制任务";
            // 
            // dgv_ControlTask
            // 
            this.dgv_ControlTask.AllowUserToAddRows = false;
            this.dgv_ControlTask.AllowUserToResizeRows = false;
            this.dgv_ControlTask.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_ControlTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_ControlTask.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskID,
            this.TaskTypeName,
            this.controlTaskID,
            this.taskType,
            this.controlTaskCode,
            this.startStoreArea,
            this.startPosition,
            this.endStoreArea,
            this.endPosition,
            this.taskStatus,
            this.startTime,
            this.completeTime});
            this.dgv_ControlTask.ContextMenuStrip = this.cms_ControlTask;
            this.dgv_ControlTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ControlTask.Location = new System.Drawing.Point(3, 17);
            this.dgv_ControlTask.Name = "dgv_ControlTask";
            this.dgv_ControlTask.ReadOnly = true;
            this.dgv_ControlTask.RowHeadersVisible = false;
            this.dgv_ControlTask.RowTemplate.Height = 23;
            this.dgv_ControlTask.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ControlTask.Size = new System.Drawing.Size(1083, 369);
            this.dgv_ControlTask.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbt_autoRefreshTask,
            this.tsb_cancelTask,
            this.tsb_completeTaskByHand,
            this.tsb_RefreshTask,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tscb_TaskTypeName,
            this.tsb_InStoreByHand,
            this.toolStripSeparator2,
            this.tsb_ExitControlForm});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1089, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbt_autoRefreshTask
            // 
            this.tsbt_autoRefreshTask.Image = ((System.Drawing.Image)(resources.GetObject("tsbt_autoRefreshTask.Image")));
            this.tsbt_autoRefreshTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_autoRefreshTask.Name = "tsbt_autoRefreshTask";
            this.tsbt_autoRefreshTask.Size = new System.Drawing.Size(100, 22);
            this.tsbt_autoRefreshTask.Text = "打开自动刷新";
            this.tsbt_autoRefreshTask.Click += new System.EventHandler(this.tsbt_autoRefreshTask_Click);
            // 
            // tsb_cancelTask
            // 
            this.tsb_cancelTask.Image = ((System.Drawing.Image)(resources.GetObject("tsb_cancelTask.Image")));
            this.tsb_cancelTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_cancelTask.Name = "tsb_cancelTask";
            this.tsb_cancelTask.Size = new System.Drawing.Size(76, 22);
            this.tsb_cancelTask.Text = "删除任务";
            this.tsb_cancelTask.Click += new System.EventHandler(this.tsb_cancelTask_Click);
            // 
            // tsb_completeTaskByHand
            // 
            this.tsb_completeTaskByHand.Image = ((System.Drawing.Image)(resources.GetObject("tsb_completeTaskByHand.Image")));
            this.tsb_completeTaskByHand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_completeTaskByHand.Name = "tsb_completeTaskByHand";
            this.tsb_completeTaskByHand.Size = new System.Drawing.Size(76, 22);
            this.tsb_completeTaskByHand.Text = "手动完成";
            this.tsb_completeTaskByHand.Click += new System.EventHandler(this.tsb_completeTaskByHand_Click);
            // 
            // tsb_RefreshTask
            // 
            this.tsb_RefreshTask.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RefreshTask.Image")));
            this.tsb_RefreshTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RefreshTask.Name = "tsb_RefreshTask";
            this.tsb_RefreshTask.Size = new System.Drawing.Size(52, 22);
            this.tsb_RefreshTask.Text = "刷新";
            this.tsb_RefreshTask.Click += new System.EventHandler(this.tsb_RefreshTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(92, 22);
            this.toolStripLabel1.Text = "任务流程名称：";
            // 
            // tscb_TaskTypeName
            // 
            this.tscb_TaskTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_TaskTypeName.Name = "tscb_TaskTypeName";
            this.tscb_TaskTypeName.Size = new System.Drawing.Size(121, 25);
            // 
            // tsb_InStoreByHand
            // 
            this.tsb_InStoreByHand.Image = ((System.Drawing.Image)(resources.GetObject("tsb_InStoreByHand.Image")));
            this.tsb_InStoreByHand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_InStoreByHand.Name = "tsb_InStoreByHand";
            this.tsb_InStoreByHand.Size = new System.Drawing.Size(100, 22);
            this.tsb_InStoreByHand.Text = "手动生成任务";
            this.tsb_InStoreByHand.Click += new System.EventHandler(this.tsb_InStoreByHand_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_ExitControlForm
            // 
            this.tsb_ExitControlForm.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ExitControlForm.Image")));
            this.tsb_ExitControlForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ExitControlForm.Name = "tsb_ExitControlForm";
            this.tsb_ExitControlForm.Size = new System.Drawing.Size(52, 22);
            this.tsb_ExitControlForm.Text = "退出";
            this.tsb_ExitControlForm.Click += new System.EventHandler(this.tsb_ExitControlForm_Click);
            // 
            // taskID
            // 
            this.taskID.HeaderText = "管理任务索引";
            this.taskID.Name = "taskID";
            this.taskID.ReadOnly = true;
            this.taskID.Width = 102;
            // 
            // TaskTypeName
            // 
            this.TaskTypeName.HeaderText = "任务流程名称";
            this.TaskTypeName.Name = "TaskTypeName";
            this.TaskTypeName.ReadOnly = true;
            this.TaskTypeName.Width = 102;
            // 
            // controlTaskID
            // 
            this.controlTaskID.HeaderText = "控制任务ID";
            this.controlTaskID.Name = "controlTaskID";
            this.controlTaskID.ReadOnly = true;
            this.controlTaskID.Width = 90;
            // 
            // taskType
            // 
            this.taskType.HeaderText = "任务类型";
            this.taskType.Name = "taskType";
            this.taskType.ReadOnly = true;
            this.taskType.Width = 78;
            // 
            // controlTaskCode
            // 
            this.controlTaskCode.HeaderText = "控制任务编码";
            this.controlTaskCode.Name = "controlTaskCode";
            this.controlTaskCode.ReadOnly = true;
            this.controlTaskCode.Width = 102;
            // 
            // startStoreArea
            // 
            this.startStoreArea.HeaderText = "开始库区";
            this.startStoreArea.Name = "startStoreArea";
            this.startStoreArea.ReadOnly = true;
            this.startStoreArea.Width = 78;
            // 
            // startPosition
            // 
            this.startPosition.HeaderText = "开始位置";
            this.startPosition.Name = "startPosition";
            this.startPosition.ReadOnly = true;
            this.startPosition.Width = 78;
            // 
            // endStoreArea
            // 
            this.endStoreArea.HeaderText = "结束库区";
            this.endStoreArea.Name = "endStoreArea";
            this.endStoreArea.ReadOnly = true;
            this.endStoreArea.Width = 78;
            // 
            // endPosition
            // 
            this.endPosition.HeaderText = "结束位置";
            this.endPosition.Name = "endPosition";
            this.endPosition.ReadOnly = true;
            this.endPosition.Width = 78;
            // 
            // taskStatus
            // 
            this.taskStatus.HeaderText = "任务状态";
            this.taskStatus.Name = "taskStatus";
            this.taskStatus.ReadOnly = true;
            this.taskStatus.Width = 78;
            // 
            // startTime
            // 
            this.startTime.HeaderText = "开始时间";
            this.startTime.Name = "startTime";
            this.startTime.ReadOnly = true;
            this.startTime.Width = 78;
            // 
            // completeTime
            // 
            this.completeTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.completeTime.HeaderText = "结束时间";
            this.completeTime.Name = "completeTime";
            this.completeTime.ReadOnly = true;
            this.completeTime.Visible = false;
            // 
            // ControlTaskView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 477);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ControlTaskView";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlTaskView_FormClosing);
            this.Shown += new System.EventHandler(this.ControlTaskView_Shown);
            this.cms_ControlTask.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ControlTask)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbt_autoRefreshTask;
        private System.Windows.Forms.ToolStripButton tsb_completeTaskByHand;
        private System.Windows.Forms.ToolStripButton tsb_RefreshTask;
        private System.Windows.Forms.ToolStripButton tsb_ExitControlForm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_ControlTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscb_TaskTypeName;
        private System.Windows.Forms.ToolStripButton tsb_InStoreByHand;
        private System.Windows.Forms.ToolStripButton tsb_cancelTask;
        private System.Windows.Forms.ContextMenuStrip cms_ControlTask;
        private System.Windows.Forms.ToolStripMenuItem 删除任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手动完成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cb_QueryStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_QueryTaskFlow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_QueryCtrlTask;
        private System.Windows.Forms.ComboBox cb_StoreHouse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_TaskCreateMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_TaskType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn controlTaskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskType;
        private System.Windows.Forms.DataGridViewTextBoxColumn controlTaskCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn startStoreArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn startPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn endStoreArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn endPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn completeTime;
    }
}