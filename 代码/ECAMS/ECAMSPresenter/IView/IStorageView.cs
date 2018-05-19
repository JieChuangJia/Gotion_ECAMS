using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using System.Data;
using ECAMSDataAccess;

namespace ECAMSPresenter
{
    public class SearchTrayEventArgs : EventArgs
    {
        public string TrayCode { get; set; }
    }

    public class GsStatusEventArgs : EventArgs
    {
        public EnumGSStoreStatus GsStoreStatus { get; set; }
        public EnumGSRunStatus GsRunStatus { get; set; }
        public EnumTaskCategory GsTaskType { get; set; }
        //public int Rowth { get; set; }
        //public int Columnth { get; set; }
        //public int Layerth { get; set; }
        public int GoodsSiteID { get; set; }

    }
    public class StoreHouseProper
    {
        public EnumStoreHouse HouseName { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Layers { get; set; }
    }

    public class StorageEventArgs:EventArgs
    {
        public EnumStoreHouse StoreHouse { get; set; }
        public int Rowth { get; set; }
    }

    public class QueryGoodsSiteEventArgs:EventArgs
    {
        public int GoodsSiteID{get;set;}
        public EnumGSStoreStatus GsStoreStatus { get; set; }
        public EnumGSRunStatus GsRunStatus { get; set; }
    }

    public class OutHouseByHandEventArgs : QueryGoodsSiteEventArgs
    {
        public string OutHouseType { get; set; }
    }


    public class QueryGsByRCLEventArgs : EventArgs
    {
        public EnumStoreHouse storeHouse { get; set; }
        public int Rowth { get; set; }
        public int Columnth { get; set; }
        public int Layerth { get; set; }
    }

    public interface IStorageView:IBaseView
    {
        #region 事件
        event EventHandler<GsStatusEventArgs> eventSetGsStatus;

        event EventHandler<StorageEventArgs> eventLoadStorageData;

        event EventHandler<QueryGoodsSiteEventArgs> eventQueryGoodsSite;

        event EventHandler<OutHouseByHandEventArgs> eventOutHouseByHand;

        event EventHandler eventLoad;
        /// <summary>
        /// 通过排列层查找货位
        /// </summary>
        event EventHandler<QueryGsByRCLEventArgs> eventQueryGsByRcl;
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月31日
        /// 内容:禁用货位
        /// </summary>
        event EventHandler<QueryGoodsSiteEventArgs> eventUnuseGs;
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月31日
        /// 内容:启用货位
        /// </summary>
        event EventHandler<QueryGoodsSiteEventArgs> eventUseGs;

        event EventHandler<TrayEventArgs> eventDeleteTray;
        event EventHandler<TrayEventArgs> eventAddTray;
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月20日
        /// 内容:通过托盘号查找托盘位置
        /// </summary>
        event EventHandler<SearchTrayEventArgs> eventSearchTray;
        #endregion

        #region 方法
        void RefreshStorage(DataTable dt);
        void RefreshGSDetail(string[] valueArr);
        void ShowStockDetail(List<View_StockListDetailModel> stockDetailModelList, List<TB_Tray_indexModel> trayDetailModelList);
        /// <summary>
        /// 更新货位有货、空料框、待用数量
        /// </summary>
        /// <param name="nullFrameNum">空料框数量</param>
        /// <param name="productNum">有货数量</param>
        /// <param name="nullNum">空货位数量</param>
        void RefreshGSStatsuNum(int nullFrameNum, int productNum, int nullNum,int taskLockNum,int forbitNum);
        /// <summary>..
        /// 弹出是否请求对话框
        /// </summary>
        /// <param name="content"></param>
        /// <returns>返回：0是，1：否</returns>
        int AskMessBox(string content);

        /// <summary>
        /// 查询排列层数量
        /// </summary>
        /// <param name="rowth"></param>
        /// <param name="columnth"></param>
        /// <param name="layers"></param>
        void QueryRCLData(List<StoreHouseProper> HouseProList);
        void ShowMessage(string titleStr, string contentStr);
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月13日
        /// 内容:设置货位启用禁用功能显隐
        /// </summary>
        /// <param name="visible"></param>
        void SetGsUseOrUnuseFunc(bool visible);
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月15日
        /// 内容:设置货位状态显示功能
        /// </summary>
        /// <param name="visible"></param>
        void SetGsStatusFunc(bool visible);

        /// <summary>
        /// 作者:np
        /// 时间:2014年8月20日
        /// 内容:通过托盘ID定位托盘
        /// </summary>
        /// <param name="storeHouseName"></param>
        /// <param name="rowth"></param>
        /// <param name="columnth"></param>
        /// <param name="layerth"></param>
        void SearchTray(string storeHouseName, string rowth, string columnth, string layerth);

        /// <summary>
        /// 刷新界面
        /// </summary>
        void RefreshGoodsSite();
        #endregion

    }
}
