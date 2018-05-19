/**  版本信息模板在安装目录下，可自行修改。
* StockList.cs
*
* 功 能： N/A
* 类 名： StockList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:05   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：np　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using ECAMSDataAccess;
using ECAMSModel;

namespace ECAMSDataAccess
{
	/// <summary>
	/// 物料信息表
	/// </summary>
	public partial class StockListBll
	{
		private readonly ECAMSDataAccess.StockListDal dal=new ECAMSDataAccess.StockListDal();
        private readonly TaskTypeBll bllTaskType = new TaskTypeBll();
		public StockListBll()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long StockListID)
        {
            return dal.Exists(StockListID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(StockListModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(StockListModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long StockListID)
        {

            return dal.Delete(StockListID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string StockListIDlist)
        {
            return dal.DeleteList(StockListIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public StockListModel GetModel(long StockListID)
        {

            return dal.GetModel(StockListID);
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
        public List<StockListModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<StockListModel> DataTableToList(DataTable dt)
        {
            List<StockListModel> modelList = new List<StockListModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                StockListModel model;
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllListByTime(DateTime dtStart,DateTime dtEnd)
        {
            string whereStr = "InHouseTime >= '" + dtStart.ToString("yyyy-MM-dd 00:00:00") 
                + "'  and  InHouseTime <= '" + dtEnd.ToString("yyyy-MM-dd 23:59:59")+ "'";

            return GetList(whereStr);
        }

        /// <summary>
        /// 获取符合条件自动出库库存列表
        /// </summary>
        /// <param name="productStatus">物料状态</param>
        /// <param name="timeInterval">当前时间减去更新时间的时间差（单位小时）</param>
        /// <returns></returns>
        public List<StockListModel> GetModelList(string productStatus,int timeInterval)
        { 
            string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");
            string whereStr = "ProductStatus = '" + productStatus + "' and datediff(hour,UpdateTime,'" + dtNow + "') >=" + timeInterval
                + " and UpdateTime != ''  ";
            List<StockListModel> stockListModelList = GetModelList(whereStr);
            return stockListModelList;
        }
        /// <summary>
        ///  获取符合条件自动出库库存列表
        /// </summary>
        /// <param name="productStatus">物料状态</param>
        /// <param name="timeInterval">当前时间减去更新时间的时间差（单位小时）</param>
        /// <returns></returns>
        public List<StockListModel> GetTimeArriveModelList(string productStatus, int timeInterval, Dictionary<string, List<string>> outStorageBatchDic)
        {
            DataTable timeArriveModelData = dal.GetTimeArriveModelList(productStatus, timeInterval, outStorageBatchDic);
            if (timeArriveModelData != null)
            {
                return DataTableToList(timeArriveModelData);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///  获取删除库存列表hash
        /// </summary>
        /// <param name="stockListID"></param>
        /// <returns></returns>
        public Hashtable GetDeleteModelHs(long stockListID)
        {
            return dal.GetDeleteModelHs(stockListID);
        }

        /// <summary>
        /// 获取库存列表
        /// </summary>
        /// <param name="houseName"></param>
        /// <param name="productStatus"></param>
        /// <param name="workFolwTime"></param>
        /// <returns></returns>
        public List<StockListModel> GetModelList(string houseName,string rowth,string columnth,string layerth, string productStatus, EnumWorkFolwStatus workFlowStatus)
        {
            List<StockListModel> modelList = null;
            string whereTaskType = "ProductStartStatus ='" + productStatus + "'";
            if (productStatus == "所有")
            {
                whereTaskType = "ProductStartStatus = '" +EnumProductStatus.B1库静置10天.ToString() +"'"
                + " or ProductStartStatus ='" + EnumProductStatus.A1库静置10小时.ToString()+"'"
                + " or ProductStartStatus = '"+EnumProductStatus.A1库老化3天.ToString() + "'";
            }
            List<TaskTypeModel> taskTypeList = bllTaskType.GetModelList(whereTaskType);

            if (taskTypeList!= null&&taskTypeList.Count > 0)
            {
                string dtNow =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int workFlowTime = (int)taskTypeList[0].NeedTime;
                
              DataSet ds =  dal.GetQueryList(houseName, rowth, columnth, layerth, productStatus, workFlowStatus.ToString(), workFlowTime);
                 
            }
            return modelList;
        }

        /// <summary>
        /// 通过任务id获取库存列表
        /// </summary>
        /// <param name="manaTaskID"></param>
        /// <returns></returns>
        public StockListModel GetModelByManaTaskID(long manaTaskID)
        {
            string whereStr = "ManaTaskID = " + manaTaskID ;
            List<StockListModel> stockModelList = GetModelList(whereStr);
            if (stockModelList.Count > 0)
            {
                return stockModelList[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 通过库房名称及货位查找货位
        /// </summary>
        /// <param name="storeHouse"></param>
        /// <param name="goodSiteName"></param>
        /// <returns></returns>
        public StockListModel GetStockListModel(EnumStoreHouse storeHouse, string goodSiteName)
        {
            string whereStr = "StoreHouseName = '" + storeHouse.ToString() + "' and GoodsSiteName='"+goodSiteName+"'";
            List<StockListModel> stockModelList = GetModelList(whereStr);
            if (stockModelList.Count > 0)
            {
                return stockModelList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 通过库房名称及货位查找货位
        /// </summary>
        /// <param name="storeHouse"></param>
        /// <param name="goodSiteName"></param>
        /// <returns></returns>
        public List<StockListModel> GetStockList(EnumStoreHouse storeHouse, string goodSiteName)
        {
            string whereStr = "StoreHouseName = '" + storeHouse.ToString() + "' and GoodsSiteName='" + goodSiteName + "'";
            List<StockListModel> stockModelList = GetModelList(whereStr);
            return stockModelList;
           
        }

        /// <summary>
        /// 库存id获取库存列表
        /// </summary>
        /// <param name="manaTaskID"></param>
        /// <returns></returns>
        public StockListModel GetModelByStockID(long stockID)
        {
            string whereStr = "StockID = " + stockID + " and UpdateTime != ''"; ;
            List<StockListModel> stockModelList = GetModelList(whereStr);
            if (stockModelList.Count > 0)
            {
                return stockModelList[0];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 更需库存列表的更新时间
        /// </summary>
        /// <param name="completeTime"></param>
        /// <param name="stockListID"></param>
        /// <returns></returns>
        public bool UpdateCompleteTime(DateTime completeTime,long stockListID)
        {
            return dal.UpdateCompleteTime(completeTime, stockListID);
        }

        public Hashtable GetUpdateCompleteTimeHs(DateTime completeTime, long stockListID)
        {
            return dal.GetUpdateCompleteTimeHs(completeTime, stockListID);
        }

        public bool Clear()
        {
            return dal.Clear();
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月11日
        /// 内容:获取所有库存产品的批次号
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllProductBatchNums()
        {
            return dal.GetAllProductBatchNums();
        }
		#endregion  ExtensionMethod
	}
}

