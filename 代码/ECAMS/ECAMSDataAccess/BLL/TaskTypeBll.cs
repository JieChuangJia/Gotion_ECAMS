using System;
using System.Data;
using System.Collections.Generic;
using ECAMSModel;
using ECAMSDataAccess;
namespace ECAMSDataAccess
{
    /// <summary>
    /// TaskType
    /// </summary>
    public partial class TaskTypeBll
    {
        private readonly TaskTypeDal dal = new TaskTypeDal();
        public TaskTypeBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TaskTypeCode)
        {
            return dal.Exists(TaskTypeCode);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TaskTypeModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TaskTypeModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TaskTypeCode)
        {

            return dal.Delete(TaskTypeCode);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string TaskTypeCodelist)
        {
            return dal.DeleteList(TaskTypeCodelist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TaskTypeModel GetModel(int TaskTypeCode)
        {

            return dal.GetModel(TaskTypeCode);
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
        public List<TaskTypeModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TaskTypeModel> DataTableToList(DataTable dt)
        {
            List<TaskTypeModel> modelList = new List<TaskTypeModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TaskTypeModel model;
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
        /// 获取任务类型
        /// </summary>
        /// <param name="device">申请任务的设备号</param>
        /// <param name="taskType">任务类型（出库、入库）</param>
        /// <returns></returns>
        public TaskTypeModel GetTaskTypeByDevice(string device, string taskType)
        {
            string whereStr = string.Empty;
            if (taskType == EnumTaskCategory.入库.ToString())
            {
                whereStr = "StartDevice ='" + device + "' and TaskTypeValue='" + taskType + "'";
            }
            else if (taskType == EnumTaskCategory.出库.ToString())
            {
                whereStr = "EndDevice ='" + device + "' and TaskTypeValue='" + taskType + "'";
            }

            List<TaskTypeModel> taskTypeList =  GetModelList(whereStr);
            if (taskTypeList.Count == 0)
            {
                return null;
            }
            else
            {
                return taskTypeList[0];
            }
          
        }

        /// <summary>
        /// 获取需要设置手动还是自动的任务流程
        /// 通过需要时间来获取
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeModel> GetAutoTaskList(string taskTypeName1,string taskTypeName2,string taskTypeName3)
        {
            //string whereStr = "TaskTypeName  = '分容出库' or TaskTypeName = '一次检测出库' or TaskTypeName = '二次检测出库'";
            string whereStr = "TaskTypeName  = '" + taskTypeName1 + "' or TaskTypeName = '" + taskTypeName2 + "' or TaskTypeName = '" + taskTypeName3 + "'";
            return GetModelList(whereStr);
        }

        /// <summary>
        /// 设置流程模式
        /// </summary>
        /// <param name="taskTypeID"></param>
        /// <param name="taskTypeMode"></param>
        /// <param name="statusNeedTime"></param>
        /// <returns></returns>
        public bool SetTaskType(int taskTypeID,string taskTypeMode,int statusNeedTime)
        {
            return dal.SetTaskType(taskTypeID, taskTypeMode, statusNeedTime);
        }
        #endregion  ExtensionMethod
    }
}

