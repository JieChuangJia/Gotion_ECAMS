/**  版本信息模板在安装目录下，可自行修改。
* GoodsSite.cs
*
* 功 能： N/A
* 类 名： GoodsSite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-11-24 17:33:01   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：np　　　　　　　　　　　　　│
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
	/// 货位信息表
	/// </summary>
	public partial class GoodsSiteBll
	{
		private readonly ECAMSDataAccess.GoodsSiteDal dal=new ECAMSDataAccess.GoodsSiteDal();
		public GoodsSiteBll()
		{
      
        }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public long GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GoodsSiteID)
        {
            return dal.Exists(GoodsSiteID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ECAMSDataAccess.GoodsSiteModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ECAMSDataAccess.GoodsSiteModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GoodsSiteID)
        {

            return dal.Delete(GoodsSiteID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string GoodsSiteIDlist)
        {
            return dal.DeleteList(GoodsSiteIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ECAMSDataAccess.GoodsSiteModel GetModel(int GoodsSiteID)
        {

            return dal.GetModel(GoodsSiteID);
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
        public List<ECAMSDataAccess.GoodsSiteModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ECAMSDataAccess.GoodsSiteModel> DataTableToList(DataTable dt)
        {
            List<ECAMSDataAccess.GoodsSiteModel> modelList = new List<ECAMSDataAccess.GoodsSiteModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ECAMSDataAccess.GoodsSiteModel model;
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
        
        public GoodsSiteModel GetGoodsSite(EnumStoreHouse storeHouse, string deviceID)
        {
            int storeAreaID = 0;
            if (storeHouse == EnumStoreHouse.A1库房)
            {
                storeAreaID = 2;
            }
            else if (storeHouse == EnumStoreHouse.B1库房)
            {
                storeAreaID = 4;
            }
           
            string whereStr = "DeviceID = '" + deviceID + "' and StoreAreaID = " + storeAreaID;
            List<GoodsSiteModel> goodsSiteList = GetModelList(whereStr);
            if (goodsSiteList != null && goodsSiteList.Count > 0)
            {
               return goodsSiteList[0];
            }
            else
            {
                return null;
            }
             
        }

        public GoodsSiteModel GetGoodsSiteByGSName(EnumStoreHouse storeHouse, string gsName)
        {
            int storeAreaID = 0;
            if (storeHouse == EnumStoreHouse.A1库房)
            {
                storeAreaID = 2;
            }
            else if (storeHouse == EnumStoreHouse.B1库房)
            {
                storeAreaID = 4;
            }

            string whereStr = "GoodsSiteName = '" + gsName + "' and StoreAreaID = " + storeAreaID;
            List<GoodsSiteModel> goodsSiteList = GetModelList(whereStr);
            if (goodsSiteList != null && goodsSiteList.Count > 0)
            {
                return goodsSiteList[0];
            }
            else
            {
                return null;
            }

        }

        public GoodsSiteModel GetGoodsSite(EnumStoreHouse storeHouse, int rowth, int columnth, int layerth)
        {
            GoodsSiteModel goodsSite = null;
            string whereStr = "";
            List<GoodsSiteModel> goodsSiteList = null;
            if (storeHouse == EnumStoreHouse.A1库房)
            {
                whereStr = "GoodsSiteRow = " + rowth + " and GoodsSiteColumn = " + columnth + " and GoodsSiteLayer ="
                    + layerth + " and LogicStoreAreaID = 4 and GoodsSiteRunStatus != '"+ EnumGSRunStatus.异常.ToString() + "'" ;
            }
            else if (storeHouse == EnumStoreHouse.B1库房)
            {
                whereStr = "GoodsSiteRow = " + rowth + " and GoodsSiteColumn = " + columnth + " and GoodsSiteLayer ="
                       + layerth + " and LogicStoreAreaID = 2 and GoodsSiteRunStatus != '" + EnumGSRunStatus.异常.ToString() + "'"; ;
            }
            goodsSiteList = GetModelList(whereStr);
            if (goodsSiteList.Count > 0)
            {
                goodsSite = goodsSiteList[0];
            }
            return goodsSite;
         
        }

        /// <summary>
        /// 从第一排第一列第一层开始查找
        /// </summary>
        /// <param name="logicAreaID">逻辑区域ID</param>
        /// <param name="gsStoreStatus">存储状态</param>
        /// <param name="gsType">货位类型</param>
        /// <returns></returns>
        public   GoodsSiteModel GetGoodsSite(int logicAreaID, EnumGSStoreStatus gsStoreStatus
            , EnumGSRunStatus gsRunStatus, EnumGSType gsType)
        {
            GoodsSiteModel goodsSite = null;
            //bool isFind = false;
            //int rows = 0;
            //int columns = 0;
            //int layers = 0;
            //GetRowColumnLayer(logicAreaID, out rows, out columns, out layers);

            //for (int i = 1; i < rows + 1; i++)
            //{
            //    if (isFind == true)
            //    {
            //        break;
            //    }
            //    for (int j = 1; j < columns + 1; j++)
            //    {
            //        if (isFind == true)
            //        {
            //            break;
            //        }
            //        for (int k = 1; k < layers + 1; k++)
            //        {
            //            string wereStr = "GoodsSiteRow=" + i + " and GoodsSiteColumn =" + j + " and GoodsSiteLayer =" + k
            //                + " and LogicStoreAreaID=" + logicAreaID + " and GoodsSiteStoreStatus ='"
            //                + gsStoreStatus.ToString() + "' and GoodsSiteType ='" + gsType.ToString() + "'"
            //                + "and GoodsSiteRunStatus = '" + gsRunStatus.ToString() +"'";
            //            //string wereStr = "GoodsSiteRow=" + i + " and GoodsSiteColumn =" + j
            //            //    + " and LogicStoreAreaID=" + logicAreaID + " and GoodsSiteStoreStatus ='"
            //            //    + gsStoreStatus.ToString() + "' and GoodsSiteType ='" + gsType.ToString() + "'";
            //            List<ECAMSDataAccess.GoodsSiteModel> goodsSiteList = GetModelList(wereStr);
            //            if (goodsSiteList.Count > 0)
            //            {
            //                goodsSite = goodsSiteList[0];
            //                isFind = true;
            //                break;
            //            }
            //        }
            //    }
            //}

            string wereStr = "LogicStoreAreaID=" + logicAreaID + " and GoodsSiteStoreStatus ='"
                 + gsStoreStatus.ToString() + "' and GoodsSiteRunStatus = '" + gsRunStatus .ToString()+ "' and GoodsSiteType ='"
                 + gsType.ToString() + "'order by GoodsSiteColumn asc,"
            + "GoodsSiteRow asc,GoodsSiteLayer asc";
            List<ECAMSDataAccess.GoodsSiteModel> goodsSiteList = GetModelList(wereStr);
            if (goodsSiteList.Count > 0)
            {
                goodsSite = goodsSiteList[0];
            }
            return goodsSite;
        }

        public bool FormatGoogsSite()
        {
            return dal.FormatGoogsSite(EnumGSStoreStatus.空货位.ToString(), EnumGSRunStatus.待用.ToString(),EnumTaskCategory.出入库.ToString());
        }

        /// <summary>
        /// 只更新控制接口状态
        /// </summary>
        /// <param name="interfaceStatus"></param>
        /// <param name="controlInterfaceID"></param>
        /// <returns></returns>
        public bool UpdateGoodsSiteStatus(string storeStatus,string runStatus,int goodssiteID)
        {
            return dal.Update(storeStatus, runStatus, goodssiteID);

        }
        /// <summary>
        /// 只更新控制接口状态
        /// </summary>
        /// <param name="interfaceStatus"></param>
        /// <param name="controlInterfaceID"></param>
        /// <returns></returns>
        public bool UpdateGoodsSiteStatus(string storeStatus, string runStatus,string gsInOrOut, int goodssiteID)
        {
            return dal.Update(storeStatus, runStatus,gsInOrOut, goodssiteID);

        }
        /// <summary>
        /// 获取排、列、层最大值 根据逻辑区ID号
        /// </summary>
        /// <param name="logicStoreAreaId"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="layers"></param>
         public void GetRowColumnLayer(int logicStoreAreaId,out int rows, out int columns, out int layers)
        {
            int rowsTemp = 0;
            int columnsTemp = 0;
            int layersTemp = 0;
            DataSet dsParameters = dal.GetRowColumnLayer(logicStoreAreaId);
            if (dsParameters != null && dsParameters.Tables.Count > 0 && dsParameters.Tables[0].Rows.Count > 0)
            {
                rowsTemp = int.Parse(dsParameters.Tables[0].Rows[0]["GoodsSiteRow"].ToString());
                columnsTemp = int.Parse(dsParameters.Tables[0].Rows[0]["GoodsSiteColumn"].ToString());
                layersTemp = int.Parse(dsParameters.Tables[0].Rows[0]["GoodsSiteLayer"].ToString());
            }
            rows = rowsTemp;
            columns = columnsTemp;
            layers = layersTemp;
        }

        /// <summary>
        /// 更新货位状态
        /// </summary>
        /// <param name="storeStatus"></param>
        /// <param name="runStatus"></param>
        /// <param name="goodsSiteID"></param>
        /// <returns></returns>
        public Hashtable GetUpdateModelHs(string storeStatus,string runStatus,string inOrOutStore,int goodsSiteID)
        {
            return dal.GetUpdateModelHs(storeStatus, runStatus, inOrOutStore, goodsSiteID);
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月10日
        /// 内容:更新货位状态
        /// </summary>
        /// <param name="storeStatus"></param>
        /// <param name="runStatus"></param>
        /// <param name="inOrOutStore"></param>
        /// <param name="goodsSiteID"></param>
        /// <returns></returns>
        public bool UpdateModelByGsID(string storeStatus, string runStatus, string inOrOutStore, int goodsSiteID)
        {
            return dal.UpdateModelByGsID(storeStatus, runStatus, inOrOutStore, goodsSiteID);
        }

        public GoodsSiteModel GetModel(string storeStatus, string runStatus, int goodsSiteID)
        {
            return dal.GetModel(storeStatus, runStatus, goodsSiteID);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="storeHouse"></param>
        /// <param name="rowth"></param>
        /// <returns></returns>
        public DataTable GetGsData(EnumStoreHouse storeHouse, int rowth)
        {
            string whereStr = "";
            if (storeHouse == EnumStoreHouse.A1库房)
            {
                whereStr += "StoreAreaID = 2 and GoodsSiteRow="+ rowth;
            }
            else if (storeHouse == EnumStoreHouse.B1库房)
            {
                whereStr += "StoreAreaID = 4 and GoodsSiteRow=" + rowth;
            }

            DataSet ds = GetList(whereStr);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }


        public DataTable GetStatusNum(EnumStoreHouse storeHouse, int rowth)
        {
            int storeAreaID = 2;
            if (storeHouse == EnumStoreHouse.A1库房)
            {
                storeAreaID = 2;
            }
            else if (storeHouse == EnumStoreHouse.B1库房)
            {
                storeAreaID = 4;
            }
            return dal.GetStatusNum(storeAreaID, rowth);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public GoodsSiteModel GetOutHouseModel(int GoodsSiteID,EnumGSStoreStatus gsStoreStatus, EnumGSRunStatus gsRunStatus)
        {
            return dal.GetOutHouseModel(GoodsSiteID, gsStoreStatus.ToString(), gsRunStatus.ToString());
        }

        public List<GoodsSiteModel> GetRowGs(int rowth, int storeAreaID)
        {
            DataTable dt = dal.GetRowGs(rowth, storeAreaID);
            List<GoodsSiteModel> gsList = new List<GoodsSiteModel>();
            if (dt != null)
            {
                return DataTableToList(dt);
            }
            else
            {
                return null;
            }
          
        }

        public List<GoodsSiteModel> GetColumnGs(int rowth, int columnth, int storeAreaID)
        {
            DataTable dt = dal.GetColumnGs(rowth,columnth, storeAreaID);
            List<GoodsSiteModel> gsList = new List<GoodsSiteModel>();
            if (dt != null)
            {
                return DataTableToList(dt);
            }
            else
            {
                return null;
            }
        }

        public GoodsSiteModel GetGoodsSiteByRCL(EnumStoreHouse StoreHouse, int rowth, int columnth, int layerth)
        {
            int storeAreaID = 0;
            if (StoreHouse == EnumStoreHouse.A1库房)
            {
                storeAreaID = 2;
            }
            else if (StoreHouse == EnumStoreHouse.B1库房)
            {
                storeAreaID = 4;
            }
            string whereStr = "StoreAreaID=" + storeAreaID + " and GoodsSiteRow=" + rowth + " and GoodsSiteColumn" + columnth + " and GoodsSiteLayer" + layerth;
            List<GoodsSiteModel> goodsSiteList = GetModelList(whereStr);
            if (goodsSiteList != null&& goodsSiteList.Count>0)
            {
                return goodsSiteList[0];
            }
            else
            {
                return null;
            }
        }

        public List<GoodsSiteModel> GetLayerGs(int rowth, int layer, int storeAreaID)
        {
            DataTable dt = dal.GetLayerGs(rowth, layer, storeAreaID);
            List<GoodsSiteModel> gsList = new List<GoodsSiteModel>();
            if (dt != null)
            {
                return DataTableToList(dt);
            }
            else
            {
                return null;
            }
        }
		#endregion  ExtensionMethod
	}
}

