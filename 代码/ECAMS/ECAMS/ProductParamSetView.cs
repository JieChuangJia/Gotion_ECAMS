using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ECAMS
{
    public partial class ProductParamSetView : Form
    {
        public bool isSet = false;
        public string trayIDs = "";

        public ProductParamSetView()
        {
            InitializeComponent();
        }

        private void bt_Sure_Click(object sender, EventArgs e)
        {
            if (this.tb_TrayID.Text == "")
            {
                MessageBox.Show("料框条码为空，请输入要入库的料框条码！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
            }
            this.trayIDs = this.tb_TrayID.Text.Trim();
            this.isSet = true;
            this.Close();
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
