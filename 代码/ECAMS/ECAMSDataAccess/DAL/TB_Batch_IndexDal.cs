using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
 
namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:TB_Batch_Index
    /// </summary>
    public partial class TB_Batch_IndexDal
    {
        public TB_Batch_IndexDal()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Tf_BatchID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TB_Batch_Index");
            strSql.Append(" where Tf_BatchID=@Tf_BatchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32)			};
            parameters[0].Value = Tf_BatchID;

            return DbHelperSQL2.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TB_Batch_IndexModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TB_Batch_Index(");
            strSql.Append("Tf_BatchID,Tf_Batchtype,Tf_TrayCount,Tf_CellCount)");
            strSql.Append(" values (");
            strSql.Append("@Tf_BatchID,@Tf_Batchtype,@Tf_TrayCount,@Tf_CellCount)");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_Batchtype", SqlDbType.Int,4),
					new SqlParameter("@Tf_TrayCount", SqlDbType.Int,4),
					new SqlParameter("@Tf_CellCount", SqlDbType.Int,4)};
            parameters[0].Value = model.Tf_BatchID;
            parameters[1].Value = model.Tf_Batchtype;
            parameters[2].Value = model.Tf_TrayCount;
            parameters[3].Value = model.Tf_CellCount;

            int rows = DbHelperSQL2.ExecuteSql(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(TB_Batch_IndexModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_Batch_Index set ");
            strSql.Append("Tf_Batchtype=@Tf_Batchtype,");
            strSql.Append("Tf_TrayCount=@Tf_TrayCount,");
            strSql.Append("Tf_CellCount=@Tf_CellCount");
            strSql.Append(" where Tf_BatchID=@Tf_BatchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_Batchtype", SqlDbType.Int,4),
					new SqlParameter("@Tf_TrayCount", SqlDbType.Int,4),
					new SqlParameter("@Tf_CellCount", SqlDbType.Int,4),
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32)};
            parameters[0].Value = model.Tf_Batchtype;
            parameters[1].Value = model.Tf_TrayCount;
            parameters[2].Value = model.Tf_CellCount;
            parameters[3].Value = model.Tf_BatchID;

            int rows = DbHelperSQL2.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(string Tf_BatchID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_Batch_Index ");
            strSql.Append(" where Tf_BatchID=@Tf_BatchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32)			};
            parameters[0].Value = Tf_BatchID;

            int rows = DbHelperSQL2.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string Tf_BatchIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_Batch_Index ");
            strSql.Append(" where Tf_BatchID in (" + Tf_BatchIDlist + ")  ");
            int rows = DbHelperSQL2.ExecuteSql(strSql.ToString());
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
        public TB_Batch_IndexModel GetModel(string Tf_BatchID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Tf_BatchID,Tf_Batchtype,Tf_TrayCount,Tf_CellCount from TB_Batch_Index ");
            strSql.Append(" where Tf_BatchID=@Tf_BatchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32)			};
            parameters[0].Value = Tf_BatchID;

            TB_Batch_IndexModel model = new TB_Batch_IndexModel();
            DataSet ds = DbHelperSQL2.Query(strSql.ToString(), parameters);
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
        public TB_Batch_IndexModel DataRowToModel(DataRow row)
        {
            TB_Batch_IndexModel model = new TB_Batch_IndexModel();
            if (row != null)
            {
                if (row["Tf_BatchID"] != null)
                {
                    model.Tf_BatchID = row["Tf_BatchID"].ToString();
                }
                if (row["Tf_Batchtype"] != null && row["Tf_Batchtype"].ToString() != "")
                {
                    model.Tf_Batchtype = int.Parse(row["Tf_Batchtype"].ToString());
                }
                if (row["Tf_TrayCount"] != null && row["Tf_TrayCount"].ToString() != "")
                {
                    model.Tf_TrayCount = int.Parse(row["Tf_TrayCount"].ToString());
                }
                if (row["Tf_CellCount"] != null && row["Tf_CellCount"].ToString() != "")
                {
                    model.Tf_CellCount = int.Parse(row["Tf_CellCount"].ToString());
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
            strSql.Append("select Tf_BatchID,Tf_Batchtype,Tf_TrayCount,Tf_CellCount ");
            strSql.Append(" FROM TB_Batch_Index ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL2.Query(strSql.ToString());
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
            strSql.Append(" Tf_BatchID,Tf_Batchtype,Tf_TrayCount,Tf_CellCount ");
            strSql.Append(" FROM TB_Batch_Index ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL2.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM TB_Batch_Index ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL2.GetSingle(strSql.ToString());
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
                strSql.Append("order by T.Tf_BatchID desc");
            }
            strSql.Append(")AS Row, T.*  from TB_Batch_Index T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL2.Query(strSql.ToString());
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
            parameters[0].Value = "TB_Batch_Index";
            parameters[1].Value = "Tf_BatchID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL2.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

