/**  版本信息模板在安装目录下，可自行修改。
* Device.cs
*
* 功 能： N/A
* 类 名： Device
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:49:59   N/A    初版
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
	/// 设备表
	/// </summary>
	public partial class DeviceBll
	{
		private readonly ECAMSDataAccess.DeviceDal dal=new ECAMSDataAccess.DeviceDal();
		public DeviceBll()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DviceID)
		{
			return dal.Exists(DviceID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ECAMSDataAccess.DeviceModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ECAMSDataAccess.DeviceModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string DviceID)
		{
			
			return dal.Delete(DviceID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string DviceIDlist )
		{
			return dal.DeleteList(DviceIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ECAMSDataAccess.DeviceModel GetModel(string DviceID)
		{
			
			return dal.GetModel(DviceID);
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
		public List<ECAMSDataAccess.DeviceModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ECAMSDataAccess.DeviceModel> DataTableToList(DataTable dt)
		{
			List<ECAMSDataAccess.DeviceModel> modelList = new List<ECAMSDataAccess.DeviceModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ECAMSDataAccess.DeviceModel model;
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
        public bool UpdateDevStatus(string devID, string devStatus)
        {
            DeviceModel devModel = dal.GetModel(devID);
            if (devModel == null)
            {
                return false;
            }
            devModel.DeviceStatus = devStatus;
            return dal.Update(devModel);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ECAMSDataAccess.DeviceModel> GetShowDevList()
        {
            string strWhere = "DeviceType in('堆垛机','机械手')";
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
		#endregion  ExtensionMethod
	}
}

