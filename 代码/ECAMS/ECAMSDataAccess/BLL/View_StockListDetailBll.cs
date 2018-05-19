using System;
using System.Data;
using System.Collections.Generic;
using ECAMSModel;
namespace ECAMSDataAccess
{
    /// <summary>
    /// View_StockListDetail
    /// </summary>
    public partial class View_StockListDetailBll
    {
        private readonly View_StockListDetailDal dal = new View_StockListDetailDal();
        public View_StockListDetailBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long StockListID, long ManaTaskID, string StoreHouseName, string ProductCode, int ProductNum, string ProductStatus, string ProductFrameCode, string ProductName, string GoodsSiteName, string ProductBatchNum, DateTime InHouseTime, DateTime UpdateTime, string Remarks, long StockDetailID, string CoreCode, int CoreQualitySign, int CorePositionID, string TrayID, string GoodsSiteType, int GoodsSiteLayer, int GoodsSiteColumn, int GoodsSiteRow, string DeviceID, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, string GoodsSiteStoreType, int LogicStoreAreaID, int StoreAreaID, int GoodsSiteID, long StockID)
        {
            return dal.Exists(StockListID, ManaTaskID, StoreHouseName, ProductCode, ProductNum, ProductStatus, ProductFrameCode, ProductName, GoodsSiteName, ProductBatchNum, InHouseTime, UpdateTime, Remarks, StockDetailID, CoreCode, CoreQualitySign, CorePositionID, TrayID, GoodsSiteType, GoodsSiteLayer, GoodsSiteColumn, GoodsSiteRow, DeviceID, GoodsSiteStoreStatus, GoodsSiteRunStatus, GoodsSiteInOutType, GoodsSiteStoreType, LogicStoreAreaID, StoreAreaID, GoodsSiteID, StockID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(View_StockListDetailModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(View_StockListDetailModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long StockListID, long ManaTaskID, string StoreHouseName, string ProductCode, int ProductNum, string ProductStatus, string ProductFrameCode, string ProductName, string GoodsSiteName, string ProductBatchNum, DateTime InHouseTime, DateTime UpdateTime, string Remarks, long StockDetailID, string CoreCode, int CoreQualitySign, int CorePositionID, string TrayID, string GoodsSiteType, int GoodsSiteLayer, int GoodsSiteColumn, int GoodsSiteRow, string DeviceID, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, string GoodsSiteStoreType, int LogicStoreAreaID, int StoreAreaID, int GoodsSiteID, long StockID)
        {

            return dal.Delete(StockListID, ManaTaskID, StoreHouseName, ProductCode, ProductNum, ProductStatus, ProductFrameCode, ProductName, GoodsSiteName, ProductBatchNum, InHouseTime, UpdateTime, Remarks, StockDetailID, CoreCode, CoreQualitySign, CorePositionID, TrayID, GoodsSiteType, GoodsSiteLayer, GoodsSiteColumn, GoodsSiteRow, DeviceID, GoodsSiteStoreStatus, GoodsSiteRunStatus, GoodsSiteInOutType, GoodsSiteStoreType, LogicStoreAreaID, StoreAreaID, GoodsSiteID, StockID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public View_StockListDetailModel GetModel(long StockListID, long ManaTaskID, string StoreHouseName, string ProductCode, int ProductNum, string ProductStatus, string ProductFrameCode, string ProductName, string GoodsSiteName, string ProductBatchNum, DateTime InHouseTime, DateTime UpdateTime, string Remarks, long StockDetailID, string CoreCode, int CoreQualitySign, int CorePositionID, string TrayID, string GoodsSiteType, int GoodsSiteLayer, int GoodsSiteColumn, int GoodsSiteRow, string DeviceID, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, string GoodsSiteStoreType, int LogicStoreAreaID, int StoreAreaID, int GoodsSiteID, long StockID)
        {

            return dal.GetModel(StockListID, ManaTaskID, StoreHouseName, ProductCode, ProductNum, ProductStatus, ProductFrameCode, ProductName, GoodsSiteName, ProductBatchNum, InHouseTime, UpdateTime, Remarks, StockDetailID, CoreCode, CoreQualitySign, CorePositionID, TrayID, GoodsSiteType, GoodsSiteLayer, GoodsSiteColumn, GoodsSiteRow, DeviceID, GoodsSiteStoreStatus, GoodsSiteRunStatus, GoodsSiteInOutType, GoodsSiteStoreType, LogicStoreAreaID, StoreAreaID, GoodsSiteID, StockID);
        }

        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<View_StockListDetailModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<View_StockListDetailModel> DataTableToList(DataTable dt)
        {
            List<View_StockListDetailModel> modelList = new List<View_StockListDetailModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                View_StockListDetailModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 作者:np
        /// 时间:2014年6月1日
        /// 内容:通过货位ID获取货位详细
        /// </summary>
        /// <param name="gsID"></param>
        /// <returns></returns>
        public List<View_StockListDetailModel> GetStockDetailByGsID(int gsID)
        {
            DataTable dt = dal.GetStockDetailByGsID(gsID);
            if (dt != null) { return DataTableToList(dt); }
            else { return null; }
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年8月29日
        /// 内容:获取托盘列表
        /// </summary>
        /// <param name="HouseName"></param>
        /// <param name="rowth"></param>
        /// <param name="columnth"></param>
        /// <param name="layerth"></param>
        /// <returns></returns>
        public List<string> GetStockTrayList(EnumStoreHouse HouseName, int rowth, int columnth, int layerth)
        {
            return dal.GetStockTrayList(HouseName, rowth, columnth, layerth);
        }
        #endregion  ExtensionMethod
    }
}

