using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ECAMSModel;
using ECAMSDataAccess;

namespace ECAMSPresenter
{
    #region 事件参数

    public class StockStatusEventArgs : EventArgs
    {
        public EnumGSStoreStatus GsStoreStatus { get; set; }
        public EnumGSRunStatus GsRunStatus { get; set; }
        public EnumTaskCategory GsTaskType { get; set; }
        //public int [] GoodsSiteID { get; set; }
        public int GoodsSiteID { get; set; }

    }

    public class TrayEventArgs:EventArgs
    {
        public string TrayID{get;set;}
        public string GoodsSiteName { get; set; }
        public string GsRunStatus { get; set; }     
    }

    /// <summary>
    /// 查询库存事件
    /// </summary>
    public class QueryStockEventArgs : EventArgs
    {
        /// <summary>
        /// 库房名称
        /// </summary>
        public string StoreHouse { get; set; }
        /// <summary>
        /// 物料状态
        /// </summary>
        public string ProductStatus { get; set; }
        public string Rowth { get; set; }
        public string Columnth { get; set; }
        public string Layerth { get; set; }
        public string GsStoreStatus { get; set; }
        public string GsRunStatus { get; set; }
        public string GsTaskType { get; set; }
        public string ProductBatchNum { get; set; }
        /// <summary>
        /// 流程时间是否到达
        /// </summary>
        public EnumWorkFolwStatus WorkFolwStatus { get; set; }
    }

    public class StockEventArgs:EventArgs
    {
        public long[] StockIDArr{get;set;}
        public EnumGSRunStatus []GsRunStatusArr { get; set; }
        public EnumGSStoreStatus[] GsStoreStatusArr { get; set; }
        public EnumTaskCategory[] GSTaskTypeArr { get; set; }
        public EnumProductStatus[] ProductStatusArr{get;set;}
    }

    public class DeleteStockEventArgs : EventArgs
    {
        public long[] StockID { get; set; }
    }
    #endregion

    public interface IStockManaView:IBaseView
    {
        #region 事件
        /// <summary>
        /// 查询库存事件
        /// </summary>
        event EventHandler<QueryStockEventArgs>  eventQueryStock;

        event EventHandler<StockEventArgs> eventOutHouseByHand;

        event EventHandler<DeleteStockEventArgs> eventDeleteStock;

        event EventHandler<StockStatusEventArgs> eventSetGsStatus;

        event EventHandler<QueryGoodsSiteEventArgs> eventStockDetail;

        event EventHandler<TrayEventArgs> eventTrayDetail;

        event EventHandler eventLoad;
        #endregion

        #region 方法
        ///// <summary>
        ///// 刷新数据
        ///// </summary>
        ///// <param name="model"></param>
        //void RefreshDGVData(List<StockListModel> modelList);

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="model"></param>
        void RefreshDGVData(List<View_QueryStockListModel> modelList);

        /// <summary>
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        int AskMessBox(string content);

        /// <summary>
        /// 查询库房属性排、列、层
        /// </summary>
        /// <param name="HouseProList"></param>
        void QueryRCLData(List<StoreHouseProper> HouseProList);
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月11日
        /// 内容:初始化产品批次号下拉列表
        /// </summary>
        /// <returns></returns>
        void SetProductBatchNumList(DataTable dt);
        void ShowMessage(string titleStr, string contentStr);
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月13日
        /// 内容:设置删除功能的显隐
        /// </summary>
        /// <param name="visible"></param>
        void SetDeleteFunc(bool visible);

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月15日
        /// 内容:设置货位状态显示功能
        /// </summary>
        /// <param name="visible"></param>
        void SetGsStatusFunc(bool visible);
        void ShowStockDetail(List<View_StockListDetailModel> stockDetailModelList, List<TB_Tray_indexModel> trayDetailModelList);
        void ShowTrayDetail(List<TB_After_GradeDataModel> coreModelList);
        void ShowTrayCoreNum(string coreNum);
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月25日
        /// 内容:设置退出按钮可用状态
        /// </summary>
        /// <param name="enabled"></param>
        void SetExitButtonEnabled(bool enabled);
        #endregion
    }
}
