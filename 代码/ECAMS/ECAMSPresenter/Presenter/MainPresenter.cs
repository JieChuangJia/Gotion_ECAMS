using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Collections;
using System.Configuration;
using ECAMSDataAccess;
using ECAMSModel;
using PLCControl;
namespace ECAMSPresenter
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        #region 全局变量
        /// <summary>
        /// 任务监控线程
        /// </summary>
        private Thread monitorTaskThread = null;
        private Thread iniSysThread = null;
        private bool monitorThreadRunning = false;
        public static bool isDebug = false;   //调试标示

        private volatile bool monitorExit = false;
        private int monitorTaskTime = 200; //扫描监控任务间隔毫秒

        private readonly ControlInterfaceBll bllControlApply = new ControlInterfaceBll();     //控制接口任务
        private readonly TaskTypeBll bllTaskType = new TaskTypeBll();                         //任务类型
        private readonly DeviceBll bllDevice = new DeviceBll();                               //设备信息
        private readonly ControlTaskBll bllControlTask = new ControlTaskBll();                //控制任务
        private readonly ManageTaskBll bllTask = new ManageTaskBll();                         //管理任务
        private readonly ManageTaskListBll bllTaskList = new ManageTaskListBll();             //管理任务列表
        private readonly LogicStoreAreaBll bllLogicStoreArea = new LogicStoreAreaBll();       //逻辑库区 
        private readonly GoodsSiteBll bllGoodsSite = new GoodsSiteBll();                      //货位
        private readonly StockBll bllStock = new StockBll();                                   //库存
        private readonly StockListBll bllStockList = new StockListBll();                      //库存列表
        private readonly StoreHouseBll bllStoreHouse = new StoreHouseBll();                   //库房逻辑
        private readonly StoreAreaBll bllStoreArea = new StoreAreaBll();                      //库区逻辑
        private  List<TaskTypeModel> sysCreateTaskList = new List<TaskTypeModel>();            //系统生成的任务列表
        private readonly HistoryManageTaskBll bllHistoryTask = new HistoryManageTaskBll();    //历史任务逻辑
        private readonly ProductBll bllProduct = new ProductBll();                            //产品表逻辑
        private readonly User_LimitBll bllUserLimit = new User_LimitBll();
        private readonly User_FunctionListBll bllFuncList = new User_FunctionListBll();
        private readonly TB_Batch_IndexBll bllBatchIndex = new TB_Batch_IndexBll();
        private readonly TB_Tray_indexBll bllTrayIndex = new TB_Tray_indexBll();
        private readonly StockDetailBll bllStockDetail = new StockDetailBll();
        private readonly View_QueryStockListBll bllViewQueryStockList = new View_QueryStockListBll();
        private readonly User_RoleBll bllRole = new User_RoleBll();
        private readonly LogBll bllLog = new LogBll();
        public static string userNameStr = "";                                                      //登录用户名

        public static ECAMWCS wcs = new ECAMWCS();
        #endregion

        #region 初始化
        public MainPresenter(IMainView view)
            : base(view)
        {


        }

        protected override void OnViewSet()
        {
            this.View.eventStartSystem += StartSystemEventHandler;
            this.View.eventStopSystem += StopSystemEventHandler;
            this.View.eventSetLimit += SetLimitEventHandler;
            this.View.eventChangeUser += ChangeUserEventHandler;
            this.View.eventSaveErrorLog += SaveErrorEventHandler;
            this.View.eventSaveLog += SaveLogEventHandler;
            this.View.eventExitSys += ExitSysEventHandler;
            this.View.eventFormLoad += FormLoadEventHandler;
            this.View.eventRefreshBatch += RefreshBatchEventHandler;
            this.View.eventClosePLCComm += PlcCommCloseHandler;
            this.View.eventReOpenPLCComm += PlcCommReOpenHandler;
            this.View.eventSetProcessTaskMode += SetProcessTaskModeEventHanlder;
        }
        #endregion

        #region 接口IMainView事件函数
        public  void SetProcessTaskModeEventHanlder(object sender,EventArgs e)
        {
            sysCreateTaskList = bllTaskType.GetAutoTaskList(EnumTaskName.电芯出库_A1.ToString(), EnumTaskName.电芯出库_B1.ToString(), EnumTaskName.分容出库_A1.ToString());
        }

        private void IniSysThread()
        {

            try
            {
                //List<DeviceModel> deviceModelList = bllDevice.GetShowDevList();
                //this.View.IniDGVDevice(deviceModelList);

                DataTable dt = bllBatchIndex.GetAllBatches();
                this.View.RefreshBatchList(dt);
                sysCreateTaskList = bllTaskType.GetAutoTaskList(EnumTaskName.电芯出库_A1.ToString(), EnumTaskName.电芯出库_B1.ToString(), EnumTaskName.分容出库_A1.ToString());

                string reStr = string.Empty;
                wcs.AttachLogHandler(LogEventHandler);
                wcs.AttachErrorHandler(LogErrorEventHandler);
                wcs.AttachCommDevStatusHandler(RefreshCommDeviceEventHandler);
                wcs.AttachDevStatusHandler(RefreshDeviceStatusEventHandler);


                if (!wcs.WCSInit(ref reStr))
                {
                    this.View.SetStartEnabled(false);
                    this.View.SetStopEnabled(false);
                    ShowLog(EnumLogCategory.控制层日志, EnumLogType.提示, reStr);
                    ShowLog(EnumLogCategory.管理层日志, EnumLogType.错误, "控制层初始化失败");
                }
                else
                {
                    ShowLog(EnumLogCategory.控制层日志, EnumLogType.提示, "控制层初始化成功!");
                    this.View.SetStartEnabled(true);
                }
                DateTime deadTime = this.View.GetLicenceDate();
                DateTime nowTime = System.DateTime.Now;
                TimeSpan ss= new TimeSpan(7,0,0,0);
                DateTime firstLockTime = deadTime-ss;
                TimeSpan s = deadTime - nowTime;
                bllControlApply.CreateLimitTrigger(deadTime);//过期时间
                if (s.TotalDays < 15.0)
                {
                    StringBuilder warnInfoBuild = new StringBuilder();
                    warnInfoBuild.AppendFormat("软件即将过期，将于{0}过期,请付费！(最终解释权归深圳基信机械有限公司)", firstLockTime);
                    this.View.AskMessBox(warnInfoBuild.ToString());
                    View.AddLog(EnumLogCategory.管理层日志, EnumLogType.提示, warnInfoBuild.ToString());
                }
                
            }
            catch (System.Exception ex)
            {
                ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, "初始化异常，" + ex.Message + ex.StackTrace);
            }


        }

        private void RefreshBatchEventHandler(object sender, EventArgs e)
        {
            DataTable dt = bllBatchIndex.GetAllBatches();
            this.View.RefreshBatchList(dt);
        }

        private void FormLoadEventHandler(object sender, EventArgs e)
        {
            try
            {
               
                PubConstant.ConnectionString2 = ConfigurationManager.AppSettings["GXDataBase"];
                monitorTaskThread = new Thread(new ThreadStart(StartTaskMonitor));
                monitorTaskThread.IsBackground = true; //设置为后台线程

                iniSysThread = new Thread(new ThreadStart(IniSysThread));
                iniSysThread.IsBackground = true;
                iniSysThread.Start();

                // RefreshBatchEventHandler(null, null);
            }
            catch (System.Exception ex)
            {
                ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, "初始化异常，" + ex.Message + ex.StackTrace);
            }

        }
        private void ExitSysEventHandler(object sender, EventArgs e)
        {
            int re = View.AskMessBox("您确认要退出系统吗？");
            if (re == 0)
            {
                ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, "系统退出");
                this.monitorExit = true;
                if (monitorTaskThread.ThreadState == (ThreadState.Running | ThreadState.Background))
                {
                    if (!this.monitorTaskThread.Join(2000))
                    {
                        this.monitorTaskThread.Abort();
                    }
                }

                string logStr = "";
                wcs.WCSExit(ref logStr);
                this.View.ExitSystem();
            }
        }

        /// <summary>
        /// 控制层显示实现函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogEventHandler(object sender, LogEventArgs e)
        {
            ShowLog(e.LogCate,e.LogType,e.LogContent);
            //SaveLogEventHandler(sender, e);
            //ShowLog(e.LogCate, e.LogType, e.LogContent);
        }

        /// <summary>
        /// 初始化的时候控制层绑定,之后由控制层调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshCommDeviceEventHandler(object sender, CommDeviceEventArgs e)
        {
            this.View.RefreshCommDeviceGridView(e.DtStatus);
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月20日
        /// 内容:设备状态，由控制层调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshDeviceStatusEventHandler(object sender, DeviceStatusEventArgs e)
        {
            this.View.RefreshDeviceStatus(e.DtStatus);
        }

        /// <summary>
        /// 控制层错误码实现函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogErrorEventHandler(object sender, ECAMSErrorEventArgs e)
        {
            if (e.RequireRunningStop == true)//控制层请求停止
            {
                StopSystemEventHandler(null, null);
                ShowLog(e.LogCate, e.LogType, "控制层请求系统停止！");
                this.View.SetStartEnabled(true);
                this.View.SetStopEnabled(false);
            }
            ShowErrorLog(e.LogCate,e.LogType,e.LogContent,e.ErrorCode);
            //SaveErrorEventHandler(sender, e);
            //this.View.AddLogErrorCode(e.LogCate, e.LogType, e.LogContent, e.ErrorCode);
        }
        private void SaveLogEventHandler(object sender, LogEventArgs e)
        {
            LogModel log = new LogModel();
            log.logTime = DateTime.Parse(e.LogTime.ToString("yyyy-MM-dd HH:mm:ss"));
            if (e.LogContent.Length > 1000)//数据库默认1000长度
            {
                log.logContent = e.LogContent.Substring(0, 1000);
            }
            else
            {
                log.logContent = e.LogContent;
            }
            log.logType = e.LogType.ToString();
            log.logCategory = e.LogCate.ToString();

           // bllLog.DeleteHistoryLog(180);//只保留一个月的日志数据
            //bllLog.Add(log);

            bllLog.AsyncAddLog(log);
        }
        private void SaveErrorEventHandler(object sender, ECAMSErrorEventArgs e)
        {
            LogModel log = new LogModel();
            log.logTime = e.LogTime;
            if (e.LogContent.Length > 1000)//数据库默认1000长度
            {
                log.logContent = e.LogContent.Substring(0, 1000);
            }
            else
            {
                log.logContent = e.LogContent;
            }
            log.logType = e.LogType.ToString();
            log.logCategory = e.LogCate.ToString();
            log.warningCode = e.ErrorCode;
            //bllLog.DeleteHistoryLog(180);//只保留一个月的日志数据
            //bllLog.Add(log);
            bllLog.AsyncAddLog(log);
        }

        private void ChangeUserEventHandler(object sender, EventArgs e)
        {
            LoginPresenter loginPre = (LoginPresenter)this.View.GetPresenter(typeof(LoginPresenter));
            if (loginPre != null)
            {
                loginPre.View.ShowLoginForm();
            }
        }

        private void SetLimitEventHandler(object sender, SetLimitEventArgs e)
        {
            LoginPresenter loginPre = (LoginPresenter)this.View.GetPresenter(typeof(LoginPresenter));
            if (loginPre != null)
            {
                userNameStr = loginPre.View.UserName;
                ECAMWCS.userName = loginPre.View.UserName;
            }
            User_RoleModel roleModel = bllRole.GetModel(e.RoleID);
            if (roleModel != null)
            {
                this.View.SetCurrentRoleName(roleModel.RoleName);
            }
            List<int> limitList = bllUserLimit.GetFunctionIDList(e.RoleID);
            if (limitList != null)
            {
                string[] funcList = new string[limitList.Count];
                for (int i = 0; i < limitList.Count; i++)
                {
                    funcList[i] = bllFuncList.GetModel(limitList[i]).FunctionName;
                }
                this.View.SetLimit(funcList);
            }
            this.View.CloseAllChildForm();
        }

        /// <summary>
        /// 监控线程
        /// </summary>
        private void StartTaskMonitor()
        {
            
            while (!monitorExit)
            {
                Thread.Sleep(monitorTaskTime);
                if ( DateTime.Now.Second == 15)//每一个小时检测一次
                {
                    bllControlApply.CreateLimitTrigger(this.View.GetLicenceDate());//过期时间
                }
                if (monitorThreadRunning == false)
                {
                    continue;
                }
                // 扫描控制层申请的任务
                ControlApplyTask();
                //自动生成任务 
                SysAutoCreateTask();
                //实时监控控制任务 是否有完成
                AutoMonitorControlTask();
                //对设备撤销任务的处理
                HandleUndoControlTask();
            }

            // ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "任务监控线程退出！！");
        }


        /// <summary>
        /// 启动系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartSystemEventHandler(object sender, EventArgs e)
        {
            this.monitorThreadRunning = true;
            string logStr = "";
            if (monitorTaskThread.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
            {
                monitorTaskThread.Start();
                monitorExit = false;


                //if (!wcs.WCSStart(ref logStr))
                //{
                //    ShowLog(EnumLogCategory.控制层日志, EnumLogType.提示, logStr);
                //    ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "系统控制层启动失败！");
                //    return;
                //}
                //ShowLog(EnumLogCategory.控制层日志, EnumLogType.提示, logStr);
                //ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "系统启动！");
            }


            if (!wcs.WCSStart(ref logStr))
            {
                ShowLog(EnumLogCategory.控制层日志, EnumLogType.提示, logStr);
                ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "系统控制层启动失败！");

            }
            else
            {
                ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "系统启动！");
            }
        }

        /// <summary>
        /// 停止系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopSystemEventHandler(object sender, EventArgs e)
        {
            this.monitorThreadRunning = false;
            if (monitorTaskThread.ThreadState == (ThreadState.Background | ThreadState.Unstarted))
            {
                return;
            }


            string logStr = "";
            wcs.WCSStop(ref logStr);
            ShowLog(EnumLogCategory.控制层日志, EnumLogType.提示, logStr);

            ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "系统停止！！");
            this.View.SetStopEnabled(false);
            this.View.SetStartEnabled(true);

        }

        private void PlcCommCloseHandler(object sender, PlcCommOpEventArgs e)
        {
            string reStr = "";
            if (!wcs.WCSClosePLCComm(e.PlcID, ref reStr))
            {
                //View.AskMessBox(reStr);
                //reStr = ("堆垛机PLC " + reStr);
                ShowLog(EnumLogCategory.管理层日志, EnumLogType.错误, reStr);
            }
            else
            {
                //View.AskMessBox("PLC通信已关闭");
                ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, reStr);
            }
        }
        private void PlcCommReOpenHandler(object sender, PlcCommOpEventArgs e)
        {
            string reStr = "";
            if (!wcs.WCSReOpenPLCComm(e.PlcID, ref reStr))
            {
                reStr = ("堆垛机PLC " + reStr);
                ShowLog(EnumLogCategory.管理层日志, EnumLogType.错误, reStr);
            }
            else
            {
                ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, "堆垛机PLC通信已重新打开");
                
            }
        }
        #endregion

        #region 逻辑私有方法


        /// <summary>
        /// 控制层申请任务逻辑
        /// </summary>
        private void ControlApplyTask()
        {
            try
            {
                List<ControlInterfaceModel> taskInterList = bllControlApply.GetControlApplyList();
                if (taskInterList.Count > 0)//证明有任务申请
                {
                    for (int i = 0; i < taskInterList.Count; i++)
                    {
                        Thread.Sleep(0);
                        bool isTrayIDExist = IsAllTrayIDExist(taskInterList[i].InterfaceParameter);
                        if (isTrayIDExist == false)
                        {
                            bllControlApply.Delete(taskInterList[i].ControlInterfaceID);//不存在就删除申请任务接口
                            ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "设备编码：" + taskInterList[i].DeviceCode + " 读取的条码在数据库中不存在！无法生成任务！申请任务已删除！");
                            continue;
                        }
                        //首先生成管理任务，在任务类型表中查询控制层申请的任务类型
                        TaskTypeModel taskType = bllTaskType.GetTaskTypeByDevice(taskInterList[i].DeviceCode, taskInterList[i].InterfaceType);
                        if (taskType == null)
                        {
                            bllControlApply.Delete(taskInterList[i].ControlInterfaceID);//不存在就删除申请任务接口
                            ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "设备编码：" + taskInterList[i].DeviceCode + " 接口类型："
                                + taskInterList[i].InterfaceType + "在任务类型表中没有找到对应的任务类型！无法生成任务！申请任务已删除！");
                            continue;
                        }
                        Hashtable updateGoodsSiteStatusHs = null;
                        GoodsSiteModel goodsSiteModel = null;//以下几个业务流程的时候才需要分配货位

                        if (taskType.TaskTypeCode == (int)EnumTaskName.电芯入库_A1
                           || taskType.TaskTypeCode == (int)EnumTaskName.分容入库_A1
                           || taskType.TaskTypeCode == (int)EnumTaskName.电芯入库_B1)
                        {
                            //这四种类型需要生成货位
                            goodsSiteModel = GetGoodsSite(taskType);
                            if (goodsSiteModel == null) //如果生成货位不成功则不能生成管理任务
                            {
                                bllControlApply.Delete(taskInterList[i].ControlInterfaceID);//不存在就删除申请任务接口
                                ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "设备编号“" + taskType.StartDevice + "”申请失败，仓库中已经没有可用货位了！申请任务已被删除！");
                                continue;
                            }
                            ModifyTaskType(goodsSiteModel, taskType);//赋值起止设备

                            //获取更新货位状态Hs                    
                            updateGoodsSiteStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString()
                                , EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.入库.ToString(), goodsSiteModel.GoodsSiteID);

                        }
                        else if (taskType.TaskTypeCode == (int)EnumTaskName.空料框入库)
                        {
                            //这四种类型需要生成货位
                            goodsSiteModel = GetGoodsSite(taskType);
                            if (goodsSiteModel == null) //如果生成货位不成功则不能生成管理任务
                            {
                                bllControlApply.Delete(taskInterList[i].ControlInterfaceID);//不存在就删除申请任务接口
                                ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "设备编号“" + taskType.StartDevice + "”申请失败，仓库中已经没有可用货位了！申请任务已被删除！");
                                continue;
                            }
                            ModifyTaskType(goodsSiteModel, taskType);//赋值起止设备
                            updateGoodsSiteStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空料框.ToString()
                                      , EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.入库.ToString(), goodsSiteModel.GoodsSiteID);  //获取更新货位状态Hs       
                        }
                        else if (taskType.TaskTypeCode == (int)EnumTaskName.空料框出库) //B1库出库
                        {
                            //查询空料框
                            goodsSiteModel = GetGoodsSite(taskType);
                            if (goodsSiteModel == null)//没有指定货位出库
                            {
                                continue;
                            }
                            ModifyTaskType(goodsSiteModel, taskType);
                            //获取更新货位状态Hs                    
                            updateGoodsSiteStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空料框.ToString()
                               , EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), goodsSiteModel.GoodsSiteID);
                        }

                        long taskID = 0;
                        //生成管理任务
                        ManageTaskModel manageTask = CreateManageTask(taskInterList[i], taskType, ref taskID);
                        //生成管理任务列表
                        long taskListID = 0;
                        ManageTaskListModel ManageTaskList = CreateManageTaskList(manageTask, taskID, taskInterList[i], ref taskListID);

                        //获取生成控制任务hs
                        ControlTaskModel controlTask = CreateControlTask(taskInterList[i], taskID, taskType);
                        Hashtable addControlTaskHs = bllControlTask.GetAddControlTaskHash(controlTask);

                        //获取更新控制接口hs
                        ControlInterfaceModel updateControlInter = taskInterList[i];
                        updateControlInter.InterfaceStatus = EnumCtrlInterStatus.已生成.ToString();
                        Hashtable updateInterfaceHs = bllControlApply.GetUpdateHash(updateControlInter);

                        List<Hashtable> hashList = new List<Hashtable>();
                        hashList.Add(addControlTaskHs);

                        hashList.Add(updateInterfaceHs);
                        hashList.Add(updateGoodsSiteStatusHs);
                        bool executeTran = ExecuteManyHashSqls(hashList);
                        if (executeTran == true)
                        {
                            ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "控制层申请“" + taskType.TaskTypeName + "”任务成功！");
                            if (goodsSiteModel != null && taskType.TaskTypeCode != (int)EnumTaskName.空料框出库)// 空料框出库不需要生成库存
                            {

                                long stockListID = 0;
                                long stockID = CreateStockModel(goodsSiteModel, taskInterList[i]); //获取生成库存hs
                                StockListModel stockListModel = CreateStockListModel(taskType,
                                        taskInterList[i], goodsSiteModel, taskID, stockID, ref stockListID);
                                CreateStockDetail(stockListModel, taskInterList[i].InterfaceParameter);
                                ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, "申请货位“" + goodsSiteModel.GoodsSiteName + "“成功，托盘号：" + taskInterList[i].InterfaceParameter);

                            }
                        }
                        else //如果生成不成功的话，要删除管理任务
                        {
                            bllTask.Delete(taskID);
                            bllTaskList.Delete(taskListID);//数据库已经设为级联了
                            StopSystemEventHandler(null, null);//停止系统
                            ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "控制层申请“" + taskType.TaskTypeName + "”任务出错！系统停止！");
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StopSystemEventHandler(null, null);//停止系统
                ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.错误, "任务申请方法：ControlApplyTask" + ex.Message+"！系统停止！");
            }
        }

        /// <summary>
        /// 实时监控控制任务完成状态
        /// </summary>
        private void AutoMonitorControlTask()
        {
            try
            {
                
                List<ControlTaskModel> controlTaskList = new List<ControlTaskModel>();
                controlTaskList = bllControlTask.GetCompleteControlTask();
                //控制任务完成后，管理任务复制到任务历史表，删除管理任务、删除管理任务列表、删除控制任务、删除控制任务接口，
                //更新库存列表的更新时间更新管理任务和管理任务列表任务完成时间
                for (int i = 0; i < controlTaskList.Count; i++)
                {
                    Thread.Sleep(0);
                    #region 手动强制出库特殊处理   任务完成后直接删掉管理任务、控制任务、库存更新货位状态 其他表不操作
                    if (controlTaskList[i].CreateMode == EnumCreateMode.手动强制.ToString())
                    {
                        View_QueryStockListModel stockListViewModel = null;
                        if (controlTaskList[i].TaskTypeName == EnumTaskName.电芯出库_A1.ToString() || controlTaskList[i].TaskTypeName == EnumTaskName.分容出库_A1.ToString())
                        {
                            string[] rcls = controlTaskList[i].StartDevice.Split('-');
                            stockListViewModel = bllViewQueryStockList.GetModelByGsName(EnumStoreHouse.A1库房.ToString(), rcls[0], rcls[1], rcls[2]);
                        }
                        else if (controlTaskList[i].TaskTypeName == EnumTaskName.电芯出库_B1.ToString() || controlTaskList[i].TaskTypeName == EnumTaskName.空料框出库.ToString())
                        {
                            string[] rcls = controlTaskList[i].StartDevice.Split('-');
                            stockListViewModel = bllViewQueryStockList.GetModelByGsName(EnumStoreHouse.B1库房.ToString(), rcls[0], rcls[1], rcls[2]);
                        }

                        if (stockListViewModel != null)//说明有库存货位状态也需要修改
                        {
                            //更新货位
                            bllGoodsSite.UpdateModelByGsID(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), stockListViewModel.GoodsSiteID);
                            //删除库存
                            bllStock.DeleteModelByGsID(stockListViewModel.GoodsSiteID);//会级联删除库存列表和库存详细

                        }
                        bllTask.Delete(controlTaskList[i].TaskID);
                        bllControlTask.Delete(controlTaskList[i].ControlTaskID);

                        ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, "任务“" + controlTaskList[i].TaskTypeName + "”强制出库处理完成！");
                        continue;
                    }
                    #endregion
                    //复制到历史表
                    ManageTaskModel manaTaskModel = bllTask.GetModel(controlTaskList[i].TaskID);
                    ManageTaskListModel manaTaskListModel = bllTaskList.GetModelByManaTaskID(manaTaskModel.TaskID);
                    StockListModel stockModelList = null;
                    StockModel stockModel = null;

                    Hashtable manaTaskComTimeHs = bllTask.GetUpdateCompleteTimeHs(DateTime.Now, manaTaskModel.TaskID);
                    Hashtable manaTaskListComTimeHs = bllTaskList.GetUpdateCompleteTimeHs(DateTime.Now, manaTaskListModel.TaskListID);
                    Hashtable updateGsStatusHs = null;
                    Hashtable deleteStockHs = null;
                    string goodSiteName = "";
                    GoodsSiteModel goodsSite = null;
                  
                    if (controlTaskList[i].TaskTypeCode == (int)EnumTaskName.电芯入库_A1
                        || controlTaskList[i].TaskTypeCode == (int)EnumTaskName.分容入库_A1
                        || controlTaskList[i].TaskTypeCode == (int)EnumTaskName.电芯入库_B1)   //任务完成后需要更新货位状态
                    {
                        if (controlTaskList[i].TaskTypeCode == (int)EnumTaskName.电芯入库_A1 || controlTaskList[i].TaskTypeCode == (int)EnumTaskName.分容入库_A1)
                        {
                            goodSiteName = GetGoodSiteName(controlTaskList[i].TargetDevice);
                            stockModelList = bllStockList.GetStockListModel(EnumStoreHouse.A1库房, goodSiteName);
                            goodsSite = bllGoodsSite.GetGoodsSite(EnumStoreHouse.A1库房, controlTaskList[i].TargetDevice);
                        }
                        else if (controlTaskList[i].TaskTypeCode == (int)EnumTaskName.电芯入库_B1)
                        {
                            goodSiteName = GetGoodSiteName(controlTaskList[i].TargetDevice);
                            stockModelList = bllStockList.GetStockListModel(EnumStoreHouse.B1库房, goodSiteName);
                            goodsSite = bllGoodsSite.GetGoodsSite(EnumStoreHouse.B1库房, controlTaskList[i].TargetDevice);
                        }

                        if (goodsSite == null)
                        {
                            //如果打印日志一旦出错会一直打印这个消息
                            //this.View.AddLog(EnumLogCategory.管理层日志, EnumLogType.错误, "流程：" + controlTaskList[i].TaskTypeName + "获取货位为空！");
                            continue;
                        }
                        if (stockModelList == null)
                        {
                            //this.View.AddLog(EnumLogCategory.管理层日志, EnumLogType.错误, "流程：" + controlTaskList[i].TaskTypeName + "获取库存列表为空！");
                            continue;
                        }
                        stockModel = bllStock.GetModel(stockModelList.StockID);

                        updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), goodsSite.GoodsSiteID);
                    }
                    else if (controlTaskList[i].TaskTypeCode == (int)EnumTaskName.空料框入库)
                    {
                        goodSiteName = GetGoodSiteName(controlTaskList[i].TargetDevice);
                        stockModelList = bllStockList.GetStockListModel(EnumStoreHouse.B1库房, goodSiteName);
                        goodsSite = bllGoodsSite.GetGoodsSite(EnumStoreHouse.B1库房, controlTaskList[i].TargetDevice);
                        if (goodsSite == null)
                        { //如果打印日志一旦出错会一直打印这个消息
                            //this.View.AddLog(EnumLogCategory.管理层日志, EnumLogType.错误, "流程：" + controlTaskList[i].TaskTypeName + "获取货位为空！");
                            continue;
                        }
                        if (stockModelList == null)
                        {
                            //this.View.AddLog(EnumLogCategory.管理层日志, EnumLogType.错误, "流程：" + controlTaskList[i].TaskTypeName + "获取库存列表为空！");
                            continue;
                        }
                        stockModel = bllStock.GetModel(stockModelList.StockID);
                        if (stockModel == null)
                        {
                            continue;
                        }
                        updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空料框.ToString(), EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(),goodsSite.GoodsSiteID);  //获取更新货位状态Hs    
                    }
                    else if (controlTaskList[i].TaskTypeCode == (int)EnumTaskName.分容出库_A1
                        || controlTaskList[i].TaskTypeCode == (int)EnumTaskName.电芯出库_A1
                        || controlTaskList[i].TaskTypeCode == (int)EnumTaskName.电芯出库_B1
                        || controlTaskList[i].TaskTypeCode == (int)EnumTaskName.空料框出库) //B1库出库 //出口
                    {
                        goodSiteName = GetGoodSiteName(controlTaskList[i].StartDevice);
                        if (controlTaskList[i].TaskTypeCode == (int)EnumTaskName.分容出库_A1
                        || controlTaskList[i].TaskTypeCode == (int)EnumTaskName.电芯出库_A1)//A1库
                        {
                            goodsSite = bllGoodsSite.GetGoodsSite(EnumStoreHouse.A1库房, controlTaskList[i].StartDevice);
                            stockModelList = bllStockList.GetStockListModel(EnumStoreHouse.A1库房, goodSiteName);
                        }
                        else if (controlTaskList[i].TaskTypeCode == (int)EnumTaskName.电芯出库_B1
                        || controlTaskList[i].TaskTypeCode == (int)EnumTaskName.空料框出库)//B1库
                        {
                            goodsSite = bllGoodsSite.GetGoodsSite(EnumStoreHouse.B1库房, controlTaskList[i].StartDevice);
                            stockModelList = bllStockList.GetStockListModel(EnumStoreHouse.B1库房, goodSiteName);
                        }
                        if (goodsSite == null)
                        { 
                            //如果打印日志一旦出错会一直打印这个消息
                            //this.View.AddLog(EnumLogCategory.管理层日志, EnumLogType.错误, "流程：" + controlTaskList[i].TaskTypeName + "获取货位为空！");
                            continue;
                        }
                        if (stockModelList == null)
                        {
                            //this.View.AddLog(EnumLogCategory.管理层日志, EnumLogType.错误, "流程：" + controlTaskList[i].TaskTypeName + "获取库存列表为空！");
                            continue;
                        }
                        updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空货位.ToString()
                              , EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), goodsSite.GoodsSiteID);  //获取更新货位状态Hs 

                        deleteStockHs = bllStock.GetDeleteModelHash(stockModelList.StockID);
                    }
                  
                    Hashtable stockListComTimeHs = null;
                    if (stockModelList != null)//如果为空的话就不需要更新库存列表的时间了已经出库了
                    {
                        stockListComTimeHs = bllStockList.GetUpdateCompleteTimeHs(DateTime.Now, stockModelList.StockListID);
                    }
                    List<Hashtable> updateHashList = new List<Hashtable>();
                    updateHashList.Add(manaTaskComTimeHs);
                    updateHashList.Add(manaTaskListComTimeHs);
                    updateHashList.Add(stockListComTimeHs);
                    updateHashList.Add(updateGsStatusHs);
                    bool updateExecuteTran = ExecuteManyHashSqls(updateHashList);
                    if (updateExecuteTran == true)
                    {
                        Hashtable insertManaHistoryHs = GetInsertManaHistoryHash(stockModelList, manaTaskModel);
                        Hashtable deleteControlInterHs = bllControlApply.GetDeleteHash(controlTaskList[i].ControlCode);
                        Hashtable deleteControlTaskHs = bllControlTask.GetDeleteModelHash(controlTaskList[i].ControlTaskID);
                        Hashtable manaTaskHs = bllTask.GetDeleteModelHash(manaTaskModel.TaskID);
                        Hashtable manaTaskListHs = bllTaskList.GetDeleteModelHash(manaTaskModel.TaskID);
                        List<Hashtable> deleteHashList = new List<Hashtable>();
                        deleteHashList.Add(insertManaHistoryHs);  
                        deleteHashList.Add(deleteControlInterHs);
                        deleteHashList.Add(deleteControlTaskHs);
                        deleteHashList.Add(manaTaskHs);
                        deleteHashList.Add(manaTaskListHs);
                        deleteHashList.Add(deleteStockHs);
                        bool deleteExecuteTran = ExecuteManyHashSqls(deleteHashList);
                        if (deleteExecuteTran == true)
                        {
                            ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "“" + controlTaskList[i].TaskTypeName + "”任务已完成！");
                        }
                        else
                        {
                            ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "“" + controlTaskList[i].TaskTypeName + "”任务完成过程中失败！");
                        }

                    }
                    else
                    {
                        StopSystemEventHandler(null, null);//停止系统
                         
                        ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "“" + controlTaskList[i].TaskTypeName + "”任务完成更新时间失败！系统停止！");
                    }

                }
            }
            catch (Exception ex)
            {
                StopSystemEventHandler(null, null);//停止系统
                ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.错误, "监控任务完成方法：AutoMonitorControlTask" + ex.StackTrace+"！系统停止！");
            }
        }

        ////private bool GoodsSiteHandle(ControlTaskModel controlTaskModel,ref StockModel stockModel,ref StockListModel stockListModel,ref GoodsSiteModel goodsSite,ref Hashtable updateGsStatusHs)
        //{
        //    try
        //    {
  
        //        string goodSiteName = "1-1-1";
        //        switch (controlTaskModel.TaskTypeCode)
        //        {
        //            case (int)EnumTaskName.电芯入库_A1 | (int)EnumTaskName.分容入库_A1:
        //                goodSiteName = GetGoodSiteName(controlTaskModel.TargetDevice);
        //                stockListModel = bllStockList.GetStockListModel(EnumStoreHouse.A1库房, goodSiteName); ;
        //                goodsSite = bllGoodsSite.GetGoodsSite(EnumStoreHouse.A1库房, controlTaskModel.TargetDevice);
        //                if (goodsSite == null)
        //                {
        //                    ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "(GoodsSiteHandle)任务流程：" + controlTaskModel.TaskTypeName + "，货位为空!");
        //                    break;
        //                }
        //                updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), goodsSite.GoodsSiteID);
        //                break;
        //            case (int)EnumTaskName.电芯入库_B1:
        //                goodSiteName = GetGoodSiteName(controlTaskModel.TargetDevice);
        //                stockListModel = bllStockList.GetStockListModel(EnumStoreHouse.B1库房, goodSiteName);
        //                goodsSite = bllGoodsSite.GetGoodsSite(EnumStoreHouse.B1库房, controlTaskModel.TargetDevice);
        //                if (goodsSite == null)
        //                {
        //                    ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "(GoodsSiteHandle)任务流程：" + controlTaskModel.TaskTypeName + "，货位为空!");
        //                    break;
        //                }
        //                updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), goodsSite.GoodsSiteID);
        //                break;
        //            case (int)EnumTaskName.空料框入库:
        //                goodSiteName = GetGoodSiteName(controlTaskModel.TargetDevice);
        //                stockListModel = bllStockList.GetStockListModel(EnumStoreHouse.B1库房, goodSiteName);
        //                goodsSite = bllGoodsSite.GetGoodsSite(EnumStoreHouse.B1库房, controlTaskModel.TargetDevice);
        //                if (goodsSite == null)
        //                {
        //                    ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "(GoodsSiteHandle)任务流程：" + controlTaskModel.TaskTypeName + "，货位为空!");
        //                    break;
        //                }
        //                updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空料框.ToString(), EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), goodsSite.GoodsSiteID);  //获取更新货位状态Hs    
        //                break;

        //        }
                
        //        if (stockListModel != null)
        //        {
        //            stockModel = bllStock.GetModel(stockListModel.StockID);
        //        }
        //        else
        //        {
        //            stockModel = null;
        //        }
             
        //        return true;
               
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.错误, "货位处理函数：GoodsSiteHandle" + ex.StackTrace + "！");
        //        return false;
        //    }
        //}

        /// <summary>
        /// 系统自动生成任务(出库任务)
        /// </summary>
        private void SysAutoCreateTask()
        {
            try
            {
                Hashtable updateGsStatusHs = null;

                for (int i = 0; i < sysCreateTaskList.Count; i++)
                {
                    if (sysCreateTaskList[i].TaskTypeMode.Trim() == EnumTaskMode.自动.ToString())      //自动生成控制任务   
                    {
                        Thread.Sleep(0);
                        Dictionary<string, List<string>> outStorageBatchesDic = this.View.LoadOutStorageBatchNum();
                        if (outStorageBatchesDic == null || outStorageBatchesDic.Count == 0)
                        {
                            ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, "产品出库批次号没有设置，系统无法自动出库！");
                            return;
                        }
                        List<StockListModel> stockListModelList = bllStockList.GetTimeArriveModelList(sysCreateTaskList[i].ProductStartStatus,
                            (int)sysCreateTaskList[i].NeedTime, outStorageBatchesDic);//任务完成后才可以出库

                        if (stockListModelList != null)
                        {
                            for (int j = 0; j < stockListModelList.Count; j++)
                            {
                                Thread.Sleep(0);
                                StockModel stockModel = bllStock.GetModel(stockListModelList[j].StockID);
                                if (stockModel == null )
                                {
                                    continue;
                                }
                                GoodsSiteModel goodsSite = null;
                                if(stockListModelList[j].StoreHouseName == EnumStoreHouse.A1库房.ToString())
                                {
                                    goodsSite = bllGoodsSite.GetGoodsSiteByGSName(EnumStoreHouse.A1库房, stockListModelList[j].GoodsSiteName);
                                }
                                else if (stockListModelList[j].StoreHouseName == EnumStoreHouse.B1库房.ToString())
                                {
                                    goodsSite = bllGoodsSite.GetGoodsSiteByGSName(EnumStoreHouse.B1库房, stockListModelList[j].GoodsSiteName);
                                }

                                if (goodsSite == null)
                                {
                                    continue;
                                }
                                GoodsSiteModel goodsSiteModel = bllGoodsSite.GetModel(EnumGSStoreStatus.有货.ToString(),
                                     EnumGSRunStatus.任务完成.ToString(), goodsSite.GoodsSiteID);
                                if (goodsSiteModel == null)
                                {
                                    continue;
                                }

                                //获取更新货位状态Hs                    
                                updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString()
                                   , EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), goodsSite.GoodsSiteID);


                                long manaTaskID = 0;
                                ManageTaskModel manaTaskModel = AutoCreateManaTask(stockModel, stockListModelList[j], sysCreateTaskList[i], "", ref manaTaskID);
                                ManageTaskListModel manaTaskListModel = AutoCreateManaTaskList(manaTaskID, manaTaskModel, stockListModelList[j]);
                                AutoCreateControlTask(manaTaskModel, manaTaskID, sysCreateTaskList[i]);

                                List<Hashtable> hashList = new List<Hashtable>();
                                hashList.Add(updateGsStatusHs);

                                bool executeStatus = ExecuteManyHashSqls(hashList);

                                if (executeStatus == true)
                                {
                                    string trayIDList = bllStockDetail.GetTrayIDStrList(stockListModelList[j].StockListID);
                                   
                                    ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "系统自动生成产品批次号为" + stockListModelList[j].ProductBatchNum + "“"
                                        + sysCreateTaskList[i].TaskTypeName + "”库存："+stockListModelList[j].GoodsSiteName+ "出库任务成功！"+ "托盘号：" + trayIDList);
                                }
                                else
                                {
                                    ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "系统自动生成产品批次号为" + stockListModelList[j].ProductBatchNum + "“"
                                       + sysCreateTaskList[i].TaskTypeName + "”库存：" + stockListModelList[j].GoodsSiteName + "出库任务失败！");
                                }
                            }

                        }

                    }
                    else if (sysCreateTaskList[i].TaskTypeMode.Trim() == EnumTaskMode.手动.ToString()) //手动就不做处理了 在手动生成任务界面做处理
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                StopSystemEventHandler(null, null);//停止系统
                ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.错误, "系统自动出库函数出错：AutoMonitorControlTask" + ex.StackTrace+"！系统停止！");
            }
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月17日
        /// 内容:对设备撤销的任务进行处理
        /// </summary>
        private void HandleUndoControlTask()
        {
            try
            {
                List<ControlTaskModel> undoControlTaskList = bllControlTask.GetUndoControlTaskList();
                for (int i = 0; i < undoControlTaskList.Count; i++)
                {
                    View_QueryStockListModel stockListViewModel = null;
                    if (undoControlTaskList[i].TaskTypeName == EnumTaskName.电芯出库_A1.ToString() || undoControlTaskList[i].TaskTypeName == EnumTaskName.分容出库_A1.ToString())
                    {
                        string[] rcls = undoControlTaskList[i].StartDevice.Split('-');
                        stockListViewModel = bllViewQueryStockList.GetModelByGsName(EnumStoreHouse.A1库房.ToString(), rcls[0], rcls[1], rcls[2]);
                        if (stockListViewModel != null)//说明有库存货位状态也需要修改
                        {
                            //更新货位
                            bllGoodsSite.UpdateModelByGsID(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), stockListViewModel.GoodsSiteID);
                        }

                    }
                    else if (undoControlTaskList[i].TaskTypeName == EnumTaskName.电芯入库_A1.ToString() || undoControlTaskList[i].TaskTypeName == EnumTaskName.分容入库_A1.ToString())
                    {
                        string[] rcls = undoControlTaskList[i].TargetDevice.Split('-');
                        stockListViewModel = bllViewQueryStockList.GetModelByGsName(EnumStoreHouse.A1库房.ToString(), rcls[0], rcls[1], rcls[2]);
                        if (stockListViewModel != null)//说明有库存货位状态也需要修改
                        {
                            //更新货位
                            bllGoodsSite.UpdateModelByGsID(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), stockListViewModel.GoodsSiteID);
                            //删除库存
                            bllStock.DeleteModelByGsID(stockListViewModel.GoodsSiteID);//会级联删除库存列表和库存详细

                        }
                    }
                    else if (undoControlTaskList[i].TaskTypeName == EnumTaskName.电芯出库_B1.ToString() || undoControlTaskList[i].TaskTypeName == EnumTaskName.空料框出库.ToString())
                    {
                        string[] rcls = undoControlTaskList[i].StartDevice.Split('-');
                        stockListViewModel = bllViewQueryStockList.GetModelByGsName(EnumStoreHouse.B1库房.ToString(), rcls[0], rcls[1], rcls[2]);
                        if (stockListViewModel != null)//说明有库存货位状态也需要修改
                        {
                            if (undoControlTaskList[i].TaskTypeName == EnumTaskName.空料框出库.ToString())
                            {
                                //更新货位
                                bllGoodsSite.UpdateModelByGsID(EnumGSStoreStatus.空料框.ToString(), EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), stockListViewModel.GoodsSiteID);
                            }
                            else
                            {
                                //更新货位
                                bllGoodsSite.UpdateModelByGsID(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务完成.ToString(), EnumTaskCategory.入库.ToString(), stockListViewModel.GoodsSiteID);
                            }
                        }
                    }
                    else if (undoControlTaskList[i].TaskTypeName == EnumTaskName.电芯入库_B1.ToString() || undoControlTaskList[i].TaskTypeName == EnumTaskName.空料框入库.ToString())
                    {
                        string[] rcls = undoControlTaskList[i].TargetDevice.Split('-');
                        stockListViewModel = bllViewQueryStockList.GetModelByGsName(EnumStoreHouse.B1库房.ToString(), rcls[0], rcls[1], rcls[2]);
                        if (stockListViewModel != null)//说明有库存货位状态也需要修改
                        {
                            //更新货位
                            bllGoodsSite.UpdateModelByGsID(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), stockListViewModel.GoodsSiteID);
                            //删除库存
                            bllStock.DeleteModelByGsID(stockListViewModel.GoodsSiteID);//会级联删除库存列表和库存详细

                        }
                    }

                    bllTask.Delete(undoControlTaskList[i].TaskID);
                    bllControlTask.Delete(undoControlTaskList[i].ControlTaskID);
                    ShowLog(EnumLogCategory.管理层日志, EnumLogType.提示, "任务“" + undoControlTaskList[i].TaskTypeName + "”撤销处理完成！");

                }
            }
            catch (Exception ex)
            {
                StopSystemEventHandler(null, null);//停止系统
                ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.错误, "系统处理任务撤销函数：HandleUndoControlTask" + ex.StackTrace + "！系统停止！");
            }
        }

        /// <summary>
        /// 拼接为例：1排2列4层
        /// </summary>
        /// <param name="rclStr">例：1-2-4（格式）</param>
        /// <returns></returns>
        private string GetGoodSiteName(string rclStr)
        {
            string goodsiteName = "";
            string[] rclArr = rclStr.Split('-');
            if (rclArr.Length != 3)
            {
                return null;
            }
            goodsiteName = rclArr[0] + "排" + rclArr[1] + "列" + rclArr[2] + "层";
            return goodsiteName;
        }

        private ManageTaskModel AutoCreateManaTask(StockModel stockModel
            , StockListModel stockListModel, TaskTypeModel taskType, string taskParameter, ref long manaTaskID)
        {
            ManageTaskModel manaTaskModel = new ManageTaskModel();//生成管理任务
            manaTaskModel.TaskCode = stockListModel.ProductFrameCode;
            manaTaskModel.TaskCreatePerson = userNameStr;
            manaTaskModel.TaskCreateTime = DateTime.Now;
            LogicStoreAreaModel logicEndModel = bllLogicStoreArea.GetModel(taskType.EndLogicAreaID);
            manaTaskModel.TaskEndArea = logicEndModel.LogicStoreAreaName;
            manaTaskModel.TaskEndPostion = taskType.EndDevice;

            LogicStoreAreaModel logicStartModel = bllLogicStoreArea.GetModel(taskType.StartLogicAreaID);
            manaTaskModel.TaskStartArea = logicStartModel.LogicStoreAreaName;
            GoodsSiteModel gsModel = bllGoodsSite.GetModel(stockModel.GoodsSiteID);
            manaTaskModel.TaskStartPostion = gsModel.DeviceID;
            manaTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
            manaTaskModel.TaskType = EnumTaskCategory.出库.ToString();
            manaTaskModel.TaskTypeName = taskType.TaskTypeName;
            manaTaskModel.TaskParameter = taskParameter;
            manaTaskID = bllTask.Add(manaTaskModel);
            return manaTaskModel;
        }

        private ManageTaskListModel AutoCreateManaTaskList(long manaTaskID, ManageTaskModel manaTaskModel, StockListModel stockListModel)
        {

            ManageTaskListModel manaTaskListModel = new ManageTaskListModel();
            manaTaskListModel.ProductBatch = stockListModel.ProductBatchNum.ToString();
            manaTaskListModel.TaskParameter = manaTaskModel.TaskParameter;
            manaTaskListModel.ProductCode = stockListModel.ProductCode;
            manaTaskListModel.StockListID = stockListModel.StockListID;
            manaTaskListModel.TaskCreatePerson = userNameStr;
            manaTaskListModel.StockListID = stockListModel.StockListID;
            manaTaskListModel.TaskCreateTime = DateTime.Now;
            manaTaskListModel.TaskEndPosition = manaTaskModel.TaskEndPostion;
            manaTaskListModel.TaskID = manaTaskID;
            manaTaskListModel.TaskStartPosition = manaTaskModel.TaskStartPostion;
            long manaTaskListID = bllTaskList.Add(manaTaskListModel);
            return manaTaskListModel;
        }

        private void AutoCreateControlTask(ManageTaskModel manaTaskModel, long taskID, TaskTypeModel taskType)
        {
            ControlTaskModel controlTaskModel = new ControlTaskModel();//生成控制任务
            controlTaskModel.ControlCode = manaTaskModel.TaskCode;
            controlTaskModel.TaskParameter = manaTaskModel.TaskParameter;
            controlTaskModel.TaskTypeName = taskType.TaskTypeName;
            controlTaskModel.StartArea = manaTaskModel.TaskStartArea;
            controlTaskModel.StartDevice = manaTaskModel.TaskStartPostion;
            controlTaskModel.TargetArea = manaTaskModel.TaskEndArea;
            controlTaskModel.TargetDevice = manaTaskModel.TaskEndPostion;
            controlTaskModel.TaskID = taskID;
            controlTaskModel.CreateMode = EnumCreateMode.系统生成.ToString();
            controlTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
            controlTaskModel.TaskType = manaTaskModel.TaskType;
            controlTaskModel.TaskTypeCode = taskType.TaskTypeCode;
            controlTaskModel.CreateTime = DateTime.Now;
            bllControlTask.Add(controlTaskModel);
        }

        private Hashtable GetInsertManaHistoryHash(StockListModel stockModelList, ManageTaskModel manaTaskModel)
        {
            HistoryManageTaskModel historyModel = new HistoryManageTaskModel();
            //historyModel.ProductID = stockModelList.ProductID;
            historyModel.TaskCompleteTime = DateTime.Now;
            //historyModel.ProductName = stockModelList.ProductName;
            historyModel.TaskCreatePerson = manaTaskModel.TaskCreatePerson;
            historyModel.TaskCreateTime = manaTaskModel.TaskCreateTime;
            historyModel.TaskEndAera = manaTaskModel.TaskEndArea;
            historyModel.TaskEndPosition = manaTaskModel.TaskEndPostion;
            historyModel.TaskParameter = manaTaskModel.TaskParameter;
            historyModel.TaskStartArea = manaTaskModel.TaskStartArea;
            historyModel.TaskStartPsotion = manaTaskModel.TaskStartPostion;
            historyModel.TaskType = manaTaskModel.TaskType;
            historyModel.TaskTypeName = manaTaskModel.TaskTypeName;
            return bllHistoryTask.GetInsertModelHash(historyModel);
        }

        /// <summary>
        /// 生成管理任务
        /// </summary>
        /// <param name="controlInter">任务接口</param>
        /// <param name="taskType">任务类型</param>
        /// <param name="taskID">管理任务ID</param>
        /// <returns></returns>
        private ManageTaskModel CreateManageTask(ControlInterfaceModel controlInter,
            TaskTypeModel taskType, ref long taskID)
        {
            //生成管理任务
            ManageTaskModel taskModel = new ManageTaskModel();
            taskModel.TaskCode = controlInter.TaskCode.ToString();               //为料框编码
            taskModel.TaskTypeName = taskType.TaskTypeName;           //业务流程名称
            taskModel.TaskStartArea = bllLogicStoreArea.GetLogicNameByID(taskType.StartLogicAreaID);
            taskModel.TaskStartPostion = taskType.StartDevice;        //开始设备
            taskModel.TaskEndArea = bllLogicStoreArea.GetLogicNameByID(taskType.EndLogicAreaID);
            taskModel.TaskEndPostion = taskType.EndDevice;            //结束设备
            taskModel.TaskStatus = EnumTaskStatus.待执行.ToString();  //任务状态
            taskModel.TaskType = taskType.TaskTypeValue;              //任务类型出库、入库
            taskModel.TaskCreatePerson = userNameStr;
            taskModel.TaskCreateTime = DateTime.Now;
            taskModel.TaskParameter = controlInter.InterfaceParameter;
            taskID = bllTask.Add(taskModel);                          //获取刚插入的管理任务ID
            return taskModel;
        }

        /// <summary>
        /// 生成任务列表
        /// </summary>
        /// <param name="taskModel">任务模型</param>
        /// <param name="taskID">任务ID</param>
        /// <param name="taskListID">任务列表ID</param>
        /// <returns>返回管理任务列表模型</returns>
        private ManageTaskListModel CreateManageTaskList(ManageTaskModel taskModel, long taskID, ControlInterfaceModel controlInter, ref long taskListID)
        {
            //生成管理任务列表
            ManageTaskListModel taskListModel = new ManageTaskListModel();
            taskListModel.TaskID = taskID;
            taskListModel.TaskStartPosition = taskModel.TaskStartPostion;
            taskListModel.TaskEndPosition = taskModel.TaskEndPostion;
            taskListModel.ProductCode = "dx"; // 正常时要从控制任务接口中解析
            taskListModel.TaskCreatePerson = userNameStr;
            taskListModel.TaskCreateTime = DateTime.Now;
            taskListID = bllTaskList.Add(taskListModel);
            return taskListModel;
        }

        /// <summary>
        /// 生成控制任务
        /// </summary>
        /// <param name="controlInter">任务接口</param>
        /// <param name="taskID">管理任务ID</param>
        /// <param name="taskType">任务类型</param>
        /// <param name="controlTaskID">控制任务ID</param>
        /// <returns></returns>
        private ControlTaskModel CreateControlTask(ControlInterfaceModel controlInter, long taskID,
        TaskTypeModel taskType)
        {
            //生成控制任务
            ControlTaskModel controlTaskModel = new ControlTaskModel();
            controlTaskModel.TaskID = taskID;
            controlTaskModel.TaskTypeName = taskType.TaskTypeName;
            controlTaskModel.TaskTypeCode = taskType.TaskTypeCode;
            controlTaskModel.ControlCode = controlInter.TaskCode;//为料框编码
            controlTaskModel.CreateMode = EnumCreateMode.系统生成.ToString();
            controlTaskModel.TaskType = controlInter.InterfaceType;
            controlTaskModel.StartArea = bllLogicStoreArea.GetLogicNameByID(taskType.StartLogicAreaID);
            controlTaskModel.StartDevice = taskType.StartDevice;
            controlTaskModel.TargetArea = bllLogicStoreArea.GetLogicNameByID(taskType.EndLogicAreaID);
            controlTaskModel.TargetDevice = taskType.EndDevice;
            controlTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
            controlTaskModel.TaskParameter = controlInter.InterfaceParameter;
            controlTaskModel.CreateTime = DateTime.Now;
            return controlTaskModel;
        }

        /// <summary>
        /// 生成库存
        /// </summary>
        /// <param name="goodsSite">货位</param>
        /// <param name="controlInterface">控制接口</param>
        /// <param name="stockID">库存ID</param>
        /// <returns></returns>
        private long CreateStockModel(GoodsSiteModel goodsSite, ControlInterfaceModel controlInterface)
        {
            //生成库存
            StockModel stockModel = new StockModel();
            stockModel.FullTraySign = EnumFullTraySign.是.ToString();
            stockModel.GoodsSiteID = goodsSite.GoodsSiteID;
            stockModel.TrayCode = controlInterface.TaskCode.ToString();//为料框编码
            long stockID = bllStock.Add(stockModel);
            return stockID;
        }

        /// <summary>
        /// 生成库存列表
        /// </summary>
        /// <param name="taskType"></param>
        /// <param name="taskInterface"></param>
        /// <param name="stockID"></param>
        /// <param name="stockListID"></param>
        /// <returns></returns>
        private StockListModel CreateStockListModel(TaskTypeModel taskType, ControlInterfaceModel taskInterface
            , GoodsSiteModel gsModel, long manaTaskID, long stockID, ref long stockListID)
        {
            //生成库存列表

            StockListModel stockListModel = new StockListModel();
            stockListModel.ManaTaskID = manaTaskID;
            StoreAreaModel areaModel = bllStoreArea.GetModel(gsModel.StoreAreaID);
            StoreHouseModel houseModel = bllStoreHouse.GetModel(areaModel.StoreHouseID);
            stockListModel.StoreHouseName = houseModel.StoreHouseName;

            string procuctCode = "dx";
            switch (taskType.TaskTypeCode)   //根据任务类型判断库存列表的 物料状态
            {
                case (int)EnumTaskName.无:
                    stockListModel.ProductStatus = EnumProductStatus.无.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品码
                    stockListModel.ProductNum = 0;//默认为满箱

                    break;
                case (int)EnumTaskName.空料框入库:
                    stockListModel.ProductStatus = EnumProductStatus.空料框.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品码
                    stockListModel.ProductNum = 0;//默认为满箱
                    stockListModel.ProductName = "无";
                    break;
                case (int)EnumTaskName.空料框出库:
                    stockListModel.ProductStatus = EnumProductStatus.空料框.ToString();

                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 0;//默认为满箱
                    stockListModel.ProductName = "无";
                    break;
                case (int)EnumTaskName.电芯入库_A1:
                    stockListModel.ProductStatus = EnumProductStatus.A1库老化3天.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 48;//默认为满箱
                    ProductModel product1 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product1.ProductName;
                    break;
                case (int)EnumTaskName.分容出库_A1:
                    stockListModel.ProductStatus = EnumProductStatus.分容.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 48;//默认为满箱
                    ProductModel product2 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product2.ProductName;
                    break;
                case (int)EnumTaskName.分容入库_A1:
                    stockListModel.ProductStatus = EnumProductStatus.A1库静置10小时.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 48;//默认为满箱
                    ProductModel product3 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product3.ProductName;
                    break;
                case (int)EnumTaskName.电芯出库_A1:
                    stockListModel.ProductStatus = EnumProductStatus.一次拣选.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 48;//默认为满箱
                    ProductModel product4 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product4.ProductName;
                    break;
                case (int)EnumTaskName.电芯一次拣选:
                    stockListModel.ProductStatus = EnumProductStatus.一次拣选.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 48;//默认为满箱
                    ProductModel product5 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product5.ProductName;
                    break;
                case (int)EnumTaskName.电芯入库_B1:
                    stockListModel.ProductStatus = EnumProductStatus.B1库静置10天.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 48;//默认为满箱
                    ProductModel product6 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product6.ProductName;
                    break;
                case (int)EnumTaskName.电芯出库_B1:
                    stockListModel.ProductStatus = EnumProductStatus.二次拣选.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 48;//默认为满箱
                    ProductModel product7 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product7.ProductName;
                    break;
                case (int)EnumTaskName.电芯二次拣选:
                    stockListModel.ProductStatus = EnumProductStatus.二次拣选.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 0;//默认为满箱
                    ProductModel product8 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product8.ProductName;
                    break;
                case (int)EnumTaskName.电芯装箱组盘:
                    stockListModel.ProductStatus = EnumProductStatus.无.ToString();
                    stockListModel.ProductCode = procuctCode; // 产品号 默认为1
                    stockListModel.ProductNum = 48;//默认为满箱
                    ProductModel product9 = bllProduct.GetModelByCode(stockListModel.ProductCode);
                    stockListModel.ProductName = product9.ProductName;
                    break;

            }
            stockListModel.ProductFrameCode = taskInterface.TaskCode.ToString();//为料框编码
            string[] productBatchNums = SplitStringArray(taskInterface.InterfaceParameter);
            if (productBatchNums != null && productBatchNums.Length > 0)//解析产品批次号
            {
                TB_Tray_indexModel trayIndexModel = bllTrayIndex.GetModel(productBatchNums[0]);
                stockListModel.ProductBatchNum = trayIndexModel.Tf_BatchID;
            }
            stockListModel.GoodsSiteName = gsModel.GoodsSiteName;
            stockListModel.InHouseTime = DateTime.Now;
            stockListModel.StockID = stockID;
            stockListID = bllStockList.Add(stockListModel);
            stockListModel.StockListID = stockListID;
            return stockListModel;
        }

        /// <summary>
        ///  分配货位，并且更行货位任务完成状态、存储状态
        /// </summary>
        /// <param name="taskType">任务类型 </param>  
        /// <returns></returns>
        private GoodsSiteModel GetGoodsSite(TaskTypeModel taskType)
        {
            GoodsSiteModel goodsSiteModel = null;

            switch (taskType.TaskTypeCode)
            {
                case (int)EnumTaskName.无:

                    break;
                case (int)EnumTaskName.空料框入库:
                    goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.EndLogicAreaID, EnumGSStoreStatus.空货位, EnumGSRunStatus.待用, EnumGSType.货位);
                    break;
                case (int)EnumTaskName.空料框出库:

                    goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.StartLogicAreaID, EnumGSStoreStatus.空料框, EnumGSRunStatus.任务完成, EnumGSType.货位);
                    break;
                case (int)EnumTaskName.电芯入库_A1:
                    goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.EndLogicAreaID, EnumGSStoreStatus.空货位, EnumGSRunStatus.待用, EnumGSType.货位);
                    break;
                case (int)EnumTaskName.分容出库_A1:

                    goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.StartLogicAreaID, EnumGSStoreStatus.有货, EnumGSRunStatus.任务完成, EnumGSType.货位);
                    break;
                case (int)EnumTaskName.分容入库_A1:
                    goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.EndLogicAreaID, EnumGSStoreStatus.空货位, EnumGSRunStatus.待用, EnumGSType.货位);
                    break;
                case (int)EnumTaskName.电芯出库_A1:

                    goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.StartLogicAreaID, EnumGSStoreStatus.有货, EnumGSRunStatus.任务完成, EnumGSType.货位);
                    break;
                case (int)EnumTaskName.电芯一次拣选:

                    break;
                case (int)EnumTaskName.电芯入库_B1:
                    goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.EndLogicAreaID, EnumGSStoreStatus.空货位, EnumGSRunStatus.待用, EnumGSType.货位);
                    break;
                case (int)EnumTaskName.电芯出库_B1:

                    goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.StartLogicAreaID, EnumGSStoreStatus.有货, EnumGSRunStatus.任务完成, EnumGSType.货位);
                    break;
                case (int)EnumTaskName.电芯二次拣选:

                    break;
                case (int)EnumTaskName.电芯装箱组盘:

                    break;


            }
            return goodsSiteModel;
        }

        /// <summary>
        /// 如果是入库则任务的
        /// 结束设备为货位号
        /// 如果是出库任务的开始设备
        /// 为货位号
        /// </summary>
        /// <param name="goodSite">货位</param>
        /// <param name="taskType">任务类型</param>
        /// <returns></returns>
        private void ModifyTaskType(GoodsSiteModel goodSite, TaskTypeModel taskType)
        {
            if (taskType.TaskTypeValue == EnumTaskCategory.出库.ToString())
            {
                taskType.StartDevice = goodSite.DeviceID;
            }
            else
            {
                taskType.EndDevice = goodSite.DeviceID;
            }
        }

        /// <summary>
        /// 执行多个sql语句 事物
        /// </summary>
        /// <param name="hs1"></param>
        /// <param name="hs2"></param>
        /// <param name="hs3"></param>
        /// <param name="hs4"></param>
        /// <returns></returns>
        private bool ExecuteManyHashSqls(List<Hashtable> hashList)
        {
            Hashtable hash = new Hashtable();
            for (int i = 0; i < hashList.Count; i++)
            {
                if (hashList[i] == null)
                {
                    continue;
                }
                foreach (DictionaryEntry hs in hashList[i])
                {
                    hash.Add(hs.Key, hs.Value);
                }
            }
            bool executeTran = DbHelperSQL.ExecuteSqlTran(hash);
            return executeTran;
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月4日
        /// 内容:创建库存详细，一次将一个货位所有的料框都记录的详细表里
        /// </summary>
        /// <param name="stockListID"></param>
        /// <param name="trayIDsStr"></param>
        private void CreateStockDetail(StockListModel stockListModel, string trayIDsStr)
        {
            StockDetailModel stockDetailModel = new StockDetailModel();
            stockDetailModel.StockListID = stockListModel.StockListID;
            string[] trayIDs = SplitStringArray(trayIDsStr);
            if (trayIDs != null)
            {
                if (stockListModel.StoreHouseName == EnumStoreHouse.A1库房.ToString())
                {
                    for (int i = 0; i <Math.Min( trayIDs.Length,2); i++)
                    {
                        stockDetailModel.TrayID = trayIDs[i];
                        bllStockDetail.Add(stockDetailModel);
                    }
                }
                else if (stockListModel.StoreHouseName == EnumStoreHouse.B1库房.ToString())
                {
                    for (int i = 0; i < Math.Min(trayIDs.Length, 6); i++)
                    {
                        stockDetailModel.TrayID = trayIDs[i];
                        bllStockDetail.Add(stockDetailModel);
                    }
                }
               
            }
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月10日
        /// 内容:判断传来的码数据库中是否存在
        /// </summary>
        /// <param name="applyParameter"></param>
        /// <returns></returns>
        private bool IsAllTrayIDExist(string applyParameter)
        {
            bool isExist = true;
            string[] trayIDs = SplitStringArray(applyParameter);
            if (trayIDs != null)
            {
                for (int i = 0; i < trayIDs.Length; i++)
                {
                    TB_Tray_indexModel trayModel = bllTrayIndex.GetModel(trayIDs[i]);
                    if (trayModel == null)
                    {
                        isExist = false;
                        break;
                    }
                }
            }
            return isExist;
        }

        private string[] SplitStringArray(string srcStrs)
        {
            string[] splitStrArr = new string[2] { ",", "，" };
            if (srcStrs == null || srcStrs == string.Empty || splitStrArr == null || splitStrArr.Count() == 0)
            {
                return null;
            }
            return srcStrs.Split(splitStrArr, StringSplitOptions.RemoveEmptyEntries);
        }

     

        #endregion

        #region 公共方法
        /// <summary>
        /// 作者:np
        /// 时间:2014年7月8日
        /// 内容:显示日志
        /// </summary>
        /// <param name="e"></param>
        public void ShowLog(EnumLogCategory category,EnumLogType logType,string logContent)
        {
            LogEventArgs logArgs = new LogEventArgs();
            logArgs.LogCate = category;
            logArgs.LogType = logType;
            logArgs.LogContent = logContent;
            logArgs.LogTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            SaveLogEventHandler(null, logArgs);
            this.View.AddLog(category, logType, logContent);
        }

        public void ShowErrorLog(EnumLogCategory category,EnumLogType logType,string logContent,int errorCode)
        {
            ECAMSErrorEventArgs errorLogArgs = new ECAMSErrorEventArgs();
            errorLogArgs.LogCate = category;
            errorLogArgs.LogType = logType;
            errorLogArgs.LogContent = logContent;
            errorLogArgs.ErrorCode = errorCode;
            errorLogArgs.LogTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            SaveErrorEventHandler(null, errorLogArgs);
            this.View.AddLogErrorCode(category, logType, logContent, errorCode);
        }
        #endregion
    }
}
