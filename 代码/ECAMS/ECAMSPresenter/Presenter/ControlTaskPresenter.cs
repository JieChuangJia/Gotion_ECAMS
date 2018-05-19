using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSDataAccess;
using System.Threading;
using ECAMSModel;
using System.Collections;

namespace ECAMSPresenter
{
    public class ControlTaskPresenter : BasePresenter<IControlTaskView>
    {
        #region 全局变量
        private readonly ControlTaskBll bllControlTask = new ControlTaskBll();
        private readonly ManageTaskBll bllManaTask = new ManageTaskBll();
        private readonly ControlInterfaceBll bllControlInterface = new ControlInterfaceBll();
        private readonly GoodsSiteBll bllGoodsSite = new GoodsSiteBll();
        private readonly StockBll bllStock = new StockBll();
        private readonly StockListBll bllStockList = new StockListBll();
        private readonly ControlInterfaceBll bllControlInter = new ControlInterfaceBll();
        private readonly TaskTypeBll bllTaskType = new TaskTypeBll();
        private readonly TB_Tray_indexBll bllTbTrayIndex = new TB_Tray_indexBll();//
        private string currentEnumTaskName = "所有";
        private string currentEnumTaskStatus = "所有";
        private string currentHouseName = "所有";
        private string currentCreateMode = "所有";
        private string currentTaskType = "所有";
        MainPresenter mainPres = null;
        LoginPresenter loginPre = null;
        string userNameStr = "";
        Thread autoRefreshThread = null;                                                    //自动刷新线程
        int autoInterval = 2000;                                                             //自动刷新间隔     
        bool shouldAutoStop = false;
        #endregion

        #region 初始化
        public ControlTaskPresenter(IControlTaskView view)
            : base(view)
        {
            autoRefreshThread = new Thread(new ThreadStart(AutoRereshData));
            autoRefreshThread.IsBackground = true;
            mainPres = (MainPresenter)this.View.GetPresenter(typeof(MainPresenter));

            loginPre = (LoginPresenter)this.View.GetPresenter(typeof(LoginPresenter));
            if (loginPre != null)
            {
                userNameStr = loginPre.View.UserName;
            }
        }

        protected override void OnViewSet()
        {
            this.View.eventAutoRefresh += AutoRefreshEventHandler;
            this.View.eventLoadData += LoadDataEventHandler;
            this.View.eventExit += ExitEventHandler;
            this.View.eventCancelTask += CancelTaskEventHandler;
            this.View.eventCompleteByHand += CompleteByHandHandler;
           
            this.View.eventInStoreByHand += InStoreByHandEventHandler;
            this.View.eventQueryCtrlTask += QueryCtrlTaskEventHandler;
        }
        #endregion

        #region 实现IManageTaskView事件函数
        private void QueryCtrlTaskEventHandler(object sender, QueryCtrlTaskEventArgs e)
        {
            this.currentEnumTaskName = e.CtrlTaskName;
            this.currentEnumTaskStatus = e.CtrlTaskStatus;
            this.currentHouseName = e.StoreHouseName;
            this.currentCreateMode = e.TaskCreateMode;
            this.currentTaskType = e.TaskType;
            DataTable dtCtrlTaskList = bllControlTask.GetCtrlDatatable(e.CtrlTaskName, e.CtrlTaskStatus,e.StoreHouseName,e.TaskCreateMode,e.TaskType);
            this.View.ShowControlTaskData(dtCtrlTaskList);
        }

