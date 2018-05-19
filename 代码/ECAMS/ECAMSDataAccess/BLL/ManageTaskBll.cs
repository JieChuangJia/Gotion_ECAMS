/**  版本信息模板在安装目录下，可自行修改。
* ManageTask.cs
*
* 功 能： N/A
* 类 名： ManageTask
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:01   N/A    初版
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
namespace ECAMSDataAccess
{
	/// <summary>
	/// 逻辑区域
	/// </summary>
	public partial class ManageTaskBll
	{
		private readonly ECAMSDataAccess.ManageTaskDal dal=new ECAMSDataAccess.ManageTaskDal();
		public ManageTaskBll()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long TaskID)
        {
            return dal.Exists(TaskID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ManageTaskModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ManageTaskModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long TaskID)
        {

            return dal.Delete(TaskID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string TaskIDlist)
        {
            return dal.DeleteList(TaskIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ManageTaskModel GetModel(long TaskID)
        {

            return dal.GetModel(TaskID);
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
        public List<ManageTaskModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ManageTaskModel> DataTableToList(DataTable dt)
        {
            List<ManageTaskModel> modelList = new List<ManageTaskModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ManageTaskModel model;
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
            string whereStr = "TaskCreateTime >='" + dtStart.ToString("yyyy-MM-dd 00:00:00") + "' and TaskCreateTime <= '" 
                +dtEnd.ToString("yyyy-MM-dd 23:59:59") + "'" ;
            return GetList(whereStr);
        }

        public Hashtable GetDeleteModelHash(long taskID)
        {
            return dal.GetDeleteModelHash(taskID);
        }

        public bool UpdateCompleteTime(DateTime completeTime, long manageTaskID)
        {
            return dal.UpdateCompleteTime(completeTime, manageTaskID);
        }

        public Hashtable GetUpdateCompleteTimeHs(DateTime completeTime, long manageTaskID)
        {
            return dal.GetUpdateCompleteTimeHs(completeTime, manageTaskID);
        }

        public bool Clear()
        {
            return dal.Clear();
        }

        public DataSet GetManaTaskList()
        {
            return dal.GetManaTaskList();
        }
		#endregion  ExtensionMethod
	}
}

