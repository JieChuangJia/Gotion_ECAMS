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
    public partial class LogDetailView : Form
    {
        
        public LogDetailView(string logStr)
        {
            InitializeComponent();
           
            this.tb_LogContent.Text = logStr;
        }

        private void LogDetailView_Load(object sender, EventArgs e)
        {

        }
    }
}
