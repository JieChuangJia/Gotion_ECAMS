using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
 
namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:TB_Tray_index
    /// </summary>
    public partial class TB_Tray_indexDal
    {
        public TB_Tray_indexDal()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Tf_TrayId,int usingState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TB_Tray_index");
            strSql.Append(" where Tf_TrayId=@Tf_TrayId and tf_traystat=@tf_traystat");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
                    new SqlParameter("@tf_traystat",SqlDbType.Int)};
            parameters[0].Value = Tf_TrayId;
            parameters[1].Value = usingState;
            return DbHelperSQL2.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TB_Tray_indexModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TB_Tray_index(");
            strSql.Append("Tf_TrayId,Tf_BatchID,Tf_Batchtype,Tf_CellCount,tf_CheckInTime)");
            strSql.Append(" values (");
            strSql.Append("@Tf_TrayId,@Tf_BatchID,@Tf_Batchtype,@Tf_CellCount,@tf_CheckInTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_Batchtype", SqlDbType.Int,4),
					new SqlParameter("@Tf_CellCount", SqlDbType.Int,4),
					new SqlParameter("@tf_CheckInTime", SqlDbType.DateTime)};
            parameters[0].Value = model.Tf_TrayId;
            parameters[1].Value = model.Tf_BatchID;
            parameters[2].Value = model.Tf_Batchtype;
            parameters[3].Value = model.Tf_CellCount;
            parameters[4].Value = model.tf_CheckInTime;

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
        public bool Update(TB_Tray_indexModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_Tray_index set ");
            strSql.Append("Tf_BatchID=@Tf_BatchID,");
            strSql.Append("Tf_Batchtype=@Tf_Batchtype,");
            strSql.Append("Tf_CellCount=@Tf_CellCount,");
            strSql.Append("tf_CheckInTime=@tf_CheckInTime,");
            strSql.Append("tf_traystat=@tf_traystat");
            strSql.Append(" where Tf_TrayId=@Tf_TrayId and tf_traystat=1");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_Batchtype", SqlDbType.Int,4),
					new SqlParameter("@Tf_CellCount", SqlDbType.Int,4),
					new SqlParameter("@tf_CheckInTime", SqlDbType.DateTime),
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
                    new SqlParameter("@tf_traystat",SqlDbType.Int,4)};
            parameters[0].Value = model.Tf_BatchID;
            parameters[1].Value = model.Tf_Batchtype;
            parameters[2].Value = model.Tf_CellCount;
            parameters[3].Value = model.tf_CheckInTime;
            parameters[4].Value = model.Tf_TrayId;
            parameters[5].Value = model.tf_traystat;
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
        public bool Delete(string Tf_TrayId,int usingState)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_Tray_index ");
            strSql.Append(" where Tf_TrayId=@Tf_TrayId and tf_traystat=@tf_traystat ");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
                    new SqlParameter("@tf_traystat",SqlDbType.Int)};
            parameters[0].Value = Tf_TrayId;
            parameters[1].Value = usingState;

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
        public bool DeleteList(string Tf_TrayIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_Tray_index ");
            strSql.Append(" where Tf_TrayId in (" + Tf_TrayIdlist + ")  ");
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
        public TB_Tray_indexModel GetModel(string Tf_TrayId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Tf_TrayId,Tf_BatchID,Tf_Batchtype,Tf_CellCount,tf_CheckInTime,tf_traystat  from TB_Tray_index");
            strSql.Append(" where Tf_TrayId=@Tf_TrayId and tf_traystat=1");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32)			};
            parameters[0].Value = Tf_TrayId;

            TB_Tray_indexModel model = new TB_Tray_indexModel();
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
        public TB_Tray_indexModel DataRowToModel(DataRow row)
        {
            TB_Tray_indexModel model = new TB_Tray_indexModel();
            if (row != null)
            {
                if (row["Tf_TrayId"] != null)
                {
                    model.Tf_TrayId = row["Tf_TrayId"].ToString();
                }
                if (row["Tf_BatchID"] != null)
                {
                    model.Tf_BatchID = row["Tf_BatchID"].ToString();
                }
                if (row["Tf_Batchtype"] != null && row["Tf_Batchtype"].ToString() != "")
                {
                    model.Tf_Batchtype = int.Parse(row["Tf_Batchtype"].ToString());
                }
                if (row["Tf_CellCount"] != null && row["Tf_CellCount"].ToString() != "")
                {
                    model.Tf_CellCount = int.Parse(row["Tf_CellCount"].ToString());
                }
                if (row["tf_CheckInTime"] != null && row["tf_CheckInTime"].ToString() != "")
                {
                    model.tf_CheckInTime = DateTime.Parse(row["tf_CheckInTime"].ToString());
                }
                if (row["tf_traystat"] != null && row["tf_traystat"].ToString() != "")
                {
                    model.tf_traystat = int.Parse(row["tf_traystat"].ToString());
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
            strSql.Append("select Tf_TrayId,Tf_BatchID,Tf_Batchtype,Tf_CellCount,tf_CheckInTime,tf_traystat ");
            strSql.Append(" FROM TB_Tray_index ");
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
            strSql.Append(" Tf_TrayId,Tf_BatchID,Tf_Batchtype,Tf_CellCount,tf_CheckInTime,tf_traystat ");
            strSql.Append(" FROM TB_Tray_index ");
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
            strSql.Append("select count(1) FROM TB_Tray_index ");
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
                strSql.Append("order by T.Tf_TrayId desc");
            }
            strSql.Append(")AS Row, T.*  from TB_Tray_index T ");
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
            parameters[0].Value = "TB_Tray_index";
            parameters[1].Value = "Tf_TrayId";
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

