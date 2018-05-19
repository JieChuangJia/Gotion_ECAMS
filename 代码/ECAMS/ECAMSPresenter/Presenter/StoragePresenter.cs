using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSDataAccess;
using ECAMSModel;
using System.Data;
using System.Collections;

namespace ECAMSPresenter
{
    public class StoragePresenter : BasePresenter<IStorageView>
    {
        #region 全局变量
        private readonly GoodsSiteBll bllGoodsSite = new GoodsSiteBll(); //货位逻辑
        private readonly StockBll bllStock = new StockBll();
        private readonly StockListBll bllStockList = new StockListBll();
        private readonly LogicStoreAreaBll bllLogicStoreArea = new LogicStoreAreaBll();
        private readonly ControlTaskBll bllControlTask = new ControlTaskBll();                //控制任务
        private readonly TaskTypeBll bllTaskType = new TaskTypeBll();
        private readonly ManageTaskBll bllTask = new ManageTaskBll();                         //管理任务
        private readonly ManageTaskListBll bllTaskList = new ManageTaskListBll();             //管理任务列表
        private readonly View_QueryStockListBll bllView_QueryStockList = new View_QueryStockListBll();
        private readonly View_StockListDetailBll bllView_StockDetail = new View_StockListDetailBll();//货位详细视图表
        private readonly TB_Tray_indexBll bllTrayDetail = new TB_Tray_indexBll();             //客户库存详细表
        private readonly StockDetailBll bllStockDetail = new StockDetailBll();
        private readonly TB_Tray_indexBll bllTrayIndex = new TB_Tray_indexBll();
        
        private StockListModel currStockListModel = new StockListModel();//当前选中库存
        private int currentSelectGSID = 999999;//默认值 没有赋值状态
        private MainPresenter mainPre = null;
        LoginPresenter loginPre = null;
        string userNameStr = "";
        #endregion

        #region 初始化
        public StoragePresenter(IStorageView view)
            : base(view)
        {
            mainPre=(MainPresenter)this.View.GetPresenter(typeof(MainPresenter));
            loginPre = (LoginPresenter)this.View.GetPresenter(typeof(LoginPresenter));
            if (loginPre != null)
            {
                userNameStr = loginPre.View.UserName;
            }
        }

        protected override void OnViewSet()
        {
            this.View.eventSetGsStatus += SetGsStatusEventHandler;
            this.View.eventLoadStorageData += LoadStorageDataEventHandler;
            this.View.eventQueryGoodsSite += QueryGoodsSiteEventHandler;
            this.View.eventOutHouseByHand += OutHouseByHandEventHandler;
            this.View.eventLoad += LoadEventHandler;
            this.View.eventQueryGsByRcl += QueryGsByRclEventHandler;
            this.View.eventUnuseGs += UnuseGsEventHandler;
            this.View.eventUseGs += UseGsEventHandler;
            this.View.eventDeleteTray += DeleteTrayEventHandler;
            this.View.eventAddTray += AddTrayEventHandler;
            this.View.eventSearchTray += SearchTrayEventHandler;
        }
        #endregion

