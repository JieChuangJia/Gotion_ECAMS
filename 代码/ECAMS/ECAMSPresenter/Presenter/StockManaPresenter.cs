using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSDataAccess;
using ECAMSModel;
using System.Collections;

namespace ECAMSPresenter
{
    public class StockManaPresenter : BasePresenter<IStockManaView>
    {
        #region 全局变量
        private readonly StockListBll bllStockList = new StockListBll();
        private readonly StockBll bllStock = new StockBll();
        private readonly GoodsSiteBll bllGoodsSite = new GoodsSiteBll();
        private readonly ProductBll bllProduct = new ProductBll();
        private readonly LogicStoreAreaBll bllLogicStoreArea = new LogicStoreAreaBll();
        private readonly ControlTaskBll bllControlTask = new ControlTaskBll();                //控制任务
        private readonly ManageTaskBll bllTask = new ManageTaskBll();                         //管理任务
        private readonly ManageTaskListBll bllTaskList = new ManageTaskListBll();             //管理任务列表
        private readonly TaskTypeBll bllTaskType = new TaskTypeBll();
        private readonly View_QueryStockListBll bllQueryStockList = new View_QueryStockListBll();
        private readonly StockDetailBll bllStockDetail = new StockDetailBll();
        private readonly View_StockListDetailBll bllView_StockDetail = new View_StockListDetailBll();//货位详细视图表
        private readonly TB_Tray_indexBll bllTrayDetail = new TB_Tray_indexBll();             //客户库存详细表
        private readonly TB_After_GradeDataBll bllAfterGradeData = new TB_After_GradeDataBll();//料框详细
        private MainPresenter mainPres = null;
        private string queryHouseName = "";
        private string queryProductStasus = "";
        private string queryRowth = "";
        private string queryColumnth = "";
        private string queryLayerth = "";
        private string queryGsStoreStatus = "";
        private string queryGsRunStatus = "";
        private string queryGsTaskType = "";
        private string queryProductBatchNum = "";
        private EnumWorkFolwStatus queryWorkFlowStatus = EnumWorkFolwStatus.所有;
        LoginPresenter loginPre = null;
        string userNameStr = "";
        #endregion

        #region 初始化
        public StockManaPresenter(IStockManaView view)
            : base(view)
        {
            mainPres = (MainPresenter)this.View.GetPresenter(typeof(MainPresenter));
            loginPre = (LoginPresenter)this.View.GetPresenter(typeof(LoginPresenter));
            if (loginPre != null)
            {
                userNameStr = loginPre.View.UserName;
            }
        }

        protected override void OnViewSet()
        {
            this.View.eventQueryStock += QueryStockEventHandler;
            this.View.eventOutHouseByHand += OutHouseEventHandler;
            this.View.eventDeleteStock += DeleteStockEventHandler;
            this.View.eventLoad += LoadEventHandler;
            this.View.eventSetGsStatus += SetGsStatusEventHandler;
            this.View.eventStockDetail += StockDetailEventHandler;
            this.View.eventTrayDetail += TrayDetailEventHandler;
        }
        #endregion

        #region 实现IManageTaskView事件函数
        private void TrayDetailEventHandler(object sender, TrayEventArgs e)
        { 
            List<TB_After_GradeDataModel> coreModelList = bllAfterGradeData.GetListByTrayCode(e.TrayID);
            this.View.ShowTrayDetail(coreModelList);
        }

        private void StockDetailEventHandler(object sender, QueryGoodsSiteEventArgs e)
        {
            List<View_StockListDetailModel> stockDetailModelList = bllView_StockDetail.GetStockDetailByGsID(e.GoodsSiteID);
            List<TB_Tray_indexModel> trayDetailModelList = new List<TB_Tray_indexModel>();
            if (stockDetailModelList != null)
            {
                for (int i = 0; i < stockDetailModelList.Count; i++)
                {
                    TB_Tray_indexModel trayDetailModel = bllTrayDetail.GetModel(stockDetailModelList[i].TrayID);//查询料框电芯详细                 
                    trayDetailModelList.Add(trayDetailModel);
                }
            }
            this.View.ShowStockDetail(stockDetailModelList, trayDetailModelList);
        }


