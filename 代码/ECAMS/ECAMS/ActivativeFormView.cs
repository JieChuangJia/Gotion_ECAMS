using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSModel;
using LicenceManager;

namespace ECAMS
{
    public partial class ActivativeFormView : Form
    {

        LicenceModel licenceModel = null;
        public bool isLicenceValid = true;
        public ActivativeFormView(LicenceModel licenceModel)
        {
            InitializeComponent();
            this.licenceModel = licenceModel;
        }

        private void bt_ActiveTrue_Click(object sender, EventArgs e)
        {
            if (this.licenceModel != null)
            {
                this.licenceModel.WriteLicenceEndTime(this.tb_DesKeyCode.Text);
                string reStr = "";
                if (!this.licenceModel.IsLicenceValid(ref reStr))
                {
                    isLicenceValid = false;
                    MessageBox.Show("激活码无效！"+reStr, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    isLicenceValid = true; 
                }

                MessageBox.Show("激活成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void bt_ActiveCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            isLicenceValid = false;
        }

        private void ActivativeFormView_Load(object sender, EventArgs e)
        {
        
        }
    }
}