        private void InStoreByHandEventHandler(object sender, InStoreByHandEventArgs e)
        {
            int taskTypeCode = (int)(EnumTaskName)Enum.Parse(typeof(EnumTaskName), e.TaskTypeName);
            TaskTypeModel taskType = bllTaskType.GetModel(taskTypeCode);
            GoodsSiteModel goodsSiteModel = null;
            string[] trayIDs = SplitStringArray(e.TrayIDs);

            if (trayIDs!= null)
            {
                for (int i = 0; i < trayIDs.Length; i++)
                {
                    TB_Tray_indexModel trayModel = bllTbTrayIndex.GetModel(trayIDs[i]);
                    if (trayModel == null)
                    {
                        this.View.ShowMessage("信息提示", "条码：“" + trayIDs[i] + "”不存在此条码信息，请重新输入");

                        return;
                    }
                }
            }
            int createStatus = this.View.AskMessBox("您确定要手动生成“" + e.TaskTypeName + "”任务么？");
            if (createStatus == 0)
            {
                if (taskType != null)//控制任务接口插入一个任务申请
                {
                    if (taskType.TaskTypeCode == (int)EnumTaskName.空料框出库)
                    {
                        //查询空料框
                        goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.StartLogicAreaID, EnumGSStoreStatus.空料框, EnumGSRunStatus.任务完成, EnumGSType.货位);
                        if (goodsSiteModel == null)
                        {
                            if (mainPres != null)
                            {
                                mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "库中没有空料框了，手动生成“"
                                    + e.TaskTypeName + "”任务失败！" );
                            }
                            return;
                        }
                        else
                        {
                            ControlInterfaceModel controlInterModel = new ControlInterfaceModel();
                            controlInterModel.CreateTime = DateTime.Now;
                            controlInterModel.DeviceCode = taskType.EndDevice;
                            controlInterModel.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                            controlInterModel.InterfaceType = EnumTaskCategory.出库.ToString();
                            //生成控制条码 需要跟控制层
                            controlInterModel.TaskCode = bllControlInter.GetNewTaskCode();
                            controlInterModel.InterfaceParameter = e.TrayIDs;
                            long controlInterID = bllControlInter.Add(controlInterModel);
                            if (controlInterID != 1)
                            {
                                if (mainPres != null)
                                {
                                    mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示,"用户："+this.userNameStr+ ",手动生成“" 
                                        + e.TaskTypeName + "”任务成功！");
                                    //mainPres.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "手动生成“" + e.TaskTypeName + "”任务成功！");
                                }
                            }
                        }
                    }
                    else if (taskType.TaskTypeCode == (int)EnumTaskName.电芯入库_A1 || taskType.TaskTypeCode == (int)EnumTaskName.电芯入库_B1
                       || taskType.TaskTypeCode == (int)EnumTaskName.分容入库_A1||taskType.TaskTypeCode == (int)EnumTaskName.空料框入库)
                    {
                        goodsSiteModel = bllGoodsSite.GetGoodsSite(taskType.EndLogicAreaID, EnumGSStoreStatus.空货位, EnumGSRunStatus.待用, EnumGSType.货位);
                        if (goodsSiteModel == null)
                        {
                            if (mainPres != null)
                            {
                                mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" + this.userNameStr + ",库中没有空料框了，手动生成“" + e.TaskTypeName + "”任务失败！");
                            }
                            return;
                        }
                        else
                        {
                            ControlInterfaceModel controlInterModel = new ControlInterfaceModel();
                            controlInterModel.CreateTime = DateTime.Now;
                            controlInterModel.DeviceCode = taskType.StartDevice;
                            controlInterModel.InterfaceStatus = EnumCtrlInterStatus.未生成.ToString();
                            controlInterModel.InterfaceType = EnumTaskCategory.入库.ToString();
                            controlInterModel.InterfaceParameter = e.TrayIDs;
                            controlInterModel.TaskCode = bllControlInter.GetNewTaskCode();
                            long controlInterID = bllControlInter.Add(controlInterModel);
                            if (controlInterID != 1)
                            {
                                if (mainPres != null)
                                {
                                    mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" + this.userNameStr 
                                        + ",手动生成“" + e.TaskTypeName + "”任务成功！" );
                                }
                            }
                        }
                    }
                }
                DataTable dtCtrlTaskList = bllControlTask.GetCtrlDatatable(this.currentEnumTaskName, this.currentEnumTaskStatus
                   , this.currentHouseName, this.currentCreateMode, this.currentTaskType);
                this.View.ShowControlTaskData(dtCtrlTaskList);
            }
        }

        private void CompleteByHandHandler(object sender, ControlTaskEventArgs e)
        {
            int taskStatus = this.View.AskMessBox("您确定要手动完成选定任务么？");
            Hashtable updateGsStatusHs = null;
            if (taskStatus == 0)
            {
                for (int i = 0; i < e.ControlTaskIDArr.Count(); i++)
                {
                    ControlTaskModel controlTask = bllControlTask.GetModel(e.ControlTaskIDArr[i]);
                    if (controlTask != null)
                    {
                        //bool gsIsValid = false;
                        //if (controlTask.TaskTypeName == EnumTaskName.电芯入库_A1.ToString()
                        //    || controlTask.TaskTypeName == EnumTaskName.分容入库_A1.ToString()
                        //    || controlTask.TaskTypeName == EnumTaskName.电芯入库_B1.ToString())//A1库房
                        //{
                        //    GoodsSiteModel gsModel = bllGoodsSite.GetGoodsSite(2, EnumGSStoreStatus.空货位, EnumGSRunStatus.待用, EnumGSType.货位);
                        //    if (gsModel != null)
                        //    {
                        //        gsIsValid = true;
                        //    }
                        //}
                        //else if (   controlTask.TaskTypeName == EnumTaskName.空料框入库.ToString())//b1库房
                        //{
                        //    GoodsSiteModel gsModel = bllGoodsSite.GetGoodsSite(2, EnumGSStoreStatus.空货位, EnumGSRunStatus.待用, EnumGSType.货位);
                        //    if (gsModel != null)
                        //    {
                        //        gsIsValid = true;
                        //    }
                        //}

                        //else if (controlTask.TaskTypeName == EnumTaskName.分容出库_A1.ToString()
                        //    ||controlTask.TaskTypeName == EnumTaskName.电芯出库_B1.ToString()
                        //    || controlTask.TaskTypeName == EnumTaskName.电芯出库_A1.ToString())
                        //{
                        //    GoodsSiteModel gsModel = bllGoodsSite.GetGoodsSite(2, EnumGSStoreStatus.有货, EnumGSRunStatus.任务完成, EnumGSType.货位);
                        //    if (gsModel != null)
                        //    {
                        //        gsIsValid = true;
                        //    }
                        //}
                        //else if (controlTask.TaskTypeName == EnumTaskName.空料框出库.ToString())
                        //{
                        //    GoodsSiteModel gsModel = bllGoodsSite.GetGoodsSite(2, EnumGSStoreStatus.空料框, EnumGSRunStatus.任务完成, EnumGSType.货位);
                        //    if (gsModel != null)
                        //    {
                        //        gsIsValid = true;
                        //    }
                        //}
                        controlTask.TaskStatus = EnumTaskStatus.已完成.ToString();

                        //if (gsIsValid == true)
                        //{
                        bool completeTask = bllControlTask.Update(controlTask);
                        if (completeTask == true)
                        {
                            if (mainPres != null)
                            {
                                mainPres.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" +this.userNameStr
                                    +",任务开始位置："+controlTask.StartDevice + "任务结束位置："+controlTask.TargetDevice +"任务名称：“" + controlTask.TaskTypeName + "”任务手动完成成功！");
                            }
                        }
                        else
                        {
                            if (mainPres != null)
                            {
                                mainPres.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" + this.userNameStr
                                    + ",任务开始位置：" + controlTask.StartDevice + "任务结束位置：" + controlTask.TargetDevice + "任务名称：“" + controlTask.TaskTypeName + "”手动任务失败！");
                            }
                        }
                        //}
                        //else
                        //{
                        //    if (mainPres != null)
                        //    {
                        //        mainPres.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "<<" + controlTask.TaskTypeName + ">>手动完成任务条件不符，手动任务失败！");
                        //    }
                        //}
                    }
                }

            }

        }

        /// <summary>
        /// 手动取消任务
        /// 删除控制任务、管理任务  
        /// 删除控制任务接口
        /// 更新货位状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelTaskEventHandler(object sender, ControlTaskEventArgs e)
        {
            int taskStatus = this.View.AskMessBox("您确定要手动删除选中任务么？");
            try
            {
                if (taskStatus == 0)
                {
                    if (e.ControlTaskIDArr.Count() > 0)//只有任务没有完成的时候才可以取消
                    {

                        for (int i = 0; i < e.ControlTaskIDArr.Count(); i++)
                        {
                            Hashtable deleteManaHs = null;
                            Hashtable deleteControlInterHs = null;
                            Hashtable deleteStockHs = null;
                            Hashtable updateGsHs = null;

                            ControlTaskModel controlTaskModel = bllControlTask.GetModel(e.ControlTaskIDArr[i]);
                            if (controlTaskModel == null)
                            {
                                continue;
                            }
                            if (controlTaskModel.TaskStatus == EnumTaskStatus.执行中.ToString())
                            {
                                this.View.ShowMessage("信息提示", "任务在执行中不允许删除！！");
                                continue;
                            }
                            if (controlTaskModel.TaskStatus == EnumTaskStatus.错误.ToString() || controlTaskModel.TaskStatus == EnumTaskStatus.超时.ToString())
                            {
                                //要调用控制层的接口，处理控制层；
                                MainPresenter.wcs.ClearTask(controlTaskModel);
                            }
                            ManageTaskModel manaTaskModel = bllManaTask.GetModel(controlTaskModel.TaskID);
                            if (manaTaskModel == null)
                            {
                                continue;
                            }

                            StockListModel stockListModel = null;
                            StockModel stockModel = null;
                            GoodsSiteModel gsModel = null;
                            if (controlTaskModel.TaskTypeCode == (int)EnumTaskName.电芯入库_A1
                              || controlTaskModel.TaskTypeCode == (int)EnumTaskName.分容入库_A1
                              || controlTaskModel.TaskTypeCode == (int)EnumTaskName.电芯入库_B1
                              || controlTaskModel.TaskTypeCode == (int)EnumTaskName.空料框入库)   //任务完成后需要更新货位状态
                            {
                                stockListModel = bllStockList.GetModelByManaTaskID(controlTaskModel.TaskID);
                                stockModel = bllStock.GetModel(stockListModel.StockID);
                                if (stockListModel != null && stockModel != null)
                                {
                                    deleteStockHs = bllStock.GetDeleteModelHash(stockModel.StockID);//入库任务取消要删除库存
                                    updateGsHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString()
                                     , EnumTaskCategory.出入库.ToString(), stockModel.GoodsSiteID);
                                }
                            }
                            else if (controlTaskModel.TaskTypeCode == (int)EnumTaskName.分容出库_A1
                            || controlTaskModel.TaskTypeCode == (int)EnumTaskName.电芯出库_A1) //出库删除任务不删除库存
                            {
                                gsModel = bllGoodsSite.GetGoodsSite(EnumStoreHouse.A1库房, controlTaskModel.StartDevice);
                                stockModel = bllStock.GetModelByGsID(gsModel.GoodsSiteID);
                                if (stockModel != null)
                                {
                                    updateGsHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务完成.ToString()
                                       , EnumTaskCategory.入库.ToString(), stockModel.GoodsSiteID);
                                }
                            }
                            else if (controlTaskModel.TaskTypeCode == (int)EnumTaskName.电芯出库_B1)
                            {
                                gsModel = bllGoodsSite.GetGoodsSite(EnumStoreHouse.B1库房, controlTaskModel.StartDevice);
                                stockModel = bllStock.GetModelByGsID(gsModel.GoodsSiteID);
                                if (stockModel != null)
                                {
                                    updateGsHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务完成.ToString()
                                       , EnumTaskCategory.入库.ToString(), stockModel.GoodsSiteID);
                                }

                            }
                            else if (controlTaskModel.TaskTypeCode == (int)EnumTaskName.空料框出库)
                            {
                                gsModel = bllGoodsSite.GetGoodsSite(EnumStoreHouse.B1库房, controlTaskModel.StartDevice);
                                stockModel = bllStock.GetModelByGsID(gsModel.GoodsSiteID);
                                if (stockModel != null)
                                {
                                    updateGsHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空料框.ToString(), EnumGSRunStatus.任务完成.ToString()
                                         , EnumTaskCategory.入库.ToString(), stockModel.GoodsSiteID);
                                }
                            }

                            deleteManaHs = bllManaTask.GetDeleteModelHash(manaTaskModel.TaskID);
                            deleteControlInterHs = bllControlInterface.GetDeleteHash(controlTaskModel.ControlCode);

                            List<Hashtable> hashList = new List<Hashtable>();
                            hashList.Add(deleteManaHs);
                            hashList.Add(deleteControlInterHs);
                            hashList.Add(deleteStockHs);
                            hashList.Add(updateGsHs);
                            bool executeTran = ExecuteManyHashSqls(hashList);
                            if (executeTran == true)
                            {
                                if (mainPres != null)
                                {
                                    mainPres.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" + this.userNameStr + ",ID:" + controlTaskModel.ControlTaskID
                                        + "任务开始位置：" + controlTaskModel.StartDevice + "任务结束位置：" + controlTaskModel.TargetDevice
                                        + ",“" + manaTaskModel.TaskTypeName + "”任务手动删除成功！");
                                }
                            }
                            else
                            {
                                if (mainPres != null)
                                {
                                    mainPres.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" + this.userNameStr + ",ID:" + controlTaskModel.ControlTaskID
                                        + "任务开始位置：" + controlTaskModel.StartDevice + "任务结束位置：" + controlTaskModel.TargetDevice
                                        + ",“" + manaTaskModel.TaskTypeName + "”任务手动删除失败！");
                                }
                            }
                        }
                    }


                    DataTable dtCtrlTaskList = bllControlTask.GetCtrlDatatable(this.currentEnumTaskName, this.currentEnumTaskStatus
                        , this.currentHouseName, this.currentCreateMode, this.currentTaskType);
                    this.View.ShowControlTaskData(dtCtrlTaskList);
                }
            }
            catch (Exception ex)
            {
                mainPres.View.AddLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.错误,ex.StackTrace);
            }
        }

        private void AutoRefreshEventHandler(object sender, AutoRefreshEventArgs e)
        {
            if (e.isAutoRefresh == true)
            {
                if (autoRefreshThread.ThreadState == (ThreadState.Unstarted | ThreadState.Background))
                {
                    autoRefreshThread.Start();
                }
                else
                {
                    shouldAutoStop = false; ;
                    autoRefreshThread.Resume();
                }
            }
            else
            {
                shouldAutoStop = true;
                autoRefreshThread.Suspend();
            }
        }

        private void ExitEventHandler(object sender, EventArgs e)
        {
            try
            {
                shouldAutoStop = true;

                autoRefreshThread.Abort();
            }
            catch
            { }
        }

        private void LoadDataEventHandler(object sender, EventArgs e)
        {

            DataSet dsData = bllControlTask.GetControlList();
            if (dsData != null && dsData.Tables.Count > 0)
            {
                this.View.ShowControlTaskData(dsData.Tables[0]);
            }
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

        #region 私有方法

        private void AutoRereshData()
        {
            while (!shouldAutoStop)
            {
                Thread.Sleep(autoInterval);
                //List<ControlTaskModel> manageTaskList = bllControlTask.GetModelList("");
                //this.View.ShowControlTaskData(manageTaskList);

                DataTable dtData = bllControlTask.GetCtrlDatatable(currentEnumTaskName, currentEnumTaskStatus,currentHouseName,currentCreateMode,currentTaskType);
                this.View.ShowControlTaskData(dtData);
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
        #endregion
    }
}
