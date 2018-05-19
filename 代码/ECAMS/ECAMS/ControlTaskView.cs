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
    public partial class ControlTaskView : BaseView, IControlTaskView
    {
        #region 全局变量
        /// <summary>
        /// 显示管理任务委托
        /// </summary>
        /// <param name="manageTaskList"></param>
        //private delegate void ShowControlTaskDataInvoke(List<ControlTaskModel> controlTaskList);
        private delegate void ShowControlTaskDataInvoke(DataTable dtTaskList);
        #endregion

        #region 初始化
        public ControlTaskView()
        {
            InitializeComponent();
        }
        private void ControlTaskView_Shown(object sender, EventArgs e)
        {
            OnLoadData();
            #region 初始化菜单栏任务流程名称下拉列表
            //this.tscb_TaskTypeName.Items.Add(EnumTaskName.电芯一次拣选.ToString());
            //this.tscb_TaskTypeName.Items.Add(EnumTaskName.电芯二次拣选.ToString());
            this.tscb_TaskTypeName.Items.Add(EnumTaskName.电芯入库_A1.ToString());
            //this.tscb_TaskTypeName.Items.Add(EnumTaskName.电芯装箱组盘.ToString());
            //this.tscb_TaskTypeName.Items.Add(EnumTaskName.一次检测.ToString());
            //this.tscb_TaskTypeName.Items.Add(EnumTaskName.二次检测.ToString());
            this.tscb_TaskTypeName.Items.Add(EnumTaskName.分容入库_A1.ToString());
            this.tscb_TaskTypeName.Items.Add(EnumTaskName.电芯入库_B1.ToString());
            this.tscb_TaskTypeName.Items.Add(EnumTaskName.空料框入库.ToString());
            this.tscb_TaskTypeName.Items.Add(EnumTaskName.空料框出库.ToString());
            #endregion
            #region 初始化任务筛选的任务流程名称下拉列表
            this.cb_QueryTaskFlow.Items.Clear();
            this.cb_QueryTaskFlow.Items.Add("所有");
            for (int i = 0; i < Enum.GetNames(typeof(EnumTaskName)).Count(); i++)
            {
                string taskTypeName = Enum.GetNames(typeof(EnumTaskName))[i];
                if (taskTypeName == "无")
                {
                    continue;
                }
                this.cb_QueryTaskFlow.Items.Add(taskTypeName);
            }
            this.cb_QueryTaskFlow.SelectedIndex = 0;
            #endregion
            #region 初始化任务筛选的任务状态下拉列表
            this.cb_QueryStatus.Items.Clear();
            this.cb_QueryStatus.Items.Add("所有");
            for (int i = 0; i < Enum.GetNames(typeof(EnumTaskStatus)).Count(); i++)
            {
                string taskTypeName = Enum.GetNames(typeof(EnumTaskStatus))[i];
                if (taskTypeName == "无")
                {
                    continue;
                }
                this.cb_QueryStatus.Items.Add(taskTypeName);
            }
            this.cb_QueryStatus.SelectedIndex = 0;
            #endregion
            #region 初始化筛选的任务类型下拉列表
            this.cb_TaskType.Items.Clear();
            this.cb_TaskType.Items.Add("所有");
            this.cb_TaskType.Items.Add(EnumTaskCategory.出库.ToString());
            this.cb_TaskType.Items.Add(EnumTaskCategory.入库.ToString());
            this.cb_TaskType.SelectedIndex = 0;
            #endregion
            #region 任务创建模式初始化下拉列表
            this.cb_TaskCreateMode.Items.Clear();
            this.cb_TaskCreateMode.Items.Add("所有");
            this.cb_TaskCreateMode.Items.Add(EnumCreateMode.系统生成.ToString());
            this.cb_TaskCreateMode.Items.Add(EnumCreateMode.手动生成.ToString());
            this.cb_TaskCreateMode.Items.Add(EnumCreateMode.手动强制.ToString());
            this.cb_TaskCreateMode.SelectedIndex = 0;
            #endregion
            #region 库房下拉列表初始化
            this.cb_StoreHouse.Items.Clear();
            this.cb_StoreHouse.Items.Add(EnumStoreHouse.所有.ToString());
            this.cb_StoreHouse.Items.Add(EnumStoreHouse.A1库房.ToString());
            this.cb_StoreHouse.Items.Add(EnumStoreHouse.B1库房.ToString());
            this.cb_StoreHouse.SelectedIndex = 0;
            #endregion
        }
       

        protected override object CreatePresenter()
        {
            ControlTaskPresenter managerPre = new ControlTaskPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(ControlTaskPresenter))
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
        private void tsb_ExitControlForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbt_autoRefreshTask_Click(object sender, EventArgs e)
        {
            if (this.tsbt_autoRefreshTask.Text == "打开自动刷新")
            {
                this.tsbt_autoRefreshTask.Text = "关闭自动刷新";
                OnAutoRefresh(true);
            }
            else
            {
                this.tsbt_autoRefreshTask.Text = "打开自动刷新";
                OnAutoRefresh(false);
            }
        }

        private void ControlTaskView_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnExit();
        }

        private void tsb_cancelTask_Click(object sender, EventArgs e)
        {
            OnCancelTask();
            OnQueryCtrlTask();
        }

        private void tsb_completeTaskByHand_Click(object sender, EventArgs e)
        {
            OnCompleteByHand();
        }

        private void tsb_RefreshTask_Click(object sender, EventArgs e)
        {
            OnQueryCtrlTask();

        }

        private void tsb_InStoreByHand_Click(object sender, EventArgs e)
        {
            OnInStoreByHand();
        }

        private void 删除任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnCancelTask();
        }

        private void 手动完成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnCompleteByHand();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnQueryCtrlTask();
        }

        private void bt_QueryCtrlTask_Click(object sender, EventArgs e)
        {
            OnQueryCtrlTask();
        }

        #endregion

        #region 实现IControlTaskView接口事件
        public event EventHandler<AutoRefreshEventArgs> eventAutoRefresh;
        /// <summary>
        /// 窗体初始化刷新管理任务列表
        /// </summary>
        public event EventHandler eventLoadData;

        /// <summary>
        /// 退出事件
        /// </summary>
        public event EventHandler eventExit;


        public event EventHandler<ControlTaskEventArgs> eventCancelTask;

        public event EventHandler<ControlTaskEventArgs> eventCompleteByHand;

        public event EventHandler<InStoreByHandEventArgs> eventInStoreByHand;

        /// <summary>
        /// 条件查询控制任务
        /// </summary>
        public event EventHandler<QueryCtrlTaskEventArgs> eventQueryCtrlTask;
        #endregion

        #region 触发IControlTaskView接口事件

        private void OnAutoRefresh(bool isAuto)
        {
            if (this.eventAutoRefresh != null)
            {
                AutoRefreshEventArgs autoArgs = new AutoRefreshEventArgs();
                autoArgs.isAutoRefresh = isAuto;
                this.eventAutoRefresh.Invoke(this, autoArgs);
            }
        }

        private void OnLoadData()
        {
            if (this.eventLoadData != null)
            {
                this.eventLoadData.Invoke(this, null);
            }
        }

        private void OnExit()
        {
            if (this.eventExit != null)
            {
                this.eventExit.Invoke(this, null);
            }
        }

        private void OnCancelTask()
        {

            if (this.eventCancelTask != null)
            {
                if (this.dgv_ControlTask.CurrentRow != null)
                {

                    ControlTaskEventArgs taskArgs = new ControlTaskEventArgs();
                    int selectRowsCount = this.dgv_ControlTask.SelectedRows.Count;

                    long[] controlIDArr = new long[selectRowsCount];
                    bool[] ControlTaskStatus = new bool[selectRowsCount];
                    for (int i = 0; i < selectRowsCount; i++)
                    {
                        DataGridViewRow rowSelect = this.dgv_ControlTask.SelectedRows[i];// 从大到小
                        if (rowSelect == null)
                        {
                            continue;
                        }
                        controlIDArr[i] = long.Parse(rowSelect.Cells["控制任务ID"].Value.ToString());
                        string ControlTaskStatusStr = rowSelect.Cells["任务状态"].Value.ToString();
                        if (ControlTaskStatusStr == EnumTaskStatus.执行中.ToString())
                        {
                            ControlTaskStatus[i] = true;
                        }
                        else
                        { ControlTaskStatus[i] = false; }
                    }
                    taskArgs.ControlTaskIDArr = controlIDArr;
                    taskArgs.ControlTaskStatus = ControlTaskStatus;
                    this.eventCancelTask.Invoke(this, taskArgs);
                }
                else
                {
                    MessageBox.Show("请选择要删除的控制任务!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OnCompleteByHand()
        {
            if (this.eventCompleteByHand != null && this.dgv_ControlTask.CurrentRow != null)
            {
                int selectRowsCount = this.dgv_ControlTask.SelectedRows.Count;
                ControlTaskEventArgs taskArgs = new ControlTaskEventArgs();
                long[] controlIDArr = new long[selectRowsCount];
                bool[] ControlTaskStatus = new bool[selectRowsCount];
                for (int i = 0; i < selectRowsCount; i++)
                {
                    DataGridViewRow rowSelect = this.dgv_ControlTask.SelectedRows[i];// 从大到小
                    if (rowSelect != null)
                    {
                        controlIDArr[i] = long.Parse(rowSelect.Cells["控制任务ID"].Value.ToString());
                        string ControlTaskStatusStr = rowSelect.Cells["任务状态"].Value.ToString();
                        if (ControlTaskStatusStr == EnumTaskStatus.已完成.ToString())
                        {
                            ControlTaskStatus[i] = true;
                        }
                        else
                        { ControlTaskStatus[i] = false; }
                    }
                }
                taskArgs.ControlTaskIDArr = controlIDArr;
                taskArgs.ControlTaskStatus = ControlTaskStatus;
                this.eventCompleteByHand.Invoke(this, taskArgs);
            }
            else
            {
                MessageBox.Show("请选择要手动完成的控制任务!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OnInStoreByHand()
        {
            if (this.eventInStoreByHand != null)
            {
                InStoreByHandEventArgs inStoreArgs = new InStoreByHandEventArgs();
                if (this.tscb_TaskTypeName.Text != "")
                {
                    if (this.tscb_TaskTypeName.Text == EnumTaskName.电芯入库_A1.ToString()
                        || this.tscb_TaskTypeName.Text == EnumTaskName.电芯入库_B1.ToString()
                        || this.tscb_TaskTypeName.Text == EnumTaskName.分容入库_A1.ToString()
                        || this.tscb_TaskTypeName.Text == EnumTaskName.空料框入库.ToString())
                    {
                        ProductParamSetView ppsv = new ProductParamSetView();
                        ppsv.ShowDialog();
                        if (ppsv.isSet == true)
                        {
                            inStoreArgs.TrayIDs = ppsv.trayIDs;
                        }
                        else
                        {
                            MessageBox.Show("请设置入库料框条码！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    inStoreArgs.TaskTypeName = this.tscb_TaskTypeName.Text.Trim();
                    this.eventInStoreByHand.Invoke(this, inStoreArgs);
                }
                else
                {
                    MessageBox.Show("请选择任务流程名称!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void OnQueryCtrlTask()
        {
            if (this.cb_QueryTaskFlow.Text == "")
            {
                MessageBox.Show("请选择任务流程名称！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.cb_QueryStatus.Text == "")
            {
                MessageBox.Show("请选择任务状态！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.cb_StoreHouse.Text == "")
            {
                MessageBox.Show("请选择库房名称！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.cb_TaskCreateMode.Text == "")
            {
                MessageBox.Show("请选择任务创建类型！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.cb_TaskType.Text == "")
            {
                MessageBox.Show("请选择任务类型！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.eventQueryCtrlTask != null)
            {
                QueryCtrlTaskEventArgs queryArgs = new QueryCtrlTaskEventArgs();
                queryArgs.CtrlTaskName = this.cb_QueryTaskFlow.Text;
                queryArgs.CtrlTaskStatus = this.cb_QueryStatus.Text;
                queryArgs.StoreHouseName = this.cb_StoreHouse.Text;
                queryArgs.TaskCreateMode = this.cb_TaskCreateMode.Text;
                queryArgs.TaskType = this.cb_TaskType.Text;
                this.eventQueryCtrlTask.Invoke(this, queryArgs);
            }
        }
        #endregion

        #region 实现IControlTaskView方法
        /// <summary>
        /// 刷新管理任务
        /// </summary>
        /// <param name="taskModel"></param>
        public void ShowControlTaskData(List<ControlTaskModel> taskModelList)
        {
            //if (this.dgv_ControlTask.InvokeRequired)
            //{
            //    ShowControlTaskDataInvoke showData = new ShowControlTaskDataInvoke(ShowControlTaskData);
            //    this.Invoke(showData, new object[1] { taskModelList });
            //}
            //else
            //{
            //    this.dgv_ControlTask.Rows.Clear();
            //    for (int j = 0; j < taskModelList.Count; j++)
            //    {
            //        this.dgv_ControlTask.Rows.Add();
            //        this.dgv_ControlTask.Rows[j].Cells["taskID"].Value = taskModelList[j].TaskID;
            //        this.dgv_ControlTask.Rows[j].Cells["taskType"].Value = taskModelList[j].TaskType;
            //        this.dgv_ControlTask.Rows[j].Cells["taskTypeName"].Value = taskModelList[j].TaskTypeName;
            //        this.dgv_ControlTask.Rows[j].Cells["controlTaskID"].Value = taskModelList[j].ControlTaskID;
            //        this.dgv_ControlTask.Rows[j].Cells["controlTaskCode"].Value = taskModelList[j].ControlCode;
            //        this.dgv_ControlTask.Rows[j].Cells["startStoreArea"].Value = taskModelList[j].StartArea;
            //        this.dgv_ControlTask.Rows[j].Cells["startPosition"].Value = taskModelList[j].StartDevice;
            //        this.dgv_ControlTask.Rows[j].Cells["endStoreArea"].Value = taskModelList[j].TargetArea;
            //        this.dgv_ControlTask.Rows[j].Cells["endPosition"].Value = taskModelList[j].TargetDevice;
            //        this.dgv_ControlTask.Rows[j].Cells["taskStatus"].Value = taskModelList[j].TaskStatus;
            //        this.dgv_ControlTask.Rows[j].Cells["startTime"].Value = DateTime.Now;
            //        this.dgv_ControlTask.Rows[j].Cells["completeTime"].Value = null;
            //    }
            //}
        }

        /// <summary>
        /// 刷新控制任务
        /// </summary>
        /// <param name="taskModel"></param>
        public void ShowControlTaskData(DataTable dtTask)
        {
            if (this.dgv_ControlTask.InvokeRequired)
            {
                ShowControlTaskDataInvoke showData = new ShowControlTaskDataInvoke(ShowControlTaskData);
                this.BeginInvoke(showData, new object[1] { dtTask });
                //this.Invoke(showData, new object[1] { dtTask });
            }
            else
            {
                this.dgv_ControlTask.Columns.Clear();
                this.dgv_ControlTask.DataSource = dtTask;
                this.dgv_ControlTask.Columns["创建时间"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                for (int i = 0; i < dtTask.Rows.Count; i++)
                {
                    string taskStatus = dtTask.Rows[i]["任务状态"].ToString();
                    if (taskStatus == EnumTaskStatus.错误.ToString() || taskStatus == EnumTaskStatus.超时.ToString())
                    {
                        this.dgv_ControlTask.Rows[i].DefaultCellStyle.BackColor = Color.Red;

                    }
                }
               
               
            }
        }
        /// <summary>
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        public int AskMessBox(string content)
        {
            DialogResult result = MessageBox.Show(content, "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="titleStr"></param>
        /// <param name="contentStr"></param>
        public void ShowMessage(string titleStr, string contentStr)
        {
            MessageBox.Show(contentStr, titleStr, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

       

    }
}
