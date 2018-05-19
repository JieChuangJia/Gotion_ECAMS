/**  版本信息模板在安装目录下，可自行修改。
* ControlTask.cs
*
* 功 能： N/A
* 类 名： ControlTask
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
using System.Collections;
using ECAMSDataAccess;
using ECAMSModel;

namespace ECAMSDataAccess
{
	/// <summary>
	/// 控制接口
	/// </summary>
	public partial class ControlTaskBll
	{
		private readonly ECAMSDataAccess.ControlTaskDal dal=new ECAMSDataAccess.ControlTaskDal();
		public ControlTaskBll()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ControlTaskID)
        {
            return dal.Exists(ControlTaskID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ControlTaskModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ControlTaskModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ControlTaskID)
        {

            return dal.Delete(ControlTaskID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ControlTaskIDlist)
        {
            return dal.DeleteList(ControlTaskIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ControlTaskModel GetModel(long ControlTaskID)
        {

            return dal.GetModel(ControlTaskID);
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
        public List<ControlTaskModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ControlTaskModel> DataTableToList(DataTable dt)
        {
            List<ControlTaskModel> modelList = new List<ControlTaskModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ControlTaskModel model;
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
        /// 获取插入控制任务hash
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Hashtable GetAddControlTaskHash(ControlTaskModel model)
        {
            return dal.GetAddModelHash(model);
        }
		/// <summary>
        /// 获取已完成的控制任务列表
        /// </summary>
        /// <returns></returns>
        public List<ControlTaskModel> GetCompleteControlTask()
        {
            string whereStr = "TaskStatus = '" + EnumTaskStatus.已完成.ToString() + "'";
            return GetModelList(whereStr);
        }

        /// <summary>
        /// 获取已完成的控制任务列表
        /// </summary>
        /// <returns></returns>
        public List<ControlTaskModel> GetUndoControlTaskList()
        {
            string whereStr = "TaskStatus = '" + EnumTaskStatus.任务撤销.ToString() + "'";
            return GetModelList(whereStr);
        }
		public Hashtable GetDeleteModelHash(long controlTaskID)
        {
            return dal.GetDeleteModelHash(controlTaskID);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllListByTime(DateTime dtStart,DateTime dtEnd)
        {
            return GetList("CreateTime between  '" + dtStart.ToString("yyyy-MM-dd 00:00:00") + "' and '"
                + dtEnd.ToString("yyyy-MM-dd 23:59:59") + "'");
        }

        public  DataTable GetCtrlDatatable(string taskName, string taskStatus,string storeHouseName,string createMode,string taskType)
        {
            DataSet ds = dal.GetCtrlModelList(taskName, taskStatus, storeHouseName, createMode, taskType);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
            
        }

        public List<ControlTaskModel> GetCtrlModelList(string taskName, string taskStatus,string storeHouseName,string createMode,string taskType)
        {
            DataSet ds = dal.GetCtrlModelList(taskName, taskStatus,storeHouseName,createMode,taskType);

            return DataTableToList(ds.Tables[0]);
        }
		#endregion  ExtensionMethod
        #region 控制层接口，zwx
		public bool Clear()
        {
            return dal.Clear();
        }
        /// <summary>
        /// 得到待执行的任务,按时间排序，得到最前的
        /// </summary>
        /// <param name="taskType">业务流程类型</param>
        /// <returns></returns>
        public ControlTaskModel GetTaskToRun(string taskType)
        {
           
            return dal.GetFirstTaskToRun(taskType);
        }

        /// <summary>
        /// 得到正在执行的任务
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public ControlTaskModel GetRunningTask(string taskType)
        {
            return dal.GetRunningTask(taskType);
        }
        /// <summary>
        /// 根据控制码获得控制任务
        /// </summary>
        /// <param name="ctlCode"></param>
        /// <returns></returns>
        public ControlTaskModel GetTaskByControlCode(string ctlCode)
        {
            string strWhere = " ControlCode='" + ctlCode.PadLeft(6,'0') + "'";
            IList<ControlTaskModel> taskList = dal.GetConditionedModel(strWhere);
            if (taskList.Count > 0)
            {
                return taskList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有控制任务
        /// </summary>
        /// <returns></returns>
        public DataSet GetControlList()
        {
            return dal.GetControlList();
        }

        #endregion
    }
}

