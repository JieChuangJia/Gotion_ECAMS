/**  版本信息模板在安装目录下，可自行修改。
* Stock.cs
*
* 功 能： N/A
* 类 名： Stock
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:04   N/A    初版
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
namespace ECAMSDataAccess
{
	/// <summary>
	/// 物料信息表
	/// </summary>
	public partial class StockBll
	{
		private readonly ECAMSDataAccess.StockDal dal=new ECAMSDataAccess.StockDal();
		public StockBll()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StockID)
		{
			return dal.Exists(StockID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(ECAMSDataAccess.StockModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ECAMSDataAccess.StockModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long StockID)
		{
			
			return dal.Delete(StockID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string StockIDlist )
		{
			return dal.DeleteList(StockIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ECAMSDataAccess.StockModel GetModel(long StockID)
		{
			
			return dal.GetModel(StockID);
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ECAMSDataAccess.StockModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ECAMSDataAccess.StockModel> DataTableToList(DataTable dt)
		{
			List<ECAMSDataAccess.StockModel> modelList = new List<ECAMSDataAccess.StockModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ECAMSDataAccess.StockModel model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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
        ///  获取添加库存hash用于事物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Hashtable GetAddModelHash(StockModel model)
        {
            return dal.GetAddModelHash(model);
        }

        /// <summary>
        /// 获取删除库存hash
        /// </summary>
        /// <param name="stockID"></param>
        /// <returns></returns>
        public Hashtable GetDeleteModelHash(long stockID)
        {
            return dal.GetDeleteModelHash(stockID);
        }

        /// <summary>
        /// 获取最大ID查询最后一条插入数据ID
        /// </summary>
        /// <returns></returns>
        public long GetMaxID()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 通过货位ID获取库存ID
        /// 如果等于0为获取失败
        /// </summary>
        /// <param name="gsID"></param>
        /// <returns></returns>
        public long GetStockIDByGsID(int gsID)
        {
            string whereStr = "GoodsSiteID = " + gsID;
            List<StockModel> modelList = GetModelList(whereStr);
            if (modelList.Count > 0)
            {
                StockModel model = GetModelList(whereStr)[0];
            
                return model.StockID;
            }
            else
            {
                return 0;
            }
           
        }

        public bool DeleteModelByGsID(int goodssiteID)
        {
            return dal.DeleteModelByGsID(goodssiteID);
        }

        public StockModel GetModelByGsID(int goodssiteID)
        {
            string whereStr = "GoodsSiteID = " + goodssiteID;
            List<StockModel> modelList = GetModelList(whereStr);
            if (modelList.Count > 0)
            {
                return modelList[0];
            }   
            else
            {
                return null;
            }
        }

        public bool Clear()
        {
            return dal.Clear();
        }
		#endregion  ExtensionMethod
	}
}

