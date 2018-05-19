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
    public partial class HistoryTaskQueryView : BaseView,IHistoryTaskQueryView
    {
     
        #region 全局变量
       
        #endregion

        #region 初始化
        public HistoryTaskQueryView()
        {
            InitializeComponent();
        }

        private void HistoryTaskQueryView_Load(object sender, EventArgs e)
        {
            IniTaskTypeName();
            //OnIniTasktypeCom();
        }
        protected override object CreatePresenter()
        {
            HistroyTaskPresenter managerPre = new HistroyTaskPresenter(this);
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
        #endregion

        #region UI事件
        private void bt_HistoryTaskEixt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_QueryHisTask_Click(object sender, EventArgs e)
        {
            OnQueryTask();
        }
        #endregion

        #region 实现IManageTaskView接口事件
        public event EventHandler<QueryHisTaskEventArgs> eventQueryTask;
        /// <summary>
        /// 初始化任务类型名称combox
        /// </summary>
        public event EventHandler eventIniTasktypeCom;
        #endregion

        #region 触发ImanageTaskview事件
        private void OnQueryTask()
        {
            if (this.eventQueryTask != null)
            { 
                QueryHisTaskEventArgs hisTaskArgs = new QueryHisTaskEventArgs();
                hisTaskArgs.StartTime = this.dtp_StartTime.Value;
                hisTaskArgs.EndTime = this.dtp_EndTime.Value;
                hisTaskArgs.TasktypeName = this.cb_TaskTypeName.Text.Trim();
                hisTaskArgs.StartPosition = this.tb_StartPosition.Text.Trim();
                hisTaskArgs.EndPostion = this.tb_EndPosition.Text.Trim();
                if (checkB_taskTypeName.Checked == true)
                {
                   
                    hisTaskArgs.TasktypeNameChecked = true;
                }
                else
                {
                    hisTaskArgs.TasktypeNameChecked = false;
                }

                if (this.cb_StartPostion.Checked)
                {
                    if (!CheckGsName(this.tb_StartPosition.Text.Trim()))
                    {
                        MessageBox.Show("开始位置格式不正确！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    hisTaskArgs.StartPositionChecked = true; }
                else
                { hisTaskArgs.StartPositionChecked = false; }

                if (this.cb_EndPostion.Checked)
                {
                    if (!CheckGsName(this.tb_EndPosition.Text.Trim()))
                    {
                        MessageBox.Show("结束位置格式不正确！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    hisTaskArgs.EndPostionChecked = true; }
                else
                { hisTaskArgs.EndPostionChecked = false; }
                this.eventQueryTask.Invoke(this, hisTaskArgs);
            }
        }

        private void OnIniTasktypeCom()
        {
            if (this.eventIniTasktypeCom != null)
            {
                this.eventIniTasktypeCom.Invoke(this, null);
            }
        }
        #endregion

        #region 实现IManageTaskView方法
        public void IniTaskTypeNameCom(DataSet ds)
        {
            this.cb_TaskTypeName.Items.Clear();
            if (ds != null && ds.Tables.Count > 0)
            {
                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    this.cb_TaskTypeName.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
        }

        private void IniTaskTypeName()
        {
            this.cb_TaskTypeName.Items.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(EnumTaskName)).Count(); i++)
            {
                if (Enum.GetNames(typeof(EnumTaskName))[i] == "无")
                {
                    continue;
                }
                this.cb_TaskTypeName.Items.Add(Enum.GetNames(typeof(EnumTaskName))[i]);
            }
        }

        public void ShowHisTaskData(List<HistoryManageTaskModel> hisTaskList)
        {
            this.dgv_historyTaskList.Rows.Clear();
            //this.SetProgressBarMaxValue(hisTaskList.Count);
            for (int i = 0; i < hisTaskList.Count; i++)
            {
                //this.SetProgressBarValue(i);
                this.dgv_historyTaskList.Rows.Add();
                this.dgv_historyTaskList.Rows[i].Cells["numID"].Value =i+1;
                this.dgv_historyTaskList.Rows[i].Cells["taskTypeName"].Value = hisTaskList[i].TaskTypeName;
                this.dgv_historyTaskList.Rows[i].Cells["taskType"].Value = hisTaskList[i].TaskType;
                this.dgv_historyTaskList.Rows[i].Cells["productName"].Value = hisTaskList[i].ProductName;
                this.dgv_historyTaskList.Rows[i].Cells["productID"].Value = hisTaskList[i].ProductID;
                this.dgv_historyTaskList.Rows[i].Cells["startArea"].Value = hisTaskList[i].TaskStartArea;
                this.dgv_historyTaskList.Rows[i].Cells["taskStartPosition"].Value = hisTaskList[i].TaskStartPsotion;
                this.dgv_historyTaskList.Rows[i].Cells["endArea"].Value = hisTaskList[i].TaskEndAera;
                this.dgv_historyTaskList.Rows[i].Cells["taskEndPosition"].Value = hisTaskList[i].TaskEndPosition;
                this.dgv_historyTaskList.Rows[i].Cells["taskCreatePerson"].Value = hisTaskList[i].TaskCreatePerson;
                this.dgv_historyTaskList.Rows[i].Cells["taskCreateTime"].Value = hisTaskList[i].TaskCreateTime;
                this.dgv_historyTaskList.Rows[i].Cells["taskCompleteTime"].Value = hisTaskList[i].TaskCompleteTime;
                this.dgv_historyTaskList.Rows[i].Cells["TaskParam"].Value = hisTaskList[i].TaskParameter;
                
                System.Threading.Thread.Sleep(0);
                this.FormDoEvent();
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
            this.bt_HistoryTaskEixt.Enabled = enabled;
        }
        #endregion

        #region UI私有方法
        private bool CheckGsName(string gsName)
        {
            string opertions = @"^(\d{1,2}-\d{1,2}-\d{1,2})$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(opertions);
            if (!regex.IsMatch(gsName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

    }
}