        private void SetGsStatusEventHandler(object sender, StockStatusEventArgs e)
        {
            int gsStatus = this.View.AskMessBox("您确定要修改选中货位状态么？");
            View_QueryStockListModel viewStockListModel = bllQueryStockList.GetModelByGSID(e.GoodsSiteID);
            if (viewStockListModel == null)
            {
                this.View.ShowMessage("信息提示", "当前货位没有库存信息，不能设置货位状态！");
            }
            else
            {
                if (gsStatus == 0)
                {
                    if (e.GsTaskType == EnumTaskCategory.出库 && (e.GsStoreStatus == EnumGSStoreStatus.有货 || e.GsStoreStatus == EnumGSStoreStatus.空料框)
                        && e.GsRunStatus == EnumGSRunStatus.任务完成)
                    {
                        int deleteStockStatus = this.View.AskMessBox("系统会自动删除库存信息，您确定要设置么？");
                        if (deleteStockStatus == 0)
                        {
                            bllStock.Delete(viewStockListModel.StockID);
                            bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), e.GoodsSiteID);
                            mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示,"用户："+this.userNameStr
                                + ",货位类型“" + e.GsTaskType+"货位存储状态：" +e.GsStoreStatus+
                                "货位运行状态："+e.GsRunStatus + "”修改成功！" + "货位号：" + viewStockListModel.GoodsSiteName);
                        }
                    }
                    else if (e.GsStoreStatus == EnumGSStoreStatus.空货位 && e.GsRunStatus == EnumGSRunStatus.待用)
                    {
                        int deleteStockStatus = this.View.AskMessBox("系统会自动删除库存信息，您确定要设置么？");
                        if (deleteStockStatus == 0)
                        {
                            bllStock.Delete(viewStockListModel.StockID);
                            bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), e.GoodsSiteID);
                            mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "用户：" + this.userNameStr
                              + ",货位类型“" + e.GsTaskType + "货位存储状态：" + e.GsStoreStatus +
                              "货位运行状态：" + e.GsRunStatus + "”修改成功！" + "货位号：" + viewStockListModel.GoodsSiteName);
                        }
                    }
                    else
                    {
                        bool updateStatus = bllGoodsSite.UpdateGoodsSiteStatus(e.GsStoreStatus.ToString(), e.GsRunStatus.ToString(), e.GoodsSiteID);
                        if (updateStatus == true)
                        {
                            mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "用户：" + this.userNameStr
                                + ",货位类型“" + e.GsTaskType + "货位存储状态：" + e.GsStoreStatus +
                                "货位运行状态：" + e.GsRunStatus + "”修改成功！" + "货位号：" + viewStockListModel.GoodsSiteName);
                        }
                    }
                }
            }

            List<View_QueryStockListModel> modelList = bllQueryStockList.GetModelList(queryHouseName, queryRowth, queryColumnth, queryLayerth, queryProductStasus, queryWorkFlowStatus
                ,queryGsRunStatus,queryGsStoreStatus,queryGsTaskType,queryProductBatchNum);
            this.View.RefreshDGVData(modelList);

        }

        private void DeleteStockEventHandler(object sender, DeleteStockEventArgs e)
        {
            if (this.View.AskMessBox("您确定要删除当前选中库存么？") == 0)
            {
                for (int i = 0; i < e.StockID.Length; i++)
                {
                    StockModel stockModel = bllStock.GetModel(e.StockID[i]);
                    if (stockModel == null)
                    {
                        return;
                    }
                    StockListModel stockListModel = bllStockList.GetModelByStockID(stockModel.StockID);
                    if (stockListModel == null)
                    {
                        return;
                    }
                    ManageTaskListModel manaTaskList = bllTaskList.GetModelByStockListID(stockListModel.StockListID);
                    if (manaTaskList != null)
                    {
                        ManageTaskModel manaTask = bllTask.GetModel(manaTaskList.TaskID);
                        if (manaTask != null)
                        {
                            bllTask.Delete(manaTask.TaskID);
                        }
                    }

                    Hashtable updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空货位.ToString()
                            , EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出库.ToString(), stockModel.GoodsSiteID);  //获取更新货位状态Hs
                    Hashtable deleteStockHs = bllStock.GetDeleteModelHash(e.StockID[i]);
                    List<Hashtable> allHs = new List<Hashtable>();
                    allHs.Add(updateGsStatusHs);
                    allHs.Add(deleteStockHs);

                    bool deleteStatus = ExecuteManyHashSqls(allHs);
                    if (deleteStatus == true)
                    {
                        if (this.mainPres != null)
                        {
                            this.mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示,"用户："
                                + this.userNameStr + ",手动删除库存ID:" + e.StockID[i] + "库存名称：" + stockListModel.GoodsSiteName  + "成功！");
                        }
                    }
                    else
                    {
                        if (this.mainPres != null)
                        {
                            this.mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户："
                                 + this.userNameStr + ",手动删除库存ID:" + e.StockID[i] + "库存名称：" + stockListModel.GoodsSiteName + "失败！");
                        }
                    }
                }

                //List<StockListModel> modelList = bllStockList.GetModelList(queryHouseName,queryRowth,queryColumnth,queryLayerth, queryProductStasus, queryWorkFlowStatus);
                //this.View.RefreshDGVData(modelList);
                List<View_QueryStockListModel> modelList = bllQueryStockList.GetModelList(queryHouseName, queryRowth, queryColumnth, queryLayerth, queryProductStasus, queryWorkFlowStatus
                    ,queryGsRunStatus,queryGsStoreStatus,queryGsTaskType,queryProductBatchNum);
                this.View.RefreshDGVData(modelList);
            }
        }

        private void OutHouseEventHandler(object sender, StockEventArgs e)
        {
            int outHouse = this.View.AskMessBox("您确定要将选中库存手动出库么？");
            if (outHouse == 0)
            {
                for (int i = 0; i < e.StockIDArr.Count(); i++)
                {
                    //if (!(e.GsStoreStatusArr[i] == (EnumGSStoreStatus.有货 | EnumGSStoreStatus.空料框)&&e.GsRunStatusArr[i] == EnumGSRunStatus.任务完成
                    //    &&e.GSTaskTypeArr[i] == EnumTaskCategory.入库))
                    //{
                    //    if (this.mainPres != null)
                    //    {
                    //        this.mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "请选择货位任务类型为“入库”，并且货位存储类型为“有货”或“空料框”，并且货位任务状态为“任务完成”的货位出库！！");
                    //        continue;
                    //    }
                    //}
                    if (!((e.GsStoreStatusArr[i] == EnumGSStoreStatus.有货 || e.GsStoreStatusArr[i] == EnumGSStoreStatus.空料框) &&e.GsRunStatusArr[i] == EnumGSRunStatus.任务完成
                      && e.GSTaskTypeArr[i]  == EnumTaskCategory.入库))
                    {
                        if (this.mainPres != null)
                        {
                            this.mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "请选择货位任务类型为“入库”，并且货位存储类型为“有货”或“空料框”，并且货位任务状态为“任务完成”的货位出库！！");
                            return;
                        }
                    }
                    Hashtable updateGsStatusHs = null;
                    TaskTypeModel taskType = null;
                    StockModel stockModel = bllStock.GetModel(e.StockIDArr[i]);
                    if (stockModel == null)
                    {
                        continue;
                    }
                    StockListModel stockListModel = bllStockList.GetModelByStockID(e.StockIDArr[i]);
                    if (stockListModel == null)
                    {
                        continue;
                    }
                    GoodsSiteModel goodsSiteModel = bllGoodsSite.GetModel(stockModel.GoodsSiteID);
                    if (goodsSiteModel == null)
                    {
                        continue;
                    }

                    ManageTaskListModel existManaTaskList = bllTaskList.GetModelByStockListID(stockListModel.StockListID);
                    if (existManaTaskList == null)
                    {
                        if (stockListModel.StoreHouseName == EnumStoreHouse.A1库房.ToString())//手动出库默认出库任务类型为一次检测出库
                        {
                            if (e.ProductStatusArr[i] == EnumProductStatus.A1库老化3天)
                            {
                                taskType = bllTaskType.GetModel((int)EnumTaskName.分容出库_A1);
                            }
                            else if (e.ProductStatusArr[i] == EnumProductStatus.A1库静置10小时)
                            {
                                taskType = bllTaskType.GetModel((int)EnumTaskName.电芯出库_A1);
                            }

                            updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString()
                                , EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(),goodsSiteModel.GoodsSiteID);  //获取更新货位状态Hs 

                        }
                        else if (stockListModel.StoreHouseName == EnumStoreHouse.B1库房.ToString())
                        {
                            if (e.ProductStatusArr[i] == EnumProductStatus.空料框)
                            {
                                taskType = bllTaskType.GetModel((int)EnumTaskName.空料框出库);
                                updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空料框.ToString()
                                , EnumGSRunStatus.任务锁定.ToString(),EnumTaskCategory.出库.ToString(), goodsSiteModel.GoodsSiteID);  //获取更新货位状态Hs 
                            }
                            else
                            {
                                taskType = bllTaskType.GetModel((int)EnumTaskName.电芯出库_B1);
                                updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString()
                                , EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), goodsSiteModel.GoodsSiteID);  //获取更新货位状态Hs 
                            }
                        }

                        long manaTaskID = 0;
                        ManageTaskModel manaTaskModel = AutoCreateManaTask(stockModel, stockListModel, taskType, ref manaTaskID);
                        ManageTaskListModel manaTaskListModel = AutoCreateManaTaskList(manaTaskID, manaTaskModel, stockListModel);
                        AutoCreateControlTask(manaTaskModel, manaTaskID, taskType);

                        List<Hashtable> hashList = new List<Hashtable>();
                        hashList.Add(updateGsStatusHs);

                        bool executeStatus = ExecuteManyHashSqls(hashList);
                        if (executeStatus == true)
                        {
                            if (this.mainPres != null)
                            {
                                string trayIDListStr = bllStockDetail.GetTrayIDStrList(stockListModel.StockListID);
                                this.mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示,"用户：" +this.userNameStr+  ",货位：" + goodsSiteModel.GoodsSiteName + "生成“" + taskType .TaskTypeName+ "”任务成功！"
                                    + "托盘号：" + trayIDListStr);
                            }
                        }
                        else
                        {
                            if (this.mainPres != null)
                            {
                                this.mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" + this.userNameStr + "货位：" + goodsSiteModel.GoodsSiteName + "生成“" + taskType.TaskTypeName + "”任务失败！");
                            }
                        }
                    }
                }
            }
        }

        private void QueryStockEventHandler(object sender, QueryStockEventArgs e)
        {
         
            queryHouseName = e.StoreHouse;
            queryProductStasus = e.ProductStatus;
            queryWorkFlowStatus = e.WorkFolwStatus;
            queryRowth = e.Rowth;
            queryColumnth = e.Columnth;
            queryLayerth = e.Layerth;
            queryGsStoreStatus = e.GsStoreStatus;
            queryGsRunStatus = e.GsRunStatus;
            queryGsTaskType = e.GsTaskType;
            queryProductBatchNum = e.ProductBatchNum;

            bool openProgress = this.View.OpenProgressBar();
            if (!openProgress)
            {
                return;
            }
          
            List<View_QueryStockListModel> modelList = bllQueryStockList.GetModelList(e.StoreHouse, e.Rowth, e.Columnth, e.Layerth, e.ProductStatus, e.WorkFolwStatus
                ,e.GsRunStatus,e.GsStoreStatus,e.GsTaskType,e.ProductBatchNum);
           
           this.View.SetExitButtonEnabled(false);
           this.View.RefreshDGVData(modelList);

           int allCoreNum = 0;

           for (int i = 0; i < modelList.Count; i++)
           {
               System.Threading.Thread.Sleep(0);
               this.View.FormDoEvent();
               List<View_StockListDetailModel> stockDetailModelList = bllView_StockDetail.GetStockDetailByGsID(modelList[i].GoodsSiteID);
               List<TB_Tray_indexModel> trayDetailModelList = new List<TB_Tray_indexModel>();
               if (stockDetailModelList != null)
               {
                   for (int j = 0; j < stockDetailModelList.Count; j++)
                   {
                       this.View.FormDoEvent();
                       System.Threading.Thread.Sleep(0);
                       TB_Tray_indexModel trayDetailModel = bllTrayDetail.GetModel(stockDetailModelList[j].TrayID);//查询料框电芯详细 
                       if (trayDetailModel != null)
                       {
                           allCoreNum += (int)trayDetailModel.Tf_CellCount;
                       }
                   }
               }
           }
           this.View.ShowTrayCoreNum(allCoreNum.ToString());

           this.View.CloseProgressBar();
           this.View.SetExitButtonEnabled(true);
        }

        private void LoadEventHandler(object sender, EventArgs e)
        {
            List<StoreHouseProper> houseProList = new List<StoreHouseProper>();
            int rows = 0;
            int columns = 0;
            int layers = 0;
            bllGoodsSite.GetRowColumnLayer(4, out rows, out columns, out layers);
            StoreHouseProper housePro1 = new StoreHouseProper();
            housePro1.HouseName = EnumStoreHouse.A1库房;
            housePro1.Rows = rows;
            housePro1.Columns = columns;
            housePro1.Layers = layers;
            houseProList.Add(housePro1);

            bllGoodsSite.GetRowColumnLayer(2, out rows, out columns, out layers);
            StoreHouseProper housePro2 = new StoreHouseProper();
            housePro2.HouseName = EnumStoreHouse.B1库房;
            housePro2.Rows = rows;
            housePro2.Columns = columns;
            housePro2.Layers = layers;
            houseProList.Add(housePro2);
            this.View.QueryRCLData(houseProList);

            DataTable dt = bllStockList.GetAllProductBatchNums();
           
            this.View.SetProductBatchNumList(dt);
            
         

            if (mainPres != null)
            {
               string currentRole = this.mainPres.View.GetCurrentRoleName();
               if (currentRole == "系统管理员")
               {
                   this.View.SetDeleteFunc(true);
                   this.View.SetGsStatusFunc(true);
               }
               else
               {
                   this.View.SetGsStatusFunc(false);
                   this.View.SetDeleteFunc(false);
               }
            }
        }
        #endregion

        #region 私有方法
        private ManageTaskModel AutoCreateManaTask(StockModel stockModel
           , StockListModel stockListModel, TaskTypeModel taskType, ref long manaTaskID)
        {
            ManageTaskModel manaTaskModel = new ManageTaskModel();//生成管理任务
            manaTaskModel.TaskCode = stockListModel.ProductFrameCode;
            LoginPresenter loginPre = (LoginPresenter)this.View.GetPresenter(typeof(LoginPresenter));
            if (loginPre != null)
            {
                manaTaskModel.TaskCreatePerson = loginPre.View.UserName;
            }
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
            manaTaskID = bllTask.Add(manaTaskModel);
            return manaTaskModel;
        }

        private ManageTaskListModel AutoCreateManaTaskList(long manaTaskID, ManageTaskModel manaTaskModel, StockListModel stockListModel)
        {

            ManageTaskListModel manaTaskListModel = new ManageTaskListModel();
            manaTaskListModel.ProductBatch = stockListModel.ProductBatchNum.ToString();
            manaTaskListModel.ProductCode = stockListModel.ProductCode;
            manaTaskListModel.StockListID = stockListModel.StockListID;
            LoginPresenter loginPre = (LoginPresenter)this.View.GetPresenter(typeof(LoginPresenter));
            if (loginPre != null)
            {
                manaTaskListModel.TaskCreatePerson = loginPre.View.UserName;
            }
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
            controlTaskModel.TaskTypeName = taskType.TaskTypeName;
            controlTaskModel.StartArea = manaTaskModel.TaskStartArea;
            controlTaskModel.StartDevice = manaTaskModel.TaskStartPostion;
            controlTaskModel.TargetArea = manaTaskModel.TaskEndArea;
            controlTaskModel.TargetDevice = manaTaskModel.TaskEndPostion;
            controlTaskModel.TaskID = taskID;
            controlTaskModel.CreateMode = EnumCreateMode.手动生成.ToString();
            controlTaskModel.TaskStatus = EnumTaskStatus.待执行.ToString();
            controlTaskModel.TaskType = manaTaskModel.TaskType;
            controlTaskModel.TaskTypeCode = taskType.TaskTypeCode;
            controlTaskModel.CreateTime = DateTime.Now;
            bllControlTask.Add(controlTaskModel);
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
