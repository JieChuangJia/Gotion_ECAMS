using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:OCVPallet
    /// </summary>
    public partial class OCVPalletDal
    {
        public OCVPalletDal()
        { }
        #region  Basic Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string palletID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OCVPallet");
            strSql.Append(" where palletID=@palletID ");
            SqlParameter[] parameters = {
					new SqlParameter("@palletID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = palletID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(OCVPalletModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OCVPallet(");
            strSql.Append("palletID,processStatus,loadInTime,batchID)");
            strSql.Append(" values (");
            strSql.Append("@palletID,@processStatus,@loadInTime,@batchID)");
            SqlParameter[] parameters = {
					new SqlParameter("@palletID", SqlDbType.NVarChar,50),
					new SqlParameter("@processStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@loadInTime", SqlDbType.DateTime),
					new SqlParameter("@batchID", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.palletID;
            parameters[1].Value = model.processStatus;
            parameters[2].Value = model.loadInTime;
            parameters[3].Value = model.batchID;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(OCVPalletModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OCVPallet set ");
            strSql.Append("processStatus=@processStatus,");
            strSql.Append("loadInTime=@loadInTime,");
            strSql.Append("batchID=@batchID");
            strSql.Append(" where palletID=@palletID ");
            SqlParameter[] parameters = {
					new SqlParameter("@processStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@loadInTime", SqlDbType.DateTime),
					new SqlParameter("@batchID", SqlDbType.NVarChar,50),
					new SqlParameter("@palletID", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.processStatus;
            parameters[1].Value = model.loadInTime;
            parameters[2].Value = model.batchID;
            parameters[3].Value = model.palletID;

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
        public bool Delete(string palletID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OCVPallet ");
            strSql.Append(" where palletID=@palletID ");
            SqlParameter[] parameters = {
					new SqlParameter("@palletID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = palletID;

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
        public bool DeleteList(string palletIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OCVPallet ");
            strSql.Append(" where palletID in (" + palletIDlist + ")  ");
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
        public OCVPalletModel GetModel(string palletID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 palletID,processStatus,loadInTime,batchID from OCVPallet ");
            strSql.Append(" where palletID=@palletID ");
            SqlParameter[] parameters = {
					new SqlParameter("@palletID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = palletID;

            OCVPalletModel model = new OCVPalletModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["palletID"] != null && ds.Tables[0].Rows[0]["palletID"].ToString() != "")
                {
                    model.palletID = ds.Tables[0].Rows[0]["palletID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["processStatus"] != null && ds.Tables[0].Rows[0]["processStatus"].ToString() != "")
                {
                    model.processStatus = ds.Tables[0].Rows[0]["processStatus"].ToString();
                }
                if (ds.Tables[0].Rows[0]["loadInTime"] != null && ds.Tables[0].Rows[0]["loadInTime"].ToString() != "")
                {
                    model.loadInTime = DateTime.Parse(ds.Tables[0].Rows[0]["loadInTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["batchID"] != null && ds.Tables[0].Rows[0]["batchID"].ToString() != "")
                {
                    model.batchID = ds.Tables[0].Rows[0]["batchID"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select palletID,processStatus,loadInTime,batchID ");
            strSql.Append(" FROM OCVPallet ");
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
            strSql.Append(" palletID,processStatus,loadInTime,batchID ");
            strSql.Append(" FROM OCVPallet ");
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
            strSql.Append("select count(1) FROM OCVPallet ");
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
                strSql.Append("order by T.palletID desc");
            }
            strSql.Append(")AS Row, T.*  from OCVPallet T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion basic method
        #region extend Method
        public bool ClearPallet()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OCVPallet");

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
        #endregion

    }
}
