using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:PalletHistoryRecord
    /// </summary>
    public partial class PalletHistoryRecordDal
    {
        public PalletHistoryRecordDal()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long serialNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PalletHistoryRecord");
            strSql.Append(" where serialNo=@serialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@serialNo", SqlDbType.BigInt)
			};
            parameters[0].Value = serialNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(PalletHistoryRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PalletHistoryRecord(");
            strSql.Append("palletID,hisEventTime,processStatus,hisEventDetail,currentUser)");
            strSql.Append(" values (");
            strSql.Append("@palletID,@hisEventTime,@processStatus,@hisEventDetail,@currentUser)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@palletID", SqlDbType.NVarChar,50),
					new SqlParameter("@hisEventTime", SqlDbType.DateTime),
					new SqlParameter("@processStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@hisEventDetail", SqlDbType.NVarChar,100),
					new SqlParameter("@currentUser", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.palletID;
            parameters[1].Value = model.hisEventTime;
            parameters[2].Value = model.processStatus;
            parameters[3].Value = model.hisEventDetail;
            parameters[4].Value = model.currentUser;

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
        public bool Update(PalletHistoryRecordModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update PalletHistoryRecord set ");
            strSql.Append("palletID=@palletID,");
            strSql.Append("hisEventTime=@hisEventTime,");
            strSql.Append("processStatus=@processStatus,");
            strSql.Append("hisEventDetail=@hisEventDetail,");
            strSql.Append("currentUser=@currentUser");
            strSql.Append(" where serialNo=@serialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@palletID", SqlDbType.NVarChar,50),
					new SqlParameter("@hisEventTime", SqlDbType.DateTime),
					new SqlParameter("@processStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@hisEventDetail", SqlDbType.NVarChar,100),
					new SqlParameter("@currentUser", SqlDbType.NVarChar,50),
					new SqlParameter("@serialNo", SqlDbType.BigInt,8)};
            parameters[0].Value = model.palletID;
            parameters[1].Value = model.hisEventTime;
            parameters[2].Value = model.processStatus;
            parameters[3].Value = model.hisEventDetail;
            parameters[4].Value = model.currentUser;
            parameters[5].Value = model.serialNo;

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
            strSql.Append("delete from PalletHistoryRecord ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string serialNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from PalletHistoryRecord ");
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
        public PalletHistoryRecordModel GetModel(long serialNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 serialNo,palletID,hisEventTime,processStatus,hisEventDetail,currentUser from PalletHistoryRecord ");
            strSql.Append(" where serialNo=@serialNo");
            SqlParameter[] parameters = {
					new SqlParameter("@serialNo", SqlDbType.BigInt)
			};
            parameters[0].Value = serialNo;

            PalletHistoryRecordModel model = new PalletHistoryRecordModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["serialNo"] != null && ds.Tables[0].Rows[0]["serialNo"].ToString() != "")
                {
                    model.serialNo = long.Parse(ds.Tables[0].Rows[0]["serialNo"].ToString());
                }
                if (ds.Tables[0].Rows[0]["palletID"] != null && ds.Tables[0].Rows[0]["palletID"].ToString() != "")
                {
                    model.palletID = ds.Tables[0].Rows[0]["palletID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["hisEventTime"] != null && ds.Tables[0].Rows[0]["hisEventTime"].ToString() != "")
                {
                    model.hisEventTime = DateTime.Parse(ds.Tables[0].Rows[0]["hisEventTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["processStatus"] != null && ds.Tables[0].Rows[0]["processStatus"].ToString() != "")
                {
                    model.processStatus = ds.Tables[0].Rows[0]["processStatus"].ToString();
                }
                if (ds.Tables[0].Rows[0]["hisEventDetail"] != null && ds.Tables[0].Rows[0]["hisEventDetail"].ToString() != "")
                {
                    model.hisEventDetail = ds.Tables[0].Rows[0]["hisEventDetail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["currentUser"] != null && ds.Tables[0].Rows[0]["currentUser"].ToString() != "")
                {
                    model.currentUser = ds.Tables[0].Rows[0]["currentUser"].ToString();
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
            strSql.Append("select serialNo,palletID,hisEventTime,processStatus,hisEventDetail,currentUser ");
            strSql.Append(" FROM PalletHistoryRecord ");
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
            strSql.Append(" serialNo,palletID,hisEventTime,processStatus,hisEventDetail,currentUser ");
            strSql.Append(" FROM PalletHistoryRecord ");
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
            strSql.Append("select count(1) FROM PalletHistoryRecord ");
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
            strSql.Append(")AS Row, T.*  from PalletHistoryRecord T ");
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
            parameters[0].Value = "PalletHistoryRecord";
            parameters[1].Value = "serialNo";
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
        /// 获得数据列表
        /// </summary>
        public List<PalletHistoryRecordModel> GetList(string strWhere,bool timeAsc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select serialNo,palletID,hisEventTime,processStatus,hisEventDetail,currentUser ");
            strSql.Append(" FROM PalletHistoryRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (timeAsc)
            {
                strSql.Append("order by hisEventTime asc");
            }
            else
            {
                strSql.Append("order by hisEventTime desc");
            }
           
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            List<PalletHistoryRecordModel> eventList = new List<PalletHistoryRecordModel>();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                PalletHistoryRecordModel model = new PalletHistoryRecordModel();
                if (ds.Tables[0].Rows[i]["serialNo"] != null && ds.Tables[0].Rows[i]["serialNo"].ToString() != "")
                {
                    model.serialNo = long.Parse(ds.Tables[0].Rows[i]["serialNo"].ToString());
                }
                if (ds.Tables[0].Rows[i]["palletID"] != null && ds.Tables[0].Rows[i]["palletID"].ToString() != "")
                {
                    model.palletID = ds.Tables[0].Rows[i]["palletID"].ToString();
                }
                if (ds.Tables[0].Rows[i]["hisEventTime"] != null && ds.Tables[0].Rows[i]["hisEventTime"].ToString() != "")
                {
                    model.hisEventTime = DateTime.Parse(ds.Tables[0].Rows[i]["hisEventTime"].ToString());
                }
                if (ds.Tables[0].Rows[i]["processStatus"] != null && ds.Tables[0].Rows[i]["processStatus"].ToString() != "")
                {
                    model.processStatus = ds.Tables[0].Rows[i]["processStatus"].ToString();
                }
                if (ds.Tables[0].Rows[i]["hisEventDetail"] != null && ds.Tables[0].Rows[i]["hisEventDetail"].ToString() != "")
                {
                    model.hisEventDetail = ds.Tables[0].Rows[i]["hisEventDetail"].ToString();
                }
                if (ds.Tables[0].Rows[i]["currentUser"] != null && ds.Tables[0].Rows[i]["currentUser"].ToString() != "")
                {
                    model.currentUser = ds.Tables[0].Rows[i]["currentUser"].ToString();
                }
                eventList.Add(model);
            }
            return eventList;
        }
        /// <summary>
        /// 删除多个月以前的数据
        /// </summary>
        /// <param name="monthes">天数</param>
        /// <returns></returns>
        public bool DeleteHistoryLog(int days)
        {
            StringBuilder strSql = new StringBuilder();
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strSql.Append("delete from PalletHistoryRecord ");
            strSql.Append(" where datediff(day,hisEventTime,'" + nowTime + "') >= " + days);
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
