/**  版本信息模板在安装目录下，可自行修改。
* LogicStoreArea.cs
*
* 功 能： N/A
* 类 名： LogicStoreArea
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:00   N/A    初版
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

using ECAMSDataAccess;
namespace ECAMSDataAccess
{
	/// <summary>
	/// 逻辑区域
	/// </summary>
	public partial class LogicStoreAreaBll
	{
		private readonly ECAMSDataAccess.LogicStoreAreaDal dal=new ECAMSDataAccess.LogicStoreAreaDal();
		public LogicStoreAreaBll()
		{}
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
		public bool Exists(int LogicStoreAreaID)
		{
			return dal.Exists(LogicStoreAreaID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ECAMSDataAccess.LogicStoreAreaModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ECAMSDataAccess.LogicStoreAreaModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int LogicStoreAreaID)
		{
			
			return dal.Delete(LogicStoreAreaID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string LogicStoreAreaIDlist )
		{
			return dal.DeleteList(LogicStoreAreaIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ECAMSDataAccess.LogicStoreAreaModel GetModel(int LogicStoreAreaID)
		{
			
			return dal.GetModel(LogicStoreAreaID);
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
		public List<ECAMSDataAccess.LogicStoreAreaModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ECAMSDataAccess.LogicStoreAreaModel> DataTableToList(DataTable dt)
		{
			List<ECAMSDataAccess.LogicStoreAreaModel> modelList = new List<ECAMSDataAccess.LogicStoreAreaModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ECAMSDataAccess.LogicStoreAreaModel model;
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
        public int GetLogicIDByLogicName(string logicName)
        {
            int logicID = 0;
            DataSet dsLogic = GetList("LogicStoreAreaName = '" + logicName + "'");
            if (dsLogic != null && dsLogic.Tables.Count > 0 && dsLogic.Tables[0].Rows.Count > 0)
            {
                logicID = int.Parse(dsLogic.Tables[0].Rows[0]["LogicStoreAreaID"].ToString());
            }
            return logicID;
        }

        /// <summary>
        /// 通过ID获取逻辑区域名称
        /// </summary>
        /// <param name="logicID"></param>
        /// <returns></returns>
        public string GetLogicNameByID(int logicID)
        {
            string logicAreaName = GetModel(logicID).LogicStoreAreaName;
            return logicAreaName;
        }
		#endregion  ExtensionMethod
	}
}

