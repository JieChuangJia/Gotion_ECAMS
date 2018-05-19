using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DECTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSrc = this.richTextBox1.Text;
            string encStr = EncAndDec.Encode(strSrc, "zwx", "xwz");
            this.richTextBox2.Text = encStr;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string encStr = this.richTextBox2.Text;
            string strSrc = EncAndDec.Decode(encStr, "zwx", "xwz");
            this.richTextBox1.Text = strSrc;
        }
    }
}
