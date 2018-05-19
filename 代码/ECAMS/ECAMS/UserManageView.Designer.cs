namespace ECAMS
{
    partial class UserManageView
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_userManageExit = new System.Windows.Forms.Button();
            this.bt_deleteUser = new System.Windows.Forms.Button();
            this.bt_saveUser = new System.Windows.Forms.Button();
            this.bt_NewUser = new System.Windows.Forms.Button();
            this.cb_AllRoleList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_usersList = new System.Windows.Forms.DataGridView();
            this.userInforID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userRoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_remark = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bt_RoleSave = new System.Windows.Forms.Button();
            this.bt_RoleManageExit = new System.Windows.Forms.Button();
            this.bt_deleteRole = new System.Windows.Forms.Button();
            this.bt_NewRole = new System.Windows.Forms.Button();
            this.tb_RoleName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgv_FunctionList = new System.Windows.Forms.DataGridView();
            this.functionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgv_SetFunction = new System.Windows.Forms.DataGridView();
            this.functionIDTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionNameTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarkTemp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_DeleteFunc = new System.Windows.Forms.Button();
            this.bt_roleAddFunc = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.dgv_roleList = new System.Windows.Forms.DataGridView();
            this.roleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_usersList)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FunctionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SetFunction)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_roleList)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(907, 527);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(899, 502);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "用户管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(893, 496);
            this.tableLayoutPanel4.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_userManageExit);
            this.groupBox2.Controls.Add(this.bt_deleteUser);
            this.groupBox2.Controls.Add(this.bt_saveUser);
            this.groupBox2.Controls.Add(this.bt_NewUser);
            this.groupBox2.Controls.Add(this.cb_AllRoleList);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tb_Password);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tb_userName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(887, 67);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新建用户";
            // 
            // bt_userManageExit
            // 
            this.bt_userManageExit.Location = new System.Drawing.Point(788, 27);
            this.bt_userManageExit.Name = "bt_userManageExit";
            this.bt_userManageExit.Size = new System.Drawing.Size(74, 23);
            this.bt_userManageExit.TabIndex = 15;
            this.bt_userManageExit.Text = "退出";
            this.bt_userManageExit.UseVisualStyleBackColor = true;
            this.bt_userManageExit.Click += new System.EventHandler(this.bt_userManageExit_Click);
            // 
            // bt_deleteUser
            // 
            this.bt_deleteUser.Location = new System.Drawing.Point(699, 27);
            this.bt_deleteUser.Name = "bt_deleteUser";
            this.bt_deleteUser.Size = new System.Drawing.Size(74, 23);
            this.bt_deleteUser.TabIndex = 14;
            this.bt_deleteUser.Text = "删除";
            this.bt_deleteUser.UseVisualStyleBackColor = true;
            this.bt_deleteUser.Click += new System.EventHandler(this.bt_deleteUser_Click);
            // 
            // bt_saveUser
            // 
            this.bt_saveUser.Location = new System.Drawing.Point(619, 27);
            this.bt_saveUser.Name = "bt_saveUser";
            this.bt_saveUser.Size = new System.Drawing.Size(74, 23);
            this.bt_saveUser.TabIndex = 13;
            this.bt_saveUser.Text = "保存";
            this.bt_saveUser.UseVisualStyleBackColor = true;
            this.bt_saveUser.Click += new System.EventHandler(this.bt_saveUser_Click);
            // 
            // bt_NewUser
            // 
            this.bt_NewUser.Location = new System.Drawing.Point(530, 27);
            this.bt_NewUser.Name = "bt_NewUser";
            this.bt_NewUser.Size = new System.Drawing.Size(74, 23);
            this.bt_NewUser.TabIndex = 12;
            this.bt_NewUser.Text = "新建";
            this.bt_NewUser.UseVisualStyleBackColor = true;
            this.bt_NewUser.Click += new System.EventHandler(this.bt_NewUser_Click);
            // 
            // cb_AllRoleList
            // 
            this.cb_AllRoleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AllRoleList.FormattingEnabled = true;
            this.cb_AllRoleList.Items.AddRange(new object[] {
            "一般用户",
            "管理员",
            "系统管理员"});
            this.cb_AllRoleList.Location = new System.Drawing.Point(87, 28);
            this.cb_AllRoleList.Name = "cb_AllRoleList";
            this.cb_AllRoleList.Size = new System.Drawing.Size(108, 20);
            this.cb_AllRoleList.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "用户角色：";
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(412, 28);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.Size = new System.Drawing.Size(100, 21);
            this.tb_Password.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "密码：";
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(256, 28);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(100, 21);
            this.tb_userName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户名：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_usersList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(887, 417);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "用户信息";
            // 
            // dgv_usersList
            // 
            this.dgv_usersList.AllowUserToAddRows = false;
            this.dgv_usersList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_usersList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userInforID,
            this.userName,
            this.userPassword,
            this.userRoleName});
            this.dgv_usersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_usersList.Location = new System.Drawing.Point(3, 17);
            this.dgv_usersList.Name = "dgv_usersList";
            this.dgv_usersList.RowHeadersVisible = false;
            this.dgv_usersList.RowTemplate.Height = 23;
            this.dgv_usersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_usersList.Size = new System.Drawing.Size(881, 397);
            this.dgv_usersList.TabIndex = 0;
            this.dgv_usersList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_usersList_CellClick);
            // 
            // userInforID
            // 
            this.userInforID.HeaderText = "用户信息ID";
            this.userInforID.Name = "userInforID";
            // 
            // userName
            // 
            this.userName.HeaderText = "用户名";
            this.userName.Name = "userName";
            // 
            // userPassword
            // 
            this.userPassword.HeaderText = "用户密码";
            this.userPassword.Name = "userPassword";
            // 
            // userRoleName
            // 
            this.userRoleName.HeaderText = "用户角色";
            this.userRoleName.Name = "userRoleName";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(899, 502);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "角色管理";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.groupBox8, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.89137F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.10863F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(893, 496);
            this.tableLayoutPanel3.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tb_remark);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.bt_RoleSave);
            this.groupBox1.Controls.Add(this.bt_RoleManageExit);
            this.groupBox1.Controls.Add(this.bt_deleteRole);
            this.groupBox1.Controls.Add(this.bt_NewRole);
            this.groupBox1.Controls.Add(this.tb_RoleName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(887, 69);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角色信息";
            // 
            // tb_remark
            // 
            this.tb_remark.Location = new System.Drawing.Point(247, 26);
            this.tb_remark.Name = "tb_remark";
            this.tb_remark.Size = new System.Drawing.Size(100, 21);
            this.tb_remark.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "备注：";
            // 
            // bt_RoleSave
            // 
            this.bt_RoleSave.Location = new System.Drawing.Point(572, 26);
            this.bt_RoleSave.Name = "bt_RoleSave";
            this.bt_RoleSave.Size = new System.Drawing.Size(74, 23);
            this.bt_RoleSave.TabIndex = 16;
            this.bt_RoleSave.Text = "保存";
            this.bt_RoleSave.UseVisualStyleBackColor = true;
            this.bt_RoleSave.Click += new System.EventHandler(this.bt_RoleSave_Click);
            // 
            // bt_RoleManageExit
            // 
            this.bt_RoleManageExit.Location = new System.Drawing.Point(660, 26);
            this.bt_RoleManageExit.Name = "bt_RoleManageExit";
            this.bt_RoleManageExit.Size = new System.Drawing.Size(74, 23);
            this.bt_RoleManageExit.TabIndex = 15;
            this.bt_RoleManageExit.Text = "退出";
            this.bt_RoleManageExit.UseVisualStyleBackColor = true;
            this.bt_RoleManageExit.Click += new System.EventHandler(this.bt_RoleManageExit_Click);
            // 
            // bt_deleteRole
            // 
            this.bt_deleteRole.Location = new System.Drawing.Point(484, 26);
            this.bt_deleteRole.Name = "bt_deleteRole";
            this.bt_deleteRole.Size = new System.Drawing.Size(74, 23);
            this.bt_deleteRole.TabIndex = 14;
            this.bt_deleteRole.Text = "删除";
            this.bt_deleteRole.UseVisualStyleBackColor = true;
            this.bt_deleteRole.Click += new System.EventHandler(this.bt_deleteRole_Click);
            // 
            // bt_NewRole
            // 
            this.bt_NewRole.Location = new System.Drawing.Point(394, 26);
            this.bt_NewRole.Name = "bt_NewRole";
            this.bt_NewRole.Size = new System.Drawing.Size(74, 23);
            this.bt_NewRole.TabIndex = 12;
            this.bt_NewRole.Text = "新建";
            this.bt_NewRole.UseVisualStyleBackColor = true;
            this.bt_NewRole.Click += new System.EventHandler(this.bt_NewRole_Click);
            // 
            // tb_RoleName
            // 
            this.tb_RoleName.Location = new System.Drawing.Point(84, 26);
            this.tb_RoleName.Name = "tb_RoleName";
            this.tb_RoleName.Size = new System.Drawing.Size(100, 21);
            this.tb_RoleName.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "用户角色：";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.tableLayoutPanel1);
            this.groupBox4.Location = new System.Drawing.Point(3, 78);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(887, 278);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "权限分配";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 258);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgv_FunctionList);
            this.groupBox5.Controls.Add(this.dataGridView1);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(407, 252);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "功能列表";
            // 
            // dgv_FunctionList
            // 
            this.dgv_FunctionList.AllowUserToAddRows = false;
            this.dgv_FunctionList.AllowUserToResizeRows = false;
            this.dgv_FunctionList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FunctionList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.functionID,
            this.functionName,
            this.remark});
            this.dgv_FunctionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_FunctionList.Location = new System.Drawing.Point(3, 17);
            this.dgv_FunctionList.Name = "dgv_FunctionList";
            this.dgv_FunctionList.RowHeadersVisible = false;
            this.dgv_FunctionList.RowTemplate.Height = 23;
            this.dgv_FunctionList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_FunctionList.Size = new System.Drawing.Size(401, 232);
            this.dgv_FunctionList.TabIndex = 3;
            // 
            // functionID
            // 
            this.functionID.HeaderText = "功能ID";
            this.functionID.Name = "functionID";
            // 
            // functionName
            // 
            this.functionName.HeaderText = "功能名称";
            this.functionName.Name = "functionName";
            // 
            // remark
            // 
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(401, 232);
            this.dataGridView1.TabIndex = 2;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgv_SetFunction);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(471, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(407, 252);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "已分配功能";
            // 
            // dgv_SetFunction
            // 
            this.dgv_SetFunction.AllowUserToAddRows = false;
            this.dgv_SetFunction.AllowUserToResizeRows = false;
            this.dgv_SetFunction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SetFunction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.functionIDTemp,
            this.functionNameTemp,
            this.remarkTemp});
            this.dgv_SetFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_SetFunction.Location = new System.Drawing.Point(3, 17);
            this.dgv_SetFunction.Name = "dgv_SetFunction";
            this.dgv_SetFunction.RowHeadersVisible = false;
            this.dgv_SetFunction.RowTemplate.Height = 23;
            this.dgv_SetFunction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_SetFunction.Size = new System.Drawing.Size(401, 232);
            this.dgv_SetFunction.TabIndex = 4;
            // 
            // functionIDTemp
            // 
            this.functionIDTemp.HeaderText = "功能ID";
            this.functionIDTemp.Name = "functionIDTemp";
            // 
            // functionNameTemp
            // 
            this.functionNameTemp.HeaderText = "功能名称";
            this.functionNameTemp.Name = "functionNameTemp";
            // 
            // remarkTemp
            // 
            this.remarkTemp.HeaderText = "备注";
            this.remarkTemp.Name = "remarkTemp";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.bt_DeleteFunc, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.bt_roleAddFunc, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(416, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(49, 252);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // bt_DeleteFunc
            // 
            this.bt_DeleteFunc.Location = new System.Drawing.Point(3, 129);
            this.bt_DeleteFunc.Name = "bt_DeleteFunc";
            this.bt_DeleteFunc.Size = new System.Drawing.Size(38, 23);
            this.bt_DeleteFunc.TabIndex = 8;
            this.bt_DeleteFunc.Text = "<<";
            this.bt_DeleteFunc.UseVisualStyleBackColor = true;
            this.bt_DeleteFunc.Click += new System.EventHandler(this.bt_DeleteFunc_Click);
            // 
            // bt_roleAddFunc
            // 
            this.bt_roleAddFunc.Location = new System.Drawing.Point(3, 66);
            this.bt_roleAddFunc.Name = "bt_roleAddFunc";
            this.bt_roleAddFunc.Size = new System.Drawing.Size(38, 23);
            this.bt_roleAddFunc.TabIndex = 7;
            this.bt_roleAddFunc.Text = ">>";
            this.bt_roleAddFunc.UseVisualStyleBackColor = true;
            this.bt_roleAddFunc.Click += new System.EventHandler(this.bt_roleAddFunc_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.dgv_roleList);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(3, 362);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(887, 131);
            this.groupBox8.TabIndex = 15;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "角色列表";
            // 
            // dgv_roleList
            // 
            this.dgv_roleList.AllowUserToAddRows = false;
            this.dgv_roleList.AllowUserToResizeRows = false;
            this.dgv_roleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_roleList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roleID,
            this.RoleName,
            this.roleRemark});
            this.dgv_roleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_roleList.Location = new System.Drawing.Point(3, 17);
            this.dgv_roleList.Name = "dgv_roleList";
            this.dgv_roleList.RowHeadersVisible = false;
            this.dgv_roleList.RowTemplate.Height = 23;
            this.dgv_roleList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_roleList.Size = new System.Drawing.Size(881, 111);
            this.dgv_roleList.TabIndex = 4;
            this.dgv_roleList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_roleList_CellMouseClick);
            // 
            // roleID
            // 
            this.roleID.HeaderText = "角色ID";
            this.roleID.Name = "roleID";
            // 
            // RoleName
            // 
            this.RoleName.HeaderText = "角色名称";
            this.RoleName.Name = "RoleName";
            // 
            // roleRemark
            // 
            this.roleRemark.HeaderText = "备注";
            this.roleRemark.Name = "roleRemark";
            // 
            // UserManageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 527);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserManageView";
            this.Text = "HandTaskView";
            this.Load += new System.EventHandler(this.UserManageView_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_usersList)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FunctionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SetFunction)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_roleList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_usersList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.ComboBox cb_AllRoleList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_RoleName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgv_FunctionList;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bt_DeleteFunc;
        private System.Windows.Forms.Button bt_roleAddFunc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.DataGridView dgv_roleList;
        private System.Windows.Forms.Button bt_NewRole;
        private System.Windows.Forms.Button bt_deleteRole;
        private System.Windows.Forms.Button bt_NewUser;
        private System.Windows.Forms.Button bt_saveUser;
        private System.Windows.Forms.Button bt_deleteUser;
        private System.Windows.Forms.Button bt_userManageExit;
        private System.Windows.Forms.Button bt_RoleManageExit;
        private System.Windows.Forms.DataGridView dgv_SetFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.Button bt_RoleSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionIDTemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionNameTemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarkTemp;
        private System.Windows.Forms.TextBox tb_remark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn userInforID;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn userPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn userRoleName;

    }
}