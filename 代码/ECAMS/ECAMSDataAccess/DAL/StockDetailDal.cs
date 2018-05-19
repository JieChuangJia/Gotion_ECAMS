/**  版本信息模板在安装目录下，可自行修改。
* StockDetail.cs
*
* 功 能： N/A
* 类 名： StockDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:04   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：np　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:StockDetail
	/// </summary>
	public partial class StockDetailDal
	{
		public StockDetailDal()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long StockDetailID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from StockDetail");
            strSql.Append(" where StockDetailID=@StockDetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockDetailID", SqlDbType.BigInt)
			};
            parameters[0].Value = StockDetailID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(StockDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StockDetail(");
            strSql.Append("StockListID,TrayID,CoreCode,CorePositionID,CoreQualitySign,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@StockListID,@TrayID,@CoreCode,@CorePositionID,@CoreQualitySign,@Remarks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@TrayID", SqlDbType.NVarChar,50),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CorePositionID", SqlDbType.Int,4),
					new SqlParameter("@CoreQualitySign", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.StockListID;
            parameters[1].Value = model.TrayID;
            parameters[2].Value = model.CoreCode;
            parameters[3].Value = model.CorePositionID;
            parameters[4].Value = model.CoreQualitySign;
            parameters[5].Value = model.Remarks;

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
        public bool Update(StockDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StockDetail set ");
            strSql.Append("StockListID=@StockListID,");
            strSql.Append("TrayID=@TrayID,");
            strSql.Append("CoreCode=@CoreCode,");
            strSql.Append("CorePositionID=@CorePositionID,");
            strSql.Append("CoreQualitySign=@CoreQualitySign,");
            strSql.Append("Remarks=@Remarks");
            strSql.Append(" where StockDetailID=@StockDetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@TrayID", SqlDbType.NVarChar,50),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CorePositionID", SqlDbType.Int,4),
					new SqlParameter("@CoreQualitySign", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100),
					new SqlParameter("@StockDetailID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.StockListID;
            parameters[1].Value = model.TrayID;
            parameters[2].Value = model.CoreCode;
            parameters[3].Value = model.CorePositionID;
            parameters[4].Value = model.CoreQualitySign;
            parameters[5].Value = model.Remarks;
            parameters[6].Value = model.StockDetailID;

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
        public bool Delete(long StockDetailID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockDetail ");
            strSql.Append(" where StockDetailID=@StockDetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockDetailID", SqlDbType.BigInt)
			};
            parameters[0].Value = StockDetailID;

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
        public bool DeleteList(string StockDetailIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockDetail ");
            strSql.Append(" where StockDetailID in (" + StockDetailIDlist + ")  ");
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
        public StockDetailModel GetModel(long StockDetailID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 StockDetailID,StockListID,TrayID,CoreCode,CorePositionID,CoreQualitySign,Remarks from StockDetail ");
            strSql.Append(" where StockDetailID=@StockDetailID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockDetailID", SqlDbType.BigInt)
			};
            parameters[0].Value = StockDetailID;

            StockDetailModel model = new StockDetailModel();
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
        public StockDetailModel DataRowToModel(DataRow row)
        {
            StockDetailModel model = new StockDetailModel();
            if (row != null)
            {
                if (row["StockDetailID"] != null && row["StockDetailID"].ToString() != "")
                {
                    model.StockDetailID = long.Parse(row["StockDetailID"].ToString());
                }
                if (row["StockListID"] != null && row["StockListID"].ToString() != "")
                {
                    model.StockListID = long.Parse(row["StockListID"].ToString());
                }
                if (row["TrayID"] != null)
                {
                    model.TrayID = row["TrayID"].ToString();
                }
                if (row["CoreCode"] != null)
                {
                    model.CoreCode = row["CoreCode"].ToString();
                }
                if (row["CorePositionID"] != null && row["CorePositionID"].ToString() != "")
                {
                    model.CorePositionID = int.Parse(row["CorePositionID"].ToString());
                }
                if (row["CoreQualitySign"] != null && row["CoreQualitySign"].ToString() != "")
                {
                    model.CoreQualitySign = int.Parse(row["CoreQualitySign"].ToString());
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
            strSql.Append("select StockDetailID,StockListID,TrayID,CoreCode,CorePositionID,CoreQualitySign,Remarks ");
            strSql.Append(" FROM StockDetail ");
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
            strSql.Append(" StockDetailID,StockListID,TrayID,CoreCode,CorePositionID,CoreQualitySign,Remarks ");
            strSql.Append(" FROM StockDetail ");
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
            strSql.Append("select count(1) FROM StockDetail ");
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
                strSql.Append("order by T.StockDetailID desc");
            }
            strSql.Append(")AS Row, T.*  from StockDetail T ");
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
            parameters[0].Value = "StockDetail";
            parameters[1].Value = "StockDetailID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod
        public bool DeteleModelByTrayID(string trayID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockDetail ");
            strSql.Append(" where TrayID=@TrayID");
            SqlParameter[] parameters = {
					new SqlParameter("@TrayID", SqlDbType.NVarChar)
			};
            parameters[0].Value = trayID;

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

        public StockDetailModel GetModelByTrayID(string trayID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 StockDetailID,StockListID,TrayID,CoreCode,CorePositionID,CoreQualitySign,Remarks from StockDetail ");
            strSql.Append(" where TrayID=@TrayID");
            SqlParameter[] parameters = {
					new SqlParameter("@TrayID", SqlDbType.NVarChar)
			};
            parameters[0].Value = trayID;

            StockDetailModel model = new StockDetailModel();
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
		#endregion  ExtensionMethod
	}
}

