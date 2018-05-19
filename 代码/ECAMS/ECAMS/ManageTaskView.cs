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
namespace ECAMS
{
    public partial class ManageTaskView :BaseView,IManageTaskView
    {
        #region 全局变量
        /// <summary>
        /// 显示管理任务委托
        /// </summary>
        /// <param name="manageTaskList"></param>
        //private delegate void ShowManageTaskDataInvoke(List<ManageTaskModel> manageTaskList);
        /// <summary>
        /// 显示管理任务委托
        /// </summary>
        /// <param name="manageTaskList"></param>
        private delegate void ShowManageTaskDataInvoke(DataTable dtManaTask);
        #endregion

        #region 初始化
        public ManageTaskView()
        {
            InitializeComponent();
         
        }

        private void ManageTaskView_Load(object sender, EventArgs e)
        {
            OnLoadData();
        }

        protected override object CreatePresenter()
        {
            ManageTaskPresenter managerPre = new ManageTaskPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(ManageTaskPresenter))
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
        private void tsb_ExitMangeTaskForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_AutoRefresh_Click(object sender, EventArgs e)
        { 
            if (this.tsb_AutoRefresh.Text == "打开自动刷新")
            {
                this.tsb_AutoRefresh.Text = "关闭自动刷新";
                OnAutoRefresh(true);
            }
            else
            {
                this.tsb_AutoRefresh.Text = "打开自动刷新";
                OnAutoRefresh(false);
            }
        }

        private void ManageTaskView_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnExit();
        }
        private void tsb_RefreshTask_Click(object sender, EventArgs e)
        {
            OnRefreshTask();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnRefreshTask();
        }

      
        #endregion

        #region 实现IManageTaskView接口事件
        /// <summary>
        /// 窗体初始化刷新管理任务列表
        /// </summary>
        public  event EventHandler eventLoadData;

        /// <summary>
        /// 自动刷新事件
        /// </summary>
        public event EventHandler<AutoRefreshEventArgs> eventAutoRefrsh;

        /// <summary>
        /// 退出事件
        /// </summary>
        public event EventHandler eventExit;

        public event EventHandler<ManaTaskEventArgs> eventDeleteTask;

        public event EventHandler eventRefreshTask;
        #endregion

        #region 触发IManageTaskView接口事件
        private void OnLoadData()
        {
            if (this.eventLoadData != null)
            {
                this.eventLoadData.Invoke(this, null); 
            }
         }

        private void OnAutoRefresh(bool isAuto)
        {
            
            if (this.eventAutoRefrsh != null)
            {
                AutoRefreshEventArgs autoArgs = new AutoRefreshEventArgs();
                autoArgs.isAutoRefresh = isAuto;
                this.eventAutoRefrsh.Invoke(this, autoArgs);
            }
        }

        private void OnExit()
        {
            if (this.eventExit != null)
            {
                this.eventExit.Invoke(this, null);
            }
        }

        private void OnRefreshTask()
        {
            if (this.eventRefreshTask != null)
            {
                this.eventRefreshTask.Invoke(this, null);
            }
        }

        private void OnDeleteTask()
        {
            if (this.eventDeleteTask != null&& this.dgv_manageTask.CurrentRow!= null)
            {
                int currentRow = this.dgv_manageTask.CurrentRow.Index;
                long manaTaskID =long.Parse( this.dgv_manageTask.Rows[currentRow].Cells["taskID"].Value.ToString());
                ManaTaskEventArgs manaTastArgs = new ManaTaskEventArgs();
                manaTastArgs.ManaTaskID = manaTaskID;

            }
        }
        #endregion

        #region 实现IManageTaskView方法
        /// <summary>
        /// 刷新管理任务
        /// </summary>
        /// <param name="taskModel"></param>
        public void ShowManageTaskData(DataTable dtManaList)
        {
            if (this.dgv_manageTask.InvokeRequired)
            {
                ShowManageTaskDataInvoke showData = new ShowManageTaskDataInvoke(ShowManageTaskData);
                this.BeginInvoke(showData, new object[1] { dtManaList });
                //this.Invoke(showData, new object[1] { dtManaList });
            }
            else
            {
                this.dgv_manageTask.Columns.Clear();
                this.dgv_manageTask.DataSource = dtManaList;
            }
        }

        /// <summary>
        /// 刷新管理任务
        /// </summary>
        /// <param name="taskModel"></param>
        public void ShowManageTaskData(List<ManageTaskModel> manageTaskList)
        {
            //if (this.dgv_manageTask.InvokeRequired)
            //{
            //    ShowManageTaskDataInvoke showData = new ShowManageTaskDataInvoke(ShowManageTaskData);
            //    this.Invoke(showData,new object[1] {manageTaskList});
            //}
            //else
            //{
            //    this.dgv_manageTask.Rows.Clear();
            //    for (int i = 0; i < manageTaskList.Count; i++)
            //    {
            //        this.dgv_manageTask.Rows.Add();
            //        this.dgv_manageTask.Rows[i].Cells["taskID"].Value = manageTaskList[i].TaskID;
            //        this.dgv_manageTask.Rows[i].Cells["taskStatus"].Value = manageTaskList[i].TaskStatus;
            //        this.dgv_manageTask.Rows[i].Cells["taskTypeName"].Value = manageTaskList[i].TaskTypeName;
            //        this.dgv_manageTask.Rows[i].Cells["taskType"].Value = manageTaskList[i].TaskType;
            //        this.dgv_manageTask.Rows[i].Cells["taskCode"].Value = manageTaskList[i].TaskCode;
            //        this.dgv_manageTask.Rows[i].Cells["taskStartArea"].Value = manageTaskList[i].TaskStartArea;
            //        this.dgv_manageTask.Rows[i].Cells["taskStartPosition"].Value = manageTaskList[i].TaskStartPostion;
            //        this.dgv_manageTask.Rows[i].Cells["taskEndArea"].Value = manageTaskList[i].TaskEndArea;
            //        this.dgv_manageTask.Rows[i].Cells["taskEndPosition"].Value = manageTaskList[i].TaskEndPostion;
            //        this.dgv_manageTask.Rows[i].Cells["taskCreatePerson"].Value = manageTaskList[i].TaskCreatePerson;
            //        this.dgv_manageTask.Rows[i].Cells["taskCreateTime"].Value = manageTaskList[i].TaskCreateTime;
            //        this.dgv_manageTask.Rows[i].Cells["taskCompleteTime"].Value = manageTaskList[i].TaskCompleteTime;
            //    }
            //}
            
        }
        #endregion

       

        
    }
}
