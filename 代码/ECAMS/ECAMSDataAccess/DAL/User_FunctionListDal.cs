using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:User_FunctionListDal
    /// </summary>
    public partial class User_FunctionListDal
    {
        public User_FunctionListDal()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FunctionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from User_FunctionList");
            strSql.Append(" where FunctionID=@FunctionID");
            SqlParameter[] parameters = {
					new SqlParameter("@FunctionID", SqlDbType.Int,4)
			};
            parameters[0].Value = FunctionID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(User_FunctionListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into User_FunctionList(");
            strSql.Append("FunctionName,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@FunctionName,@Remarks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@FunctionName", SqlDbType.NVarChar,50),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.FunctionName;
            parameters[1].Value = model.Remarks;

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
        public bool Update(User_FunctionListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update User_FunctionList set ");
            strSql.Append("FunctionName=@FunctionName,");
            strSql.Append("Remarks=@Remarks");
            strSql.Append(" where FunctionID=@FunctionID");
            SqlParameter[] parameters = {
					new SqlParameter("@FunctionName", SqlDbType.NVarChar,50),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,50),
					new SqlParameter("@FunctionID", SqlDbType.Int,4)};
            parameters[0].Value = model.FunctionName;
            parameters[1].Value = model.Remarks;
            parameters[2].Value = model.FunctionID;

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
        public bool Delete(int FunctionID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from User_FunctionList ");
            strSql.Append(" where FunctionID=@FunctionID");
            SqlParameter[] parameters = {
					new SqlParameter("@FunctionID", SqlDbType.Int,4)
			};
            parameters[0].Value = FunctionID;

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
        public bool DeleteList(string FunctionIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from User_FunctionList ");
            strSql.Append(" where FunctionID in (" + FunctionIDlist + ")  ");
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
        public User_FunctionListModel GetModel(int FunctionID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 FunctionID,FunctionName,Remarks from User_FunctionList ");
            strSql.Append(" where FunctionID=@FunctionID");
            SqlParameter[] parameters = {
					new SqlParameter("@FunctionID", SqlDbType.Int,4)
			};
            parameters[0].Value = FunctionID;

            User_FunctionListModel model = new User_FunctionListModel();
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
        public User_FunctionListModel DataRowToModel(DataRow row)
        {
            User_FunctionListModel model = new User_FunctionListModel();
            if (row != null)
            {
                if (row["FunctionID"] != null && row["FunctionID"].ToString() != "")
                {
                    model.FunctionID = int.Parse(row["FunctionID"].ToString());
                }
                if (row["FunctionName"] != null)
                {
                    model.FunctionName = row["FunctionName"].ToString();
                }
                if (row["Remarks"] != null)
                {
                    model.Remarks = row["Remarks"].ToString();
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
            strSql.Append("select FunctionID,FunctionName,Remarks ");
            strSql.Append(" FROM User_FunctionList ");
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
            strSql.Append(" FunctionID,FunctionName,Remarks ");
            strSql.Append(" FROM User_FunctionList ");
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
            strSql.Append("select count(1) FROM User_FunctionList ");
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
                strSql.Append("order by T.FunctionID desc");
            }
            strSql.Append(")AS Row, T.*  from User_FunctionList T ");
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
            parameters[0].Value = "User_FunctionList";
            parameters[1].Value = "FunctionID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

