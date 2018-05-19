using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:LogModelDal
    /// </summary>
    public partial class LogDal
    {
        public LogDal()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long logID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Log");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
					new SqlParameter("@logID", SqlDbType.BigInt)
			};
            parameters[0].Value = logID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Log(");
            strSql.Append("logContent,logTime,logCategory,logType,warningCode)");
            strSql.Append(" values (");
            strSql.Append("@logContent,@logTime,@logCategory,@logType,@warningCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@logContent", SqlDbType.NVarChar,1000),
					new SqlParameter("@logTime", SqlDbType.DateTime),
					new SqlParameter("@logCategory", SqlDbType.NVarChar,50),
					new SqlParameter("@logType", SqlDbType.NVarChar,50),
					new SqlParameter("@warningCode", SqlDbType.Int,4)};
            parameters[0].Value = model.logContent;
            parameters[1].Value = model.logTime;
            parameters[2].Value = model.logCategory;
            parameters[3].Value = model.logType;
            parameters[4].Value = model.warningCode;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Log set ");
            strSql.Append("logContent=@logContent,");
            strSql.Append("logTime=@logTime,");
            strSql.Append("logCategory=@logCategory,");
            strSql.Append("logType=@logType,");
            strSql.Append("warningCode=@warningCode");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
					new SqlParameter("@logContent", SqlDbType.NVarChar,1000),
					new SqlParameter("@logTime", SqlDbType.DateTime),
					new SqlParameter("@logCategory", SqlDbType.NVarChar,50),
					new SqlParameter("@logType", SqlDbType.NVarChar,50),
					new SqlParameter("@warningCode", SqlDbType.Int,4),
					new SqlParameter("@logID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.logContent;
            parameters[1].Value = model.logTime;
            parameters[2].Value = model.logCategory;
            parameters[3].Value = model.logType;
            parameters[4].Value = model.warningCode;
            parameters[5].Value = model.logID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long logID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Log ");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
					new SqlParameter("@logID", SqlDbType.BigInt)
			};
            parameters[0].Value = logID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string logIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Log ");
            strSql.Append(" where logID in (" + logIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LogModel GetModel(long logID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 logID,logContent,logTime,logCategory,logType,warningCode from Log ");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
					new SqlParameter("@logID", SqlDbType.BigInt)
			};
            parameters[0].Value = logID;

            LogModel model = new LogModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LogModel DataRowToModel(DataRow row)
        {
            LogModel model = new LogModel();
            if (row != null)
            {
                if (row["logID"] != null && row["logID"].ToString() != "")
                {
                    model.logID = long.Parse(row["logID"].ToString());
                }
                if (row["logContent"] != null)
                {
                    model.logContent = row["logContent"].ToString();
                }
                if (row["logTime"] != null && row["logTime"].ToString() != "")
                {
                    model.logTime = DateTime.Parse(row["logTime"].ToString());
                }
                if (row["logCategory"] != null)
                {
                    model.logCategory = row["logCategory"].ToString();
                }
                if (row["logType"] != null)
                {
                    model.logType = row["logType"].ToString();
                }
                if (row["warningCode"] != null && row["warningCode"].ToString() != "")
                {
                    model.warningCode = int.Parse(row["warningCode"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select logID,logContent,logTime,logCategory,logType,warningCode ");
            strSql.Append(" FROM Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" logID,logContent,logTime,logCategory,logType,warningCode ");
            strSql.Append(" FROM Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.logID desc");
            }
            strSql.Append(")AS Row, T.*  from Log T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Log";
            parameters[1].Value = "logID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataTable DistinctLogCategory()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct logCategory from Log ");
         
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable DistinctLogType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct logType from Log ");

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除多个月以前的数据
        /// </summary>
        /// <param name="monthes">月数</param>
        /// <returns></returns>
        public bool DeleteHistoryLog(int days)
        {
            StringBuilder strSql = new StringBuilder();
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strSql.Append("delete from Log ");
            strSql.Append(" where datediff(day,logTime,'" + nowTime + "') >= " + days);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AsyncAddLog(LogModel log)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Log(");
            strSql.Append("logContent,logTime,logCategory,logType,warningCode)");
            strSql.Append(" values (");
            if (log.warningCode == null)
            {
                strSql.Append("'" + log.logContent + "','" + log.logTime + "','" + log.logCategory + "','" + log.logType + "',null");
            }
            else
            {
                strSql.Append("'" + log.logContent + "','" + log.logTime + "','" + log.logCategory + "','" + log.logType + "'," + log.warningCode);
            }
            strSql.Append(")");
            DbHelperSQL.AsyncExecuteNonQuery(strSql.ToString(), DbHelperSQL.CallbackAsyncExecuteNonQuery);

        }
        public DataTable GetLogModelList(DateTime startTime, DateTime endTime, string logCategory, string logType, bool isLikeQuery, string likeQueryStr)
        {
            string sqlStr = "select logID as 日志序号,logTime as 日期,logCategory as 日志类别,logType as 日志类型,logContent as 日志内容,warningCode as 报警码 from log";
            StringBuilder strSql = new StringBuilder();
            string startTimeStr = startTime.ToString("yyyy-MM-dd 0:00:00");
            string endTimeStr = endTime.ToString("yyyy-MM-dd 23:59:59");
            string whereStr = " where logTime between '" + startTimeStr + "' and  '" + endTimeStr + "' ";
            if (logCategory != "所有")
            {
                whereStr += " and  logCategory = '" + logCategory + "' ";
            }

            if (logType != "所有")
            {
                whereStr += "and logType  = '" + logType + "'";
            }
            DataSet ds = null;
            if (isLikeQuery)
            {
               
                SqlParameter[] parameters = {
					new SqlParameter("@likeQueryStr",  "%" + likeQueryStr + "%")};
                whereStr += "and logContent like @likeQueryStr ";
                whereStr += " order by logTime desc";
                strSql.Append(sqlStr);
                strSql.Append(whereStr);
                ds =DbHelperSQL.Query(strSql.ToString(), parameters);
            }
            else
            {
                whereStr += "order by logTime desc";
                sqlStr += whereStr;
                ds = DbHelperSQL.Query(sqlStr);
            }
           
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

      
        #endregion  ExtensionMethod
    }
}

