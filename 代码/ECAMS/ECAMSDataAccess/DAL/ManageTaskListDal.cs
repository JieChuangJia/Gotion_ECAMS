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
using System.Text;
using System.Data.SqlClient;
using System.Collections;


namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:ManageTaskList
	/// </summary>
	public partial class ManageTaskListDal
	{
		public ManageTaskListDal()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long TaskListID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ManageTaskList");
            strSql.Append(" where TaskListID=@TaskListID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskListID", SqlDbType.BigInt)
			};
            parameters[0].Value = TaskListID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ManageTaskListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ManageTaskList(");
            strSql.Append("TaskID,ProductCode,StockListID,TaskStartPosition,TaskEndPosition,TaskCreatePerson,ProductBatch,TaskCreateTime,TaskCompleteTime,TaskParameter,TaskRemark)");
            strSql.Append(" values (");
            strSql.Append("@TaskID,@ProductCode,@StockListID,@TaskStartPosition,@TaskEndPosition,@TaskCreatePerson,@ProductBatch,@TaskCreateTime,@TaskCompleteTime,@TaskParameter,@TaskRemark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@TaskStartPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductBatch", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreateTime", SqlDbType.DateTime),
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskRemark", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.TaskID;
            parameters[1].Value = model.ProductCode;
            parameters[2].Value = model.StockListID;
            parameters[3].Value = model.TaskStartPosition;
            parameters[4].Value = model.TaskEndPosition;
            parameters[5].Value = model.TaskCreatePerson;
            parameters[6].Value = model.ProductBatch;
            parameters[7].Value = model.TaskCreateTime;
            parameters[8].Value = model.TaskCompleteTime;
            parameters[9].Value = model.TaskParameter;
            parameters[10].Value = model.TaskRemark;

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
        public bool Update(ManageTaskListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ManageTaskList set ");
            strSql.Append("TaskID=@TaskID,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("StockListID=@StockListID,");
            strSql.Append("TaskStartPosition=@TaskStartPosition,");
            strSql.Append("TaskEndPosition=@TaskEndPosition,");
            strSql.Append("TaskCreatePerson=@TaskCreatePerson,");
            strSql.Append("ProductBatch=@ProductBatch,");
            strSql.Append("TaskCreateTime=@TaskCreateTime,");
            strSql.Append("TaskCompleteTime=@TaskCompleteTime,");
            strSql.Append("TaskParameter=@TaskParameter,");
            strSql.Append("TaskRemark=@TaskRemark");
            strSql.Append(" where TaskListID=@TaskListID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@TaskStartPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndPosition", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductBatch", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreateTime", SqlDbType.DateTime),
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskRemark", SqlDbType.NVarChar,100),
					new SqlParameter("@TaskListID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.TaskID;
            parameters[1].Value = model.ProductCode;
            parameters[2].Value = model.StockListID;
            parameters[3].Value = model.TaskStartPosition;
            parameters[4].Value = model.TaskEndPosition;
            parameters[5].Value = model.TaskCreatePerson;
            parameters[6].Value = model.ProductBatch;
            parameters[7].Value = model.TaskCreateTime;
            parameters[8].Value = model.TaskCompleteTime;
            parameters[9].Value = model.TaskParameter;
            parameters[10].Value = model.TaskRemark;
            parameters[11].Value = model.TaskListID;

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
        public bool Delete(long TaskListID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ManageTaskList ");
            strSql.Append(" where TaskListID=@TaskListID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskListID", SqlDbType.BigInt)
			};
            parameters[0].Value = TaskListID;

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
        public bool DeleteList(string TaskListIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ManageTaskList ");
            strSql.Append(" where TaskListID in (" + TaskListIDlist + ")  ");
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
        public ManageTaskListModel GetModel(long TaskListID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TaskListID,TaskID,ProductCode,StockListID,TaskStartPosition,TaskEndPosition,TaskCreatePerson,ProductBatch,TaskCreateTime,TaskCompleteTime,TaskParameter,TaskRemark from ManageTaskList ");
            strSql.Append(" where TaskListID=@TaskListID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskListID", SqlDbType.BigInt)
			};
            parameters[0].Value = TaskListID;

            ManageTaskListModel model = new ManageTaskListModel();
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
        public ManageTaskListModel DataRowToModel(DataRow row)
        {
            ManageTaskListModel model = new ManageTaskListModel();
            if (row != null)
            {
                if (row["TaskListID"] != null && row["TaskListID"].ToString() != "")
                {
                    model.TaskListID = long.Parse(row["TaskListID"].ToString());
                }
                if (row["TaskID"] != null && row["TaskID"].ToString() != "")
                {
                    model.TaskID = long.Parse(row["TaskID"].ToString());
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["StockListID"] != null && row["StockListID"].ToString() != "")
                {
                    model.StockListID = long.Parse(row["StockListID"].ToString());
                }
                if (row["TaskStartPosition"] != null)
                {
                    model.TaskStartPosition = row["TaskStartPosition"].ToString();
                }
                if (row["TaskEndPosition"] != null)
                {
                    model.TaskEndPosition = row["TaskEndPosition"].ToString();
                }
                if (row["TaskCreatePerson"] != null)
                {
                    model.TaskCreatePerson = row["TaskCreatePerson"].ToString();
                }
                if (row["ProductBatch"] != null)
                {
                    model.ProductBatch = row["ProductBatch"].ToString();
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
                if (row["TaskRemark"] != null)
                {
                    model.TaskRemark = row["TaskRemark"].ToString();
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
            strSql.Append("select TaskListID,TaskID,ProductCode,StockListID,TaskStartPosition,TaskEndPosition,TaskCreatePerson,ProductBatch,TaskCreateTime,TaskCompleteTime,TaskParameter,TaskRemark ");
            strSql.Append(" FROM ManageTaskList ");
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
            strSql.Append(" TaskListID,TaskID,ProductCode,StockListID,TaskStartPosition,TaskEndPosition,TaskCreatePerson,ProductBatch,TaskCreateTime,TaskCompleteTime,TaskParameter,TaskRemark ");
            strSql.Append(" FROM ManageTaskList ");
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
            strSql.Append("select count(1) FROM ManageTaskList ");
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
                strSql.Append("order by T.TaskListID desc");
            }
            strSql.Append(")AS Row, T.*  from ManageTaskList T ");
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
            parameters[0].Value = "ManageTaskList";
            parameters[1].Value = "TaskListID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod

        public Hashtable GetDeleteModelHash(long manaTaskID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ManageTaskList ");
            strSql.Append(" where TaskID=@TaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = manaTaskID;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }

        public bool UpdateCompleteTime(DateTime completeTime, long manaTaskListID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ManageTaskList set ");
            strSql.Append("TaskCompleteTime=@TaskCompleteTime");
            strSql.Append("TaskRemark=@TaskRemark");
            strSql.Append(" where TaskListID=@TaskListID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskListID", SqlDbType.BigInt,8)};
            parameters[0].Value = completeTime;
            parameters[1].Value = manaTaskListID;
        
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

        public Hashtable GetUpdateCompleteTimeHs(DateTime completeTime, long manaTaskListID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ManageTaskList set ");
            strSql.Append("TaskCompleteTime=@TaskCompleteTime");
       
            strSql.Append(" where TaskListID=@TaskListID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskListID", SqlDbType.BigInt,8)};
            parameters[0].Value = completeTime;
            parameters[1].Value = manaTaskListID;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }
		#endregion  ExtensionMethod
	}
}

