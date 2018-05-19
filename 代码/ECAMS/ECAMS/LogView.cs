using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ECAMSPresenter;
using ECAMSDataAccess;
using ECAMSModel;

namespace ECAMS
{
    public partial class LogView : BaseView,ILogView
    {
        #region 全局变量
        private delegate void ShowLogInvoke(DataTable dt);
        #endregion

        #region 初始化
        public LogView()
        {
            InitializeComponent();
        }
        private void LogView_Load(object sender, EventArgs e)
        {
            OnIniLogCategory();
            OnIniLogType();
        }

        protected override object CreatePresenter()
        {
            LogPresenter managerPre = new LogPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(LogPresenter))
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
      
        #endregion

        #region UI事件
        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bt_QueryLog_Click(object sender, EventArgs e)
        {
            OnQueryLog();
        }
        #endregion

        #region 实现ILogView接口事件
        public event EventHandler eventIniLogCategory;
        public event EventHandler eventIniLogType;
        public event EventHandler<QueryLogEventArgs> eventQueryLog;
        public event EventHandler<ExportTxtEventArgs> eventExportTxt;
        #endregion

        #region 触发ILogView接口事件
        private void OnExportTxt()
        {
            if (this.eventExportTxt != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = ".txt|*.txt";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                  
                    ExportTxtEventArgs exportArgs = new ExportTxtEventArgs();
                    exportArgs.FilePath = sfd.FileName;
                    this.eventExportTxt.Invoke(this, exportArgs);
                }
            }
        }

        private void OnIniLogCategory()
        {
            this.cb_logCategory.Items.Clear();
            for(int i=0;i<Enum.GetNames(typeof(EnumLogCategory)).Count();i++)
            {
                this.cb_logCategory.Items.Add(Enum.GetNames(typeof(EnumLogCategory))[i]);
            }
            //if (this.eventIniLogCategory != null)
            //{
            //    this.eventIniLogCategory.Invoke(this, null);
            //}
        }
        private void OnIniLogType()
        {
            this.cb_logType.Items.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(EnumLogType)).Count(); i++)
            {
                this.cb_logType.Items.Add(Enum.GetNames(typeof(EnumLogType))[i]);
            }
            //if (this.eventIniLogType != null)
            //{
            //    this.eventIniLogType.Invoke(this, null);
            //}
        }
        private void OnQueryLog()
        {
            if (this.eventQueryLog != null)
            {
                QueryLogEventArgs queryLogArgs = new QueryLogEventArgs();

                if (this.cb_logCategory.Text  == "")
                {
                    MessageBox.Show("请选择日志类别", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if(this.cb_logType.Text =="")
                {
                    MessageBox.Show("请选择日志类型", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (this.cb_LikeQuery.Checked)
                {
                    if (this.tb_LikeQueryTxt.Text == "")
                    {
                        MessageBox.Show("请输入模糊查询的关键字！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    queryLogArgs.IsLikeQuery = true;
                    queryLogArgs.LikeQueryStr = this.tb_LikeQueryTxt.Text.Trim();
                }
                else
                {
                    queryLogArgs.IsLikeQuery = false;
                }
            
                queryLogArgs.StartTime = this.dtp_StartTime.Value;
                queryLogArgs.EndTime = this.dtp_EndTime.Value;
                queryLogArgs.LogCategory = (EnumLogCategory)Enum.Parse(typeof(EnumLogCategory), this.cb_logCategory.Text.Trim());
                queryLogArgs.LogType = (EnumLogType)Enum.Parse(typeof(EnumLogType), this.cb_logType.Text.Trim());
                this.eventQueryLog.Invoke(this, queryLogArgs);
            }
        }
        #endregion

        #region 实现ILogView方法
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="titleStr"></param>
        /// <param name="contentStr"></param>
        public void ShowMessage(string titleStr, string contentStr)
        {
            MessageBox.Show(contentStr, titleStr, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void IniLogCategory(DataTable dt)
        {
            this.cb_logCategory.DataSource = dt;
            this.cb_logCategory.DisplayMember = "logCategory";
        
        }
        public void IniLogType(DataTable dt)
        {
            this.cb_logType.DataSource = dt;
            this.cb_logType.DisplayMember = "logType";
        }

        public void ShowLog(DataTable dt)
        {
            if (this.dgv_logDetail.InvokeRequired)
            {
                ShowLogInvoke slInvoke = new ShowLogInvoke(ShowLog);
                this.dgv_logDetail.Invoke(slInvoke, new object[1] { dt });
            }
            else
            {
                System.Threading.Thread.Sleep(0);
                Application.DoEvents();
                this.dgv_logDetail.DataSource = dt;
                string dateColum = "日期";
                if (this.dgv_logDetail.Columns.Contains(dateColum))
                {
                    this.dgv_logDetail.Columns[dateColum].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
                
                //this.dgv_logDetail.Rows.Clear();

                //this.SetProgressBarMaxValue(logList.Count);
                //this.dgv_logDetail.DataSource = logList;
                //for (int i = 0; i < logList.Count(); i++)
                //{
                //    this.SetProgressBarValue(i);
                //    this.dgv_logDetail.Rows.Add();
                //    this.dgv_logDetail.Rows[i].Cells["logID"].Value = logList[i].logID;
                //    this.dgv_logDetail.Rows[i].Cells["logContent"].Value = logList[i].logContent;
                //    this.dgv_logDetail.Rows[i].Cells["logCategory"].Value = logList[i].logCategory;
                //    this.dgv_logDetail.Rows[i].Cells["logType"].Value = logList[i].logType;
                //    this.dgv_logDetail.Rows[i].Cells["logTime"].Value = logList[i].logTime;
                //    this.dgv_logDetail.Rows[i].Cells["LogErrorCode"].Value = logList[i].warningCode;
                //    System.Threading.Thread.Sleep(0);
                //    Application.DoEvents();
                //}
            }
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年8月25日
        /// 内容:设置退出按钮可用状态
        /// </summary>
        /// <param name="enabled"></param>
        public void SetExitButtonEnabled(bool enabled)
        {
            this.bt_Exit.Enabled = enabled;
        }
        #endregion

        private void bt_ExportTxt_Click(object sender, EventArgs e)
        {
            OnExportTxt();
        }

        
        private void dgv_logDetail_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string logStr = this.dgv_logDetail.CurrentRow.Cells["日志内容"].Value.ToString();
            LogDetailView ldv = new LogDetailView(logStr);
            ldv.ShowDialog();

        }

       
    }
}
