using System;
using System.Data;
using System.Collections.Generic;
using ECAMSModel;

namespace ECAMSDataAccess
{
    /// <summary>
    /// View_QueryStockList
    /// </summary>
    public partial class View_QueryStockListBll
    {
        private readonly View_QueryStockListDal dal = new View_QueryStockListDal();
        private readonly TaskTypeBll bllTaskType = new TaskTypeBll();
        public View_QueryStockListBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GoodsSiteID, string GoodsSiteName, int GoodsSiteRow, int GoodsSiteColumn, int GoodsSiteLayer, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, long StockID, long StockListID, string StoreHouseName, string ProductStatus, string ProductName, DateTime InHouseTime, DateTime UpdateTime, long ManaTaskID, string ProductCode, int ProductNum, string ProductBatchNum)
        {
            return dal.Exists(GoodsSiteID, GoodsSiteName, GoodsSiteRow, GoodsSiteColumn, GoodsSiteLayer, GoodsSiteStoreStatus, GoodsSiteRunStatus, GoodsSiteInOutType, StockID, StockListID, StoreHouseName, ProductStatus, ProductName, InHouseTime, UpdateTime, ManaTaskID, ProductCode, ProductNum, ProductBatchNum);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(View_QueryStockListModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(View_QueryStockListModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GoodsSiteID, string GoodsSiteName, int GoodsSiteRow, int GoodsSiteColumn, int GoodsSiteLayer, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, long StockID, long StockListID, string StoreHouseName, string ProductStatus, string ProductName, DateTime InHouseTime, DateTime UpdateTime, long ManaTaskID, string ProductCode, int ProductNum, string ProductBatchNum)
        {

            return dal.Delete(GoodsSiteID, GoodsSiteName, GoodsSiteRow, GoodsSiteColumn, GoodsSiteLayer, GoodsSiteStoreStatus, GoodsSiteRunStatus, GoodsSiteInOutType, StockID, StockListID, StoreHouseName, ProductStatus, ProductName, InHouseTime, UpdateTime, ManaTaskID, ProductCode, ProductNum, ProductBatchNum);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public View_QueryStockListModel GetModel(int GoodsSiteID, string GoodsSiteName, int GoodsSiteRow, int GoodsSiteColumn, int GoodsSiteLayer, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, long StockID, long StockListID, string StoreHouseName, string ProductStatus, string ProductName, DateTime InHouseTime, DateTime UpdateTime, long ManaTaskID, string ProductCode, int ProductNum, string ProductBatchNum)
        {

            return dal.GetModel(GoodsSiteID, GoodsSiteName, GoodsSiteRow, GoodsSiteColumn, GoodsSiteLayer, GoodsSiteStoreStatus, GoodsSiteRunStatus, GoodsSiteInOutType, StockID, StockListID, StoreHouseName, ProductStatus, ProductName, InHouseTime, UpdateTime, ManaTaskID, ProductCode, ProductNum, ProductBatchNum);
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
        public List<View_QueryStockListModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<View_QueryStockListModel> DataTableToList(DataTable dt)
        {
            List<View_QueryStockListModel> modelList = new List<View_QueryStockListModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                View_QueryStockListModel model;
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
        /// 获取库存列表
        /// </summary>
        /// <param name="houseName"></param>
        /// <param name="productStatus"></param>
        /// <param name="workFolwTime"></param>
        /// <returns></returns>
        public List<View_QueryStockListModel> GetModelList(string houseName, string rowth, string columnth, string layerth
            , string productStatus, EnumWorkFolwStatus workFlowStatus,string gsRunStatus,string gsStoreStatus,string gsTaskType,string productBatchNum)
        {
            List<View_QueryStockListModel> modelList = null;
            string whereTaskType = "ProductStartStatus ='" + productStatus + "'";
            if (productStatus == "所有")
            {
                whereTaskType = "ProductStartStatus = '" + EnumProductStatus.B1库静置10天.ToString() + "'"
                + " or ProductStartStatus ='" + EnumProductStatus.A1库静置10小时.ToString() + "'"
                + " or ProductStartStatus = '" + EnumProductStatus.A1库老化3天.ToString() + "'";
            }
            List<TaskTypeModel> taskTypeList = bllTaskType.GetModelList(whereTaskType);

            if (taskTypeList != null && taskTypeList.Count > 0)
            {
                string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int workFlowTime = (int)taskTypeList[0].NeedTime;
               
                DataSet ds = dal.GetQueryList(houseName, rowth, columnth, layerth, productStatus, workFlowStatus.ToString(), workFlowTime
                    , gsRunStatus, gsStoreStatus, gsTaskType, productBatchNum);
                modelList = DataTableToList(ds.Tables[0]);

            }
            return modelList;
        }
 
        /// <summary>
        /// 通过货位ID获取视图
        /// 主要是在改货位状态时判断是否有库存
        /// </summary>
        /// <param name="gsID"></param>
        /// <returns></returns>
        public View_QueryStockListModel GetModelByGSID(int gsID)
        {
            return dal.GetModelByGSID(gsID);
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月10日
        /// 内容:通过库房、排列层查找库存
        /// </summary>
        /// <param name="storeHouseName"></param>
        /// <param name="rowth"></param>
        /// <param name="columnth"></param>
        /// <param name="layerth"></param>
        /// <returns></returns>
        public View_QueryStockListModel GetModelByGsName(string storeHouseName,string rowth,string columnth,string layerth)
        {
            return dal.GetModelByGsName(storeHouseName, rowth, columnth, layerth);
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年4月16日
        /// 内容:通过货位状态获取货位模型
        /// </summary>
        public View_QueryStockListModel GetOutHouseModel(int goodsSiteID, EnumGSStoreStatus gsStoreStatus, EnumGSRunStatus gsRunStatus)
        {
            return dal.GetOutHouseModel(goodsSiteID, gsStoreStatus.ToString(), gsRunStatus.ToString());
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年4月16日
        /// 内容:通过货位ID获取模型
        /// </summary>
        /// <param name="goodsSiteID"></param>
        /// <returns></returns>
        public View_QueryStockListModel GetModelByGsID(int goodsSiteID)
        {
            string whereStr = "GoodsSiteID =" + goodsSiteID;
            List<View_QueryStockListModel> modelList = GetModelList(whereStr);
            if (modelList != null && modelList.Count > 0)
            {
                return modelList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年4月16日
        /// 内容:通过货位ID获取模型
        /// </summary>
        /// <param name="goodsSiteID"></param>
        /// <returns></returns>
        public View_QueryStockListModel GetModelByManaTaskID(Int64 manaTaskID)
        {
            string whereStr = "ManaTaskID =" + manaTaskID;
            List<View_QueryStockListModel> modelList = GetModelList(whereStr);
            if (modelList != null && modelList.Count > 0)
            {
                return modelList[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年4月16日
        /// 内容:通过库存列表ID获取模型
        /// </summary>
        /// <param name="goodsSiteID"></param>
        /// <returns></returns>
        public View_QueryStockListModel GetModelByStockListID(Int64 stockListID)
        {
            string whereStr = "StockListID =" + stockListID;
            List<View_QueryStockListModel> modelList = GetModelList(whereStr);
            if (modelList != null && modelList.Count > 0)
            {
                return modelList[0];
            }
            else
            {
                return null;
            }
        }
        #endregion  ExtensionMethod
    }
}

