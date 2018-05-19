using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSDataAccess;
using ECAMSModel;
using System.Data;

namespace ECAMSPresenter
{
    public class DataManaPresenter:BasePresenter<IDataManaView>
    {   
        #region 全局变量
        private readonly ControlInterfaceBll bllControlInter = new ControlInterfaceBll();
        private readonly ControlTaskBll bllControlTask = new ControlTaskBll();
        private readonly ManageTaskBll bllManaTask = new ManageTaskBll();
        private readonly StockListBll bllStockList = new StockListBll();
        private readonly StockBll bllStock = new StockBll();
        private readonly GoodsSiteBll bllGoodsSite = new GoodsSiteBll();
        private readonly OCVPalletBll palletBll = new OCVPalletBll();
        private readonly OCVBatteryBll batteryBll = new OCVBatteryBll();
        private MainPresenter mainPres = null;
        #endregion

        #region 初始化
        public DataManaPresenter(IDataManaView view)
            : base(view)
        {
            mainPres = (MainPresenter)this.View.GetPresenter(typeof(MainPresenter));
        }

        protected override void OnViewSet()
        {
            this.View.eventQuery += QueryEventHandler;
            this.View.eventSave += SaveEventHandler;
            this.View.eventDelete += DeleteEventHandler;
            this.View.eventFormatSystem += FormatSystemEventHandler;
            this.View.eventDatabaseBakRecover += DatabaseBakRecoverEventHandler;
        }

       
        #endregion

