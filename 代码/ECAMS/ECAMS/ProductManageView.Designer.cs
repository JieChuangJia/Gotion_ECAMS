namespace ECAMS
{
    partial class ProductManageView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductManageView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_AddProduct = new System.Windows.Forms.ToolStripButton();
            this.tsb_DeleteProduct = new System.Windows.Forms.ToolStripButton();
            this.tsb_EditProduct = new System.Windows.Forms.ToolStripButton();
            this.tsb_SaveProduct = new System.Windows.Forms.ToolStripButton();
            this.tsb_Exit = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_ProductManage = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_ProductUnit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_FullNum = new System.Windows.Forms.TextBox();
            this.tb_ProductName = new System.Windows.Forms.TextBox();
            this.tb_ProductCode = new System.Windows.Forms.TextBox();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProductManage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_AddProduct,
            this.tsb_DeleteProduct,
            this.tsb_EditProduct,
            this.tsb_SaveProduct,
            this.tsb_Exit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(864, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_AddProduct
            // 
            this.tsb_AddProduct.Image = ((System.Drawing.Image)(resources.GetObject("tsb_AddProduct.Image")));
            this.tsb_AddProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AddProduct.Name = "tsb_AddProduct";
            this.tsb_AddProduct.Size = new System.Drawing.Size(76, 22);
            this.tsb_AddProduct.Text = "添加产品";
            this.tsb_AddProduct.Click += new System.EventHandler(this.tsb_AddProduct_Click);
            // 
            // tsb_DeleteProduct
            // 
            this.tsb_DeleteProduct.Image = ((System.Drawing.Image)(resources.GetObject("tsb_DeleteProduct.Image")));
            this.tsb_DeleteProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_DeleteProduct.Name = "tsb_DeleteProduct";
            this.tsb_DeleteProduct.Size = new System.Drawing.Size(76, 22);
            this.tsb_DeleteProduct.Text = "删除产品";
            this.tsb_DeleteProduct.Click += new System.EventHandler(this.tsb_DeleteProduct_Click);
            // 
            // tsb_EditProduct
            // 
            this.tsb_EditProduct.Image = ((System.Drawing.Image)(resources.GetObject("tsb_EditProduct.Image")));
            this.tsb_EditProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_EditProduct.Name = "tsb_EditProduct";
            this.tsb_EditProduct.Size = new System.Drawing.Size(76, 22);
            this.tsb_EditProduct.Text = "编辑产品";
            this.tsb_EditProduct.Click += new System.EventHandler(this.tsb_EditProduct_Click);
            // 
            // tsb_SaveProduct
            // 
            this.tsb_SaveProduct.Image = ((System.Drawing.Image)(resources.GetObject("tsb_SaveProduct.Image")));
            this.tsb_SaveProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_SaveProduct.Name = "tsb_SaveProduct";
            this.tsb_SaveProduct.Size = new System.Drawing.Size(52, 22);
            this.tsb_SaveProduct.Text = "保存";
            this.tsb_SaveProduct.Click += new System.EventHandler(this.tsb_SaveProduct_Click);
            // 
            // tsb_Exit
            // 
            this.tsb_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Exit.Image")));
            this.tsb_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Exit.Name = "tsb_Exit";
            this.tsb_Exit.Size = new System.Drawing.Size(52, 22);
            this.tsb_Exit.Text = "退出";
            this.tsb_Exit.Click += new System.EventHandler(this.tsb_Exit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgv_ProductManage);
            this.groupBox2.Location = new System.Drawing.Point(2, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(860, 340);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "产品列表";
            // 
            // dgv_ProductManage
            // 
            this.dgv_ProductManage.AllowUserToAddRows = false;
            this.dgv_ProductManage.AllowUserToDeleteRows = false;
            this.dgv_ProductManage.AllowUserToResizeRows = false;
            this.dgv_ProductManage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_ProductManage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_ProductManage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductID,
            this.ProductCode,
            this.ProductName,
            this.ProductUnit,
            this.FullNum});
            this.dgv_ProductManage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ProductManage.Location = new System.Drawing.Point(3, 17);
            this.dgv_ProductManage.Name = "dgv_ProductManage";
            this.dgv_ProductManage.ReadOnly = true;
            this.dgv_ProductManage.RowHeadersVisible = false;
            this.dgv_ProductManage.RowTemplate.Height = 23;
            this.dgv_ProductManage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ProductManage.Size = new System.Drawing.Size(854, 320);
            this.dgv_ProductManage.TabIndex = 0;
            this.dgv_ProductManage.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ProductManage_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_ProductUnit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_FullNum);
            this.groupBox1.Controls.Add(this.tb_ProductName);
            this.groupBox1.Controls.Add(this.tb_ProductCode);
            this.groupBox1.Location = new System.Drawing.Point(4, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(857, 59);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "产品信息";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(540, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "满料框个数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "产品单位：";
            // 
            // tb_ProductUnit
            // 
            this.tb_ProductUnit.Location = new System.Drawing.Point(434, 23);
            this.tb_ProductUnit.Name = "tb_ProductUnit";
            this.tb_ProductUnit.Size = new System.Drawing.Size(100, 21);
            this.tb_ProductUnit.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "产品编码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(186, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "产品名称：";
            // 
            // tb_FullNum
            // 
            this.tb_FullNum.Location = new System.Drawing.Point(623, 23);
            this.tb_FullNum.Name = "tb_FullNum";
            this.tb_FullNum.Size = new System.Drawing.Size(100, 21);
            this.tb_FullNum.TabIndex = 8;
            // 
            // tb_ProductName
            // 
            this.tb_ProductName.Location = new System.Drawing.Point(257, 23);
            this.tb_ProductName.Name = "tb_ProductName";
            this.tb_ProductName.Size = new System.Drawing.Size(100, 21);
            this.tb_ProductName.TabIndex = 3;
            // 
            // tb_ProductCode
            // 
            this.tb_ProductCode.Location = new System.Drawing.Point(80, 23);
            this.tb_ProductCode.Name = "tb_ProductCode";
            this.tb_ProductCode.Size = new System.Drawing.Size(100, 21);
            this.tb_ProductCode.TabIndex = 0;
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "产品ID";
            this.ProductID.Name = "ProductID";
            this.ProductID.ReadOnly = true;
            this.ProductID.Width = 66;
            // 
            // ProductCode
            // 
            this.ProductCode.HeaderText = "产品编码";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 78;
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "产品名称";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 78;
            // 
            // ProductUnit
            // 
            this.ProductUnit.HeaderText = "产品单位";
            this.ProductUnit.Name = "ProductUnit";
            this.ProductUnit.ReadOnly = true;
            this.ProductUnit.Width = 78;
            // 
            // FullNum
            // 
            this.FullNum.HeaderText = "满料框个数";
            this.FullNum.Name = "FullNum";
            this.FullNum.ReadOnly = true;
            this.FullNum.Width = 90;
            // 
            // ProductManageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 434);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProductManageView";
            this.Text = "产品管理";
            this.Load += new System.EventHandler(this.ProductManageView_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProductManage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ProductManage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_ProductCode;
        private System.Windows.Forms.TextBox tb_ProductName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_ProductUnit;
        private System.Windows.Forms.TextBox tb_FullNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_AddProduct;
        private System.Windows.Forms.ToolStripButton tsb_DeleteProduct;
        private System.Windows.Forms.ToolStripButton tsb_EditProduct;
        private System.Windows.Forms.ToolStripButton tsb_SaveProduct;
        private System.Windows.Forms.ToolStripButton tsb_Exit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullNum;
    }
}