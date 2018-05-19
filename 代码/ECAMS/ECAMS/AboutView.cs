using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
 
namespace ECAMS
{
    public partial class AboutView : Form
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void AboutView_Load(object sender, EventArgs e)
        {
            this.lb_NowTime.Text = "2015-12-13";
            //this.lb_Version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();//获取修订号
            this.lb_Version.Text = "V1.6.1";//获取修订号
        }

     
    }
}