        #region 实现IDataManaView事件方法
        private void DatabaseBakRecoverEventHandler(object sender, DatabaseEventArgs e)
        {
            int status = DbHelperSQL.BakRecoverSql(e.Path, e.IsBackUp);
            if (mainPres != null)
            {
                if (status == 1)
                {

                    mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "数据库恢复成功！");
                }
                else if (status == 2)
                {
                    mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "数据库备份成功！");
                }
                else
                {
                    mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "数据库操作失败！");
                }
            }
        }

        private void FormatSystemEventHandler(object sender, EventArgs e)
        {
            if (this.View.AskMessBox("您确定要恢复出厂设置么？恢复出厂设置系统将清空所有数据！！！") == 0)
            {
                bool formatManaTask = bllManaTask.Clear();
                bool formatControlTask = bllControlTask.Clear();
                bool formatControlInter = bllControlInter.Clear();
                bool formatStock = bllStock.Clear();
                palletBll.ClearPallet();
                batteryBll.ClearBattery();
                bool formatGoodsSite = bllGoodsSite.FormatGoogsSite();
               
                if (mainPres != null)
                {
                    mainPres.View.OnStop();
                    mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "出厂设置成功！");
                }
            }
        }

        private void DeleteEventHandler(object sender, DeleteEventArgs e)
        {
            int deleteSure = this.View.AskMessBox("您确定要删除选中数据么？");
            if (deleteSure == 0)
            {
                switch (e.EnumDataList)
                {
                    case EnumDataList.管理任务表:
                        for (int i = 0; i < e.ListID.Count(); i++)
                        {
                            bool deleteStatus = bllManaTask.Delete(e.ListID[i]);
                        }

                        DataSet dsManaTask = bllManaTask.GetAllList();
                        if (dsManaTask != null && dsManaTask.Tables.Count > 0)
                        {
                            DataTable dtManaTask = dsManaTask.Tables[0];
                            this.View.RefreshDataList(dtManaTask, e.EnumDataList);
                        }
                        break;
                    case EnumDataList.控制接口表:
                        for (int i = 0; i < e.ListID.Count(); i++)
                        {
                            bool deleteStatus = bllControlInter.Delete(e.ListID[i]);
                        }

                        DataSet dsControlInter = bllControlInter.GetAllList();
                        if (dsControlInter != null && dsControlInter.Tables.Count > 0)
                        {
                            DataTable dtControlInter = dsControlInter.Tables[0];
                            this.View.RefreshDataList(dtControlInter, e.EnumDataList);
                        }

                        break;
                    case EnumDataList.控制任务表:
                        for (int i = 0; i < e.ListID.Count(); i++)
                        {
                            bool deleteStatus = bllControlTask.Delete(e.ListID[i]);
                        }
                        DataSet dsControlTask = bllControlTask.GetAllList();
                        if (dsControlTask != null && dsControlTask.Tables.Count > 0)
                        {
                            DataTable dtControlTask = dsControlTask.Tables[0];
                            this.View.RefreshDataList(dtControlTask, e.EnumDataList);
                        }
                        break;
                    case EnumDataList.库存列表:
                        for (int i = 0; i < e.ListID.Count(); i++)
                        {
                            bool deleteStatus = bllStockList.Delete(e.ListID[i]);
                        }
                        DataSet dsStockList = bllStockList.GetAllList();
                        if (dsStockList != null && dsStockList.Tables.Count > 0)
                        {
                            DataTable dtStockList = dsStockList.Tables[0];
                            this.View.RefreshDataList(dtStockList, e.EnumDataList);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private void SaveEventHandler(object sender, SaveEventArgs e)
        {
            switch (e.EnumDataList)
            {
                case EnumDataList.管理任务表:
                     bool editManaStatus = bllManaTask.Update((ManageTaskModel)e.EditModel);
                     if (editManaStatus == true)
                    {
                        if (mainPres != null)
                        {
                            mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "修改成功！");
                        }
                    }
                    break;
                case EnumDataList.控制接口表:
                    bool editStatus = bllControlInter.Update((ControlInterfaceModel)e.EditModel);
                    if (editStatus == true)
                    {
                        if (mainPres != null)
                        {
                            mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "修改成功！");
                        }
                    }
                    break;
                case EnumDataList.控制任务表:
                    bool editControlTastStatus = bllControlTask.Update((ControlTaskModel)e.EditModel);
                    if (editControlTastStatus == true)
                    {
                        if (mainPres != null)
                        {
                            mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "修改成功！");
                        }
                    }
                    break;
                case EnumDataList.库存列表:
                    bool editStockListStatus = bllStockList.Update((StockListModel)e.EditModel);
                    if (editStockListStatus == true)
                    {
                        if (mainPres != null)
                        {
                            mainPres.ShowLog(ECAMSModel.EnumLogCategory.管理层日志, EnumLogType.提示, "修改成功！");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private void QueryEventHandler(object sender, DataEventArgs e)
        {
            switch (e.EnumDataList)
            {
                case EnumDataList.管理任务表:
                    DataSet dsManaTask = bllManaTask.GetAllListByTime(e.StartTime,e.EndTime);
                    if (bllManaTask != null && dsManaTask.Tables.Count > 0)
                    {
                        DataTable dtManaTask = dsManaTask.Tables[0];
                        this.View.RefreshDataList(dtManaTask, e.EnumDataList);
                    }
                    break;
                case EnumDataList.控制接口表:
                    DataSet dsControlInter = bllControlInter.GetAllListByTime(e.StartTime, e.EndTime);
                    if (dsControlInter != null && dsControlInter.Tables.Count > 0)
                    {
                        DataTable dtControlInter = dsControlInter.Tables[0];
                        this.View.RefreshDataList(dtControlInter, e.EnumDataList);
                    }
                    break;
                case EnumDataList.控制任务表:
                    DataSet dsControlTask = bllControlTask.GetAllListByTime(e.StartTime,e.EndTime);
                    if (dsControlTask != null && dsControlTask.Tables.Count > 0)
                    {
                        DataTable dtControlTask = dsControlTask.Tables[0];
                        this.View.RefreshDataList(dtControlTask, e.EnumDataList);
                    }
                    break;
                case EnumDataList.库存列表:
                    DataSet dsStockList = bllStockList.GetAllListByTime(e.StartTime,e.EndTime);
                    if (dsStockList != null && dsStockList.Tables.Count > 0)
                    {
                        DataTable dtStockList = dsStockList.Tables[0];
                        this.View.RefreshDataList(dtStockList, e.EnumDataList);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
