using System;
using System.Data;
using System.Collections.Generic;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 日志表
    /// </summary>
    public partial class LogBll
    {
        private readonly LogDal dal = new LogDal();
        public LogBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long logID)
        {
            return dal.Exists(logID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(LogModel model)
        {
            dal.DeleteHistoryLog(30);
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LogModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long logID)
        {

            return dal.Delete(logID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string logIDlist)
        {
            return dal.DeleteList(logIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LogModel GetModel(long logID)
        {

            return dal.GetModel(logID);
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
        public List<LogModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<LogModel> DataTableToList(DataTable dt)
        {
            List<LogModel> modelList = new List<LogModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                LogModel model;
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
        public DataTable DistinctLogCategory()
        {
            return dal.DistinctLogCategory();
        }

        public DataTable DistinctLogType()
        {
            return dal.DistinctLogType();
        }

        public DataTable GetLogModelList(DateTime startTime, DateTime endTime, string logCategory, string logType, bool isLikeQuery, string likeQueryStr)
        {
            return dal.GetLogModelList(startTime, endTime, logCategory, logType, isLikeQuery, likeQueryStr);
        }

        public List<LogModel> GetModelList(DateTime startTime, DateTime endTime, string logCategory, string logType,bool isLikeQuery,string likeQueryStr)
        {
            string startTimeStr = startTime.ToString("yyyy-MM-dd 0:00:00");
            string endTimeStr = endTime.ToString("yyyy-MM-dd 23:59:59");
            string whereStr = "logTime between '" + startTimeStr + "' and  '" + endTimeStr + "' ";
            if (logCategory != "所有")
            {
                whereStr += " and  logCategory = '" + logCategory + "' "; 
            }
            
            if (logType != "所有")
            {
                whereStr += "and logType  = '" + logType + "'";
            }
            if (isLikeQuery)
            {
                whereStr += "and logContent like '%" + likeQueryStr + "%'";
            }

            whereStr += "order by logTime desc";
            return GetModelList(whereStr);
        }

        ///// <summary>
        ///// 删除多个月以前的数据
        ///// </summary>
        ///// <param name="monthes">天数</param>
        ///// <returns></returns>
        //public bool DeleteHistoryLog(int days)
        //{
        //    return dal.DeleteHistoryLog(days);
        //}

        public void AsyncAddLog(LogModel log)
        {
            dal.DeleteHistoryLog(30);
            dal.AsyncAddLog(log);
        }
        #endregion  ExtensionMethod
    }
}

