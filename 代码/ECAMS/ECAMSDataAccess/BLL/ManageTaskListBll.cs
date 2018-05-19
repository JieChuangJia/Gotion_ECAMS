/**  版本信息模板在安装目录下，可自行修改。
* ManageTaskList.cs
*
* 功 能： N/A
* 类 名： ManageTaskList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:02   N/A    初版
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
	/// 逻辑区域
	/// </summary>
	public partial class ManageTaskListBll
	{
		private readonly ECAMSDataAccess.ManageTaskListDal dal=new ECAMSDataAccess.ManageTaskListDal();
		public ManageTaskListBll()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long TaskListID)
        {
            return dal.Exists(TaskListID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ManageTaskListModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ManageTaskListModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long TaskListID)
        {

            return dal.Delete(TaskListID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string TaskListIDlist)
        {
            return dal.DeleteList(TaskListIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManageTaskListModel GetModel(long TaskListID)
        {

            return dal.GetModel(TaskListID);
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
        public List<ManageTaskListModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ManageTaskListModel> DataTableToList(DataTable dt)
        {
            List<ManageTaskListModel> modelList = new List<ManageTaskListModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ManageTaskListModel model;
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
     
        public ManageTaskListModel GetModelByManaTaskID(long manaTaskID)
        {
            string whereStr = "TaskID = " + manaTaskID;
            List<ManageTaskListModel> manaModelList = GetModelList(whereStr);
            if(manaModelList.Count>0)
            {
                return manaModelList[0];
            }
            else
            {
            return null;
            }
        }

        public ManageTaskListModel GetModelByStockListID(long stockListID)
        {
            string whereStr = "StockListID = " + stockListID;
            List<ManageTaskListModel> manaModelList = GetModelList(whereStr);
            if (manaModelList.Count > 0)
            {
                return manaModelList[0];
            }
            else
            {
                return null;
            }
        }

        public Hashtable GetDeleteModelHash(long manaTaskID)
        {
            return dal.GetDeleteModelHash(manaTaskID);
        }

        public bool UpdateCompleteTime(DateTime completeTime, long manaTaskListID)
        {
            return dal.UpdateCompleteTime(completeTime, manaTaskListID);
        }

        public Hashtable GetUpdateCompleteTimeHs(DateTime completeTime, long manaTaskListID)
        {
            return dal.GetUpdateCompleteTimeHs(completeTime, manaTaskListID);
        }
		#endregion  ExtensionMethod
	}
}

