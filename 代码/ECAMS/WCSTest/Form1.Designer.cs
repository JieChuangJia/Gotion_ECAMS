namespace WCSTest
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDevClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDevID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxHouse = new System.Windows.Forms.ComboBox();
            this.comboBoxHouseInTasks = new System.Windows.Forms.ComboBox();
            this.buttonResetStacker1001 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton_2002_empty = new System.Windows.Forms.RadioButton();
            this.radioButton_2002_full = new System.Windows.Forms.RadioButton();
            this.buttonHouseInA1TaskReset = new System.Windows.Forms.Button();
            this.buttonHouseInA1NewTask = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button5001Reset = new System.Windows.Forms.Button();
            this.button5001NewTask = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.comboBoxGripTask = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPalletID = new System.Windows.Forms.TextBox();
            this.buttonApplyGripTask = new System.Windows.Forms.Button();
            this.buttonGenerateGrispData = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonWcsStart = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClearLog);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.buttonWcsStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1115, 712);
            this.panel1.TabIndex = 0;
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(259, 1);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(122, 34);
            this.buttonClearLog.TabIndex = 2;
            this.buttonClearLog.Text = "清空日志";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(3, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1112, 509);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1104, 483);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "入库模拟";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightBlue;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.68493F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.315068F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 444F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1098, 477);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkOrange;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1086, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "A1,B1库";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Teal;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(6, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1086, 438);
            this.panel2.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDevClear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxDevID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxHouse);
            this.groupBox1.Controls.Add(this.comboBoxHouseInTasks);
            this.groupBox1.Controls.Add(this.buttonResetStacker1001);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radioButton_2002_empty);
            this.groupBox1.Controls.Add(this.radioButton_2002_full);
            this.groupBox1.Controls.Add(this.buttonHouseInA1TaskReset);
            this.groupBox1.Controls.Add(this.buttonHouseInA1NewTask);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1086, 438);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // buttonDevClear
            // 
            this.buttonDevClear.Location = new System.Drawing.Point(426, 81);
            this.buttonDevClear.Name = "buttonDevClear";
            this.buttonDevClear.Size = new System.Drawing.Size(160, 45);
            this.buttonDevClear.TabIndex = 8;
            this.buttonDevClear.Text = "数据清零";
            this.buttonDevClear.UseVisualStyleBackColor = true;
            this.buttonDevClear.Click += new System.EventHandler(this.buttonDevClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(424, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "设备号";
            // 
            // textBoxDevID
            // 
            this.textBoxDevID.Location = new System.Drawing.Point(486, 36);
            this.textBoxDevID.Name = "textBoxDevID";
            this.textBoxDevID.Size = new System.Drawing.Size(100, 21);
            this.textBoxDevID.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "库房";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "入库类型";
            // 
            // comboBoxHouse
            // 
            this.comboBoxHouse.FormattingEnabled = true;
            this.comboBoxHouse.Items.AddRange(new object[] {
            "A1",
            "B1"});
            this.comboBoxHouse.Location = new System.Drawing.Point(76, 20);
            this.comboBoxHouse.Name = "comboBoxHouse";
            this.comboBoxHouse.Size = new System.Drawing.Size(121, 20);
            this.comboBoxHouse.TabIndex = 4;
            // 
            // comboBoxHouseInTasks
            // 
            this.comboBoxHouseInTasks.FormattingEnabled = true;
            this.comboBoxHouseInTasks.Location = new System.Drawing.Point(76, 63);
            this.comboBoxHouseInTasks.Name = "comboBoxHouseInTasks";
            this.comboBoxHouseInTasks.Size = new System.Drawing.Size(121, 20);
            this.comboBoxHouseInTasks.TabIndex = 4;
            // 
            // buttonResetStacker1001
            // 
            this.buttonResetStacker1001.Location = new System.Drawing.Point(75, 185);
            this.buttonResetStacker1001.Name = "buttonResetStacker1001";
            this.buttonResetStacker1001.Size = new System.Drawing.Size(242, 41);
            this.buttonResetStacker1001.TabIndex = 3;
            this.buttonResetStacker1001.Text = "复位";
            this.buttonResetStacker1001.UseVisualStyleBackColor = true;
            this.buttonResetStacker1001.Click += new System.EventHandler(this.buttonResetStacker1001_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "任务申请";
            // 
            // radioButton_2002_empty
            // 
            this.radioButton_2002_empty.AutoSize = true;
            this.radioButton_2002_empty.Checked = true;
            this.radioButton_2002_empty.Location = new System.Drawing.Point(96, 103);
            this.radioButton_2002_empty.Name = "radioButton_2002_empty";
            this.radioButton_2002_empty.Size = new System.Drawing.Size(59, 16);
            this.radioButton_2002_empty.TabIndex = 1;
            this.radioButton_2002_empty.TabStop = true;
            this.radioButton_2002_empty.Text = "入口空";
            this.radioButton_2002_empty.UseVisualStyleBackColor = true;
            // 
            // radioButton_2002_full
            // 
            this.radioButton_2002_full.AutoSize = true;
            this.radioButton_2002_full.Location = new System.Drawing.Point(18, 103);
            this.radioButton_2002_full.Name = "radioButton_2002_full";
            this.radioButton_2002_full.Size = new System.Drawing.Size(59, 16);
            this.radioButton_2002_full.TabIndex = 1;
            this.radioButton_2002_full.Text = "入口满";
            this.radioButton_2002_full.UseVisualStyleBackColor = true;
            this.radioButton_2002_full.CheckedChanged += new System.EventHandler(this.radioButton_2002_full_CheckedChanged);
            // 
            // buttonHouseInA1TaskReset
            // 
            this.buttonHouseInA1TaskReset.Location = new System.Drawing.Point(201, 130);
            this.buttonHouseInA1TaskReset.Name = "buttonHouseInA1TaskReset";
            this.buttonHouseInA1TaskReset.Size = new System.Drawing.Size(116, 38);
            this.buttonHouseInA1TaskReset.TabIndex = 0;
            this.buttonHouseInA1TaskReset.Text = "清除";
            this.buttonHouseInA1TaskReset.UseVisualStyleBackColor = true;
            this.buttonHouseInA1TaskReset.Click += new System.EventHandler(this.buttonHouseInA1TaskReset_Click);
            // 
            // buttonHouseInA1NewTask
            // 
            this.buttonHouseInA1NewTask.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonHouseInA1NewTask.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.buttonHouseInA1NewTask.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonHouseInA1NewTask.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonHouseInA1NewTask.Location = new System.Drawing.Point(75, 130);
            this.buttonHouseInA1NewTask.Name = "buttonHouseInA1NewTask";
            this.buttonHouseInA1NewTask.Size = new System.Drawing.Size(112, 38);
            this.buttonHouseInA1NewTask.TabIndex = 0;
            this.buttonHouseInA1NewTask.Text = "生成";
            this.buttonHouseInA1NewTask.UseVisualStyleBackColor = true;
            this.buttonHouseInA1NewTask.Click += new System.EventHandler(this.buttonHouseInA1NewTask_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Teal;
            this.tabPage4.Controls.Add(this.button5001Reset);
            this.tabPage4.Controls.Add(this.button5001NewTask);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1104, 483);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "组盘模拟";
            // 
            // button5001Reset
            // 
            this.button5001Reset.Location = new System.Drawing.Point(67, 28);
            this.button5001Reset.Name = "button5001Reset";
            this.button5001Reset.Size = new System.Drawing.Size(136, 46);
            this.button5001Reset.TabIndex = 0;
            this.button5001Reset.Text = "状态复位";
            this.button5001Reset.UseVisualStyleBackColor = true;
            this.button5001Reset.Click += new System.EventHandler(this.button5001Reset_Click);
            // 
            // button5001NewTask
            // 
            this.button5001NewTask.Location = new System.Drawing.Point(67, 92);
            this.button5001NewTask.Name = "button5001NewTask";
            this.button5001NewTask.Size = new System.Drawing.Size(136, 46);
            this.button5001NewTask.TabIndex = 0;
            this.button5001NewTask.Text = "申请组盘";
            this.button5001NewTask.UseVisualStyleBackColor = true;
            this.button5001NewTask.Click += new System.EventHandler(this.button5001NewTask_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.Teal;
            this.tabPage5.Controls.Add(this.comboBoxGripTask);
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Controls.Add(this.label6);
            this.tabPage5.Controls.Add(this.textBoxPalletID);
            this.tabPage5.Controls.Add(this.buttonApplyGripTask);
            this.tabPage5.Controls.Add(this.buttonGenerateGrispData);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1104, 483);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "分拣模拟";
            // 
            // comboBoxGripTask
            // 
            this.comboBoxGripTask.FormattingEnabled = true;
            this.comboBoxGripTask.Items.AddRange(new object[] {
            "一次分拣",
            "二次分拣"});
            this.comboBoxGripTask.Location = new System.Drawing.Point(107, 25);
            this.comboBoxGripTask.Name = "comboBoxGripTask";
            this.comboBoxGripTask.Size = new System.Drawing.Size(121, 20);
            this.comboBoxGripTask.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "任务类型";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "料框ID";
            // 
            // textBoxPalletID
            // 
            this.textBoxPalletID.Location = new System.Drawing.Point(108, 67);
            this.textBoxPalletID.Name = "textBoxPalletID";
            this.textBoxPalletID.Size = new System.Drawing.Size(120, 21);
            this.textBoxPalletID.TabIndex = 1;
            // 
            // buttonApplyGripTask
            // 
            this.buttonApplyGripTask.Location = new System.Drawing.Point(36, 163);
            this.buttonApplyGripTask.Name = "buttonApplyGripTask";
            this.buttonApplyGripTask.Size = new System.Drawing.Size(192, 45);
            this.buttonApplyGripTask.TabIndex = 0;
            this.buttonApplyGripTask.Text = "申请任务";
            this.buttonApplyGripTask.UseVisualStyleBackColor = true;
            this.buttonApplyGripTask.Click += new System.EventHandler(this.buttonApplyGripTask_Click);
            // 
            // buttonGenerateGrispData
            // 
            this.buttonGenerateGrispData.Location = new System.Drawing.Point(36, 112);
            this.buttonGenerateGrispData.Name = "buttonGenerateGrispData";
            this.buttonGenerateGrispData.Size = new System.Drawing.Size(192, 45);
            this.buttonGenerateGrispData.TabIndex = 0;
            this.buttonGenerateGrispData.Text = "不良品数据生成";
            this.buttonGenerateGrispData.UseVisualStyleBackColor = true;
            this.buttonGenerateGrispData.Click += new System.EventHandler(this.buttonGenerateGrispData_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(131, 0);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(122, 36);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 557);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1112, 155);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // buttonWcsStart
            // 
            this.buttonWcsStart.Location = new System.Drawing.Point(3, 0);
            this.buttonWcsStart.Name = "buttonWcsStart";
            this.buttonWcsStart.Size = new System.Drawing.Size(122, 36);
            this.buttonWcsStart.TabIndex = 1;
            this.buttonWcsStart.Text = "启动";
            this.buttonWcsStart.UseVisualStyleBackColor = true;
            this.buttonWcsStart.Click += new System.EventHandler(this.buttonWcsStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 712);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "测试";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonWcsStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonDevClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDevID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxHouse;
        private System.Windows.Forms.ComboBox comboBoxHouseInTasks;
        private System.Windows.Forms.Button buttonResetStacker1001;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton_2002_empty;
        private System.Windows.Forms.RadioButton radioButton_2002_full;
        private System.Windows.Forms.Button buttonHouseInA1TaskReset;
        private System.Windows.Forms.Button buttonHouseInA1NewTask;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button5001Reset;
        private System.Windows.Forms.Button button5001NewTask;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ComboBox comboBoxGripTask;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPalletID;
        private System.Windows.Forms.Button buttonApplyGripTask;
        private System.Windows.Forms.Button buttonGenerateGrispData;
    }
}

