using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:TaskType
    /// </summary>
    public partial class TaskTypeDal
    {
        public TaskTypeDal()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int TaskTypeCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TaskType");
            strSql.Append(" where TaskTypeCode=@TaskTypeCode");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4)
			};
            parameters[0].Value = TaskTypeCode;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TaskTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TaskType(");
            strSql.Append("TaskTypeName,TaskTypeValue,StartLogicAreaID,StartDevice,EndLogicAreaID,EndDevice,ProductStartStatus,ProductEndStatus,NeedTime,TaskTypeMode,TaskTypeDescribe)");
            strSql.Append(" values (");
            strSql.Append("@TaskTypeName,@TaskTypeValue,@StartLogicAreaID,@StartDevice,@EndLogicAreaID,@EndDevice,@ProductStartStatus,@ProductEndStatus,@NeedTime,@TaskTypeMode,@TaskTypeDescribe)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskTypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@StartLogicAreaID", SqlDbType.Int,4),
					new SqlParameter("@StartDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@EndLogicAreaID", SqlDbType.Int,4),
					new SqlParameter("@EndDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductStartStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductEndStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@NeedTime", SqlDbType.Int,4),
					new SqlParameter("@TaskTypeMode", SqlDbType.NChar,10),
					new SqlParameter("@TaskTypeDescribe", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.TaskTypeName;
            parameters[1].Value = model.TaskTypeValue;
            parameters[2].Value = model.StartLogicAreaID;
            parameters[3].Value = model.StartDevice;
            parameters[4].Value = model.EndLogicAreaID;
            parameters[5].Value = model.EndDevice;
            parameters[6].Value = model.ProductStartStatus;
            parameters[7].Value = model.ProductEndStatus;
            parameters[8].Value = model.NeedTime;
            parameters[9].Value = model.TaskTypeMode;
            parameters[10].Value = model.TaskTypeDescribe;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(TaskTypeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TaskType set ");
            strSql.Append("TaskTypeName=@TaskTypeName,");
            strSql.Append("TaskTypeValue=@TaskTypeValue,");
            strSql.Append("StartLogicAreaID=@StartLogicAreaID,");
            strSql.Append("StartDevice=@StartDevice,");
            strSql.Append("EndLogicAreaID=@EndLogicAreaID,");
            strSql.Append("EndDevice=@EndDevice,");
            strSql.Append("ProductStartStatus=@ProductStartStatus,");
            strSql.Append("ProductEndStatus=@ProductEndStatus,");
            strSql.Append("NeedTime=@NeedTime,");
            strSql.Append("TaskTypeMode=@TaskTypeMode,");
            strSql.Append("TaskTypeDescribe=@TaskTypeDescribe");
            strSql.Append(" where TaskTypeCode=@TaskTypeCode");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskTypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@StartLogicAreaID", SqlDbType.Int,4),
					new SqlParameter("@StartDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@EndLogicAreaID", SqlDbType.Int,4),
					new SqlParameter("@EndDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductStartStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductEndStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@NeedTime", SqlDbType.Int,4),
					new SqlParameter("@TaskTypeMode", SqlDbType.NChar,10),
					new SqlParameter("@TaskTypeDescribe", SqlDbType.NVarChar,100),
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4)};
            parameters[0].Value = model.TaskTypeName;
            parameters[1].Value = model.TaskTypeValue;
            parameters[2].Value = model.StartLogicAreaID;
            parameters[3].Value = model.StartDevice;
            parameters[4].Value = model.EndLogicAreaID;
            parameters[5].Value = model.EndDevice;
            parameters[6].Value = model.ProductStartStatus;
            parameters[7].Value = model.ProductEndStatus;
            parameters[8].Value = model.NeedTime;
            parameters[9].Value = model.TaskTypeMode;
            parameters[10].Value = model.TaskTypeDescribe;
            parameters[11].Value = model.TaskTypeCode;

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
        public bool Delete(int TaskTypeCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TaskType ");
            strSql.Append(" where TaskTypeCode=@TaskTypeCode");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4)
			};
            parameters[0].Value = TaskTypeCode;

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
        public bool DeleteList(string TaskTypeCodelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TaskType ");
            strSql.Append(" where TaskTypeCode in (" + TaskTypeCodelist + ")  ");
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
        public TaskTypeModel GetModel(int TaskTypeCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 TaskTypeCode,TaskTypeName,TaskTypeValue,StartLogicAreaID,StartDevice,EndLogicAreaID,EndDevice,ProductStartStatus,ProductEndStatus,NeedTime,TaskTypeMode,TaskTypeDescribe from TaskType ");
            strSql.Append(" where TaskTypeCode=@TaskTypeCode");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4)
			};
            parameters[0].Value = TaskTypeCode;

            TaskTypeModel model = new TaskTypeModel();
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
        public TaskTypeModel DataRowToModel(DataRow row)
        {
            TaskTypeModel model = new TaskTypeModel();
            if (row != null)
            {
                if (row["TaskTypeCode"] != null && row["TaskTypeCode"].ToString() != "")
                {
                    model.TaskTypeCode = int.Parse(row["TaskTypeCode"].ToString());
                }
                if (row["TaskTypeName"] != null)
                {
                    model.TaskTypeName = row["TaskTypeName"].ToString();
                }
                if (row["TaskTypeValue"] != null)
                {
                    model.TaskTypeValue = row["TaskTypeValue"].ToString();
                }
                if (row["StartLogicAreaID"] != null && row["StartLogicAreaID"].ToString() != "")
                {
                    model.StartLogicAreaID = int.Parse(row["StartLogicAreaID"].ToString());
                }
                if (row["StartDevice"] != null)
                {
                    model.StartDevice = row["StartDevice"].ToString();
                }
                if (row["EndLogicAreaID"] != null && row["EndLogicAreaID"].ToString() != "")
                {
                    model.EndLogicAreaID = int.Parse(row["EndLogicAreaID"].ToString());
                }
                if (row["EndDevice"] != null)
                {
                    model.EndDevice = row["EndDevice"].ToString();
                }
                if (row["ProductStartStatus"] != null)
                {
                    model.ProductStartStatus = row["ProductStartStatus"].ToString();
                }
                if (row["ProductEndStatus"] != null)
                {
                    model.ProductEndStatus = row["ProductEndStatus"].ToString();
                }
                if (row["NeedTime"] != null && row["NeedTime"].ToString() != "")
                {
                    model.NeedTime = int.Parse(row["NeedTime"].ToString());
                }
                if (row["TaskTypeMode"] != null)
                {
                    model.TaskTypeMode = row["TaskTypeMode"].ToString();
                }
                if (row["TaskTypeDescribe"] != null)
                {
                    model.TaskTypeDescribe = row["TaskTypeDescribe"].ToString();
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
            strSql.Append("select TaskTypeCode,TaskTypeName,TaskTypeValue,StartLogicAreaID,StartDevice,EndLogicAreaID,EndDevice,ProductStartStatus,ProductEndStatus,NeedTime,TaskTypeMode,TaskTypeDescribe ");
            strSql.Append(" FROM TaskType ");
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
            strSql.Append(" TaskTypeCode,TaskTypeName,TaskTypeValue,StartLogicAreaID,StartDevice,EndLogicAreaID,EndDevice,ProductStartStatus,ProductEndStatus,NeedTime,TaskTypeMode,TaskTypeDescribe ");
            strSql.Append(" FROM TaskType ");
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
            strSql.Append("select count(1) FROM TaskType ");
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
                strSql.Append("order by T.TaskTypeCode desc");
            }
            strSql.Append(")AS Row, T.*  from TaskType T ");
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
            parameters[0].Value = "TaskType";
            parameters[1].Value = "TaskTypeCode";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public bool SetTaskType(int taskTypeID, string taskTypeMode, int statusNeedTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TaskType set ");
         
            strSql.Append("NeedTime=@NeedTime,");
            strSql.Append("TaskTypeMode=@TaskTypeMode");
      
            strSql.Append(" where TaskTypeCode=@TaskTypeCode");
            SqlParameter[] parameters = {
				 
					new SqlParameter("@NeedTime", SqlDbType.Int,4),
					new SqlParameter("@TaskTypeMode", SqlDbType.NChar,10),
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4)};
            parameters[0].Value = statusNeedTime;
            parameters[1].Value = taskTypeMode;
            parameters[2].Value = taskTypeID;
           
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
        #endregion  ExtensionMethod
    }
}

