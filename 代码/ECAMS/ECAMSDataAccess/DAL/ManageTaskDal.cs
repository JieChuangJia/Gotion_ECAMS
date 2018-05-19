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
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:ManageTask
	/// </summary>
	public partial class ManageTaskDal
	{
		public ManageTaskDal()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long TaskID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ManageTask");
            strSql.Append(" where TaskID=@TaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = TaskID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ManageTaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ManageTask(");
            strSql.Append("TaskStatus,TaskTypeName,TaskType,TaskCode,TaskStartArea,TaskStartPostion,TaskEndArea,TaskEndPostion,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter)");
            strSql.Append(" values (");
            strSql.Append("@TaskStatus,@TaskTypeName,@TaskType,@TaskCode,@TaskStartArea,@TaskStartPostion,@TaskEndArea,@TaskEndPostion,@TaskCreatePerson,@TaskCreateTime,@TaskCompleteTime,@TaskParameter)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskType", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCode", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStartArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStartPostion", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndPostion", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreateTime", SqlDbType.DateTime),
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.TaskStatus;
            parameters[1].Value = model.TaskTypeName;
            parameters[2].Value = model.TaskType;
            parameters[3].Value = model.TaskCode;
            parameters[4].Value = model.TaskStartArea;
            parameters[5].Value = model.TaskStartPostion;
            parameters[6].Value = model.TaskEndArea;
            parameters[7].Value = model.TaskEndPostion;
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
        public bool Update(ManageTaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ManageTask set ");
            strSql.Append("TaskStatus=@TaskStatus,");
            strSql.Append("TaskTypeName=@TaskTypeName,");
            strSql.Append("TaskType=@TaskType,");
            strSql.Append("TaskCode=@TaskCode,");
            strSql.Append("TaskStartArea=@TaskStartArea,");
            strSql.Append("TaskStartPostion=@TaskStartPostion,");
            strSql.Append("TaskEndArea=@TaskEndArea,");
            strSql.Append("TaskEndPostion=@TaskEndPostion,");
            strSql.Append("TaskCreatePerson=@TaskCreatePerson,");
            strSql.Append("TaskCreateTime=@TaskCreateTime,");
            strSql.Append("TaskCompleteTime=@TaskCompleteTime,");
            strSql.Append("TaskParameter=@TaskParameter");
            strSql.Append(" where TaskID=@TaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskType", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCode", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStartArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStartPostion", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskEndPostion", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreatePerson", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCreateTime", SqlDbType.DateTime),
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.TaskStatus;
            parameters[1].Value = model.TaskTypeName;
            parameters[2].Value = model.TaskType;
            parameters[3].Value = model.TaskCode;
            parameters[4].Value = model.TaskStartArea;
            parameters[5].Value = model.TaskStartPostion;
            parameters[6].Value = model.TaskEndArea;
            parameters[7].Value = model.TaskEndPostion;
            parameters[8].Value = model.TaskCreatePerson;
            parameters[9].Value = model.TaskCreateTime;
            parameters[10].Value = model.TaskCompleteTime;
            parameters[11].Value = model.TaskParameter;
            parameters[12].Value = model.TaskID;

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
        public bool Delete(long TaskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ManageTask ");
            strSql.Append(" where TaskID=@TaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = TaskID;

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
        public bool DeleteList(string TaskIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ManageTask ");
            strSql.Append(" where TaskID in (" + TaskIDlist + ")  ");
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
        public ManageTaskModel GetModel(long TaskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TaskID,TaskStatus,TaskTypeName,TaskType,TaskCode,TaskStartArea,TaskStartPostion,TaskEndArea,TaskEndPostion,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter from ManageTask ");
            strSql.Append(" where TaskID=@TaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = TaskID;

            ManageTaskModel model = new ManageTaskModel();
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
        public ManageTaskModel DataRowToModel(DataRow row)
        {
            ManageTaskModel model = new ManageTaskModel();
            if (row != null)
            {
                if (row["TaskID"] != null && row["TaskID"].ToString() != "")
                {
                    model.TaskID = long.Parse(row["TaskID"].ToString());
                }
                if (row["TaskStatus"] != null)
                {
                    model.TaskStatus = row["TaskStatus"].ToString();
                }
                if (row["TaskTypeName"] != null)
                {
                    model.TaskTypeName = row["TaskTypeName"].ToString();
                }
                if (row["TaskType"] != null)
                {
                    model.TaskType = row["TaskType"].ToString();
                }
                if (row["TaskCode"] != null)
                {
                    model.TaskCode = row["TaskCode"].ToString();
                }
                if (row["TaskStartArea"] != null)
                {
                    model.TaskStartArea = row["TaskStartArea"].ToString();
                }
                if (row["TaskStartPostion"] != null)
                {
                    model.TaskStartPostion = row["TaskStartPostion"].ToString();
                }
                if (row["TaskEndArea"] != null)
                {
                    model.TaskEndArea = row["TaskEndArea"].ToString();
                }
                if (row["TaskEndPostion"] != null)
                {
                    model.TaskEndPostion = row["TaskEndPostion"].ToString();
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
            strSql.Append("select TaskID,TaskStatus,TaskTypeName,TaskType,TaskCode,TaskStartArea,TaskStartPostion,TaskEndArea,TaskEndPostion,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter ");
            strSql.Append(" FROM ManageTask ");
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
            strSql.Append(" TaskID,TaskStatus,TaskTypeName,TaskType,TaskCode,TaskStartArea,TaskStartPostion,TaskEndArea,TaskEndPostion,TaskCreatePerson,TaskCreateTime,TaskCompleteTime,TaskParameter ");
            strSql.Append(" FROM ManageTask ");
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
            strSql.Append("select count(1) FROM ManageTask ");
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
                strSql.Append("order by T.TaskID desc");
            }
            strSql.Append(")AS Row, T.*  from ManageTask T ");
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
            parameters[0].Value = "ManageTask";
            parameters[1].Value = "TaskID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod
        public DataSet GetManaTaskList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  TaskID as 任务ID,TaskStatus as 任务状态,TaskTypeName as 业务流程名称,TaskType as 任务类型,"
            +"TaskCode as 任务编码,TaskStartArea as 任务开始区域,TaskStartPostion as 任务开始位置,TaskEndArea as 任务结束区域,"
            +"TaskEndPostion as 任务结束位置,TaskCreatePerson as 任务创建人,TaskCreateTime as 任务创建时间,"
            +"TaskCompleteTime as 任务完成时间 from ManageTask ");
           
            return DbHelperSQL.Query(strSql.ToString());
        }

        public Hashtable GetDeleteModelHash(long taskID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ManageTask ");
            strSql.Append(" where TaskID=@TaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = taskID;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }

        public bool UpdateCompleteTime(DateTime completeTime, long manageTaskID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ManageTask set ");
            strSql.Append("TaskCompleteTime=@TaskCompleteTime");
            strSql.Append(" where TaskID=@TaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskID", SqlDbType.BigInt,8)};
            parameters[0].Value = completeTime;
            parameters[1].Value = manageTaskID;
          
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

        public Hashtable GetUpdateCompleteTimeHs(DateTime completeTime, long manageTaskID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ManageTask set ");
            strSql.Append("TaskCompleteTime=@TaskCompleteTime");
            strSql.Append(" where TaskID=@TaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskCompleteTime", SqlDbType.DateTime),
					new SqlParameter("@TaskID", SqlDbType.BigInt,8)};
            parameters[0].Value = completeTime;
            parameters[1].Value = manageTaskID;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }

        public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ManageTask");

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
	
		#endregion  ExtensionMethod
	}
}

