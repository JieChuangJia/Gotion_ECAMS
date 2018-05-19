namespace ECAMS
{
    partial class StockManaView
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
            this.cms_StockMana = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动出库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_TrayDetail = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.料框详细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lb_CoreNum = new System.Windows.Forms.Label();
            this.lb_ProductPatch = new System.Windows.Forms.Label();
            this.lb_HouseName = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_OutStorageBatchNum = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cb_QueryGSTaskType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_QueryGSTaskStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_QueryGSStoreType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tscb_StockLayer = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tscb_workFlowTime = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tscb_ProductStatus = new System.Windows.Forms.ComboBox();
            this.tscb_StockColumn = new System.Windows.Forms.ComboBox();
            this.tscb_StockRow = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tscb_StoreHouse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_GsStatusModify = new System.Windows.Forms.GroupBox();
            this.tsb_SetByHand = new System.Windows.Forms.Button();
            this.tscb_TaskRunStatus = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tscb_GoodsSiteStatus = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_GSDetail = new System.Windows.Forms.DataGridView();
            this.gsID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrayID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gsStoreStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gsRunStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GsTaskStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNameStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductStatusStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntoHouseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockIDStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_StockQuery = new System.Windows.Forms.DataGridView();
            this.StockID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductPatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.goodsSiteID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoodsSiteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gs_StoreStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gs_RunStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gsTaskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InHouseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.bt_RefreshStorage = new System.Windows.Forms.Button();
            this.bt_OutHouseByHand = new System.Windows.Forms.Button();
            this.cms_StockMana.SuspendLayout();
            this.cms_TrayDetail.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gb_GsStatusModify.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GSDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StockQuery)).BeginInit();
            this.SuspendLayout();
            // 
            // cms_StockMana
            // 
            this.cms_StockMana.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查询ToolStripMenuItem,
            this.手动设置ToolStripMenuItem,
            this.手动出库ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.cms_StockMana.Name = "cms_StockMana";
            this.cms_StockMana.Size = new System.Drawing.Size(149, 92);
            // 
            // 查询ToolStripMenuItem
            // 
            this.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem";
            this.查询ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.查询ToolStripMenuItem.Text = "刷新";
            this.查询ToolStripMenuItem.Click += new System.EventHandler(this.查询ToolStripMenuItem_Click);
            // 
            // 手动设置ToolStripMenuItem
            // 
            this.手动设置ToolStripMenuItem.Name = "手动设置ToolStripMenuItem";
            this.手动设置ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.手动设置ToolStripMenuItem.Text = "修改";
            this.手动设置ToolStripMenuItem.Click += new System.EventHandler(this.手动设置ToolStripMenuItem_Click);
            // 
            // 手动出库ToolStripMenuItem
            // 
            this.手动出库ToolStripMenuItem.Name = "手动出库ToolStripMenuItem";
            this.手动出库ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.手动出库ToolStripMenuItem.Text = "手动批量出库";
            this.手动出库ToolStripMenuItem.Click += new System.EventHandler(this.手动出库ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // cms_TrayDetail
            // 
            this.cms_TrayDetail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.料框详细ToolStripMenuItem});
            this.cms_TrayDetail.Name = "cms_TrayDetail";
            this.cms_TrayDetail.Size = new System.Drawing.Size(125, 26);
            // 
            // 料框详细ToolStripMenuItem
            // 
            this.料框详细ToolStripMenuItem.Name = "料框详细ToolStripMenuItem";
            this.料框详细ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.料框详细ToolStripMenuItem.Text = "料框详细";
            this.料框详细ToolStripMenuItem.Click += new System.EventHandler(this.料框详细ToolStripMenuItem_Click);
            // 
            // lb_CoreNum
            // 
            this.lb_CoreNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_CoreNum.AutoSize = true;
            this.lb_CoreNum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_CoreNum.Location = new System.Drawing.Point(431, 450);
            this.lb_CoreNum.Name = "lb_CoreNum";
            this.lb_CoreNum.Size = new System.Drawing.Size(28, 14);
            this.lb_CoreNum.TabIndex = 29;
            this.lb_CoreNum.Text = "---";
            // 
            // lb_ProductPatch
            // 
            this.lb_ProductPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_ProductPatch.AutoSize = true;
            this.lb_ProductPatch.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ProductPatch.Location = new System.Drawing.Point(232, 450);
            this.lb_ProductPatch.Name = "lb_ProductPatch";
            this.lb_ProductPatch.Size = new System.Drawing.Size(35, 14);
            this.lb_ProductPatch.TabIndex = 28;
            this.lb_ProductPatch.Text = "所有";
            // 
            // lb_HouseName
            // 
            this.lb_HouseName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_HouseName.AutoSize = true;
            this.lb_HouseName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_HouseName.Location = new System.Drawing.Point(67, 450);
            this.lb_HouseName.Name = "lb_HouseName";
            this.lb_HouseName.Size = new System.Drawing.Size(35, 14);
            this.lb_HouseName.TabIndex = 27;
            this.lb_HouseName.Text = "所有";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(348, 450);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 14);
            this.label15.TabIndex = 26;
            this.label15.Text = "电芯数量：";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(177, 450);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 14);
            this.label14.TabIndex = 25;
            this.label14.Text = "批次：";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(12, 450);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 14);
            this.label13.TabIndex = 24;
            this.label13.Text = "库房：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_OutStorageBatchNum);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.cb_QueryGSTaskType);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cb_QueryGSTaskStatus);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cb_QueryGSStoreType);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tscb_StockLayer);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tscb_workFlowTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tscb_ProductStatus);
            this.groupBox2.Controls.Add(this.tscb_StockColumn);
            this.groupBox2.Controls.Add(this.tscb_StockRow);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tscb_StoreHouse);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(1, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(885, 65);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "货位筛选";
            // 
            // cb_OutStorageBatchNum
            // 
            this.cb_OutStorageBatchNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OutStorageBatchNum.FormattingEnabled = true;
            this.cb_OutStorageBatchNum.Location = new System.Drawing.Point(609, 42);
            this.cb_OutStorageBatchNum.Name = "cb_OutStorageBatchNum";
            this.cb_OutStorageBatchNum.Size = new System.Drawing.Size(80, 20);
            this.cb_OutStorageBatchNum.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(522, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "出库批次号：";
            // 
            // cb_QueryGSTaskType
            // 
            this.cb_QueryGSTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_QueryGSTaskType.FormattingEnabled = true;
            this.cb_QueryGSTaskType.Location = new System.Drawing.Point(95, 41);
            this.cb_QueryGSTaskType.Name = "cb_QueryGSTaskType";
            this.cb_QueryGSTaskType.Size = new System.Drawing.Size(80, 20);
            this.cb_QueryGSTaskType.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "货位任务类型：";
            // 
            // cb_QueryGSTaskStatus
            // 
            this.cb_QueryGSTaskStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_QueryGSTaskStatus.FormattingEnabled = true;
            this.cb_QueryGSTaskStatus.Location = new System.Drawing.Point(436, 41);
            this.cb_QueryGSTaskStatus.Name = "cb_QueryGSTaskStatus";
            this.cb_QueryGSTaskStatus.Size = new System.Drawing.Size(80, 20);
            this.cb_QueryGSTaskStatus.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "货位任务状态：";
            // 
            // cb_QueryGSStoreType
            // 
            this.cb_QueryGSStoreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_QueryGSStoreType.FormattingEnabled = true;
            this.cb_QueryGSStoreType.Location = new System.Drawing.Point(266, 41);
            this.cb_QueryGSStoreType.Name = "cb_QueryGSStoreType";
            this.cb_QueryGSStoreType.Size = new System.Drawing.Size(80, 20);
            this.cb_QueryGSStoreType.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(176, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "货位存储类型：";
            // 
            // tscb_StockLayer
            // 
            this.tscb_StockLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_StockLayer.FormattingEnabled = true;
            this.tscb_StockLayer.Location = new System.Drawing.Point(609, 16);
            this.tscb_StockLayer.Name = "tscb_StockLayer";
            this.tscb_StockLayer.Size = new System.Drawing.Size(80, 20);
            this.tscb_StockLayer.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(558, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "层数：";
            // 
            // tscb_workFlowTime
            // 
            this.tscb_workFlowTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_workFlowTime.FormattingEnabled = true;
            this.tscb_workFlowTime.Location = new System.Drawing.Point(760, 41);
            this.tscb_workFlowTime.Name = "tscb_workFlowTime";
            this.tscb_workFlowTime.Size = new System.Drawing.Size(90, 20);
            this.tscb_workFlowTime.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(701, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "状态时间：";
            // 
            // tscb_ProductStatus
            // 
            this.tscb_ProductStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_ProductStatus.FormattingEnabled = true;
            this.tscb_ProductStatus.Location = new System.Drawing.Point(760, 15);
            this.tscb_ProductStatus.Name = "tscb_ProductStatus";
            this.tscb_ProductStatus.Size = new System.Drawing.Size(90, 20);
            this.tscb_ProductStatus.TabIndex = 9;
            // 
            // tscb_StockColumn
            // 
            this.tscb_StockColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_StockColumn.FormattingEnabled = true;
            this.tscb_StockColumn.Location = new System.Drawing.Point(436, 15);
            this.tscb_StockColumn.Name = "tscb_StockColumn";
            this.tscb_StockColumn.Size = new System.Drawing.Size(80, 20);
            this.tscb_StockColumn.TabIndex = 8;
            // 
            // tscb_StockRow
            // 
            this.tscb_StockRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_StockRow.FormattingEnabled = true;
            this.tscb_StockRow.Location = new System.Drawing.Point(266, 15);
            this.tscb_StockRow.Name = "tscb_StockRow";
            this.tscb_StockRow.Size = new System.Drawing.Size(80, 20);
            this.tscb_StockRow.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(701, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "物料状态：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(395, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "列数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "排数：";
            // 
            // tscb_StoreHouse
            // 
            this.tscb_StoreHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_StoreHouse.FormattingEnabled = true;
            this.tscb_StoreHouse.Items.AddRange(new object[] {
            " "});
            this.tscb_StoreHouse.Location = new System.Drawing.Point(94, 15);
            this.tscb_StoreHouse.Name = "tscb_StoreHouse";
            this.tscb_StoreHouse.Size = new System.Drawing.Size(80, 20);
            this.tscb_StoreHouse.TabIndex = 1;
            this.tscb_StoreHouse.SelectedIndexChanged += new System.EventHandler(this.tscb_StoreHouse_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "存储仓库：";
            // 
            // gb_GsStatusModify
            // 
            this.gb_GsStatusModify.Controls.Add(this.tsb_SetByHand);
            this.gb_GsStatusModify.Controls.Add(this.tscb_TaskRunStatus);
            this.gb_GsStatusModify.Controls.Add(this.label12);
            this.gb_GsStatusModify.Controls.Add(this.tscb_GoodsSiteStatus);
            this.gb_GsStatusModify.Controls.Add(this.label11);
            this.gb_GsStatusModify.Location = new System.Drawing.Point(219, 68);
            this.gb_GsStatusModify.Name = "gb_GsStatusModify";
            this.gb_GsStatusModify.Size = new System.Drawing.Size(667, 42);
            this.gb_GsStatusModify.TabIndex = 20;
            this.gb_GsStatusModify.TabStop = false;
            this.gb_GsStatusModify.Text = "货位状态修改";
            // 
            // tsb_SetByHand
            // 
            this.tsb_SetByHand.Location = new System.Drawing.Point(418, 14);
            this.tsb_SetByHand.Name = "tsb_SetByHand";
            this.tsb_SetByHand.Size = new System.Drawing.Size(76, 23);
            this.tsb_SetByHand.TabIndex = 22;
            this.tsb_SetByHand.Text = "修改";
            this.tsb_SetByHand.UseVisualStyleBackColor = true;
            this.tsb_SetByHand.Click += new System.EventHandler(this.tsb_SetByHand_Click);
            // 
            // tscb_TaskRunStatus
            // 
            this.tscb_TaskRunStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_TaskRunStatus.FormattingEnabled = true;
            this.tscb_TaskRunStatus.Location = new System.Drawing.Point(311, 15);
            this.tscb_TaskRunStatus.Name = "tscb_TaskRunStatus";
            this.tscb_TaskRunStatus.Size = new System.Drawing.Size(92, 20);
            this.tscb_TaskRunStatus.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(216, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 12);
            this.label12.TabIndex = 20;
            this.label12.Text = "货位任务状态：";
            // 
            // tscb_GoodsSiteStatus
            // 
            this.tscb_GoodsSiteStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_GoodsSiteStatus.FormattingEnabled = true;
            this.tscb_GoodsSiteStatus.Location = new System.Drawing.Point(109, 15);
            this.tscb_GoodsSiteStatus.Name = "tscb_GoodsSiteStatus";
            this.tscb_GoodsSiteStatus.Size = new System.Drawing.Size(97, 20);
            this.tscb_GoodsSiteStatus.TabIndex = 19;
            this.tscb_GoodsSiteStatus.SelectedIndexChanged += new System.EventHandler(this.tscb_GoodsSiteStatus_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "货位存储状态：";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgv_GSDetail);
            this.groupBox3.Location = new System.Drawing.Point(1, 288);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(991, 149);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "货位详细";
            // 
            // dgv_GSDetail
            // 
            this.dgv_GSDetail.AllowUserToAddRows = false;
            this.dgv_GSDetail.AllowUserToDeleteRows = false;
            this.dgv_GSDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_GSDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gsID,
            this.TrayID,
            this.batch,
            this.batchType,
            this.cellCount,
            this.GsName,
            this.gsStoreStatus,
            this.gsRunStatus,
            this.GsTaskStatus,
            this.productNameStr,
            this.ProductStatusStr,
            this.IntoHouseTime,
            this.StockIDStr});
            this.dgv_GSDetail.ContextMenuStrip = this.cms_TrayDetail;
            this.dgv_GSDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_GSDetail.Location = new System.Drawing.Point(3, 17);
            this.dgv_GSDetail.Name = "dgv_GSDetail";
            this.dgv_GSDetail.RowHeadersVisible = false;
            this.dgv_GSDetail.RowTemplate.Height = 23;
            this.dgv_GSDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_GSDetail.Size = new System.Drawing.Size(985, 129);
            this.dgv_GSDetail.TabIndex = 1;
            this.dgv_GSDetail.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_GSDetail_CellMouseDoubleClick);
            // 
            // gsID
            // 
            this.gsID.HeaderText = "货位编号";
            this.gsID.Name = "gsID";
            this.gsID.Width = 78;
            // 
            // TrayID
            // 
            this.TrayID.HeaderText = "料框条码";
            this.TrayID.Name = "TrayID";
            this.TrayID.Width = 78;
            // 
            // batch
            // 
            this.batch.HeaderText = "批次号";
            this.batch.Name = "batch";
            this.batch.Width = 66;
            // 
            // batchType
            // 
            this.batchType.HeaderText = "批次类型";
            this.batchType.Name = "batchType";
            this.batchType.Width = 78;
            // 
            // cellCount
            // 
            this.cellCount.HeaderText = "电芯个数";
            this.cellCount.Name = "cellCount";
            this.cellCount.Width = 78;
            // 
            // GsName
            // 
            this.GsName.HeaderText = "货位名称";
            this.GsName.Name = "GsName";
            this.GsName.Width = 78;
            // 
            // gsStoreStatus
            // 
            this.gsStoreStatus.HeaderText = "货位存储状态";
            this.gsStoreStatus.Name = "gsStoreStatus";
            this.gsStoreStatus.Width = 102;
            // 
            // gsRunStatus
            // 
            this.gsRunStatus.HeaderText = "货位任务完成状态";
            this.gsRunStatus.Name = "gsRunStatus";
            this.gsRunStatus.Width = 126;
            // 
            // GsTaskStatus
            // 
            this.GsTaskStatus.HeaderText = "货位任务类型";
            this.GsTaskStatus.Name = "GsTaskStatus";
            this.GsTaskStatus.Width = 102;
            // 
            // productNameStr
            // 
            this.productNameStr.HeaderText = "物料名称";
            this.productNameStr.Name = "productNameStr";
            this.productNameStr.Width = 78;
            // 
            // ProductStatusStr
            // 
            this.ProductStatusStr.HeaderText = "物料状态";
            this.ProductStatusStr.Name = "ProductStatusStr";
            this.ProductStatusStr.Width = 78;
            // 
            // IntoHouseTime
            // 
            this.IntoHouseTime.HeaderText = "入库时间";
            this.IntoHouseTime.Name = "IntoHouseTime";
            this.IntoHouseTime.Width = 78;
            // 
            // StockIDStr
            // 
            this.StockIDStr.HeaderText = "库存ID";
            this.StockIDStr.Name = "StockIDStr";
            this.StockIDStr.Width = 66;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgv_StockQuery);
            this.groupBox1.Location = new System.Drawing.Point(3, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(991, 172);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "库存信息";
            // 
            // dgv_StockQuery
            // 
            this.dgv_StockQuery.AllowUserToAddRows = false;
            this.dgv_StockQuery.AllowUserToDeleteRows = false;
            this.dgv_StockQuery.AllowUserToResizeRows = false;
            this.dgv_StockQuery.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_StockQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_StockQuery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockID,
            this.ProductPatch,
            this.goodsSiteID,
            this.ProductCode,
            this.ProductStatus,
            this.ProductNum,
            this.ProductName,
            this.GoodsSiteName,
            this.gs_StoreStatus,
            this.gs_RunStatus,
            this.gsTaskType,
            this.InHouseTime,
            this.updateTime,
            this.remarks});
            this.dgv_StockQuery.ContextMenuStrip = this.cms_StockMana;
            this.dgv_StockQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_StockQuery.Location = new System.Drawing.Point(3, 17);
            this.dgv_StockQuery.Name = "dgv_StockQuery";
            this.dgv_StockQuery.RowHeadersVisible = false;
            this.dgv_StockQuery.RowTemplate.Height = 23;
            this.dgv_StockQuery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_StockQuery.Size = new System.Drawing.Size(985, 152);
            this.dgv_StockQuery.TabIndex = 0;
            this.dgv_StockQuery.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_StockQuery_CellClick);
            // 
            // StockID
            // 
            this.StockID.HeaderText = "库存ID";
            this.StockID.Name = "StockID";
            this.StockID.Width = 66;
            // 
            // ProductPatch
            // 
            this.ProductPatch.HeaderText = "物料批次";
            this.ProductPatch.Name = "ProductPatch";
            this.ProductPatch.Width = 78;
            // 
            // goodsSiteID
            // 
            this.goodsSiteID.HeaderText = "货位ID";
            this.goodsSiteID.Name = "goodsSiteID";
            this.goodsSiteID.Visible = false;
            this.goodsSiteID.Width = 66;
            // 
            // ProductCode
            // 
            this.ProductCode.HeaderText = "物料ID";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Width = 66;
            // 
            // ProductStatus
            // 
            this.ProductStatus.HeaderText = "物料状态";
            this.ProductStatus.Name = "ProductStatus";
            this.ProductStatus.Width = 78;
            // 
            // ProductNum
            // 
            this.ProductNum.HeaderText = "物料数量";
            this.ProductNum.Name = "ProductNum";
            this.ProductNum.Visible = false;
            this.ProductNum.Width = 78;
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "物料名称";
            this.ProductName.Name = "ProductName";
            this.ProductName.Width = 78;
            // 
            // GoodsSiteName
            // 
            this.GoodsSiteName.HeaderText = "货位名称";
            this.GoodsSiteName.Name = "GoodsSiteName";
            this.GoodsSiteName.Width = 78;
            // 
            // gs_StoreStatus
            // 
            this.gs_StoreStatus.HeaderText = "货位存储状态";
            this.gs_StoreStatus.Name = "gs_StoreStatus";
            this.gs_StoreStatus.Width = 102;
            // 
            // gs_RunStatus
            // 
            this.gs_RunStatus.HeaderText = "货位任务状态";
            this.gs_RunStatus.Name = "gs_RunStatus";
            this.gs_RunStatus.Width = 102;
            // 
            // gsTaskType
            // 
            this.gsTaskType.HeaderText = "货位任务类型";
            this.gsTaskType.Name = "gsTaskType";
            this.gsTaskType.Width = 102;
            // 
            // InHouseTime
            // 
            this.InHouseTime.HeaderText = "入库时间";
            this.InHouseTime.Name = "InHouseTime";
            this.InHouseTime.Width = 78;
            // 
            // updateTime
            // 
            this.updateTime.HeaderText = "更新时间";
            this.updateTime.Name = "updateTime";
            this.updateTime.Width = 78;
            // 
            // remarks
            // 
            this.remarks.HeaderText = "备注";
            this.remarks.Name = "remarks";
            this.remarks.Visible = false;
            this.remarks.Width = 54;
            // 
            // bt_Exit
            // 
            this.bt_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Exit.Location = new System.Drawing.Point(892, 10);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(76, 23);
            this.bt_Exit.TabIndex = 23;
            this.bt_Exit.Text = "退出";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.tsbt_Exit_Click);
            // 
            // bt_RefreshStorage
            // 
            this.bt_RefreshStorage.Location = new System.Drawing.Point(5, 78);
            this.bt_RefreshStorage.Name = "bt_RefreshStorage";
            this.bt_RefreshStorage.Size = new System.Drawing.Size(90, 23);
            this.bt_RefreshStorage.TabIndex = 16;
            this.bt_RefreshStorage.Text = "刷新";
            this.bt_RefreshStorage.UseVisualStyleBackColor = true;
            this.bt_RefreshStorage.Click += new System.EventHandler(this.tsb_QueryStockList_Click);
            // 
            // bt_OutHouseByHand
            // 
            this.bt_OutHouseByHand.Location = new System.Drawing.Point(101, 78);
            this.bt_OutHouseByHand.Name = "bt_OutHouseByHand";
            this.bt_OutHouseByHand.Size = new System.Drawing.Size(99, 23);
            this.bt_OutHouseByHand.TabIndex = 17;
            this.bt_OutHouseByHand.Text = "手动批量出库";
            this.bt_OutHouseByHand.UseVisualStyleBackColor = true;
            this.bt_OutHouseByHand.Click += new System.EventHandler(this.tsbt_OutByHand_Click);
            // 
            // StockManaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 481);
            this.Controls.Add(this.lb_CoreNum);
            this.Controls.Add(this.lb_ProductPatch);
            this.Controls.Add(this.lb_HouseName);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gb_GsStatusModify);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.bt_RefreshStorage);
            this.Controls.Add(this.bt_OutHouseByHand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockManaView";
            this.Text = "StockQueryView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StockManaView_FormClosing);
            this.Load += new System.EventHandler(this.StockManaView_Load);
            this.cms_StockMana.ResumeLayout(false);
            this.cms_TrayDetail.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gb_GsStatusModify.ResumeLayout(false);
            this.gb_GsStatusModify.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GSDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StockQuery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_StockQuery;
        private System.Windows.Forms.ContextMenuStrip cms_StockMana;
        private System.Windows.Forms.ToolStripMenuItem 手动出库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tscb_StoreHouse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tscb_workFlowTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox tscb_ProductStatus;
        private System.Windows.Forms.ComboBox tscb_StockColumn;
        private System.Windows.Forms.ComboBox tscb_StockRow;
        private System.Windows.Forms.ComboBox tscb_StockLayer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem 查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手动设置ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cb_QueryGSTaskType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_QueryGSTaskStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_QueryGSStoreType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_OutStorageBatchNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button bt_RefreshStorage;
        private System.Windows.Forms.Button bt_OutHouseByHand;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox tscb_GoodsSiteStatus;
        private System.Windows.Forms.ComboBox tscb_TaskRunStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button tsb_SetByHand;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductPatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn goodsSiteID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodsSiteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gs_StoreStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn gs_RunStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn gsTaskType;
        private System.Windows.Forms.DataGridViewTextBoxColumn InHouseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn updateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarks;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lb_HouseName;
        private System.Windows.Forms.Label lb_ProductPatch;
        private System.Windows.Forms.Label lb_CoreNum;
        private System.Windows.Forms.GroupBox gb_GsStatusModify;
        private System.Windows.Forms.DataGridView dgv_GSDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn gsID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrayID;
        private System.Windows.Forms.DataGridViewTextBoxColumn batch;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn gsStoreStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn gsRunStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn GsTaskStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNameStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductStatusStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntoHouseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockIDStr;
        private System.Windows.Forms.ContextMenuStrip cms_TrayDetail;
        private System.Windows.Forms.ToolStripMenuItem 料框详细ToolStripMenuItem;

    }
}