namespace ECAMS
{
    partial class ManageTaskView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageTaskView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_manageTask = new System.Windows.Forms.DataGridView();
            this.taskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskStartArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskStartPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskEndArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskEndPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskCreatePerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskCompleteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms_ManaTask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_AutoRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsb_RefreshTask = new System.Windows.Forms.ToolStripButton();
            this.tsb_ExitMangeTaskForm = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_manageTask)).BeginInit();
            this.cms_ManaTask.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_manageTask);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(986, 430);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "管理任务";
            // 
            // dgv_manageTask
            // 
            this.dgv_manageTask.AllowUserToAddRows = false;
            this.dgv_manageTask.AllowUserToDeleteRows = false;
            this.dgv_manageTask.AllowUserToResizeRows = false;
            this.dgv_manageTask.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_manageTask.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_manageTask.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgv_manageTask.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgv_manageTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_manageTask.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskID,
            this.taskStatus,
            this.taskTypeName,
            this.taskType,
            this.taskCode,
            this.taskStartArea,
            this.taskStartPosition,
            this.taskEndArea,
            this.taskEndPosition,
            this.taskCreatePerson,
            this.taskCreateTime,
            this.taskCompleteTime});
            this.dgv_manageTask.ContextMenuStrip = this.cms_ManaTask;
            this.dgv_manageTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_manageTask.Location = new System.Drawing.Point(3, 17);
            this.dgv_manageTask.MultiSelect = false;
            this.dgv_manageTask.Name = "dgv_manageTask";
            this.dgv_manageTask.ReadOnly = true;
            this.dgv_manageTask.RowHeadersVisible = false;
            this.dgv_manageTask.RowTemplate.Height = 23;
            this.dgv_manageTask.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_manageTask.Size = new System.Drawing.Size(980, 410);
            this.dgv_manageTask.TabIndex = 0;
            // 
            // taskID
            // 
            this.taskID.HeaderText = "任务ID";
            this.taskID.Name = "taskID";
            this.taskID.ReadOnly = true;
            this.taskID.Width = 66;
            // 
            // taskStatus
            // 
            this.taskStatus.HeaderText = "任务状态";
            this.taskStatus.Name = "taskStatus";
            this.taskStatus.ReadOnly = true;
            this.taskStatus.Width = 78;
            // 
            // taskTypeName
            // 
            this.taskTypeName.HeaderText = "业务流程名称";
            this.taskTypeName.Name = "taskTypeName";
            this.taskTypeName.ReadOnly = true;
            this.taskTypeName.Width = 102;
            // 
            // taskType
            // 
            this.taskType.HeaderText = "任务类型";
            this.taskType.Name = "taskType";
            this.taskType.ReadOnly = true;
            this.taskType.Width = 78;
            // 
            // taskCode
            // 
            this.taskCode.HeaderText = "任务编码";
            this.taskCode.Name = "taskCode";
            this.taskCode.ReadOnly = true;
            this.taskCode.Width = 78;
            // 
            // taskStartArea
            // 
            this.taskStartArea.HeaderText = "任务开始区域";
            this.taskStartArea.Name = "taskStartArea";
            this.taskStartArea.ReadOnly = true;
            this.taskStartArea.Width = 102;
            // 
            // taskStartPosition
            // 
            this.taskStartPosition.HeaderText = "任务开始位置";
            this.taskStartPosition.Name = "taskStartPosition";
            this.taskStartPosition.ReadOnly = true;
            this.taskStartPosition.Width = 102;
            // 
            // taskEndArea
            // 
            this.taskEndArea.HeaderText = "任务结束区域";
            this.taskEndArea.Name = "taskEndArea";
            this.taskEndArea.ReadOnly = true;
            this.taskEndArea.Width = 102;
            // 
            // taskEndPosition
            // 
            this.taskEndPosition.HeaderText = "任务结束位置";
            this.taskEndPosition.Name = "taskEndPosition";
            this.taskEndPosition.ReadOnly = true;
            this.taskEndPosition.Width = 102;
            // 
            // taskCreatePerson
            // 
            this.taskCreatePerson.HeaderText = "任务创建人";
            this.taskCreatePerson.Name = "taskCreatePerson";
            this.taskCreatePerson.ReadOnly = true;
            this.taskCreatePerson.Width = 90;
            // 
            // taskCreateTime
            // 
            this.taskCreateTime.HeaderText = "任务创建时间";
            this.taskCreateTime.Name = "taskCreateTime";
            this.taskCreateTime.ReadOnly = true;
            this.taskCreateTime.Width = 102;
            // 
            // taskCompleteTime
            // 
            this.taskCompleteTime.HeaderText = "任务完成时间";
            this.taskCompleteTime.Name = "taskCompleteTime";
            this.taskCompleteTime.ReadOnly = true;
            this.taskCompleteTime.Visible = false;
            this.taskCompleteTime.Width = 102;
            // 
            // cms_ManaTask
            // 
            this.cms_ManaTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem});
            this.cms_ManaTask.Name = "cms_ManaTask";
            this.cms_ManaTask.Size = new System.Drawing.Size(95, 26);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_AutoRefresh,
            this.tsb_RefreshTask,
            this.tsb_ExitMangeTaskForm});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(986, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_AutoRefresh
            // 
            this.tsb_AutoRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AutoRefresh.Image")));
            this.tsb_AutoRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AutoRefresh.Name = "tsb_AutoRefresh";
            this.tsb_AutoRefresh.Size = new System.Drawing.Size(97, 22);
            this.tsb_AutoRefresh.Text = "打开自动刷新";
            this.tsb_AutoRefresh.Click += new System.EventHandler(this.tsb_AutoRefresh_Click);
            // 
            // tsb_RefreshTask
            // 
            this.tsb_RefreshTask.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RefreshTask.Image")));
            this.tsb_RefreshTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RefreshTask.Name = "tsb_RefreshTask";
            this.tsb_RefreshTask.Size = new System.Drawing.Size(49, 22);
            this.tsb_RefreshTask.Text = "刷新";
            this.tsb_RefreshTask.Click += new System.EventHandler(this.tsb_RefreshTask_Click);
            // 
            // tsb_ExitMangeTaskForm
            // 
            this.tsb_ExitMangeTaskForm.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ExitMangeTaskForm.Image")));
            this.tsb_ExitMangeTaskForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ExitMangeTaskForm.Name = "tsb_ExitMangeTaskForm";
            this.tsb_ExitMangeTaskForm.Size = new System.Drawing.Size(49, 22);
            this.tsb_ExitMangeTaskForm.Text = "退出";
            this.tsb_ExitMangeTaskForm.Click += new System.EventHandler(this.tsb_ExitMangeTaskForm_Click);
            // 
            // ManageTaskView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 455);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ManageTaskView";
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManageTaskView_FormClosing);
            this.Load += new System.EventHandler(this.ManageTaskView_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_manageTask)).EndInit();
            this.cms_ManaTask.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_AutoRefresh;
        private System.Windows.Forms.ToolStripButton tsb_RefreshTask;
        private System.Windows.Forms.ToolStripButton tsb_ExitMangeTaskForm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_manageTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskType;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskStartArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskStartPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskEndArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskEndPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskCreatePerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskCompleteTime;
        private System.Windows.Forms.ContextMenuStrip cms_ManaTask;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
    }
}