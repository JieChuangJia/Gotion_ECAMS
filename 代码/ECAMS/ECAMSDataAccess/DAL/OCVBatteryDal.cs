using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:OCVBattery
    /// </summary>
    public partial class OCVBatteryDal
    {
        public OCVBatteryDal()
        { }
        #region  Basic Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string batteryID, string palletID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from OCVBattery");
            strSql.Append(" where batteryID=@batteryID and palletID=@palletID ");
            SqlParameter[] parameters = {
					new SqlParameter("@batteryID", SqlDbType.NVarChar,50),
					new SqlParameter("@palletID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = batteryID;
            parameters[1].Value = palletID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(OCVBatteryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OCVBattery(");
            strSql.Append("batteryID,rowIndex,columnIndex,positionCode,checkResult,palletID,hasBattery)");
            strSql.Append(" values (");
            strSql.Append("@batteryID,@rowIndex,@columnIndex,@positionCode,@checkResult,@palletID,@hasBattery)");
            SqlParameter[] parameters = {
					new SqlParameter("@batteryID", SqlDbType.NVarChar,50),
					new SqlParameter("@rowIndex", SqlDbType.Int,4),
					new SqlParameter("@columnIndex", SqlDbType.Int,4),
					new SqlParameter("@positionCode", SqlDbType.Int,4),
					new SqlParameter("@checkResult", SqlDbType.Char,10),
					new SqlParameter("@palletID", SqlDbType.NVarChar,50),
					new SqlParameter("@hasBattery", SqlDbType.Bit,1)};
            parameters[0].Value = model.batteryID;
            parameters[1].Value = model.rowIndex;
            parameters[2].Value = model.columnIndex;
            parameters[3].Value = model.positionCode;
            parameters[4].Value = model.checkResult;
            parameters[5].Value = model.palletID;
            parameters[6].Value = model.hasBattery;

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
        public bool Update(OCVBatteryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OCVBattery set ");
            strSql.Append("rowIndex=@rowIndex,");
            strSql.Append("columnIndex=@columnIndex,");
            strSql.Append("positionCode=@positionCode,");
            strSql.Append("checkResult=@checkResult,");
            strSql.Append("hasBattery=@hasBattery");
            strSql.Append(" where batteryID=@batteryID and palletID=@palletID ");
            SqlParameter[] parameters = {
					new SqlParameter("@rowIndex", SqlDbType.Int,4),
					new SqlParameter("@columnIndex", SqlDbType.Int,4),
					new SqlParameter("@positionCode", SqlDbType.Int,4),
					new SqlParameter("@checkResult", SqlDbType.Char,10),
					new SqlParameter("@hasBattery", SqlDbType.Bit,1),
					new SqlParameter("@batteryID", SqlDbType.NVarChar,50),
					new SqlParameter("@palletID", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.rowIndex;
            parameters[1].Value = model.columnIndex;
            parameters[2].Value = model.positionCode;
            parameters[3].Value = model.checkResult;
            parameters[4].Value = model.hasBattery;
            parameters[5].Value = model.batteryID;
            parameters[6].Value = model.palletID;

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
        public bool Delete(string batteryID, string palletID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OCVBattery ");
            strSql.Append(" where batteryID=@batteryID and palletID=@palletID ");
            SqlParameter[] parameters = {
					new SqlParameter("@batteryID", SqlDbType.NVarChar,50),
					new SqlParameter("@palletID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = batteryID;
            parameters[1].Value = palletID;

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
        /// 得到一个对象实体
        /// </summary>
        public OCVBatteryModel GetModel(string batteryID, string palletID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 batteryID,rowIndex,columnIndex,positionCode,checkResult,palletID,hasBattery from OCVBattery ");
            strSql.Append(" where batteryID=@batteryID and palletID=@palletID ");
            SqlParameter[] parameters = {
					new SqlParameter("@batteryID", SqlDbType.NVarChar,50),
					new SqlParameter("@palletID", SqlDbType.NVarChar,50)			};
            parameters[0].Value = batteryID;
            parameters[1].Value = palletID;

            OCVBatteryModel model = new OCVBatteryModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["batteryID"] != null && ds.Tables[0].Rows[0]["batteryID"].ToString() != "")
                {
                    model.batteryID = ds.Tables[0].Rows[0]["batteryID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["rowIndex"] != null && ds.Tables[0].Rows[0]["rowIndex"].ToString() != "")
                {
                    model.rowIndex = int.Parse(ds.Tables[0].Rows[0]["rowIndex"].ToString());
                }
                if (ds.Tables[0].Rows[0]["columnIndex"] != null && ds.Tables[0].Rows[0]["columnIndex"].ToString() != "")
                {
                    model.columnIndex = int.Parse(ds.Tables[0].Rows[0]["columnIndex"].ToString());
                }
                if (ds.Tables[0].Rows[0]["positionCode"] != null && ds.Tables[0].Rows[0]["positionCode"].ToString() != "")
                {
                    model.positionCode = int.Parse(ds.Tables[0].Rows[0]["positionCode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["checkResult"] != null && ds.Tables[0].Rows[0]["checkResult"].ToString() != "")
                {
                    model.checkResult = ds.Tables[0].Rows[0]["checkResult"].ToString();
                }
                if (ds.Tables[0].Rows[0]["palletID"] != null && ds.Tables[0].Rows[0]["palletID"].ToString() != "")
                {
                    model.palletID = ds.Tables[0].Rows[0]["palletID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["hasBattery"] != null && ds.Tables[0].Rows[0]["hasBattery"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["hasBattery"].ToString() == "1") || (ds.Tables[0].Rows[0]["hasBattery"].ToString().ToLower() == "true"))
                    {
                        model.hasBattery = true;
                    }
                    else
                    {
                        model.hasBattery = false;
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
            strSql.Append("select batteryID,rowIndex,columnIndex,positionCode,checkResult,palletID,hasBattery ");
            strSql.Append(" FROM OCVBattery ");
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
            strSql.Append(" batteryID,rowIndex,columnIndex,positionCode,checkResult,palletID,hasBattery ");
            strSql.Append(" FROM OCVBattery ");
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
            strSql.Append("select count(1) FROM OCVBattery ");
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
        #endregion  Method
        #region 扩展方法
        public bool ClearBattery()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from OCVBattery");

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
