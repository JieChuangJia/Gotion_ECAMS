namespace ECAMS
{
    partial class LogDetailView
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
            this.tb_LogContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_LogContent
            // 
            this.tb_LogContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_LogContent.Location = new System.Drawing.Point(0, 0);
            this.tb_LogContent.Multiline = true;
            this.tb_LogContent.Name = "tb_LogContent";
            this.tb_LogContent.ReadOnly = true;
            this.tb_LogContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_LogContent.Size = new System.Drawing.Size(598, 326);
            this.tb_LogContent.TabIndex = 0;
            // 
            // LogDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 326);
            this.Controls.Add(this.tb_LogContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LogDetailView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "日志详情";
            this.Load += new System.EventHandler(this.LogDetailView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_LogContent;
    }
}