        #region 实现函数
        private void SearchTrayEventHandler(object sender, SearchTrayEventArgs e)
        {
            StockDetailModel stockDetailModel = bllStockDetail.GetModelByTrayID(e.TrayCode);
            if (stockDetailModel == null)
            {
                this.View.ShowMessage("信息提示", "此条码在库存中不存在，请核对条码是否正确！");
                return;
            }
            StockListModel stockListModel = bllStockList.GetModel(stockDetailModel.StockListID);
            if (stockListModel == null)
            {
                this.View.ShowMessage("信息提示", "此条码在库存中不存在，请核对条码是否正确！");
                return;
            }
            else
            {
                string[] splitArr = new string[3] { "排", "列", "层" };
                string[] rclArr = stockListModel.GoodsSiteName.Split(splitArr,StringSplitOptions.RemoveEmptyEntries);
                this.View.SearchTray(stockListModel.StoreHouseName, rclArr[0], rclArr[1], rclArr[2]);
                this.View.ShowMessage("信息提示", "托盘位置：" + stockListModel.StoreHouseName + "," + stockListModel.GoodsSiteName + "!");
            }
        }
        private void AddTrayEventHandler(object sender, TrayEventArgs e)
        {
            if (e.GsRunStatus != EnumGSRunStatus.任务完成.ToString())
            {
                this.View.ShowMessage("信息提示", "货位正在运行不允许添加料框信息！");
                return;
            }
            if (this.currentSelectGSID != 999999)
            {
                View_QueryStockListModel viewStockListModel = bllView_QueryStockList.GetModelByGSID(currentSelectGSID);
                if (viewStockListModel != null)
                {
                    TB_Tray_indexModel trayModel = bllTrayIndex.GetModel(e.TrayID);
                    if (trayModel == null)
                    {
                        this.View.ShowMessage("信息提示", "条码在数据库不存在！");
                        return;
                    }
                    if (currStockListModel != null)
                    {
                        List<StockDetailModel> stockDetailList = bllStockDetail.GetDetailModelList(currStockListModel.StockListID);
                        if (stockDetailList != null)
                        {
                            if (viewStockListModel.StoreHouseName == EnumStoreHouse.A1库房.ToString())
                            {
                                if (stockDetailList.Count >= 2)
                                {
                                    this.View.ShowMessage("信息提示", "A库房每个货位只能存储2个料框！");
                                    return;
                                }
                            }
                            else if (viewStockListModel.StoreHouseName == EnumStoreHouse.B1库房.ToString())
                            {
                                if (stockDetailList.Count >= 6)
                                {
                                    this.View.ShowMessage("信息提示", "B库房每个货位只能存储6个料框！");
                                    return;
                                }
                            }
                            
                            bool isTheSame = true;
                            for (int i = 0; i < stockDetailList.Count; i++)
                            {
                                TB_Tray_indexModel existTrayModel = bllTrayIndex.GetModel(stockDetailList[i].TrayID);
                                if (existTrayModel.Tf_BatchID != trayModel.Tf_BatchID)
                                {
                                    isTheSame = false;
                                    this.View.ShowMessage("信息提示", "您添加的托盘号所属批次号与现有库存的托盘批次不一致！若您要添加请将现有库存的托盘删除！");
                                    break;
                                }
                            }
                            if (isTheSame == false)
                            {
                                return;
                            }
                        }
                    }
                
                    StockDetailModel stockDetailModel = new StockDetailModel();
                    stockDetailModel.TrayID = e.TrayID;
                    stockDetailModel.StockListID = viewStockListModel.StockListID;
                    bllStockDetail.Add(stockDetailModel);

                    currStockListModel.ProductBatchNum = trayModel.Tf_BatchID;
                    bllStockList.Update(currStockListModel);//更新库存列表

                    RefreshStockDetail(currentSelectGSID);
                    this.mainPre.View.AddLog(EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" +this.userNameStr+ ",添加条码+" +trayModel.Tf_TrayId
                        +"，货位号：" + viewStockListModel.GoodsSiteName );
                }
                else
                {
                    this.View.ShowMessage("信息提示", "此货位没有库存信息！");
                }
            }
            else
            {
                this.View.ShowMessage("信息提示", "请选择要添加料框的货位！");
            }
        }

        private void DeleteTrayEventHandler(object sender, TrayEventArgs e)
        {
            if (e.GsRunStatus != EnumGSRunStatus.任务完成.ToString())
            {
                this.View.ShowMessage("信息提示", "货位正在运行不允许删除料框信息！");
                return;
            }
            int ask = this.View.AskMessBox("您确定要删除条码为：" +e.TrayID+"的料框信息么？");
            
            //this.mainPre.ShowLog(EnumLogCategory
            if (ask == 0)
            {
                StockDetailModel deleteStockDetailModel = bllStockDetail.GetModelByTrayID(e.TrayID);
                bool status = bllStockDetail.DeteleModelByTrayID(e.TrayID);
                if (status)
                {
                    this.View.ShowMessage("信息提示", "删除料框信息成功！");
                }
                this.mainPre.View.AddLog(EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" + this.userNameStr + ",删除条码+" + e.TrayID
                        + "，货位号：" + e.GoodsSiteName);

                //库存删掉后要清空库存和货位状态,只有任务完成的时候才可以删除此时只需更新库存状态和货位状态
                if(deleteStockDetailModel == null)
                {
                    this.View.ShowMessage("信息提示", "料框详细为空！！");
                    return;
                }
                StockListModel deleteStockListModel = bllStockList.GetModel(deleteStockDetailModel.StockListID);

                if (deleteStockListModel == null)
                {
                    this.View.ShowMessage("信息提示", "库存列表信息为空！！");
                    return;
                }

                StockModel deleteStockModel = bllStock.GetModel(deleteStockListModel.StockID);
                if (deleteStockModel == null)
                {
                    return;
                }
                List<StockDetailModel> stockDetailList = bllStockDetail.GetDetailModelList(deleteStockListModel.StockListID);//查询当前库存料框所有
                if (stockDetailList == null || stockDetailList.Count == 0)
                {
                   
                    bllStock.Delete(deleteStockListModel.StockID);

                    bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), deleteStockModel.GoodsSiteID);
                    this.View.RefreshGoodsSite();
                }

