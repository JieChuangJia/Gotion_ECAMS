using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:HistoryManageTask
    /// </summary>
    public partial class HistoryManageTaskDal
    {
        public HistoryManageTaskDal()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long HistoryTaskID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from HistoryManageTask");
            strSql.Append(" where HistoryTaskID=@HistoryTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@HistoryTaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = HistoryTaskID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(HistoryManageTaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HistoryManageTask(");
            strSql.Append("TaskTypeName,TaskType,ProductID,ProductName,TaskStartPsotion,TaskStartArea,TaskEndPosition,TaskEndAera,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter)");
            strSql.Append(" values (");
            strSql.Append("@TaskTypeName,@TaskType,@ProductID,@ProductName,@TaskStartPsotion,@TaskStartArea,@TaskEndPosition,@TaskEndAera,@TaskCreatePerson,@TaskCreateTime,@TaskCompleteTime,@TaskParameter)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskType", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@TaskStartPsotion", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStartArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndAera", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreateTime", SqlDbType.DateTime),
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.TaskTypeName;
            parameters[1].Value = model.TaskType;
            parameters[2].Value = model.ProductID;
            parameters[3].Value = model.ProductName;
            parameters[4].Value = model.TaskStartPsotion;
            parameters[5].Value = model.TaskStartArea;
            parameters[6].Value = model.TaskEndPosition;
            parameters[7].Value = model.TaskEndAera;
            parameters[8].Value = model.TaskCreatePerson;
            parameters[9].Value = model.TaskCreateTime;
            parameters[10].Value = model.TaskCompleteTime;
            parameters[11].Value = model.TaskParameter;

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
        public bool Update(HistoryManageTaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HistoryManageTask set ");
            strSql.Append("TaskTypeName=@TaskTypeName,");
            strSql.Append("TaskType=@TaskType,");
            strSql.Append("ProductID=@ProductID,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("TaskStartPsotion=@TaskStartPsotion,");
            strSql.Append("TaskStartArea=@TaskStartArea,");
            strSql.Append("TaskEndPosition=@TaskEndPosition,");
            strSql.Append("TaskEndAera=@TaskEndAera,");
            strSql.Append("TaskCreatePerson=@TaskCreatePerson,");
            strSql.Append("TaskCreateTime=@TaskCreateTime,");
            strSql.Append("TaskCompleteTime=@TaskCompleteTime,");
            strSql.Append("TaskParameter=@TaskParameter");
            strSql.Append(" where HistoryTaskID=@HistoryTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskType", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@TaskStartPsotion", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStartArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndAera", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreateTime", SqlDbType.DateTime),
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,100),
					new SqlParameter("@HistoryTaskID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.TaskTypeName;
            parameters[1].Value = model.TaskType;
            parameters[2].Value = model.ProductID;
            parameters[3].Value = model.ProductName;
            parameters[4].Value = model.TaskStartPsotion;
            parameters[5].Value = model.TaskStartArea;
            parameters[6].Value = model.TaskEndPosition;
            parameters[7].Value = model.TaskEndAera;
            parameters[8].Value = model.TaskCreatePerson;
            parameters[9].Value = model.TaskCreateTime;
            parameters[10].Value = model.TaskCompleteTime;
            parameters[11].Value = model.TaskParameter;
            parameters[12].Value = model.HistoryTaskID;

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
        public bool Delete(long HistoryTaskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HistoryManageTask ");
            strSql.Append(" where HistoryTaskID=@HistoryTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@HistoryTaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = HistoryTaskID;

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
        public bool DeleteList(string HistoryTaskIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HistoryManageTask ");
            strSql.Append(" where HistoryTaskID in (" + HistoryTaskIDlist + ")  ");
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
        public HistoryManageTaskModel GetModel(long HistoryTaskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 HistoryTaskID,TaskTypeName,TaskType,ProductID,ProductName,TaskStartPsotion,TaskStartArea,TaskEndPosition,TaskEndAera,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter from HistoryManageTask ");
            strSql.Append(" where HistoryTaskID=@HistoryTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@HistoryTaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = HistoryTaskID;

            HistoryManageTaskModel model = new HistoryManageTaskModel();
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
        public HistoryManageTaskModel DataRowToModel(DataRow row)
        {
            HistoryManageTaskModel model = new HistoryManageTaskModel();
            if (row != null)
            {
                if (row["HistoryTaskID"] != null && row["HistoryTaskID"].ToString() != "")
                {
                    model.HistoryTaskID = long.Parse(row["HistoryTaskID"].ToString());
                }
                if (row["TaskTypeName"] != null)
                {
                    model.TaskTypeName = row["TaskTypeName"].ToString();
                }
                if (row["TaskType"] != null)
                {
                    model.TaskType = row["TaskType"].ToString();
                }
                if (row["ProductID"] != null && row["ProductID"].ToString() != "")
                {
                    model.ProductID = int.Parse(row["ProductID"].ToString());
                }
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["TaskStartPsotion"] != null)
                {
                    model.TaskStartPsotion = row["TaskStartPsotion"].ToString();
                }
                if (row["TaskStartArea"] != null)
                {
                    model.TaskStartArea = row["TaskStartArea"].ToString();
                }
                if (row["TaskEndPosition"] != null)
                {
                    model.TaskEndPosition = row["TaskEndPosition"].ToString();
                }
                if (row["TaskEndAera"] != null)
                {
                    model.TaskEndAera = row["TaskEndAera"].ToString();
                }
                if (row["TaskCreatePerson"] != null)
                {
                    model.TaskCreatePerson = row["TaskCreatePerson"].ToString();
                }
                if (row["TaskCreateTime"] != null && row["TaskCreateTime"].ToString() != "")
                {
                    model.TaskCreateTime = DateTime.Parse(row["TaskCreateTime"].ToString());
                }
                if (row["TaskCompleteTime"] != null && row["TaskCompleteTime"].ToString() != "")
                {
                    model.TaskCompleteTime = DateTime.Parse(row["TaskCompleteTime"].ToString());
                }
                if (row["TaskParameter"] != null)
                {
                    model.TaskParameter = row["TaskParameter"].ToString();
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
            strSql.Append("select HistoryTaskID,TaskTypeName,TaskType,ProductID,ProductName,TaskStartPsotion,TaskStartArea,TaskEndPosition,TaskEndAera,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter ");
            strSql.Append(" FROM HistoryManageTask ");
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
            strSql.Append(" HistoryTaskID,TaskTypeName,TaskType,ProductID,ProductName,TaskStartPsotion,TaskStartArea,TaskEndPosition,TaskEndAera,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter ");
            strSql.Append(" FROM HistoryManageTask ");
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
            strSql.Append("select count(1) FROM HistoryManageTask ");
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
                strSql.Append("order by T.HistoryTaskID desc");
            }
            strSql.Append(")AS Row, T.*  from HistoryManageTask T ");
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
            parameters[0].Value = "HistoryManageTask";
            parameters[1].Value = "HistoryTaskID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public Hashtable GetInsertModelHash(HistoryManageTaskModel model)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HistoryManageTask(");
            strSql.Append("TaskTypeName,TaskType,ProductID,ProductName,TaskStartPsotion,TaskStartArea,TaskEndPosition,TaskEndAera,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter)");
            strSql.Append(" values (");
            strSql.Append("@TaskTypeName,@TaskType,@ProductID,@ProductName,@TaskStartPsotion,@TaskStartArea,@TaskEndPosition,@TaskEndAera,@TaskCreatePerson,@TaskCreateTime,@TaskCompleteTime,@TaskParameter)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskType", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@TaskStartPsotion", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStartArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndAera", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreateTime", SqlDbType.DateTime),
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.TaskTypeName;
            parameters[1].Value = model.TaskType;
            parameters[2].Value = model.ProductID;
            parameters[3].Value = model.ProductName;
            parameters[4].Value = model.TaskStartPsotion;
            parameters[5].Value = model.TaskStartArea;
            parameters[6].Value = model.TaskEndPosition;
            parameters[7].Value = model.TaskEndAera;
            parameters[8].Value = model.TaskCreatePerson;
            parameters[9].Value = model.TaskCreateTime;
            parameters[10].Value = model.TaskCompleteTime;
            parameters[11].Value = model.TaskParameter;

            hs.Add(strSql.ToString(), parameters);
            return hs;
        }

        public bool DeleteHisData(int days)
        {
            StringBuilder strSql = new StringBuilder();
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strSql.Append("delete from HistoryManageTask ");
            strSql.Append(" where datediff(day,TaskCreateTime,'" + nowTime + "') >= " + days);
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

        public DataSet GetDistinctTaskTypeName()
        {
            string sqlStr = "select distinct TaskTypeName from HistoryManageTask";
            return DbHelperSQL.Query(sqlStr);
        }
        #endregion  ExtensionMethod
    }
}

