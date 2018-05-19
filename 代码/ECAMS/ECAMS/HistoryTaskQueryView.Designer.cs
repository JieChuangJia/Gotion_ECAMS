namespace ECAMS
{
    partial class HistoryTaskQueryView
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
            this.dgv_historyTaskList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_EndPosition = new System.Windows.Forms.TextBox();
            this.cb_EndPostion = new System.Windows.Forms.CheckBox();
            this.tb_StartPosition = new System.Windows.Forms.TextBox();
            this.cb_StartPostion = new System.Windows.Forms.CheckBox();
            this.checkB_taskTypeName = new System.Windows.Forms.CheckBox();
            this.cb_TaskTypeName = new System.Windows.Forms.ComboBox();
            this.bt_HistoryTaskEixt = new System.Windows.Forms.Button();
            this.bt_QueryHisTask = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_EndTime = new System.Windows.Forms.DateTimePicker();
            this.dtp_StartTime = new System.Windows.Forms.DateTimePicker();
            this.numID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskStartPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskEndPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskCreatePerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskCompleteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_historyTaskList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_historyTaskList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(831, 430);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务详情";
            // 
            // dgv_historyTaskList
            // 
            this.dgv_historyTaskList.AllowUserToAddRows = false;
            this.dgv_historyTaskList.AllowUserToDeleteRows = false;
            this.dgv_historyTaskList.AllowUserToResizeRows = false;
            this.dgv_historyTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_historyTaskList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numID,
            this.TaskTypeName,
            this.taskType,
            this.productName,
            this.ProductID,
            this.StartArea,
            this.taskStartPosition,
            this.TaskParam,
            this.EndArea,
            this.taskEndPosition,
            this.taskCreatePerson,
            this.taskCreateTime,
            this.taskCompleteTime});
            this.dgv_historyTaskList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_historyTaskList.Location = new System.Drawing.Point(3, 17);
            this.dgv_historyTaskList.Name = "dgv_historyTaskList";
            this.dgv_historyTaskList.RowHeadersVisible = false;
            this.dgv_historyTaskList.RowTemplate.Height = 23;
            this.dgv_historyTaskList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_historyTaskList.Size = new System.Drawing.Size(825, 410);
            this.dgv_historyTaskList.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.tb_EndPosition);
            this.panel1.Controls.Add(this.cb_EndPostion);
            this.panel1.Controls.Add(this.tb_StartPosition);
            this.panel1.Controls.Add(this.cb_StartPostion);
            this.panel1.Controls.Add(this.checkB_taskTypeName);
            this.panel1.Controls.Add(this.cb_TaskTypeName);
            this.panel1.Controls.Add(this.bt_HistoryTaskEixt);
            this.panel1.Controls.Add(this.bt_QueryHisTask);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtp_EndTime);
            this.panel1.Controls.Add(this.dtp_StartTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 60);
            this.panel1.TabIndex = 0;
            // 
            // tb_EndPosition
            // 
            this.tb_EndPosition.Location = new System.Drawing.Point(318, 34);
            this.tb_EndPosition.Name = "tb_EndPosition";
            this.tb_EndPosition.Size = new System.Drawing.Size(121, 21);
            this.tb_EndPosition.TabIndex = 23;
            // 
            // cb_EndPostion
            // 
            this.cb_EndPostion.AutoSize = true;
            this.cb_EndPostion.Location = new System.Drawing.Point(245, 38);
            this.cb_EndPostion.Name = "cb_EndPostion";
            this.cb_EndPostion.Size = new System.Drawing.Size(72, 16);
            this.cb_EndPostion.TabIndex = 22;
            this.cb_EndPostion.Text = "结束位置";
            this.cb_EndPostion.UseVisualStyleBackColor = true;
            // 
            // tb_StartPosition
            // 
            this.tb_StartPosition.Location = new System.Drawing.Point(318, 9);
            this.tb_StartPosition.Name = "tb_StartPosition";
            this.tb_StartPosition.Size = new System.Drawing.Size(121, 21);
            this.tb_StartPosition.TabIndex = 21;
            // 
            // cb_StartPostion
            // 
            this.cb_StartPostion.AutoSize = true;
            this.cb_StartPostion.Location = new System.Drawing.Point(245, 13);
            this.cb_StartPostion.Name = "cb_StartPostion";
            this.cb_StartPostion.Size = new System.Drawing.Size(72, 16);
            this.cb_StartPostion.TabIndex = 20;
            this.cb_StartPostion.Text = "开始位置";
            this.cb_StartPostion.UseVisualStyleBackColor = true;
            // 
            // checkB_taskTypeName
            // 
            this.checkB_taskTypeName.AutoSize = true;
            this.checkB_taskTypeName.Location = new System.Drawing.Point(448, 12);
            this.checkB_taskTypeName.Name = "checkB_taskTypeName";
            this.checkB_taskTypeName.Size = new System.Drawing.Size(72, 16);
            this.checkB_taskTypeName.TabIndex = 19;
            this.checkB_taskTypeName.Text = "业务流程";
            this.checkB_taskTypeName.UseVisualStyleBackColor = true;
            // 
            // cb_TaskTypeName
            // 
            this.cb_TaskTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TaskTypeName.FormattingEnabled = true;
            this.cb_TaskTypeName.Items.AddRange(new object[] {
            "出库",
            "入库"});
            this.cb_TaskTypeName.Location = new System.Drawing.Point(521, 10);
            this.cb_TaskTypeName.Name = "cb_TaskTypeName";
            this.cb_TaskTypeName.Size = new System.Drawing.Size(121, 20);
            this.cb_TaskTypeName.TabIndex = 16;
            // 
            // bt_HistoryTaskEixt
            // 
            this.bt_HistoryTaskEixt.Location = new System.Drawing.Point(661, 30);
            this.bt_HistoryTaskEixt.Name = "bt_HistoryTaskEixt";
            this.bt_HistoryTaskEixt.Size = new System.Drawing.Size(103, 23);
            this.bt_HistoryTaskEixt.TabIndex = 15;
            this.bt_HistoryTaskEixt.Text = "退出";
            this.bt_HistoryTaskEixt.UseVisualStyleBackColor = true;
            this.bt_HistoryTaskEixt.Click += new System.EventHandler(this.bt_HistoryTaskEixt_Click);
            // 
            // bt_QueryHisTask
            // 
            this.bt_QueryHisTask.Location = new System.Drawing.Point(661, 8);
            this.bt_QueryHisTask.Name = "bt_QueryHisTask";
            this.bt_QueryHisTask.Size = new System.Drawing.Size(103, 23);
            this.bt_QueryHisTask.TabIndex = 14;
            this.bt_QueryHisTask.Text = "查询";
            this.bt_QueryHisTask.UseVisualStyleBackColor = true;
            this.bt_QueryHisTask.Click += new System.EventHandler(this.bt_QueryHisTask_Click);
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
            this.label5.Location = new System.Drawing.Point(11, 13);
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
            this.dtp_StartTime.Location = new System.Drawing.Point(82, 9);
            this.dtp_StartTime.Name = "dtp_StartTime";
            this.dtp_StartTime.Size = new System.Drawing.Size(155, 21);
            this.dtp_StartTime.TabIndex = 8;
            // 
            // numID
            // 
            this.numID.HeaderText = "序号";
            this.numID.Name = "numID";
            // 
            // TaskTypeName
            // 
            this.TaskTypeName.HeaderText = "任务流程";
            this.TaskTypeName.Name = "TaskTypeName";
            // 
            // taskType
            // 
            this.taskType.HeaderText = "任务类型";
            this.taskType.Name = "taskType";
            // 
            // productName
            // 
            this.productName.HeaderText = "物料名称";
            this.productName.Name = "productName";
            this.productName.Visible = false;
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "产品编号";
            this.ProductID.Name = "ProductID";
            this.ProductID.Visible = false;
            // 
            // StartArea
            // 
            this.StartArea.HeaderText = "开始区域";
            this.StartArea.Name = "StartArea";
            // 
            // taskStartPosition
            // 
            this.taskStartPosition.HeaderText = "开始位置";
            this.taskStartPosition.Name = "taskStartPosition";
            // 
            // TaskParam
            // 
            this.TaskParam.HeaderText = "任务参数";
            this.TaskParam.Name = "TaskParam";
            // 
            // EndArea
            // 
            this.EndArea.HeaderText = "结束区域";
            this.EndArea.Name = "EndArea";
            // 
            // taskEndPosition
            // 
            this.taskEndPosition.HeaderText = "结束位置";
            this.taskEndPosition.Name = "taskEndPosition";
            // 
            // taskCreatePerson
            // 
            this.taskCreatePerson.HeaderText = "任务创建人";
            this.taskCreatePerson.Name = "taskCreatePerson";
            // 
            // taskCreateTime
            // 
            this.taskCreateTime.HeaderText = "任务创建时间";
            this.taskCreateTime.Name = "taskCreateTime";
            // 
            // taskCompleteTime
            // 
            this.taskCompleteTime.HeaderText = "任务完成时间";
            this.taskCompleteTime.Name = "taskCompleteTime";
            // 
            // HistoryTaskQueryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 490);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HistoryTaskQueryView";
            this.Text = "HistoryTaskQuery";
            this.Load += new System.EventHandler(this.HistoryTaskQueryView_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_historyTaskList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_historyTaskList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_EndTime;
        private System.Windows.Forms.DateTimePicker dtp_StartTime;
        private System.Windows.Forms.Button bt_HistoryTaskEixt;
        private System.Windows.Forms.Button bt_QueryHisTask;
        private System.Windows.Forms.ComboBox cb_TaskTypeName;
        private System.Windows.Forms.CheckBox checkB_taskTypeName;
        private System.Windows.Forms.TextBox tb_EndPosition;
        private System.Windows.Forms.CheckBox cb_EndPostion;
        private System.Windows.Forms.TextBox tb_StartPosition;
        private System.Windows.Forms.CheckBox cb_StartPostion;
        private System.Windows.Forms.DataGridViewTextBoxColumn numID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskType;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskStartPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskEndPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskCreatePerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskCreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskCompleteTime;
    }
}