using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using ECAMSModel;
using ECAMSPresenter;
using ECAMSDataAccess;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using LicenceManager;

namespace ECAMS
{

    public partial class MainView : BaseView, IMainView
    {

        #region 全局变量
        /// <summary>
        /// 添加日志委托 多线程访问
        /// </summary>
        /// <param name="EnumLogType"></param>
        /// <param name="logContent"></param>
        private delegate void AddLogInvoke(EnumLogCategory logcate, EnumLogType EnumLogType, string logContent);
        private delegate void AddErrorLogInvoke(EnumLogCategory logcate, EnumLogType EnumLogType, string logContent,int errorCode);
        private delegate void RefreshDeviceStatusInvoke(DataTable dtDevice);
        private delegate void DelegateRefreshCommDeviceGridView(DataTable dtComm);
        private delegate void SetStopEnabledInvoke(bool enabled);
        private delegate void SetStartEnabledInvoke(bool enabled);
        private bool showLog = true;//是否显示日志标示初始为不显示
        private bool isExistSystem = false;
        private bool isLogOutSystem = false;
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月20日
        /// 内容:库存管理货位状态的货位状态修改模块
        /// </summary>
        private bool isHasSetGsStatusFunc = false;
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月20日
        /// 内容:出库列表，用于序列化记录
        /// </summary>
        private Dictionary<string, List<string>> outStorageBatchDic= new Dictionary<string,List<string>>();
        //private  string batchListPath = AppDomain.CurrentDomain.BaseDirectory + @"Data\OutStorageBatchDic.osb";
      //  private static string licenceFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\licence.lic";
        private string batchListPath = AppDomain.CurrentDomain.BaseDirectory + @"OutStorageBatchDic.osb";
        private static string licenceFilePath = AppDomain.CurrentDomain.BaseDirectory + @"licence.lic";
        private string currentRoleName = "";
        private string lastLogContent = string.Empty;//记录上一次的日志如果相同就不显示也不存数据库
        private string lastErrorLogContent = string.Empty;
        LicenceModel licenceModel = new LicenceModel(licenceFilePath);
        LicenceModel licenseModel2 = new LicenceModel(AppDomain.CurrentDomain.BaseDirectory + @"gotionLicense2.lic");
        #endregion

        #region 初始化
        public MainView(int roleID)
        {
            InitializeComponent();
            OnSetLimit(roleID);
          
        }

       
        private void MainView_Shown(object sender, EventArgs e)
        {
            IniListView_LogImages();
            OnFormLoad();
            LoadOutStorageBatchNum();
            IniHousrNameList();
            IniLogTypeList();
            SetLogTypeLocation();
            licenceModel = licenceModel.LoadLicence();
            licenseModel2 = licenseModel2.LoadLicence();
            //if (licenceModel == null)
            //{
            //    licenceModel = new LicenceModel();
            //}
        }


        protected override object CreatePresenter()
        {
            MainPresenter mainPresenter = new MainPresenter(this);
            for (int i = 0; i < allPresenterList.Count; i++)
            {
                if (allPresenterList[i].GetType() == typeof(MainPresenter))
                {
                    allPresenterList.RemoveAt(i);
                }
            }
            allPresenterList.Add(mainPresenter);
            return mainPresenter;
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
        private void MainView_SizeChanged(object sender, EventArgs e)
        {
            IniListView_LogImages();
            SetLogTypeLocation();
        }
 

        private void tsmi_StartSystem_Click(object sender, EventArgs e)
        {
            if (IsSetOutStorageBathchNum())
            {
                this.tsmi_StartSystem.Enabled = false;
                this.tsmi_StopSystem.Enabled = true;
                OnStartSystem();
            }
            else
            {
                MessageBox.Show("自动出库产品批次号没有设置，请到系统配置文件中配置！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void tsmi_StopSystem_Click(object sender, EventArgs e)
        {
            this.tsmi_StartSystem.Enabled = true;
            this.tsmi_StopSystem.Enabled = false;
            OnStopSystem();

        }

        private void tsmi_openLog_Click(object sender, EventArgs e)
        {
            this.showLog = true;
            this.tsmi_openLog.Enabled = false;
            this.tsmi_closeLog.Enabled = true;
            AddLog(EnumLogCategory.控制层日志, EnumLogType.提示, "日志已打开");
        }

        private void tsmi_closeLog_Click(object sender, EventArgs e)
        {
            AddLog(EnumLogCategory.控制层日志, EnumLogType.提示, "日志已关闭");
            this.showLog = false;
            this.tsmi_openLog.Enabled = true;
            this.tsmi_closeLog.Enabled = false;
            
        }

        private void tsmi_ControlTaskMana_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "仓储管理-->控制任务";
            ShowChildForm("ControlTaskView");

            //ClearChildrenForms();

            //ControlTaskView frm = new ControlTaskView();
            //frm.MdiParent = this;
            //frm.Dock = DockStyle.Fill;

            //frm.Show();
        }

        private void tsmi_ManageTask_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "仓储管理-->管理任务";
            ShowChildForm("ManageTaskView");
            //ClearChildrenForms();
            //ManageTaskView frm = new ManageTaskView();
            //frm.MdiParent = this;
            //frm.Dock = DockStyle.Fill;

            //frm.Show();
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ListView_Log.Items.Clear();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ListView_Log.FocusedItem != null)
            {
                string selectLogTxt = "错误信息：" + this.ListView_Log.FocusedItem.ToolTipText +
                    "。时间：" + this.ListView_Log.FocusedItem.SubItems[2].Text + "。错误类型：" + this.ListView_Log.FocusedItem.SubItems[3].Text + "。";
                Clipboard.SetDataObject(selectLogTxt);
            }
        }

       
        private void 系统配置管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "系统设置-->出库设置";
            ShowChildForm("ConfigView");
          
        }

