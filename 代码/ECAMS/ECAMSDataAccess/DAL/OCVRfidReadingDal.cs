using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:OCVRfidReading
    /// </summary>
    public partial class OCVRfidReadingDal
    {
        public OCVRfidReadingDal()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return (int)DbHelperSQL.GetMaxID("readerID", "OCVRfidReading");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int readerID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OCVRfidReading");
            strSql.Append(" where readerID=@readerID ");
            SqlParameter[] parameters = {
					new SqlParameter("@readerID", SqlDbType.Int,4)			};
            parameters[0].Value = readerID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(OCVRfidReadingModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OCVRfidReading(");
            strSql.Append("readerID,rfidValue,readRequire,readComplete)");
            strSql.Append(" values (");
            strSql.Append("@readerID,@rfidValue,@readRequire,@readComplete)");
            SqlParameter[] parameters = {
					new SqlParameter("@readerID", SqlDbType.Int,4),
					new SqlParameter("@rfidValue", SqlDbType.NVarChar,50),
					new SqlParameter("@readRequire", SqlDbType.Bit,1),
					new SqlParameter("@readComplete", SqlDbType.Bit,1)};
            parameters[0].Value = model.readerID;
            parameters[1].Value = model.rfidValue;
            parameters[2].Value = model.readRequire;
            parameters[3].Value = model.readComplete;

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
        public bool Update(OCVRfidReadingModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OCVRfidReading set ");
            strSql.Append("rfidValue=@rfidValue,");
            strSql.Append("readRequire=@readRequire,");
            strSql.Append("readComplete=@readComplete");
            strSql.Append(" where readerID=@readerID ");
            SqlParameter[] parameters = {
					new SqlParameter("@rfidValue", SqlDbType.NVarChar,50),
					new SqlParameter("@readRequire", SqlDbType.Bit,1),
					new SqlParameter("@readComplete", SqlDbType.Bit,1),
					new SqlParameter("@readerID", SqlDbType.Int,4)};
            parameters[0].Value = model.rfidValue;
            parameters[1].Value = model.readRequire;
            parameters[2].Value = model.readComplete;
            parameters[3].Value = model.readerID;

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
        public bool Delete(int readerID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OCVRfidReading ");
            strSql.Append(" where readerID=@readerID ");
            SqlParameter[] parameters = {
					new SqlParameter("@readerID", SqlDbType.Int,4)			};
            parameters[0].Value = readerID;

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
        public bool DeleteList(string readerIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OCVRfidReading ");
            strSql.Append(" where readerID in (" + readerIDlist + ")  ");
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
        public OCVRfidReadingModel GetModel(int readerID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 readerID,rfidValue,readRequire,readComplete from OCVRfidReading ");
            strSql.Append(" where readerID=@readerID ");
            SqlParameter[] parameters = {
					new SqlParameter("@readerID", SqlDbType.Int,4)			};
            parameters[0].Value = readerID;

            OCVRfidReadingModel model = new OCVRfidReadingModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["readerID"] != null && ds.Tables[0].Rows[0]["readerID"].ToString() != "")
                {
                    model.readerID = int.Parse(ds.Tables[0].Rows[0]["readerID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["rfidValue"] != null && ds.Tables[0].Rows[0]["rfidValue"].ToString() != "")
                {
                    model.rfidValue = ds.Tables[0].Rows[0]["rfidValue"].ToString();
                }
                if (ds.Tables[0].Rows[0]["readRequire"] != null && ds.Tables[0].Rows[0]["readRequire"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["readRequire"].ToString() == "1") || (ds.Tables[0].Rows[0]["readRequire"].ToString().ToLower() == "true"))
                    {
                        model.readRequire = true;
                    }
                    else
                    {
                        model.readRequire = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["readComplete"] != null && ds.Tables[0].Rows[0]["readComplete"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["readComplete"].ToString() == "1") || (ds.Tables[0].Rows[0]["readComplete"].ToString().ToLower() == "true"))
                    {
                        model.readComplete = true;
                    }
                    else
                    {
                        model.readComplete = false;
                    }
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
            strSql.Append("select readerID,rfidValue,readRequire,readComplete ");
            strSql.Append(" FROM OCVRfidReading ");
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
            strSql.Append(" readerID,rfidValue,readRequire,readComplete ");
            strSql.Append(" FROM OCVRfidReading ");
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
            strSql.Append("select count(1) FROM OCVRfidReading ");
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
                strSql.Append("order by T.readerID desc");
            }
            strSql.Append(")AS Row, T.*  from OCVRfidReading T ");
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
            parameters[0].Value = "OCVRfidReading";
            parameters[1].Value = "readerID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}
