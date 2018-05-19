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
    public partial class AddTrayDetailView : Form
    {
        public string trayCode = "";
        public bool isSet = false;
        public AddTrayDetailView()
        {
            InitializeComponent();
        }

        private void bt_Sure_Click(object sender, EventArgs e)
        {
            this.isSet = true;
            trayCode = this.tb_TrayCode.Text;
            this.Close();
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {

            this.isSet = false;
            this.Close();
        }
    }
}
