using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:RfidRdRecord
    /// </summary>
    public partial class RfidRdRecordDal
    {
        public RfidRdRecordDal()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long readingSerialNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from RfidRdRecord");
            strSql.Append(" where readingSerialNo=@readingSerialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@readingSerialNo", SqlDbType.BigInt)
			};
            parameters[0].Value = readingSerialNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(RfidRdRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RfidRdRecord(");
            strSql.Append("rfidReaderID,readerName,readingContent,readingTime )");
            strSql.Append(" values (");
            strSql.Append("@rfidReaderID,@readerName,@readingContent,@readingTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@rfidReaderID", SqlDbType.Int,4),
					new SqlParameter("@readerName", SqlDbType.NVarChar,50),
					new SqlParameter("@readingContent",SqlDbType.NVarChar,50 ),
					new SqlParameter("@readingTime", SqlDbType.DateTime)};
            parameters[0].Value = model.rfidReaderID;
            parameters[1].Value = model.readerName;
            parameters[2].Value = model.readingContent;
            parameters[3].Value = model.readingTime;

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
        public bool Update(RfidRdRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RfidRdRecord set ");
            strSql.Append("rfidReaderID=@rfidReaderID,");
            strSql.Append("readingContent=@readingContent,");
            strSql.Append("readingTime=@readingTime,");
            strSql.Append("readerName=@readerName");
            strSql.Append(" where readingSerialNo=@readingSerialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@rfidReaderID", SqlDbType.Int,4),
					new SqlParameter("@readingContent", SqlDbType.NVarChar,50),
					new SqlParameter("@readingTime", SqlDbType.DateTime),
					new SqlParameter("@readerName", SqlDbType.NVarChar,50),
					new SqlParameter("@readingSerialNo", SqlDbType.BigInt,8)};
            parameters[0].Value = model.rfidReaderID;
            parameters[1].Value = model.readingContent;
            parameters[2].Value = model.readingTime;
            parameters[3].Value = model.readerName;
            parameters[4].Value = model.readingSerialNo;

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
        public bool Delete(long readingSerialNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RfidRdRecord ");
            strSql.Append(" where readingSerialNo=@readingSerialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@readingSerialNo", SqlDbType.BigInt)
			};
            parameters[0].Value = readingSerialNo;

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
        public bool DeleteList(string readingSerialNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RfidRdRecord ");
            strSql.Append(" where readingSerialNo in (" + readingSerialNolist + ")  ");
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
        public RfidRdRecordModel GetModel(long readingSerialNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 readingSerialNo,rfidReaderID,readerName,readingContent,readingTime ");
            strSql.Append(" where readingSerialNo=@readingSerialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@readingSerialNo", SqlDbType.BigInt)
			};
            parameters[0].Value = readingSerialNo;

            RfidRdRecordModel model = new RfidRdRecordModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["rfidReaderID"] != null && ds.Tables[0].Rows[0]["rfidReaderID"].ToString() != "")
                {
                    model.rfidReaderID = int.Parse(ds.Tables[0].Rows[0]["rfidReaderID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["readingContent"] != null && ds.Tables[0].Rows[0]["readingContent"].ToString() != "")
                {
                    model.readingContent = ds.Tables[0].Rows[0]["readingContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["readingTime"] != null && ds.Tables[0].Rows[0]["readingTime"].ToString() != "")
                {
                    model.readingTime = DateTime.Parse(ds.Tables[0].Rows[0]["readingTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["readerName"] != null && ds.Tables[0].Rows[0]["readerName"].ToString() != "")
                {
                    model.readerName = ds.Tables[0].Rows[0]["readerName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["readingSerialNo"] != null && ds.Tables[0].Rows[0]["readingSerialNo"].ToString() != "")
                {
                    model.readingSerialNo = long.Parse(ds.Tables[0].Rows[0]["readingSerialNo"].ToString());
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
            strSql.Append("select readingTime,readingSerialNo,rfidReaderID,readerName,readingContent ");
            strSql.Append(" FROM RfidRdRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by readingTime desc"); 
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
            strSql.Append(" readingSerialNo,rfidReaderID,readerName,readingContent,readingTime");
            strSql.Append(" FROM RfidRdRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            strSql.Append(" order by readingTime asc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM RfidRdRecord ");
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
                strSql.Append("order by T.readingSerialNo desc");
            }
            strSql.Append(")AS Row, T.*  from RfidRdRecord T ");
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
            parameters[0].Value = "RfidRdRecord";
            parameters[1].Value = "readingSerialNo";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
        #region 扩展方法
        /// <summary>
        /// 删除多个月以前的数据
        /// </summary>
        /// <param name="monthes">天数</param>
        /// <returns></returns>
        public bool DeleteHistoryLog(int days)
        {
            StringBuilder strSql = new StringBuilder();
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strSql.Append("delete from RfidRdRecord ");
            strSql.Append(" where datediff(day,readingTime,'" + nowTime + "') >= " + days);
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