        private void 库存管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "仓储管理-->库存管理";
            ShowChildForm("StockManaView");
            //ClearChildrenForms();
            //StockManaView handTask = new StockManaView();

            //handTask.MdiParent = this;
            //handTask.Dock = DockStyle.Fill;

            //handTask.Show();

        }

        private void 数据管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "数据管理-->数据管理";
            ShowChildForm("DataManageView");
            //ClearChildrenForms();
            //DataManageView dataManage = new DataManageView();

            //dataManage.MdiParent = this;
            //dataManage.Dock = DockStyle.Fill;

            //dataManage.Show();
        }

        private void 货位状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "仓储管理-->货位状态";
            ShowChildForm("StorageView");
            //ClearChildrenForms();
            //StorageView goodsSite = new StorageView();

            //goodsSite.MdiParent = this;
            //goodsSite.Dock = DockStyle.Fill;

            //goodsSite.Show();
        }

        private void 历史任务查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "数据管理-->历史任务查询";
            ShowChildForm("HistoryTaskQueryView");
            //ClearChildrenForms();
            //HistoryTaskQueryView history = new HistoryTaskQueryView();

            //history.MdiParent = this;
            //history.Dock = DockStyle.Fill;

            //history.Show();
        }

        private void tsmi_TrayTrace_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "统计查询-->托盘跟踪";
            ShowChildForm("ProductTraceView");
        }

        private void 用户信息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "系统设置-->权限设置";
            ShowChildForm("UserManageView");
            //ClearChildrenForms();
            //UserManageView history = new UserManageView();

            //history.MdiParent = this;
            //history.Dock = DockStyle.Fill;

            //history.Show();
        }
        private void tsmi_LogMana_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "数据管理-->日志管理";
            ShowChildForm("LogView");
            //ClearChildrenForms();
            //LogView history = new LogView();

            //history.MdiParent = this;
            //history.Dock = DockStyle.Fill;

            //history.Show();

        }

        private void 产品信息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "数据管理-->产品信息管理";
            ShowChildForm("ProductManageView");
            //ClearChildrenForms();
            //ProductManageView productMana = new ProductManageView();
            //productMana.MdiParent = this;
            //productMana.Dock = DockStyle.Fill;
            //productMana.Show();
        }
        private void tsmi_DeviceMonitor_Click(object sender, EventArgs e)
        {
            this.tssl_CurrentViewName.Text = "数据管理-->设备监控";
            ShowChildForm("DeviceMonitorView");
        }
        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExistSystem == true  )
            {
                e.Cancel = false;
            }
            else
            {
                OnExitSys();
                if (isExistSystem == true)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }


        private void 切换用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("您确定要切换用户么！", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == System.Windows.Forms.DialogResult.Yes)
            {
                isLogOutSystem = true;

                OnChangeUser();
            }
         
        }

        private void ListView_Log_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string logStr = this.ListView_Log.FocusedItem.ToolTipText;
            LogDetailView ldv = new LogDetailView(logStr);
            ldv.ShowDialog();

        }
        private void 版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutView av = new AboutView();
            av.ShowDialog();
        }
        /// <summary>
        /// "退出" 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExitSys();
        }

        private void 控制层测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ShowChildForm("TestModuleForm");
        }

        private void bt_RefreshBatch_Click(object sender, EventArgs e)
        {
            OnRefreshBatch();
        }

        private void bt_AddSet_Click(object sender, EventArgs e)
        {
            if (this.cb_HouseName.Text == "")
            {
                MessageBox.Show("请选择要设置的库房！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.cb_BatchList.Text == "")
            {
                MessageBox.Show("请选择要设置出库的批次号！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.outStorageBatchDic.ContainsKey(this.cb_HouseName.Text))
            {
                this.outStorageBatchDic[this.cb_HouseName.Text].Clear();
                this.outStorageBatchDic[this.cb_HouseName.Text].Add(this.cb_BatchList.Text.Trim()); 
            }
            else
            {
                List<string> batchesList = new List<string>();
                batchesList.Add(this.cb_BatchList.Text.Trim());
                this.outStorageBatchDic.Add(this.cb_HouseName.Text, batchesList);
            }
          
            bool saveStatus = this.SaveProductBatch();
            if (this.outStorageBatchDic[EnumStoreHouse.A1库房.ToString()] != null)
            {
                this.lb_A1OutBatch.Text = this.outStorageBatchDic[EnumStoreHouse.A1库房.ToString()][0];
            }
            if (this.outStorageBatchDic[EnumStoreHouse.B1库房.ToString()] != null)
            {
                this.lb_B1OutBatch.Text = this.outStorageBatchDic[EnumStoreHouse.B1库房.ToString()][0];
            }
            if (saveStatus)
            {
                MessageBox.Show("产品出库批次设置成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("产品出库批次设置失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 堆垛机FX PLC通信关闭,add by zwx,2014-07-09
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemStackerCommClose_Click(object sender, EventArgs e)
        {
            if (this.eventClosePLCComm != null)
            {
                PlcCommOpEventArgs args = new PlcCommOpEventArgs();
                args.PlcID = EnumDevPLC.PLC_STACKER_FX;
                this.eventClosePLCComm.Invoke(this, args);
            }
        }

        /// <summary>
        ///  堆垛机FX PLC通信重新打开,,add by zwx,2014-07-09
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemStackerCommReOpen_Click(object sender, EventArgs e)
        {
            if (this.eventReOpenPLCComm != null)
            {
                PlcCommOpEventArgs args = new PlcCommOpEventArgs();
                args.PlcID = EnumDevPLC.PLC_STACKER_FX;
                this.eventReOpenPLCComm.Invoke(this, args);
            }
        }

        #endregion

        #region 实现IMainView接口事件
        public event EventHandler eventStartSystem;

        public event EventHandler eventStopSystem;

        public event EventHandler<SetLimitEventArgs> eventSetLimit;
        public event EventHandler eventChangeUser;
        public event EventHandler<LogEventArgs> eventSaveLog;
        public event EventHandler<ECAMSErrorEventArgs> eventSaveErrorLog;
        public event EventHandler eventExitSys;
        public  event EventHandler eventFormLoad;
        public event EventHandler eventRefreshBatch;
        public event EventHandler<PlcCommOpEventArgs> eventClosePLCComm; //关闭PLC通信
        public event EventHandler<PlcCommOpEventArgs> eventReOpenPLCComm; //重新打开PLC通信
        public event EventHandler eventSetProcessTaskMode;
        #endregion

        #region 触发IMainView接口事件
        private void OnRefreshBatch()
        {
            if (this.eventRefreshBatch != null)
            {
                this.eventRefreshBatch.Invoke(this, null);
            }
        }
        private void OnFormLoad()
        {
            if (this.eventFormLoad != null)
            {
                this.eventFormLoad.Invoke(this, null);
            }
        }
        public void OnSetLimit(int roleID)
        {
            if (this.eventSetLimit != null)
            {
                SetLimitEventArgs limitArgs = new SetLimitEventArgs();
                limitArgs.RoleID = roleID;
                this.eventSetLimit.Invoke(this, limitArgs);
            }
        }
        public void OnExitSys()
        {
            if (this.eventExitSys != null)
            {
                this.licenceTime.Stop();
                this.eventExitSys.Invoke(this, null);
            }
        }

        public void OnStop()
        {
            this.tsmi_StartSystem.Enabled = true;
            this.tsmi_StopSystem.Enabled = false;
            OnStopSystem();
        }


        private void OnSaveErrorLog(EnumLogCategory logCate, EnumLogType logType, string logContent, int errorCode)
        {
            if (this.eventSaveLog != null)
            {
                ECAMSErrorEventArgs logArgs = new ECAMSErrorEventArgs();
                logArgs.LogCate = logCate;
                logArgs.LogType = logType;
                logArgs.LogContent = logContent;
                logArgs.ErrorCode = errorCode;
                logArgs.LogTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"));
                this.eventSaveErrorLog.Invoke(this, logArgs);
            }
        }

        private void OnSaveLog(EnumLogCategory logCate, EnumLogType logType, string logContent)
        {
            if (this.eventSaveLog != null)
            {
                LogEventArgs logArgs = new LogEventArgs();
                logArgs.LogCate = logCate;
                logArgs.LogType = logType;
                logArgs.LogContent = logContent;
                logArgs.LogTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                this.BeginInvoke(eventSaveLog,this, logArgs);
            }
        }



        private void OnChangeUser()
        {
            if (this.eventChangeUser != null)
            {
                this.eventChangeUser.Invoke(this, null);
            }
        }

     
        private void OnStartSystem()
        {
            if (this.eventStartSystem != null)
            {
                this.eventStartSystem.Invoke(this, null);
            }
        }

        private void OnStopSystem()
        {
            if (this.eventStopSystem != null)
            {
                this.eventStopSystem.Invoke(this, null);
            }
        }

        #endregion

        #region 实现IMainView方法
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月13日
        /// 内容:获取登录角色名称
        /// </summary>
        /// <returns></returns>
        public string GetCurrentRoleName()
        {
            return this.currentRoleName;
        }
        /// <summary>
        /// 获取授权时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLicenceDate()
        {
            try
            {
                DateTime deadTime =  DateTime.Parse("2015-03-19 08:00:00");
                if (this.licenceModel != null && this.licenceModel.LicenceEndTime != null && this.licenceModel.LicenceEndTime != "")
                {
                    deadTime=DateTime.Parse(this.licenceModel.Decrypt(this.licenceModel.LicenceEndTime));
                }
                if (this.licenseModel2 != null && this.licenseModel2.LicenceEndTime != null && this.licenseModel2.LicenceEndTime != "")
                {
                    DateTime deadTime2 = DateTime.Parse(this.licenseModel2.Decrypt(this.licenseModel2.LicenceEndTime));
                    if(deadTime2<deadTime)
                    {
                        deadTime = deadTime2;
                    }
                }
                
                return deadTime;
            }
            catch(Exception ex)
            {
                DateTime deadTime = DateTime.Parse("2015-03-19 08:00:00");
                return deadTime;// DateTime.MinValue;
               // return DateTime.MinValue;//有异常则返回时间最小值
            }
        }

        /// 作者:np
        /// 时间:2014年6月13日
        /// 内容:设置登录角色名称
        /// </summary>
        /// <returns></returns>
        public void SetCurrentRoleName(string roleName)
        {
            this.currentRoleName = roleName;
        }

        public void SetStopEnabled(bool enabled)
        {
            if (this.InvokeRequired)
            {
                SetStopEnabledInvoke ssei = new SetStopEnabledInvoke(SetStopEnabled);
                this.Invoke(ssei,new object[1]{enabled});
            }
            else
            {
                this.tsmi_StopSystem.Enabled = enabled;
            }
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月27日
        /// 内容:设置按钮可用状态
        /// </summary>
        /// <param name="enabled"></param>
        public void SetStartEnabled(bool enabled)
        {
            if (this.InvokeRequired)
            {
                SetStartEnabledInvoke ssei = new SetStartEnabledInvoke(SetStartEnabled);
                this.Invoke(ssei, new object[1] { enabled });
            }
            else
            {
                this.tsmi_StartSystem.Enabled = enabled;
            }
        }

        /// <summary>
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        public int AskMessBox(string content)
        {
            DialogResult result = MessageBox.Show(content, "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                isExistSystem = true;
                return 0;
            }
            else
            {
                isExistSystem = false;
                return 1;
            }

        }

         //<summary>
         //刷新通信设备状态
         //</summary>
         //<param name="dtComm"></param>
        public void RefreshCommDeviceGridView(DataTable dtComm)
        {
            //if (this.dgv_CommDeviceStatus.InvokeRequired)
            //{
            //    DelegateRefreshCommDeviceGridView deleateCommDeviceGdieView = new DelegateRefreshCommDeviceGridView(RefreshCommDeviceGridView);
            //    this.Invoke(deleateCommDeviceGdieView, new object[1] { dtComm });
            //}
            //else
            //{
            //    this.dgv_CommDeviceStatus.DataSource = dtComm;
            //    for (int i = 0; i < dtComm.Rows.Count; i++)
            //    {
            //        if (dtComm.Rows[i]["通信状态"].ToString() == EnumDevCommStatus.通信断开.ToString())
            //        {
            //            this.dgv_CommDeviceStatus.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            //        }
            //    }
            //}
        }

        public void RefreshDeviceStatus(DataTable dtDevice)
        {

            if (this.dgv_DeviceStatus.InvokeRequired)
            {
                RefreshDeviceStatusInvoke deviceInvoke = new RefreshDeviceStatusInvoke(RefreshDeviceStatus);
                this.BeginInvoke(deviceInvoke, new object[1] { dtDevice });
            }
            else
            {
                this.dgv_DeviceStatus.DataSource = dtDevice;
                for (int i = 0; i < this.dgv_DeviceStatus.Columns.Count; i++)
                {
                    this.dgv_DeviceStatus.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                }

                for (int i = 0; i < dtDevice.Rows.Count; i++)
                {
                    if (this.dgv_DeviceStatus.Rows.Count == 0)
                    {
                        break;
                    }

                    if (this.dgv_DeviceStatus.Rows[i].Cells["设备故障码"].Value.ToString() != "0")
                    {
                        this.dgv_DeviceStatus.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }

                }
                Application.DoEvents();
            }

        }

        public void SetProcessTaskMode()
        {
            if (this.eventSetProcessTaskMode != null)
            {
                this.eventSetProcessTaskMode.Invoke(this, null);
            }
        }
        public void SetViewHide()
        {
            this.Hide();
        }

        public void ExitSystem()
        {
            this.isExistSystem = true;
            Application.Exit();
        }

        public void ShowView()
        {
            this.Show();
        }

        public void SetLimit(string[] funclist)
        {
            #region 出库设置
            if (IsExit(funclist, this.tsmi_SysCongig.Text))
            {
                this.tsmi_SysCongig.Visible = true;
            }
            else
            {
                this.tsmi_SysCongig.Visible = false;
            }
            #endregion 
            #region 权限设置
            if (IsExit(funclist, this.tsmi_UserInfor.Text))
            {
                this.tsmi_UserInfor.Visible = true;
            }
            else
            {
                this.tsmi_UserInfor.Visible = false;
            }
            #endregion
            #region 管理任务
            if (IsExit(funclist, this.tsmi_ManageTask.Text))
            {
                this.tsmi_ManageTask.Visible = true;
            }
            else
            {
                this.tsmi_ManageTask.Visible = false;
            }
            #endregion
            #region 控制任务
            if (IsExit(funclist, this.tsmi_ControlTaskMana.Text))
            {
                this.tsmi_ControlTaskMana.Visible = true;
            }
            else
            {
                this.tsmi_ControlTaskMana.Visible = false;
            }
            #endregion
            #region 库存管理
            if (IsExit(funclist, this.tsmi_StockManage.Text))
            {
                this.tsmi_StockManage.Visible = true;
            }
            else
            {
                this.tsmi_StockManage.Visible = false;
            }
            #endregion
            #region 货位管理
            if (IsExit(funclist, this.tsmi_GoodsSiteMana.Text))
            {
                this.tsmi_GoodsSiteMana.Visible = true;
            }
            else
            {
                this.tsmi_GoodsSiteMana.Visible = false;
            }
            #endregion 
            #region 日志查询
            if (IsExit(funclist, this.tsmi_LogQuery.Text))
            {
                this.tsmi_LogQuery.Visible = true;
            }
            else
            {
                this.tsmi_LogQuery.Visible = false;
            }
            #endregion
            #region 数据管理
            if (IsExit(funclist, this.tsmi_DataMana.Text))
            {
                this.tsmi_DataMana.Visible = true;
            }
            else
            {
                this.tsmi_DataMana.Visible = false;
            }
            #endregion
            #region 设备监控
            if (IsExit(funclist, this.tsmi_DeviceMonitor.Text))
            {
                this.tsmi_DeviceMonitor.Visible = true;
            }
            else
            {
                this.tsmi_DeviceMonitor.Visible = false;
            }
            #endregion
            #region 产品信息管理
            if (IsExit(funclist, this.tsmi_ProductMana.Text))
            {
                this.tsmi_ProductMana.Visible = true;
            }
            else
            {
                this.tsmi_ProductMana.Visible = false;
            }
            #endregion
            #region 历史任务查询
            if (IsExit(funclist, this.tsmi_HistoryTaskMana.Text))
            {
                this.tsmi_HistoryTaskMana.Visible = true;
            }
            else
            {
                this.tsmi_HistoryTaskMana.Visible = false;
            }
            #endregion
            #region 货位状态修改
            if (IsExit(funclist, "货位状态修改"))
            {
                this.isHasSetGsStatusFunc = true;
            }
            else
            {
                this.isHasSetGsStatusFunc = false;
            }
            #endregion
            #region 数据监测与修正
            if (IsExit(funclist, "数据监测与修正"))
            {
                this.tsmi_DataMonitor.Visible = true;
            }
            else
            {
                this.tsmi_DataMonitor.Visible = false;
            }
            #endregion
            #region 托盘跟踪
            if (IsExit(funclist, "托盘跟踪"))
            {
                this.tsmi_TrayTrace.Visible = true;
            }
            else
            {
                this.tsmi_TrayTrace.Visible = false;
            }
            
            #endregion
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="EnumLogType"></param>
        /// <param name="logContent"></param>
        public void AddLog(EnumLogCategory logcate, EnumLogType EnumLogType, string logContent)
        {
            if (this.lastLogContent == logContent)
            {
                return;
            }
          
            if (showLog == true)
            {
                if (this.ListView_Log.InvokeRequired)
                {
                    AddLogInvoke addLogInvoke = new AddLogInvoke(AddLog);
                    this.Invoke(addLogInvoke, new object[3] { logcate, EnumLogType, logContent });
                }
                else
                {
                    ListViewItem viewItem = new ListViewItem();
                    if (EnumLogType == EnumLogType.错误)//错误用红色显示
                    {
                        viewItem.BackColor = Color.Red;
                    }
                    viewItem.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    viewItem.SubItems.Add(logContent);
                    viewItem.ToolTipText = logContent;
                    viewItem.SubItems.Add(logcate.ToString());
                    viewItem.SubItems.Add(EnumLogType.ToString());
                    viewItem.ImageIndex = 0;
                    this.lastLogContent = logContent;
                    if (this.cb_LogType.Text == EnumLogType.ToString() || this.cb_LogType.Text == EnumLogType.所有.ToString())
                    {
                        this.ListView_Log.Items.Add(viewItem);
                        this.ListView_Log.Items[this.ListView_Log.Items.Count - 1].EnsureVisible();
                        if (this.ListView_Log.Items.Count > 200)
                        {
                            this.ListView_Log.Items.RemoveAt(0);
                        }
                    }
                    Application.DoEvents();
                 
                    //modify by zwx,防止重复添加日志到数据库
                    //OnSaveLog(logcate, EnumLogType, logContent);
                }

            }
            //else
            //{
            //    OnSaveLog(logcate, EnumLogType, logContent);
            //}
            
        }

        public void AddLogErrorCode(EnumLogCategory logcate, EnumLogType EnumLogType, string logContent, int errorCode)
        {
            if (this.lastErrorLogContent == logContent)
            {
                return;
            }
        
            if (showLog == true)
            {
               
                if (this.ListView_Log.InvokeRequired)
                {
                    AddErrorLogInvoke addErrorLogInvoke = new AddErrorLogInvoke(AddLogErrorCode);
                    this.Invoke(addErrorLogInvoke, new object[4] { logcate, EnumLogType, logContent,errorCode });
                }
                else
                {
                    ListViewItem viewItem = new ListViewItem();
                    if (EnumLogType == EnumLogType.错误)//错误用红色显示
                    {
                        viewItem.BackColor = Color.Red;
                    }
                    viewItem.SubItems.Add(DateTime.Now.ToString());
                    viewItem.SubItems.Add(logContent);
                    viewItem.ToolTipText = logContent;
                    viewItem.SubItems.Add(logcate.ToString());
                    viewItem.SubItems.Add(EnumLogType.ToString());
                    viewItem.SubItems.Add(errorCode.ToString());
                    viewItem.ImageIndex = 0;
                    this.lastErrorLogContent = logContent;
                    if (this.cb_LogType.Text == EnumLogType.ToString() || this.cb_LogType.Text == EnumLogType.所有.ToString())
                    {
                        this.ListView_Log.Items.Add(viewItem);
                        this.ListView_Log.Items[this.ListView_Log.Items.Count - 1].EnsureVisible();
                        if (this.ListView_Log.Items.Count > 200)//保留200条日志
                        {
                            this.ListView_Log.Items.RemoveAt(0);
                        }
                    }
                    Application.DoEvents();
                    //OnSaveErrorLog(logcate, EnumLogType, logContent, errorCode);
                }

            }
            //else
            //{
            //    OnSaveErrorLog(logcate, EnumLogType, logContent, errorCode);
            //}
           
        }

        /// <summary>
        /// 初始化dgv列表
        /// </summary>
        /// <param name="count"></param>
        //public void IniDGVDevice(List<DeviceModel> deviceModelList)
        //{
        //    if (this.dgv_DeviceStatus.InvokeRequired)
        //    {
        //        RefreshDeviceStatusInvoke deviceInvoke = new RefreshDeviceStatusInvoke(IniDGVDevice);
        //        this.Invoke(deviceInvoke, new object[1] { deviceModelList });
        //    }
        //    else
        //    {
        //        this.dgv_DeviceStatus.Rows.Clear();
        //        for (int i = 0; i < deviceModelList.Count; i++)
        //        {
        //            this.dgv_DeviceStatus.Rows.Add();
        //            this.dgv_DeviceStatus.Rows[i].Cells["deviceName"].Value = deviceModelList[i].DeviceType;
        //            this.dgv_DeviceStatus.Rows[i].Cells["deviceID"].Value = deviceModelList[i].DeviceID;
        //            this.dgv_DeviceStatus.Rows[i].Cells["deviceStatus"].Value = deviceModelList[i].DeviceStatus;
        //            if (deviceModelList[i].DeviceStatus == EnumDevStatus.故障.ToString())
        //            {
        //                this.dgv_DeviceStatus.Rows[i].DefaultCellStyle.BackColor = Color.Red;
        //            }
        //            //if (deviceModelList[i].DeviceStatus == EnumDevStatus.故障.ToString())
        //            //{
        //            //    this.dgv_DeviceStatus.Rows[i].Cells["deviceStatus"].Value = this.imageList1.Images[1];
        //            //}
        //            //else
        //            //{
        //            //    this.dgv_DeviceStatus.Rows[i].Cells["deviceStatus"].Value = this.imageList1.Images[0];
        //            //}

        //        }
        //    }
        //}

       
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月19日
        /// 内容:刷新产品批次下拉列表
        /// </summary>
        public void RefreshBatchList(DataTable dt) 
        {
            this.cb_BatchList.DataSource = dt;
            this.cb_BatchList.DisplayMember = "Tf_BatchID";
        }
        /// <summary>
        /// 作者：np
        /// 时间：2014年3月13日 星期四
        /// 内容：保存临时控制任务(序列化)
        /// </summary>
        /// <returns></returns>
        public bool SaveProductBatch()
        {
            FileStream fs1 = null;
            try
            {
                fs1 = new FileStream(batchListPath, FileMode.OpenOrCreate);
                BinaryFormatter formatter1 = new BinaryFormatter();
                formatter1.Serialize(fs1, this.outStorageBatchDic);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (fs1 != null)
                {
                    fs1.Close();
                }
            }
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年5月10日
        /// 内容:显示出库产品的批次号
        /// </summary>
        public Dictionary<string, List<string>> LoadOutStorageBatchNum()
        {
            FileStream fs = null;
            try
            {
                if (!File.Exists(batchListPath))
                {
                    SaveProductBatch();
                }
                fs = new FileStream(batchListPath, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Dictionary<string, List<string>> outStorageBatchList = formatter.Deserialize(fs) as Dictionary<string, List<string>>;
                outStorageBatchDic = outStorageBatchList;
                if (this.outStorageBatchDic[EnumStoreHouse.A1库房.ToString()] != null)
                {
                    this.lb_A1OutBatch.Text = this.outStorageBatchDic[EnumStoreHouse.A1库房.ToString()][0];
                }
                if (this.outStorageBatchDic[EnumStoreHouse.B1库房.ToString()] != null)
                {
                    this.lb_B1OutBatch.Text = this.outStorageBatchDic[EnumStoreHouse.B1库房.ToString()][0];
                }
                return outStorageBatchDic;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
          
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月16日
        /// 内容:关闭所有子窗体
        /// </summary>
        public void CloseAllChildForm()
        {
            //遍历主窗体的所有子窗体实例 
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }
        }

        #endregion
       
        #region 界面私有方法
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月10日
        /// 内容:判断是否设置了出库批次号
        /// </summary>
        /// <returns></returns>
        private bool IsSetOutStorageBathchNum()
        {
            //string outStorageBatchNum = ConfigurationManager.AppSettings["OutStorageBatchNum"];
            //string[] splitStrArr = new string[4] { ",", "-", "_", "+" };
            //string[] productBatchNums = SplitStringArray(outStorageBatchNum, splitStrArr);
            Dictionary<string, List<string>> productBatchDic = LoadOutStorageBatchNum();
            if (productBatchDic != null && productBatchDic.Count== 2)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
           
        public  string[] SplitStringArray(string srcStrs, string[] splitStr)
        {
            if (srcStrs == null || srcStrs == string.Empty || splitStr == null || splitStr.Count() == 0)
            {
                return null;
            }
            return srcStrs.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool IsExit(string[] funcList, string func)
        {
            bool isExist = false;
            for (int i = 0; i < funcList.Length; i++)
            {
                if (func == funcList[i])
                {
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }
        
        private void ShowChildForm(string formClassName)
        {
            bool isHad = false;
            //根据类名（先转换为类的FullName形式，带命名空间名）得到子窗体的Type    
            Type formType = Type.GetType(this.GetType().Namespace + "." + formClassName);   
            //遍历主窗体的所有子窗体实例 
            foreach (Form form in this.MdiChildren)         
            { 
                //判断某子窗体实例f，是否是指定类型的实例              
                if (formType.IsInstanceOfType(form))
                {
                    form.Visible = true;
                    isHad = true;
                }
                else
                {
                    form.Visible = false;
                }
            }
            if (isHad == false)
            {
                //使用Activator创建formType类型的对象，显示为子窗体 
                Form formInstance = (Form)Activator.CreateInstance(formType, null);
                formInstance.MdiParent = this;
                formInstance.Dock = DockStyle.Fill;
                formInstance.Show();
            }
        }

        /// <summary>
        /// 关闭所有子窗体，在打开新子窗体时调用
        /// </summary>
        private void ClearChildrenForms()
        {
            //foreach (Form form in this.MdiChildren)
            //{
            //    form.Close();
            //}
        }

        /// <summary>
        /// 作者：np
        /// 时间：2013.6.24
        /// 内容：初始化输出界面信息类型图标
        /// </summary>
        private void IniListView_LogImages()
        {
            ImageList smallImageList = new ImageList();
            smallImageList.Images.Add(System.Drawing.SystemIcons.Information);
            smallImageList.Images.Add(System.Drawing.SystemIcons.Error);
            this.ListView_Log.SmallImageList = smallImageList;

            this.log_Content.Width = (this.ListView_Log.Width - 20) / 2;
            this.log_Date.Width = (this.ListView_Log.Width - this.log_Content.Width - 20) / 4;
            this.log_Category.Width = this.log_Date.Width;
            this.log_Type.Width = this.log_Date.Width;
            this.log_ErrorCode.Width = this.log_Date.Width;
        }

        private void IniHousrNameList()
        {
            this.cb_HouseName.Items.Clear();
            this.cb_HouseName.Items.Add(EnumStoreHouse.A1库房.ToString());
            this.cb_HouseName.Items.Add(EnumStoreHouse.B1库房.ToString());
        }
        private void SetLogTypeLocation()
        {
            Point logControlLoca = this.ListView_Log.Location;
            int x = 0, y = 0;
            x = logControlLoca.X + this.log_Image.Width + this.log_Content.Width + this.log_Date.Width + this.log_Category.Width+40;
            y = logControlLoca.Y+4;
            this.cb_LogType.Location = new Point(x,y);
            if (this.cb_LogType.Width > this.log_Type.Width - 40)
            {
                this.cb_LogType.Width = this.log_Type.Width - 40;
            }
            else
            {
                this.cb_LogType.Width = 83;
            }
        }
        private void IniLogTypeList()
        {
            this.cb_LogType.Items.Clear();        
            for (int i = 0; i < Enum.GetNames(typeof(EnumLogType)).Count(); i++)
            {
                this.cb_LogType.Items.Add(Enum.GetNames(typeof(EnumLogType))[i]);
            }
            if (this.cb_LogType.Items.Count > 0)
            {
                this.cb_LogType.Text = this.cb_LogType.Items[this.cb_LogType.Items.Count - 1].ToString();
            }
        }
        #endregion
        private void LicenseCheck1()
        {
            string reStr = "";
            if (licenceModel == null || !licenceModel.IsLicenceValid(ref reStr))
            {
                this.licenceTime.Enabled = false;
                OnStop();//停止系统
                AddLog(EnumLogCategory.管理层日志, EnumLogType.提示, "软件使用期限已到，然后激活系统！" + reStr);
                ActivativeFormView activativeFrom = new ActivativeFormView(this.licenceModel);
                activativeFrom.ShowDialog();
                if (!activativeFrom.isLicenceValid)
                {
                    this.menuStrip1.Enabled = false;
                }
                else
                {
                    this.menuStrip1.Enabled = true;
                }

                this.licenceTime.Enabled = true;

            }

            if (licenceModel != null)
            {
                licenceModel.WriteLastRunTime();
            }
            else
            {
                MessageBox.Show("licence.lic文件丢失！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LicenseCheck2()
        {
            string reStr = "";
            if (licenseModel2 == null || !licenseModel2.IsLicenceValid(ref reStr))
            {
                this.licenceTime.Enabled = false;
                OnStop();//停止系统
                AddLog(EnumLogCategory.管理层日志, EnumLogType.提示, "软件使用期限已到，然后激活系统！" + reStr);
                ActivativeFormView activativeFrom = new ActivativeFormView(licenseModel2);
                activativeFrom.ShowDialog();
                if (!activativeFrom.isLicenceValid)
                {
                    this.menuStrip1.Enabled = false;
                }
                else
                {
                    this.menuStrip1.Enabled = true;
                }

                this.licenceTime.Enabled = true;

            }

            if (licenseModel2 != null)
            {
                licenseModel2.WriteLastRunTime();
            }
            else
            {
                MessageBox.Show("licence.lic文件丢失！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void licenceTime_Tick(object sender, EventArgs e)
        {
            LicenseCheck1();
            LicenseCheck2();
        }

       
    }
    
}
