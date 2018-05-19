namespace ECAMS
{
    partial class ModifyLoadFillForm
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
            this.textBoxPalletID_XG = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonQueryBattery_XG = new System.Windows.Forms.Button();
            this.buttonModify_XG = new System.Windows.Forms.Button();
            this.textBoxNewBatteryID_XG = new System.Windows.Forms.TextBox();
            this.textBoxBatteryPos_XG = new System.Windows.Forms.TextBox();
            this.textBoxCurrentBatteryID_XG = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxCurrentBatch_XG = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxNewBatch_XG = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPalletID_XG
            // 
            this.textBoxPalletID_XG.Enabled = false;
            this.textBoxPalletID_XG.Location = new System.Drawing.Point(150, 38);
            this.textBoxPalletID_XG.Name = "textBoxPalletID_XG";
            this.textBoxPalletID_XG.Size = new System.Drawing.Size(121, 21);
            this.textBoxPalletID_XG.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel1.Controls.Add(this.buttonQuit);
            this.panel1.Controls.Add(this.buttonQueryBattery_XG);
            this.panel1.Controls.Add(this.buttonModify_XG);
            this.panel1.Controls.Add(this.textBoxNewBatteryID_XG);
            this.panel1.Controls.Add(this.textBoxBatteryPos_XG);
            this.panel1.Controls.Add(this.textBoxCurrentBatteryID_XG);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textboxCurrentBatch_XG);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.comboBoxNewBatch_XG);
            this.panel1.Controls.Add(this.textBoxPalletID_XG);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 331);
            this.panel1.TabIndex = 1;
            // 
            // buttonQuit
            // 
            this.buttonQuit.Location = new System.Drawing.Point(329, 252);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(117, 42);
            this.buttonQuit.TabIndex = 6;
            this.buttonQuit.Text = "退出";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQueryBattery_XG_Click);
            // 
            // buttonQueryBattery_XG
            // 
            this.buttonQueryBattery_XG.Location = new System.Drawing.Point(191, 252);
            this.buttonQueryBattery_XG.Name = "buttonQueryBattery_XG";
            this.buttonQueryBattery_XG.Size = new System.Drawing.Size(117, 42);
            this.buttonQueryBattery_XG.TabIndex = 6;
            this.buttonQueryBattery_XG.Text = "查询";
            this.buttonQueryBattery_XG.UseVisualStyleBackColor = true;
            this.buttonQueryBattery_XG.Visible = false;
            this.buttonQueryBattery_XG.Click += new System.EventHandler(this.buttonQueryBattery_XG_Click);
            // 
            // buttonModify_XG
            // 
            this.buttonModify_XG.Location = new System.Drawing.Point(38, 252);
            this.buttonModify_XG.Name = "buttonModify_XG";
            this.buttonModify_XG.Size = new System.Drawing.Size(122, 42);
            this.buttonModify_XG.TabIndex = 6;
            this.buttonModify_XG.Text = "修改提交";
            this.buttonModify_XG.UseVisualStyleBackColor = true;
            // 
            // textBoxNewBatteryID_XG
            // 
            this.textBoxNewBatteryID_XG.Location = new System.Drawing.Point(150, 192);
            this.textBoxNewBatteryID_XG.Name = "textBoxNewBatteryID_XG";
            this.textBoxNewBatteryID_XG.Size = new System.Drawing.Size(121, 21);
            this.textBoxNewBatteryID_XG.TabIndex = 5;
            // 
            // textBoxBatteryPos_XG
            // 
            this.textBoxBatteryPos_XG.Enabled = false;
            this.textBoxBatteryPos_XG.Location = new System.Drawing.Point(370, 33);
            this.textBoxBatteryPos_XG.Name = "textBoxBatteryPos_XG";
            this.textBoxBatteryPos_XG.Size = new System.Drawing.Size(51, 21);
            this.textBoxBatteryPos_XG.TabIndex = 5;
            // 
            // textBoxCurrentBatteryID_XG
            // 
            this.textBoxCurrentBatteryID_XG.Enabled = false;
            this.textBoxCurrentBatteryID_XG.Location = new System.Drawing.Point(150, 105);
            this.textBoxCurrentBatteryID_XG.Name = "textBoxCurrentBatteryID_XG";
            this.textBoxCurrentBatteryID_XG.Size = new System.Drawing.Size(121, 21);
            this.textBoxCurrentBatteryID_XG.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(34, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "重新设定条码";
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(34, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "重新设定批次";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(306, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "1~48";
            this.label6.Click += new System.EventHandler(this.label1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(297, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "通道号";
            this.label5.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(34, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "当前条码";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textboxCurrentBatch_XG
            // 
            this.textboxCurrentBatch_XG.Enabled = false;
            this.textboxCurrentBatch_XG.Location = new System.Drawing.Point(150, 73);
            this.textboxCurrentBatch_XG.Name = "textboxCurrentBatch_XG";
            this.textboxCurrentBatch_XG.Size = new System.Drawing.Size(121, 21);
            this.textboxCurrentBatch_XG.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(34, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "托盘号";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(34, 78);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 16);
            this.label13.TabIndex = 4;
            this.label13.Text = "当前批次";
            // 
            // comboBoxNewBatch_XG
            // 
            this.comboBoxNewBatch_XG.FormattingEnabled = true;
            this.comboBoxNewBatch_XG.Location = new System.Drawing.Point(150, 157);
            this.comboBoxNewBatch_XG.Name = "comboBoxNewBatch_XG";
            this.comboBoxNewBatch_XG.Size = new System.Drawing.Size(121, 20);
            this.comboBoxNewBatch_XG.TabIndex = 3;
            // 
            // ModifyLoadFillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 331);
            this.Controls.Add(this.panel1);
            this.Name = "ModifyLoadFillForm";
            this.Text = "修改托盘的装载信息";
            this.Load += new System.EventHandler(this.ModifyLoadFillForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPalletID_XG;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxNewBatch_XG;
        private System.Windows.Forms.TextBox textBoxCurrentBatteryID_XG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxCurrentBatch_XG;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonModify_XG;
        private System.Windows.Forms.Button buttonQueryBattery_XG;
        private System.Windows.Forms.TextBox textBoxNewBatteryID_XG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxBatteryPos_XG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonQuit;
    }
}