                RefreshStockDetail(currentSelectGSID);
                //this.View.RefreshStorage();
            }
        }

        private void UseGsEventHandler(object sender, QueryGoodsSiteEventArgs e)
        {
            int gsStatus = this.View.AskMessBox("您确定启用当选中前货位么？");
            if (gsStatus == 0)
            {
                bool status = bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), e.GoodsSiteID);
                if (status)
                {
                    this.View.ShowMessage("信息提示", "货位启用成功！");
                }
                else
                {
                    this.View.ShowMessage("信息提示", "货位启用失败！");
                }
            }
        }
        private void UnuseGsEventHandler(object sender, QueryGoodsSiteEventArgs e)
        {
            int gsStatus = this.View.AskMessBox("您确定禁用当选中前货位么？");
            if (gsStatus == 0)
            {
                View_QueryStockListModel viewStockListModel = bllView_QueryStockList.GetModelByGSID(e.GoodsSiteID);
                if (viewStockListModel != null)
                {
                    this.View.ShowMessage("信息提示", "当前货位存有货物，不能禁用此货位，如要禁用当前货位，请将当前货物出库！");
                    return;
                }
                bool status = bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.异常.ToString(), EnumTaskCategory.出入库.ToString(), e.GoodsSiteID);
                if (status)
                {
                    this.View.ShowMessage("信息提示", "货位禁用成功！");
                }
                else
                {
                    this.View.ShowMessage("信息提示", "货位禁用失败！");
                }
            }
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

            string currentRole = this.mainPre.View.GetCurrentRoleName();
            if (currentRole == "系统管理员")
            {
                this.View.SetGsUseOrUnuseFunc(true);
                this.View.SetGsStatusFunc(true);
            }
            else
            {
                this.View.SetGsStatusFunc(false);
                this.View.SetGsUseOrUnuseFunc(false);
            }
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年4月17日
        /// 内容:当前货物状态的出库类型
        /// </summary>
        /// <param name="houseName"></param>
        /// <param name="productStatus"></param>
        /// <returns></returns>
        private string GetTaskName(string houseName,string productStatus)
        {
            string backTaskName = "";
            if (houseName == EnumStoreHouse.A1库房.ToString())//手动出库根据物料状态出库
            {
                if (productStatus == EnumProductStatus.A1库静置10小时.ToString())
                {
                    backTaskName= EnumTaskName.电芯出库_A1.ToString();
                }
                else if (productStatus == EnumProductStatus.A1库老化3天.ToString())
                {
                    backTaskName= EnumTaskName.分容出库_A1.ToString();
                }

            }
            else if (houseName == EnumStoreHouse.B1库房.ToString())
            {
                if (productStatus == EnumProductStatus.空料框.ToString())
                {
                    backTaskName= EnumTaskName.空料框出库.ToString();
                }
                else
                {
                    backTaskName= EnumTaskName.电芯出库_B1.ToString();                   
                }
            }
            return backTaskName;
        }
        private void OutHouseByHandEventHandler(object sender, OutHouseByHandEventArgs e)
        {
            int outHouse = this.View.AskMessBox("您确定要将选中货位手动出库么？");
            if (outHouse == 0)
            {
                GoodsSiteModel gsModel = bllGoodsSite.GetModel(e.GoodsSiteID);
                if (gsModel == null)
                {
                    if (this.mainPre != null)
                    {
                        this.mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "此货位为空，不能出库！");
                        return;
                    }
                }
                if (!((e.GsStoreStatus == EnumGSStoreStatus.有货 ||e.GsStoreStatus== EnumGSStoreStatus.空料框) && e.GsRunStatus == EnumGSRunStatus.任务完成
                       && gsModel.GoodsSiteInOutType == EnumTaskCategory.入库.ToString()))
                {
                    if (this.mainPre != null)
                    {
                        this.mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "请选择货位任务类型为“入库”，并且货位存储类型为“有货”或“空料框”，并且货位任务状态为“任务完成”的货位出库！！");
                        return;
                    }
                }

                View_QueryStockListModel viewGsStockModel = bllView_QueryStockList.GetOutHouseModel(e.GoodsSiteID, e.GsStoreStatus, e.GsRunStatus);
                if (viewGsStockModel != null)
                {
                    Hashtable updateGsStatusHs = null;
                    TaskTypeModel taskType = null;
                    StockModel stockModel = bllStock.GetModelByGsID(e.GoodsSiteID);
                    bool exeHandTask = false;//执行手动选择的任务类型
                    if (stockModel != null)
                    {
                        StockListModel stockListModel = bllStockList.GetModelByStockID(stockModel.StockID);
                        if (stockListModel != null)
                        {
                            string shouldTaskName = GetTaskName(stockListModel.StoreHouseName, stockListModel.ProductStatus);
                            #region 根据人工选定的出库类型出库
                            if (e.OutHouseType != shouldTaskName)
                            {
                                if (this.View.AskMessBox("当前产品状态出库类型应为“" + shouldTaskName + "”,您确定要“" + e.OutHouseType + "”么？") == 0)
                                {
                                    EnumTaskName handTaskName = (EnumTaskName)Enum.Parse(typeof(EnumTaskName), e.OutHouseType);
                                    taskType = bllTaskType.GetModel((int)handTaskName);
                                    switch (handTaskName)
                                    {
                                        case EnumTaskName.电芯出库_A1:
                                            updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), viewGsStockModel.GoodsSiteID);  //获取更新货位状态Hs 
                                            break;
                                        case EnumTaskName.电芯出库_B1:
                                            updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), viewGsStockModel.GoodsSiteID);  //获取更新货位状态Hs 
                                            break;

                                        case EnumTaskName.空料框出库:
                                            updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空料框.ToString(), EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), viewGsStockModel.GoodsSiteID);  //获取更新货位状态Hs 
                                            break;
                                        case EnumTaskName.分容出库_A1:
                                            updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), viewGsStockModel.GoodsSiteID);  //获取更新货位状态Hs 
                                            break;
                                    }

                                    exeHandTask = true;
                                }
                            }
                            #endregion
                            #region 根据货物状态出库
                            if (exeHandTask == false)
                            {
                                if (stockListModel.StoreHouseName == EnumStoreHouse.A1库房.ToString())//手动出库根据物料状态出库
                                {
                                    if (viewGsStockModel.ProductStatus == EnumProductStatus.A1库静置10小时.ToString())
                                    {
                                        taskType = bllTaskType.GetModel((int)EnumTaskName.电芯出库_A1);
                                        updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), viewGsStockModel.GoodsSiteID);  //获取更新货位状态Hs 

                                    }
                                    else if (viewGsStockModel.ProductStatus == EnumProductStatus.A1库老化3天.ToString())
                                    {
                                        taskType = bllTaskType.GetModel((int)EnumTaskName.分容出库_A1);
                                        updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), viewGsStockModel.GoodsSiteID);  //获取更新货位状态Hs 
                                    }

                                }
                                else if (stockListModel.StoreHouseName == EnumStoreHouse.B1库房.ToString())
                                {
                                    if (stockListModel.ProductStatus == EnumProductStatus.空料框.ToString())
                                    {
                                        taskType = bllTaskType.GetModel((int)EnumTaskName.空料框出库);
                                        updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.空料框.ToString(), EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), viewGsStockModel.GoodsSiteID);  //获取更新货位状态Hs 
                                    }
                                    else
                                    {
                                        taskType = bllTaskType.GetModel((int)EnumTaskName.电芯出库_B1);
                                        updateGsStatusHs = bllGoodsSite.GetUpdateModelHs(EnumGSStoreStatus.有货.ToString(), EnumGSRunStatus.任务锁定.ToString(), EnumTaskCategory.出库.ToString(), viewGsStockModel.GoodsSiteID);  //获取更新货位状态Hs 
                                    }
                                }
                            }
                            #endregion
                            #region 出库处理逻辑
                            long manaTaskID = 0;
                            ManageTaskModel manaTaskModel = AutoCreateManaTask(stockModel, stockListModel, taskType, ref manaTaskID);
                            ManageTaskListModel manaTaskListModel = AutoCreateManaTaskList(manaTaskID, manaTaskModel, stockListModel);
                            AutoCreateControlTask(manaTaskModel, manaTaskID, taskType);

                            List<Hashtable> hashList = new List<Hashtable>();
                            hashList.Add(updateGsStatusHs);

                            bool executeStatus = ExecuteManyHashSqls(hashList);
                            if (executeStatus == true)
                            {
                                if (this.mainPre != null)
                                {
                                    string trayIDListStr = bllStockDetail.GetTrayIDStrList(stockListModel.StockListID);
                                    this.mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示,"用户："+this.userNameStr + "生成出库“" 
                                        + taskType.TaskTypeName + "”任务成功！" +"开始位置:" + manaTaskModel.TaskStartPostion + "，结束位置:"
                                        + manaTaskModel.TaskEndPostion + "托盘号：" + trayIDListStr);
                                }
                            }
                            else
                            {
                                if (this.mainPre != null)
                                {
                                    this.mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "用户：" + this.userNameStr + "生成出库“"
                                       + taskType.TaskTypeName + "”任务失败！" + "开始位置:" + manaTaskModel.TaskStartPostion + "，结束位置:"
                                       + manaTaskModel.TaskEndPostion);
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        if (this.mainPre != null)
                        {
                            this.mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "没有货物出库！");
                            this.View.ShowMessage("信息提示", "没有货物出库！");
                        }
                    }
                }
            }
        }

        private void QueryGsByRclEventHandler(object sender, QueryGsByRCLEventArgs e)
        {
            string[] valueArr = new string[9];
            GoodsSiteModel goodsSiteModel = bllGoodsSite.GetGoodsSite(e.storeHouse, e.Rowth, e.Columnth, e.Layerth);
            if (goodsSiteModel != null)
            {
                StockModel stockModel = bllStock.GetModelByGsID(goodsSiteModel.GoodsSiteID);
                if (stockModel != null)
                {
                    StockListModel stockListModel = bllStockList.GetModelByStockID(stockModel.StockID);
                    if (stockListModel != null)
                    {
                        valueArr[0] = goodsSiteModel.GoodsSiteID.ToString();
                        valueArr[1] = goodsSiteModel.GoodsSiteStoreStatus;
                        valueArr[2] = goodsSiteModel.GoodsSiteRunStatus;
                        valueArr[3] = stockListModel.ProductName;
                        valueArr[4] = stockListModel.ProductStatus;
                        valueArr[5] = stockListModel.InHouseTime.ToString();
                        valueArr[6] = stockModel.StockID.ToString();
                        valueArr[7] = goodsSiteModel.GoodsSiteName;
                        valueArr[8] = goodsSiteModel.GoodsSiteInOutType;
                        this.View.RefreshGSDetail(valueArr);
                    }
                    else
                    {
                        valueArr[0] = goodsSiteModel.GoodsSiteID.ToString();
                        valueArr[1] = goodsSiteModel.GoodsSiteStoreStatus;
                        valueArr[2] = goodsSiteModel.GoodsSiteRunStatus;
                        valueArr[3] = "无";
                        valueArr[4] = "无";
                        valueArr[5] = "无";
                        valueArr[6] = "无";
                        valueArr[7] = goodsSiteModel.GoodsSiteName;
                        valueArr[8] = goodsSiteModel.GoodsSiteInOutType;
                        this.View.RefreshGSDetail(valueArr);
                    }
                }
                else
                {
                    valueArr[0] = goodsSiteModel.GoodsSiteID.ToString();
                    valueArr[1] = goodsSiteModel.GoodsSiteStoreStatus;
                    valueArr[2] = goodsSiteModel.GoodsSiteRunStatus;
                    valueArr[3] = "无";
                    valueArr[4] = "无";
                    valueArr[5] = "无";
                    valueArr[6] = "无";
                    valueArr[7] =  goodsSiteModel.GoodsSiteName;
                    valueArr[8] = goodsSiteModel.GoodsSiteInOutType;
                    this.View.RefreshGSDetail(valueArr);
                }
            }
        }

        private void QueryGoodsSiteEventHandler(object sender, QueryGoodsSiteEventArgs e)
        {
           currentSelectGSID = e.GoodsSiteID;
           RefreshStockDetail(e.GoodsSiteID);

        }

      

        private void LoadStorageDataEventHandler(object sender, StorageEventArgs e)
        {   
            DataTable dt = bllGoodsSite.GetGsData(e.StoreHouse, e.Rowth);
            DataTable dtStatusNum = bllGoodsSite.GetStatusNum(e.StoreHouse, e.Rowth);
            int nullFramNum = 0;
            int productNum = 0;
            int nullNum = 0;
            int taskLockNum = 0;
            int forbitNum = 0;
            for (int i = 0; i < dtStatusNum.Rows.Count; i++)
            {
                nullNum += int.Parse(dtStatusNum.Rows[i][0].ToString());
                productNum += int.Parse(dtStatusNum.Rows[i][1].ToString());
                nullFramNum += int.Parse(dtStatusNum.Rows[i][2].ToString());
                taskLockNum += int.Parse(dtStatusNum.Rows[i][3].ToString());
                forbitNum += int.Parse(dtStatusNum.Rows[i][4].ToString());
            }

            this.View.RefreshGSStatsuNum(nullFramNum, productNum, nullNum, taskLockNum, forbitNum);
           
            this.View.RefreshStorage(dt);
        }

        private void SetGsStatusEventHandler(object sender, GsStatusEventArgs e)
        {
            int gsStatus = this.View.AskMessBox("您确定要修改选中货位状态么？");
            View_QueryStockListModel viewStockListModel = bllView_QueryStockList.GetModelByGSID(e.GoodsSiteID);
            if (viewStockListModel == null)
            {
                this.View.ShowMessage("信息提示", "当前货位没有库存信息，不能设置货位状态！");
            }
            else
            {
                if (gsStatus == 0)
                {
                    if (e.GsTaskType == EnumTaskCategory.出库 &&(e.GsStoreStatus ==EnumGSStoreStatus.有货||e.GsStoreStatus ==EnumGSStoreStatus.空料框)
                        && e.GsRunStatus == EnumGSRunStatus.任务完成)
                    {
                        int deleteStockStatus = this.View.AskMessBox("系统会自动删除库存信息，您确定要设置么？");
                        if (deleteStockStatus == 0)
                        {
                            bllStock.Delete(viewStockListModel.StockID);
                            bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), e.GoodsSiteID);
                            mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "用户：" + this.userNameStr
                           + ",货位类型“" + e.GsTaskType + "货位存储状态：" + e.GsStoreStatus +
                           "货位运行状态：" + e.GsRunStatus + "”修改成功！" + "货位号：" + viewStockListModel.GoodsSiteName);
                        } 
                    }
                    else if (e.GsStoreStatus == EnumGSStoreStatus.空货位 && e.GsRunStatus == EnumGSRunStatus.待用)
                    {
                        int deleteStockStatus = this.View.AskMessBox("系统会自动删除库存信息，您确定要设置么？");
                        if (deleteStockStatus == 0)
                        {
                            bllStock.Delete(viewStockListModel.StockID);
                            bllGoodsSite.UpdateGoodsSiteStatus(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(), EnumTaskCategory.出入库.ToString(), e.GoodsSiteID);
                            mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "用户：" + this.userNameStr
                             + ",货位类型“" + e.GsTaskType + "货位存储状态：" + e.GsStoreStatus +
                             "货位运行状态：" + e.GsRunStatus + "”修改成功！" + "货位号：" + viewStockListModel.GoodsSiteName);
                        }
                    }
                    else
                    {
                        bool updateStatus = bllGoodsSite.UpdateGoodsSiteStatus(e.GsStoreStatus.ToString(), e.GsRunStatus.ToString(), e.GoodsSiteID);
                        if (updateStatus == true)
                        {
                            mainPre.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, ECAMSModel.EnumLogType.提示, "用户：" + this.userNameStr
                            + ",货位类型“" + e.GsTaskType + "货位存储状态：" + e.GsStoreStatus +
                            "货位运行状态：" + e.GsRunStatus + "”修改成功！" + "货位号：" + viewStockListModel.GoodsSiteName);
                        }
                    }
                }
            }
        }
        #endregion

        #region 私有方法
        private void RefreshStockDetail(int gsID)
        {
            View_QueryStockListModel viewStockListModel = bllView_QueryStockList.GetModelByGSID(gsID);
            if (viewStockListModel != null)
            {
                StockListModel slModel = bllStockList.GetModel(viewStockListModel.StockListID);
                currStockListModel = slModel;
            }
            List<View_StockListDetailModel> stockDetailModelList = bllView_StockDetail.GetStockDetailByGsID(gsID);
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
