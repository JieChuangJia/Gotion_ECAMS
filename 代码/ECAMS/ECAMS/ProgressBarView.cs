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
    public partial class ProgressBarView : Form
    {  
        public ProgressBarView()
        {
            InitializeComponent();
           
        }
        public ProgressBar GetProcessBar()
        {
            return this.progressBar1;
        }
        private void ProgressBarView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
 
    }
}
