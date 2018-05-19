using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using ECAMSDataAccess;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 管理任务记录
    /// </summary>
    public partial class HistoryManageTaskBll
    {
        private readonly HistoryManageTaskDal dal = new HistoryManageTaskDal();
        public HistoryManageTaskBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long HistoryTaskID)
        {
            return dal.Exists(HistoryTaskID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(HistoryManageTaskModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HistoryManageTaskModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long HistoryTaskID)
        {

            return dal.Delete(HistoryTaskID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string HistoryTaskIDlist)
        {
            return dal.DeleteList(HistoryTaskIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HistoryManageTaskModel GetModel(long HistoryTaskID)
        {

            return dal.GetModel(HistoryTaskID);
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
        public List<HistoryManageTaskModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HistoryManageTaskModel> DataTableToList(DataTable dt)
        {
            List<HistoryManageTaskModel> modelList = new List<HistoryManageTaskModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                HistoryManageTaskModel model;
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
        public Hashtable GetInsertModelHash(HistoryManageTaskModel model)
        {
            dal.DeleteHisData(180);//180天前的数据删除
            return dal.GetInsertModelHash(model);
        }

        public DataSet GetDistinctTaskTypeName()
        {
            return dal.GetDistinctTaskTypeName();
        }

        public List<HistoryManageTaskModel> GetModeListByFactor(DateTime startTime, DateTime endTime
            , string taskTypeName, bool taskTypeNameChecked,string startPosition,bool startPostionChecked,string endPostion,bool endPositionChecked)
        {
            string whereStr = string.Empty;
            whereStr = "TaskCompleteTime between '" + startTime.ToString("yyyy-MM-dd 0:00:00") + "' and  '" 
                + endTime.ToString("yyyy-MM-dd 23:59:59") + "'";
            if (taskTypeNameChecked == true)
            {
                whereStr += "and TaskTypeName = '" + taskTypeName+"'";
            }
            if (startPostionChecked == true)
            {
                whereStr += "and TaskStartPsotion = '" + startPosition + "'";
            }
            if (endPositionChecked == true)
            {
                whereStr += "and TaskEndPosition = '" + endPostion + "'";
            }
            whereStr += " order by TaskCreateTime desc";
            List<HistoryManageTaskModel> modelList = GetModelList(whereStr);

            return modelList;
        }
        #endregion  ExtensionMethod
    }
}

