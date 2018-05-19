namespace ECAMS
{
    partial class MainView
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.menuStrip_log = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cms_CommDeviceStatus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.连接ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_B1OutBatch = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_HouseName = new System.Windows.Forms.ComboBox();
            this.lb_A1OutBatch = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_RefreshBatch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_BatchList = new System.Windows.Forms.ComboBox();
            this.bt_AddSet = new System.Windows.Forms.Button();
            this.tb_CommStatus = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_DeviceStatus = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_LogType = new System.Windows.Forms.ComboBox();
            this.ListView_Log = new System.Windows.Forms.ListView();
            this.log_Image = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.log_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.log_Content = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.log_Category = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.log_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.log_ErrorCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_currentpage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_CurrentViewName = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_StartSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_StopSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_openLog = new System.Windows.Forms.ToolStripMenuItem();
            this.堆垛机通信ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStackerCommClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStackerCommReOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_closeLog = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_SysCongig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_UserInfor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ManageTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ControlTaskMana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_StockManage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_GoodsSiteMana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_QueryMana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_LogQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_HistoryTaskMana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_TrayTrace = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ProductMana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DataMana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DeviceMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DataMonitor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ChangeUser = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenceTime = new System.Windows.Forms.Timer(this.components);
            this.menuStrip_log.SuspendLayout();
            this.cms_CommDeviceStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tb_CommStatus.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DeviceStatus)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_log
            // 
            this.menuStrip_log.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.menuStrip_log.Name = "menuStrip_log";
            this.menuStrip_log.Size = new System.Drawing.Size(101, 48);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dialog-ok-apply-2.ico");
            this.imageList1.Images.SetKeyName(1, "dialog-cancel-2.ico");
            // 
            // cms_CommDeviceStatus
            // 
            this.cms_CommDeviceStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接ToolStripMenuItem});
            this.cms_CommDeviceStatus.Name = "cms_CommDeviceStatus";
            this.cms_CommDeviceStatus.Size = new System.Drawing.Size(101, 26);
            // 
            // 连接ToolStripMenuItem
            // 
            this.连接ToolStripMenuItem.Name = "连接ToolStripMenuItem";
            this.连接ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.连接ToolStripMenuItem.Text = "连接";
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(232, 25);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 334);
            this.splitter2.TabIndex = 25;
            this.splitter2.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tb_CommStatus);
            this.splitContainer1.Size = new System.Drawing.Size(232, 334);
            this.splitContainer1.SplitterDistance = 116;
            this.splitContainer1.TabIndex = 24;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox2.Controls.Add(this.lb_B1OutBatch);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cb_HouseName);
            this.groupBox2.Controls.Add(this.lb_A1OutBatch);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.bt_RefreshBatch);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cb_BatchList);
            this.groupBox2.Controls.Add(this.bt_AddSet);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 116);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "产品出库批次号设置";
            // 
            // lb_B1OutBatch
            // 
            this.lb_B1OutBatch.AutoSize = true;
            this.lb_B1OutBatch.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_B1OutBatch.ForeColor = System.Drawing.Color.Yellow;
            this.lb_B1OutBatch.Location = new System.Drawing.Point(132, 91);
            this.lb_B1OutBatch.Name = "lb_B1OutBatch";
            this.lb_B1OutBatch.Size = new System.Drawing.Size(35, 16);
            this.lb_B1OutBatch.TabIndex = 10;
            this.lb_B1OutBatch.Text = "---";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(6, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "B1出库批次号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "库房：";
            // 
            // cb_HouseName
            // 
            this.cb_HouseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_HouseName.FormattingEnabled = true;
            this.cb_HouseName.Location = new System.Drawing.Point(59, 16);
            this.cb_HouseName.Name = "cb_HouseName";
            this.cb_HouseName.Size = new System.Drawing.Size(75, 20);
            this.cb_HouseName.TabIndex = 7;
            // 
            // lb_A1OutBatch
            // 
            this.lb_A1OutBatch.AutoSize = true;
            this.lb_A1OutBatch.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_A1OutBatch.ForeColor = System.Drawing.Color.Yellow;
            this.lb_A1OutBatch.Location = new System.Drawing.Point(132, 71);
            this.lb_A1OutBatch.Name = "lb_A1OutBatch";
            this.lb_A1OutBatch.Size = new System.Drawing.Size(35, 16);
            this.lb_A1OutBatch.TabIndex = 6;
            this.lb_A1OutBatch.Text = "---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(6, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "A1出库批次号：";
            // 
            // bt_RefreshBatch
            // 
            this.bt_RefreshBatch.Location = new System.Drawing.Point(184, 39);
            this.bt_RefreshBatch.Name = "bt_RefreshBatch";
            this.bt_RefreshBatch.Size = new System.Drawing.Size(43, 23);
            this.bt_RefreshBatch.TabIndex = 4;
            this.bt_RefreshBatch.Text = "刷新";
            this.bt_RefreshBatch.UseVisualStyleBackColor = true;
            this.bt_RefreshBatch.Click += new System.EventHandler(this.bt_RefreshBatch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "批次号：";
            // 
            // cb_BatchList
            // 
            this.cb_BatchList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_BatchList.FormattingEnabled = true;
            this.cb_BatchList.Location = new System.Drawing.Point(59, 40);
            this.cb_BatchList.Name = "cb_BatchList";
            this.cb_BatchList.Size = new System.Drawing.Size(75, 20);
            this.cb_BatchList.TabIndex = 2;
            // 
            // bt_AddSet
            // 
            this.bt_AddSet.Location = new System.Drawing.Point(137, 39);
            this.bt_AddSet.Name = "bt_AddSet";
            this.bt_AddSet.Size = new System.Drawing.Size(43, 23);
            this.bt_AddSet.TabIndex = 1;
            this.bt_AddSet.Text = "设置";
            this.bt_AddSet.UseVisualStyleBackColor = true;
            this.bt_AddSet.Click += new System.EventHandler(this.bt_AddSet_Click);
            // 
            // tb_CommStatus
            // 
            this.tb_CommStatus.Controls.Add(this.tabPage1);
            this.tb_CommStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_CommStatus.Location = new System.Drawing.Point(0, 0);
            this.tb_CommStatus.Name = "tb_CommStatus";
            this.tb_CommStatus.SelectedIndex = 0;
            this.tb_CommStatus.Size = new System.Drawing.Size(232, 214);
            this.tb_CommStatus.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SteelBlue;
            this.tabPage1.Controls.Add(this.dgv_DeviceStatus);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(224, 188);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "设备状态";
            // 
            // dgv_DeviceStatus
            // 
            this.dgv_DeviceStatus.AllowUserToAddRows = false;
            this.dgv_DeviceStatus.AllowUserToDeleteRows = false;
            this.dgv_DeviceStatus.AllowUserToResizeRows = false;
            this.dgv_DeviceStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_DeviceStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DeviceStatus.Location = new System.Drawing.Point(3, 3);
            this.dgv_DeviceStatus.MultiSelect = false;
            this.dgv_DeviceStatus.Name = "dgv_DeviceStatus";
            this.dgv_DeviceStatus.RowHeadersVisible = false;
            this.dgv_DeviceStatus.RowTemplate.Height = 23;
            this.dgv_DeviceStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DeviceStatus.Size = new System.Drawing.Size(218, 182);
            this.dgv_DeviceStatus.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 359);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(934, 3);
            this.splitter1.TabIndex = 17;
            this.splitter1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.cb_LogType);
            this.groupBox1.Controls.Add(this.ListView_Log);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 362);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(934, 137);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志";
            // 
            // cb_LogType
            // 
            this.cb_LogType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_LogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_LogType.FormattingEnabled = true;
            this.cb_LogType.Location = new System.Drawing.Point(753, 20);
            this.cb_LogType.Name = "cb_LogType";
            this.cb_LogType.Size = new System.Drawing.Size(83, 20);
            this.cb_LogType.TabIndex = 1;
            // 
            // ListView_Log
            // 
            this.ListView_Log.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ListView_Log.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.log_Image,
            this.log_Date,
            this.log_Content,
            this.log_Category,
            this.log_Type,
            this.log_ErrorCode});
            this.ListView_Log.ContextMenuStrip = this.menuStrip_log;
            this.ListView_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView_Log.FullRowSelect = true;
            this.ListView_Log.GridLines = true;
            this.ListView_Log.Location = new System.Drawing.Point(3, 17);
            this.ListView_Log.Name = "ListView_Log";
            this.ListView_Log.Size = new System.Drawing.Size(928, 117);
            this.ListView_Log.TabIndex = 0;
            this.ListView_Log.UseCompatibleStateImageBehavior = false;
            this.ListView_Log.View = System.Windows.Forms.View.Details;
            this.ListView_Log.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_Log_MouseDoubleClick);
            // 
            // log_Image
            // 
            this.log_Image.Text = "";
            this.log_Image.Width = 20;
            // 
            // log_Date
            // 
            this.log_Date.Text = "日期";
            this.log_Date.Width = 259;
            // 
            // log_Content
            // 
            this.log_Content.Text = "内容";
            this.log_Content.Width = 285;
            // 
            // log_Category
            // 
            this.log_Category.Text = "类别";
            this.log_Category.Width = 150;
            // 
            // log_Type
            // 
            this.log_Type.Text = "类型";
            this.log_Type.Width = 150;
            // 
            // log_ErrorCode
            // 
            this.log_ErrorCode.Text = "报警码";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_currentpage,
            this.tssl_CurrentViewName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 499);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(934, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_currentpage
            // 
            this.tssl_currentpage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tssl_currentpage.Name = "tssl_currentpage";
            this.tssl_currentpage.Size = new System.Drawing.Size(70, 17);
            this.tssl_currentpage.Text = "当前页面：";
            // 
            // tssl_CurrentViewName
            // 
            this.tssl_CurrentViewName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tssl_CurrentViewName.Name = "tssl_CurrentViewName";
            this.tssl_CurrentViewName.Size = new System.Drawing.Size(19, 17);
            this.tssl_CurrentViewName.Text = "--";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.tsmi_QueryMana,
            this.工具ToolStripMenuItem,
            this.测试ToolStripMenuItem,
            this.tsmi_ChangeUser,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(934, 25);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_StartSystem,
            this.tsmi_StopSystem,
            this.tsmi_openLog,
            this.堆垛机通信ToolStripMenuItem,
            this.tsmi_closeLog,
            this.退出ToolStripMenuItem});
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(60, 21);
            this.toolStripMenuItem1.Text = "系统";
            // 
            // tsmi_StartSystem
            // 
            this.tsmi_StartSystem.Enabled = false;
            this.tsmi_StartSystem.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_StartSystem.Image")));
            this.tsmi_StartSystem.Name = "tsmi_StartSystem";
            this.tsmi_StartSystem.Size = new System.Drawing.Size(136, 22);
            this.tsmi_StartSystem.Text = "启动系统";
            this.tsmi_StartSystem.Click += new System.EventHandler(this.tsmi_StartSystem_Click);
            // 
            // tsmi_StopSystem
            // 
            this.tsmi_StopSystem.Enabled = false;
            this.tsmi_StopSystem.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_StopSystem.Image")));
            this.tsmi_StopSystem.Name = "tsmi_StopSystem";
            this.tsmi_StopSystem.Size = new System.Drawing.Size(136, 22);
            this.tsmi_StopSystem.Text = "停止系统";
            this.tsmi_StopSystem.Click += new System.EventHandler(this.tsmi_StopSystem_Click);
            // 
            // tsmi_openLog
            // 
            this.tsmi_openLog.Enabled = false;
            this.tsmi_openLog.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_openLog.Image")));
            this.tsmi_openLog.Name = "tsmi_openLog";
            this.tsmi_openLog.Size = new System.Drawing.Size(136, 22);
            this.tsmi_openLog.Text = "打开日志";
            this.tsmi_openLog.Click += new System.EventHandler(this.tsmi_openLog_Click);
            // 
            // 堆垛机通信ToolStripMenuItem
            // 
            this.堆垛机通信ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemStackerCommClose,
            this.ToolStripMenuItemStackerCommReOpen});
            this.堆垛机通信ToolStripMenuItem.Name = "堆垛机通信ToolStripMenuItem";
            this.堆垛机通信ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.堆垛机通信ToolStripMenuItem.Text = "堆垛机通信";
            // 
            // ToolStripMenuItemStackerCommClose
            // 
            this.ToolStripMenuItemStackerCommClose.Name = "ToolStripMenuItemStackerCommClose";
            this.ToolStripMenuItemStackerCommClose.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemStackerCommClose.Text = "关闭";
            this.ToolStripMenuItemStackerCommClose.Click += new System.EventHandler(this.ToolStripMenuItemStackerCommClose_Click);
            // 
            // ToolStripMenuItemStackerCommReOpen
            // 
            this.ToolStripMenuItemStackerCommReOpen.Name = "ToolStripMenuItemStackerCommReOpen";
            this.ToolStripMenuItemStackerCommReOpen.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemStackerCommReOpen.Text = "重新打开";
            this.ToolStripMenuItemStackerCommReOpen.Click += new System.EventHandler(this.ToolStripMenuItemStackerCommReOpen_Click);
            // 
            // tsmi_closeLog
            // 
            this.tsmi_closeLog.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_closeLog.Image")));
            this.tsmi_closeLog.Name = "tsmi_closeLog";
            this.tsmi_closeLog.Size = new System.Drawing.Size(136, 22);
            this.tsmi_closeLog.Text = "关闭日志";
            this.tsmi_closeLog.Click += new System.EventHandler(this.tsmi_closeLog_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("退出ToolStripMenuItem.Image")));
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_SysCongig,
            this.tsmi_UserInfor});
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(84, 21);
            this.toolStripMenuItem2.Text = "系统设置";
            // 
            // tsmi_SysCongig
            // 
            this.tsmi_SysCongig.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_SysCongig.Image")));
            this.tsmi_SysCongig.Name = "tsmi_SysCongig";
            this.tsmi_SysCongig.Size = new System.Drawing.Size(124, 22);
            this.tsmi_SysCongig.Text = "出库设置";
            this.tsmi_SysCongig.Click += new System.EventHandler(this.系统配置管理ToolStripMenuItem_Click);
            // 
            // tsmi_UserInfor
            // 
            this.tsmi_UserInfor.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_UserInfor.Image")));
            this.tsmi_UserInfor.Name = "tsmi_UserInfor";
            this.tsmi_UserInfor.Size = new System.Drawing.Size(124, 22);
            this.tsmi_UserInfor.Text = "权限设置";
            this.tsmi_UserInfor.Click += new System.EventHandler(this.用户信息管理ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ManageTask,
            this.tsmi_ControlTaskMana,
            this.tsmi_StockManage,
            this.tsmi_GoodsSiteMana});
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(84, 21);
            this.toolStripMenuItem3.Text = "仓储管理";
            // 
            // tsmi_ManageTask
            // 
            this.tsmi_ManageTask.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_ManageTask.Image")));
            this.tsmi_ManageTask.Name = "tsmi_ManageTask";
            this.tsmi_ManageTask.Size = new System.Drawing.Size(124, 22);
            this.tsmi_ManageTask.Text = "管理任务";
            this.tsmi_ManageTask.Click += new System.EventHandler(this.tsmi_ManageTask_Click);
            // 
            // tsmi_ControlTaskMana
            // 
            this.tsmi_ControlTaskMana.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_ControlTaskMana.Image")));
            this.tsmi_ControlTaskMana.Name = "tsmi_ControlTaskMana";
            this.tsmi_ControlTaskMana.Size = new System.Drawing.Size(124, 22);
            this.tsmi_ControlTaskMana.Text = "控制任务";
            this.tsmi_ControlTaskMana.Click += new System.EventHandler(this.tsmi_ControlTaskMana_Click);
            // 
            // tsmi_StockManage
            // 
            this.tsmi_StockManage.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_StockManage.Image")));
            this.tsmi_StockManage.Name = "tsmi_StockManage";
            this.tsmi_StockManage.Size = new System.Drawing.Size(124, 22);
            this.tsmi_StockManage.Text = "库存管理";
            this.tsmi_StockManage.Click += new System.EventHandler(this.库存管理ToolStripMenuItem_Click);
            // 
            // tsmi_GoodsSiteMana
            // 
            this.tsmi_GoodsSiteMana.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_GoodsSiteMana.Image")));
            this.tsmi_GoodsSiteMana.Name = "tsmi_GoodsSiteMana";
            this.tsmi_GoodsSiteMana.Size = new System.Drawing.Size(124, 22);
            this.tsmi_GoodsSiteMana.Text = "货位状态";
            this.tsmi_GoodsSiteMana.Click += new System.EventHandler(this.货位状态ToolStripMenuItem_Click);
            // 
            // tsmi_QueryMana
            // 
            this.tsmi_QueryMana.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_LogQuery,
            this.tsmi_HistoryTaskMana,
            this.tsmi_TrayTrace});
            this.tsmi_QueryMana.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_QueryMana.Image")));
            this.tsmi_QueryMana.Name = "tsmi_QueryMana";
            this.tsmi_QueryMana.Size = new System.Drawing.Size(84, 21);
            this.tsmi_QueryMana.Text = "统计查询";
            // 
            // tsmi_LogQuery
            // 
            this.tsmi_LogQuery.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_LogQuery.Image")));
            this.tsmi_LogQuery.Name = "tsmi_LogQuery";
            this.tsmi_LogQuery.Size = new System.Drawing.Size(148, 22);
            this.tsmi_LogQuery.Text = "日志管理";
            this.tsmi_LogQuery.Click += new System.EventHandler(this.tsmi_LogMana_Click);
            // 
            // tsmi_HistoryTaskMana
            // 
            this.tsmi_HistoryTaskMana.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_HistoryTaskMana.Image")));
            this.tsmi_HistoryTaskMana.Name = "tsmi_HistoryTaskMana";
            this.tsmi_HistoryTaskMana.Size = new System.Drawing.Size(148, 22);
            this.tsmi_HistoryTaskMana.Text = "历史任务查询";
            this.tsmi_HistoryTaskMana.Click += new System.EventHandler(this.历史任务查询ToolStripMenuItem_Click);
            // 
            // tsmi_TrayTrace
            // 
            this.tsmi_TrayTrace.Name = "tsmi_TrayTrace";
            this.tsmi_TrayTrace.Size = new System.Drawing.Size(148, 22);
            this.tsmi_TrayTrace.Text = "托盘跟踪";
            this.tsmi_TrayTrace.Click += new System.EventHandler(this.tsmi_TrayTrace_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ProductMana,
            this.tsmi_DataMana,
            this.tsmi_DeviceMonitor});
            this.工具ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("工具ToolStripMenuItem.Image")));
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(108, 21);
            this.工具ToolStripMenuItem.Text = "系统数据维护";
            // 
            // tsmi_ProductMana
            // 
            this.tsmi_ProductMana.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_ProductMana.Image")));
            this.tsmi_ProductMana.Name = "tsmi_ProductMana";
            this.tsmi_ProductMana.Size = new System.Drawing.Size(148, 22);
            this.tsmi_ProductMana.Text = "产品信息管理";
            this.tsmi_ProductMana.Click += new System.EventHandler(this.产品信息管理ToolStripMenuItem_Click);
            // 
            // tsmi_DataMana
            // 
            this.tsmi_DataMana.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_DataMana.Image")));
            this.tsmi_DataMana.Name = "tsmi_DataMana";
            this.tsmi_DataMana.Size = new System.Drawing.Size(148, 22);
            this.tsmi_DataMana.Text = "数据管理";
            this.tsmi_DataMana.Click += new System.EventHandler(this.数据管理ToolStripMenuItem_Click);
            // 
            // tsmi_DeviceMonitor
            // 
            this.tsmi_DeviceMonitor.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_DeviceMonitor.Image")));
            this.tsmi_DeviceMonitor.Name = "tsmi_DeviceMonitor";
            this.tsmi_DeviceMonitor.Size = new System.Drawing.Size(148, 22);
            this.tsmi_DeviceMonitor.Text = "设备监控";
            this.tsmi_DeviceMonitor.Click += new System.EventHandler(this.tsmi_DeviceMonitor_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_DataMonitor});
            this.测试ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("测试ToolStripMenuItem.Image")));
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.测试ToolStripMenuItem.Text = "实用工具";
            // 
            // tsmi_DataMonitor
            // 
            this.tsmi_DataMonitor.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_DataMonitor.Image")));
            this.tsmi_DataMonitor.Name = "tsmi_DataMonitor";
            this.tsmi_DataMonitor.Size = new System.Drawing.Size(160, 22);
            this.tsmi_DataMonitor.Text = "数据监测与修正";
            this.tsmi_DataMonitor.Click += new System.EventHandler(this.控制层测试ToolStripMenuItem_Click);
            // 
            // tsmi_ChangeUser
            // 
            this.tsmi_ChangeUser.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_ChangeUser.Image")));
            this.tsmi_ChangeUser.Name = "tsmi_ChangeUser";
            this.tsmi_ChangeUser.Size = new System.Drawing.Size(84, 21);
            this.tsmi_ChangeUser.Text = "切换用户";
            this.tsmi_ChangeUser.Click += new System.EventHandler(this.切换用户ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.版本ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("帮助ToolStripMenuItem.Image")));
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 版本ToolStripMenuItem
            // 
            this.版本ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("版本ToolStripMenuItem.Image")));
            this.版本ToolStripMenuItem.Name = "版本ToolStripMenuItem";
            this.版本ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.版本ToolStripMenuItem.Text = "版本";
            this.版本ToolStripMenuItem.Click += new System.EventHandler(this.版本ToolStripMenuItem_Click);
            // 
            // licenceTime
            // 
            this.licenceTime.Enabled = true;
            this.licenceTime.Interval = 10000;
            this.licenceTime.Tick += new System.EventHandler(this.licenceTime_Tick);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(934, 521);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainView_FormClosing);
            this.Shown += new System.EventHandler(this.MainView_Shown);
            this.SizeChanged += new System.EventHandler(this.MainView_SizeChanged);
            this.menuStrip_log.ResumeLayout(false);
            this.cms_CommDeviceStatus.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tb_CommStatus.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DeviceStatus)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView ListView_Log;
        private System.Windows.Forms.TabControl tb_CommStatus;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_StartSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_StopSystem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ControlTaskMana;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ManageTask;
        private System.Windows.Forms.ToolStripMenuItem tsmi_UserInfor;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SysCongig;
        private System.Windows.Forms.ToolStripMenuItem tsmi_GoodsSiteMana;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DataMana;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ChangeUser;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openLog;
        private System.Windows.Forms.ToolStripMenuItem tsmi_closeLog;
        private System.Windows.Forms.ToolStripMenuItem tsmi_StockManage;
        private System.Windows.Forms.ColumnHeader log_Image;
        private System.Windows.Forms.ColumnHeader log_Content;
        private System.Windows.Forms.ColumnHeader log_Date;
        private System.Windows.Forms.ColumnHeader log_Type;
        private System.Windows.Forms.ContextMenuStrip menuStrip_log;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv_DeviceStatus;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader log_Category;
        private System.Windows.Forms.ToolStripStatusLabel tssl_currentpage;
        private System.Windows.Forms.ToolStripStatusLabel tssl_CurrentViewName;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ProductMana;
        private System.Windows.Forms.ColumnHeader log_ErrorCode;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DeviceMonitor;
        private System.Windows.Forms.ContextMenuStrip cms_CommDeviceStatus;
        private System.Windows.Forms.ToolStripMenuItem 连接ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_QueryMana;
        private System.Windows.Forms.ToolStripMenuItem tsmi_LogQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmi_HistoryTaskMana;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Splitter splitter2;
  		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_BatchList;
        private System.Windows.Forms.Button bt_AddSet;
        private System.Windows.Forms.Button bt_RefreshBatch;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DataMonitor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_A1OutBatch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_HouseName;
        private System.Windows.Forms.Label lb_B1OutBatch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_LogType;
        private System.Windows.Forms.ToolStripMenuItem 堆垛机通信ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStackerCommClose;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStackerCommReOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmi_TrayTrace;
        private System.Windows.Forms.Timer licenceTime;

    }
}

