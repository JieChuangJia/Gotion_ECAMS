using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:MakeCardRecordDal
    /// </summary>
    public partial class MakeCardRecordDal
    {
        public MakeCardRecordDal()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string cardID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MakeCardRecord");
            strSql.Append(" where cardID=@cardID ");
            SqlParameter[] parameters = {
					new SqlParameter("@cardID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = cardID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(MakeCardRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MakeCardRecord(");
            strSql.Append("cardID,makedTime,reserve1,reserve2)");
            strSql.Append(" values (");
            strSql.Append("@cardID,@makedTime,@reserve1,@reserve2)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@cardID", SqlDbType.NVarChar,50),
					new SqlParameter("@makedTime", SqlDbType.DateTime),
					new SqlParameter("@reserve1", SqlDbType.NVarChar,50),
					new SqlParameter("@reserve2", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.cardID;
            parameters[1].Value = model.makedTime;
            parameters[2].Value = model.reserve1;
            parameters[3].Value = model.reserve2;

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
        public bool Update(MakeCardRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MakeCardRecord set ");
            strSql.Append("makedTime=@makedTime,");
            strSql.Append("reserve1=@reserve1,");
            strSql.Append("reserve2=@reserve2");
            strSql.Append(" where serialNo=@serialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@makedTime", SqlDbType.DateTime),
					new SqlParameter("@reserve1", SqlDbType.NVarChar,50),
					new SqlParameter("@reserve2", SqlDbType.NVarChar,50),
					new SqlParameter("@cardID", SqlDbType.NVarChar,50),
					new SqlParameter("@serialNo", SqlDbType.BigInt,8)};
            parameters[0].Value = model.makedTime;
            parameters[1].Value = model.reserve1;
            parameters[2].Value = model.reserve2;
            parameters[3].Value = model.cardID;
            parameters[4].Value = model.serialNo;

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
        public bool Delete(long serialNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MakeCardRecord ");
            strSql.Append(" where serialNo=@serialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@serialNo", SqlDbType.BigInt)
			};
            parameters[0].Value = serialNo;

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
        public bool Delete(string cardID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MakeCardRecord ");
            strSql.Append(" where cardID=@cardID ");
            SqlParameter[] parameters = {
					new SqlParameter("@cardID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = cardID;

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
        public bool DeleteList(string serialNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MakeCardRecord ");
            strSql.Append(" where serialNo in (" + serialNolist + ")  ");
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
        public MakeCardRecordModel GetModel(long serialNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 cardID,makedTime,serialNo,reserve1,reserve2 from MakeCardRecord ");
            strSql.Append(" where serialNo=@serialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@serialNo", SqlDbType.BigInt)
			};
            parameters[0].Value = serialNo;

            MakeCardRecordModel model = new MakeCardRecordModel();
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
        public MakeCardRecordModel DataRowToModel(DataRow row)
        {
            MakeCardRecordModel model = new MakeCardRecordModel();
            if (row != null)
            {
                if (row["cardID"] != null)
                {
                    model.cardID = row["cardID"].ToString();
                }
                if (row["makedTime"] != null && row["makedTime"].ToString() != "")
                {
                    model.makedTime = DateTime.Parse(row["makedTime"].ToString());
                }
                if (row["serialNo"] != null && row["serialNo"].ToString() != "")
                {
                    model.serialNo = long.Parse(row["serialNo"].ToString());
                }
                if (row["reserve1"] != null)
                {
                    model.reserve1 = row["reserve1"].ToString();
                }
                if (row["reserve2"] != null)
                {
                    model.reserve2 = row["reserve2"].ToString();
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
            strSql.Append("select cardID,makedTime,serialNo,reserve1,reserve2 ");
            strSql.Append(" FROM MakeCardRecord ");
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
            strSql.Append(" cardID,makedTime,serialNo,reserve1,reserve2 ");
            strSql.Append(" FROM MakeCardRecord ");
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
            strSql.Append("select count(1) FROM MakeCardRecord ");
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
                strSql.Append("order by T.serialNo desc");
            }
            strSql.Append(")AS Row, T.*  from MakeCardRecord T ");
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
            parameters[0].Value = "MakeCardRecord";
            parameters[1].Value = "serialNo";
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

