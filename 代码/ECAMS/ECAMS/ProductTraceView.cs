using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSPresenter;
namespace ECAMS
{
    public partial class ProductTraceView : BaseView,IProductTraceView
    {
        public ProductTraceView()
        {
            InitializeComponent();
        }
        protected override object CreatePresenter()
        {
            ProductTracePresenter managerPre = new ProductTracePresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(HistroyTaskPresenter))
                {
                    allPresenterList.RemoveAt(i);
                }
            }
            allPresenterList.Add(managerPre);
            return managerPre;
        }

        /// <summary>
        /// 获取指定逻辑
        /// </summary>
        /// <param name="presenterType"></param>
        /// <returns></returns>
        public object GetPresenter(Type presenterType)
        {
            object presenter = null;
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == presenterType)
                {
                    presenter = allPresenterList[i];
                    break;
                }
            }
            return presenter;
        }
        public event EventHandler<PalletTraceEventArgs> eventPalletTrace;

        /// <summary>
        /// 显示查询结果
        /// </summary>
        /// <returns></returns>
        public void DispQueryResult(string palletID,DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                this.label4.Text = "没有查到相关记录！";
                this.dataGridView1.DataSource = null;
                return;
            }
            //StringBuilder strBuilder = new StringBuilder();
            //strBuilder.AppendLine("托盘号:" + palletID);
            //strBuilder.AppendLine(dt.Columns[0].ColumnName+"  ")
            //this.richTextBox1.Text = 
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[this.dataGridView1.Columns.Count - 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        //public void ClearDisp()
        //{
        //    this.dataGridView1.DataSource = null;
        //    this.label4.Text = "没有查到相关记录！";
        //}
        #region UI事件
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void OnQueryPalletTrace(string palletID,bool orderByTimeAsc)
        {
            PalletTraceEventArgs args = new PalletTraceEventArgs();
            args.palletID = palletID;
            args.sortReAsc = orderByTimeAsc;
            if (this.eventPalletTrace == null)
            {
                return;
            }
            this.eventPalletTrace.Invoke(this, args);
        }
        private void buttonQuery_Click(object sender, EventArgs e)
        {
            string palletID = this.textBoxTray.Text;
            if (string.IsNullOrWhiteSpace(palletID))
            {
                MessageBox.Show("托盘为空,请重新输入!");
                return;
            }
            string sortType = this.comboBoxSort.Text;
            if (string.IsNullOrWhiteSpace(sortType))
            {
                MessageBox.Show("未设置时间排序方式!");
                return;
            }
            bool sortAsc = true;
            
            if(sortType == "按时间升序")
            {
                sortAsc = true;
            }
            else
            {
                sortAsc = false;
            }
            OnQueryPalletTrace(palletID, sortAsc);
            
        }
        #endregion
     
    }
}
