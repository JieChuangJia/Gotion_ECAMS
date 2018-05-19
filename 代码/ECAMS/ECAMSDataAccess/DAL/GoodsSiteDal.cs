/**  版本信息模板在安装目录下，可自行修改。
* GoodsSite.cs
*
* 功 能： N/A
* 类 名： GoodsSite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-11-24 17:33:01   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：np　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using ECAMSModel;

namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:GoodsSite
	/// </summary>
	public partial class GoodsSiteDal
	{
		public GoodsSiteDal()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public long GetMaxId()
        {
            return DbHelperSQL.GetMaxID("GoodsSiteID", "GoodsSite");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GoodsSiteID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from GoodsSite");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)			};
            parameters[0].Value = GoodsSiteID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ECAMSDataAccess.GoodsSiteModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into GoodsSite(");
            strSql.Append("GoodsSiteID,GoodsSiteName,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID)");
            strSql.Append(" values (");
            strSql.Append("@GoodsSiteID,@GoodsSiteName,@GoodsSiteType,@GoodsSiteLayer,@GoodsSiteColumn,@GoodsSiteRow,@DeviceID,@GoodsSiteStoreStatus,@GoodsSiteRunStatus,@GoodsSiteInOutType,@GoodsSiteStoreType,@LogicStoreAreaID,@StoreAreaID)");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteType", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteLayer", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteColumn", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteRow", SqlDbType.Int,4),
					new SqlParameter("@DeviceID", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteStoreType", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaID", SqlDbType.Int,4),
					new SqlParameter("@StoreAreaID", SqlDbType.Int,4)};
            parameters[0].Value = model.GoodsSiteID;
            parameters[1].Value = model.GoodsSiteName;
            parameters[2].Value = model.GoodsSiteType;
            parameters[3].Value = model.GoodsSiteLayer;
            parameters[4].Value = model.GoodsSiteColumn;
            parameters[5].Value = model.GoodsSiteRow;
            parameters[6].Value = model.DeviceID;
            parameters[7].Value = model.GoodsSiteStoreStatus;
            parameters[8].Value = model.GoodsSiteRunStatus;
            parameters[9].Value = model.GoodsSiteInOutType;
            parameters[10].Value = model.GoodsSiteStoreType;
            parameters[11].Value = model.LogicStoreAreaID;
            parameters[12].Value = model.StoreAreaID;

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
        public bool Update(ECAMSDataAccess.GoodsSiteModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsSite set ");
            strSql.Append("GoodsSiteName=@GoodsSiteName,");
            strSql.Append("GoodsSiteType=@GoodsSiteType,");
            strSql.Append("GoodsSiteLayer=@GoodsSiteLayer,");
            strSql.Append("GoodsSiteColumn=@GoodsSiteColumn,");
            strSql.Append("GoodsSiteRow=@GoodsSiteRow,");
            strSql.Append("DeviceID=@DeviceID,");
            strSql.Append("GoodsSiteStoreStatus=@GoodsSiteStoreStatus,");
            strSql.Append("GoodsSiteRunStatus=@GoodsSiteRunStatus,");
            strSql.Append("GoodsSiteInOutType=@GoodsSiteInOutType,");
            strSql.Append("GoodsSiteStoreType=@GoodsSiteStoreType,");
            strSql.Append("LogicStoreAreaID=@LogicStoreAreaID,");
            strSql.Append("StoreAreaID=@StoreAreaID");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteType", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteLayer", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteColumn", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteRow", SqlDbType.Int,4),
					new SqlParameter("@DeviceID", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteStoreType", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaID", SqlDbType.Int,4),
					new SqlParameter("@StoreAreaID", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)};
            parameters[0].Value = model.GoodsSiteName;
            parameters[1].Value = model.GoodsSiteType;
            parameters[2].Value = model.GoodsSiteLayer;
            parameters[3].Value = model.GoodsSiteColumn;
            parameters[4].Value = model.GoodsSiteRow;
            parameters[5].Value = model.DeviceID;
            parameters[6].Value = model.GoodsSiteStoreStatus;
            parameters[7].Value = model.GoodsSiteRunStatus;
            parameters[8].Value = model.GoodsSiteInOutType;
            parameters[9].Value = model.GoodsSiteStoreType;
            parameters[10].Value = model.LogicStoreAreaID;
            parameters[11].Value = model.StoreAreaID;
            parameters[12].Value = model.GoodsSiteID;

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
        public bool Delete(int GoodsSiteID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GoodsSite ");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)			};
            parameters[0].Value = GoodsSiteID;

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
        public bool DeleteList(string GoodsSiteIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from GoodsSite ");
            strSql.Append(" where GoodsSiteID in (" + GoodsSiteIDlist + ")  ");
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
        public ECAMSDataAccess.GoodsSiteModel GetModel(int GoodsSiteID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GoodsSiteID,GoodsSiteName,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID from GoodsSite ");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)			};
            parameters[0].Value = GoodsSiteID;

            ECAMSDataAccess.GoodsSiteModel model = new ECAMSDataAccess.GoodsSiteModel();
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
        public ECAMSDataAccess.GoodsSiteModel DataRowToModel(DataRow row)
        {
            ECAMSDataAccess.GoodsSiteModel model = new ECAMSDataAccess.GoodsSiteModel();
            if (row != null)
            {
                if (row["GoodsSiteID"] != null && row["GoodsSiteID"].ToString() != "")
                {
                    model.GoodsSiteID = int.Parse(row["GoodsSiteID"].ToString());
                }
                if (row["GoodsSiteName"] != null)
                {
                    model.GoodsSiteName = row["GoodsSiteName"].ToString();
                }
                if (row["GoodsSiteType"] != null)
                {
                    model.GoodsSiteType = row["GoodsSiteType"].ToString();
                }
                if (row["GoodsSiteLayer"] != null && row["GoodsSiteLayer"].ToString() != "")
                {
                    model.GoodsSiteLayer = int.Parse(row["GoodsSiteLayer"].ToString());
                }
                if (row["GoodsSiteColumn"] != null && row["GoodsSiteColumn"].ToString() != "")
                {
                    model.GoodsSiteColumn = int.Parse(row["GoodsSiteColumn"].ToString());
                }
                if (row["GoodsSiteRow"] != null && row["GoodsSiteRow"].ToString() != "")
                {
                    model.GoodsSiteRow = int.Parse(row["GoodsSiteRow"].ToString());
                }
                if (row["DeviceID"] != null)
                {
                    model.DeviceID = row["DeviceID"].ToString();
                }
                if (row["GoodsSiteStoreStatus"] != null)
                {
                    model.GoodsSiteStoreStatus = row["GoodsSiteStoreStatus"].ToString();
                }
                if (row["GoodsSiteRunStatus"] != null)
                {
                    model.GoodsSiteRunStatus = row["GoodsSiteRunStatus"].ToString();
                }
                if (row["GoodsSiteInOutType"] != null)
                {
                    model.GoodsSiteInOutType = row["GoodsSiteInOutType"].ToString();
                }
                if (row["GoodsSiteStoreType"] != null)
                {
                    model.GoodsSiteStoreType = row["GoodsSiteStoreType"].ToString();
                }
                if (row["LogicStoreAreaID"] != null && row["LogicStoreAreaID"].ToString() != "")
                {
                    model.LogicStoreAreaID = int.Parse(row["LogicStoreAreaID"].ToString());
                }
                if (row["StoreAreaID"] != null && row["StoreAreaID"].ToString() != "")
                {
                    model.StoreAreaID = int.Parse(row["StoreAreaID"].ToString());
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
            strSql.Append("select GoodsSiteID,GoodsSiteName,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID ");
            strSql.Append(" FROM GoodsSite ");
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
            strSql.Append(" GoodsSiteID,GoodsSiteName,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID ");
            strSql.Append(" FROM GoodsSite ");
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
            strSql.Append("select count(1) FROM GoodsSite ");
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
                strSql.Append("order by T.GoodsSiteID desc");
            }
            strSql.Append(")AS Row, T.*  from GoodsSite T ");
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
            parameters[0].Value = "GoodsSite";
            parameters[1].Value = "GoodsSiteID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
		#region  ExtensionMethod
         /// <summary>
         /// 更新货位存储状态、任务完成状态
         /// </summary>
         /// <param name="storeStatus"></param>
         /// <param name="runStatus"></param>
         /// <param name="goodssiteID"></param>
         /// <returns></returns>
         public  bool Update (string storeStatus,string runStatus,int goodssiteID)
         {
             StringBuilder strSql = new StringBuilder();
             strSql.Append("update GoodsSite set ");
      
             strSql.Append("GoodsSiteStoreStatus=@GoodsSiteStoreStatus,");
             strSql.Append("GoodsSiteRunStatus=@GoodsSiteRunStatus");
          
             strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
             SqlParameter[] parameters = {
					
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
				
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)};
             parameters[0].Value = storeStatus;
             parameters[1].Value = runStatus;
             parameters[2].Value = goodssiteID;
      
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
         /// 更新货位存储状态、任务完成状态
         /// </summary>
         /// <param name="storeStatus"></param>
         /// <param name="runStatus"></param>
         /// <param name="goodssiteID"></param>
         /// <returns></returns>
         public bool Update(string storeStatus, string runStatus,string gsInOrOut, int goodssiteID)
         {
             StringBuilder strSql = new StringBuilder();
             strSql.Append("update GoodsSite set ");

             strSql.Append("GoodsSiteStoreStatus=@GoodsSiteStoreStatus,");
             strSql.Append("GoodsSiteRunStatus=@GoodsSiteRunStatus,");
             strSql.Append("GoodsSiteInOutType=@GoodsSiteInOutType");
             strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
             SqlParameter[] parameters = {
					
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
				    new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)};
             parameters[0].Value = storeStatus;
             parameters[1].Value = runStatus;
             parameters[2].Value = gsInOrOut;
             parameters[3].Value = goodssiteID;

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
        /// 获取排、列、层最大值
        /// </summary>
        /// <param name="logicStoreAreaID"></param>
        /// <returns></returns>
        public DataSet GetRowColumnLayer(int logicStoreAreaID)
        {
            string sql = "select max(GoodsSiteRow) as GoodsSiteRow,max(GoodsSiteColumn) as GoodsSiteColumn ,"
                +"max(GoodsSiteLayer) as GoodsSiteLayer from GoodsSite "
                + " where LogicStoreAreaID =" + logicStoreAreaID;
            return DbHelperSQL.Query(sql);
        }

        public GoodsSiteModel GetModel(string storeStatus, string runStatus, int goodsSiteID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GoodsSiteID,GoodsSiteName,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID from GoodsSite ");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
            strSql.Append(" and GoodsSiteStoreStatus=@GoodsSiteStoreStatus ");
            strSql.Append(" and GoodsSiteRunStatus=@GoodsSiteRunStatus ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),	
                    new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
                    new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50)};
            parameters[0].Value = goodsSiteID;
            parameters[1].Value = storeStatus;
            parameters[2].Value = runStatus;

            ECAMSDataAccess.GoodsSiteModel model = new ECAMSDataAccess.GoodsSiteModel();
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
        /// 更新货位状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="storeStatus"></param>
        /// <param name="runStatus"></param>
        /// <param name="goodssiteID"></param>
        /// <returns></returns>
        public Hashtable GetUpdateModelHs(string storeStatus,string runStatus,string inOrOutStore,int goodssiteID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsSite set ");

            strSql.Append("GoodsSiteStoreStatus=@GoodsSiteStoreStatus,");
            strSql.Append("GoodsSiteRunStatus=@GoodsSiteRunStatus,");
            strSql.Append("GoodsSiteInOutType=@GoodsSiteInOutType");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
            SqlParameter[] parameters = {
					
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)};
            parameters[0].Value = storeStatus;
            parameters[1].Value = runStatus;
            parameters[2].Value = inOrOutStore;
            parameters[3].Value = goodssiteID;
          
            hs.Add(strSql.ToString(), parameters);
            return hs;
      
        }

        /// <summary>
        /// 作者:np
        /// 时间:2014年6月10日
        /// 内容:更新货位状态
        /// </summary>
        /// <param name="storeStatus"></param>
        /// <param name="runStatus"></param>
        /// <param name="inOrOutStore"></param>
        /// <param name="goodsSiteID"></param>
        /// <returns></returns>
        public bool UpdateModelByGsID(string storeStatus, string runStatus, string inOrOutStore, int goodsSiteID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsSite set ");

            strSql.Append("GoodsSiteStoreStatus=@GoodsSiteStoreStatus,");
            strSql.Append("GoodsSiteRunStatus=@GoodsSiteRunStatus,");
            strSql.Append("GoodsSiteInOutType=@GoodsSiteInOutType");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
            SqlParameter[] parameters = {
					
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)};
            parameters[0].Value = storeStatus;
            parameters[1].Value = runStatus;
            parameters[2].Value = inOrOutStore;
            parameters[3].Value = goodsSiteID;
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

        public DataTable GetStatusNum(int  logisAreaID, int rowth)
        {
            string sql = "select (case when GoodsSiteStoreStatus ='空货位' and GoodsSiteRunStatus = '待用' then count(GoodsSiteStoreStatus) else 0 end) as  空货位";
            sql += ",(case when GoodsSiteStoreStatus ='有货' and GoodsSiteRunStatus = '任务完成' then count(GoodsSiteStoreStatus) else 0 end) as  有货";
            sql += ",(case when GoodsSiteStoreStatus ='空料框' and  GoodsSiteRunStatus = '任务完成' then count(GoodsSiteStoreStatus) else 0 end) as  空料框";
            sql += ",(case when (GoodsSiteStoreStatus ='空料框' or GoodsSiteStoreStatus = '有货') and  GoodsSiteRunStatus = '任务锁定' then count(GoodsSiteStoreStatus) else 0 end) as  任务锁定";
            sql += ",(case when   GoodsSiteRunStatus = '异常' then count(GoodsSiteStoreStatus) else 0 end) as  异常";

            sql += " from GoodsSite where  StoreAreaID =" + logisAreaID + " and GoodsSiteRow = " + rowth + " group by GoodsSiteStoreStatus,GoodsSiteRunStatus";

            DataSet ds = DbHelperSQL.Query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }  
        }

        public bool FormatGoogsSite(string storeStatus,string runStatus,string gsInOrOutType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GoodsSite set ");
            strSql.Append("GoodsSiteStoreStatus='" + storeStatus+"',");
            strSql.Append("GoodsSiteRunStatus='" + runStatus+"',");
            strSql.Append("GoodsSiteInOutType='" + gsInOrOutType + "'");
            
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
        public GoodsSiteModel GetOutHouseModel(int GoodsSiteID, string gsStoreStatus, string gsRunStatus)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GoodsSiteID,GoodsSiteName,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID from GoodsSite ");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID ");
            strSql.Append(" and GoodsSiteStoreStatus=@GoodsSiteStoreStatus ");
            strSql.Append(" and GoodsSiteRunStatus=@GoodsSiteRunStatus ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4)	,
                    new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
        	        new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50)};
            parameters[0].Value = GoodsSiteID;
            parameters[1].Value = gsStoreStatus;
            parameters[2].Value = gsRunStatus;

            ECAMSDataAccess.GoodsSiteModel model = new ECAMSDataAccess.GoodsSiteModel();
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
        /// 作者:np
        /// 时间:2014年6月5日
        /// 内容:获取某库房某一排的所有货位
        /// </summary>
        /// <param name="rowth"></param>
        /// <param name="storeAreaID"></param>
        /// <returns></returns>
        public DataTable GetRowGs(int rowth, int storeAreaID)
        {
            string sql = "select * from GoodsSite where  StoreAreaID =" + storeAreaID + " and GoodsSiteRow = " + rowth 
                + "and GoodsSiteRunStatus != '" + EnumGSRunStatus.异常.ToString() + "'" ;
          
            DataSet ds = DbHelperSQL.Query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }  
        }

        public DataTable GetColumnGs(int rowth, int columnth, int storeAreaID)
        {
            string sql = "select * from GoodsSite where  StoreAreaID =" + storeAreaID + " and GoodsSiteRow = " + rowth + " and GoodsSiteColumn=" + columnth
                 + "and GoodsSiteRunStatus != '" + EnumGSRunStatus.异常.ToString() + "'";

            DataSet ds = DbHelperSQL.Query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }  
        }

        public DataTable GetLayerGs(int rowsth, int layerth, int storeAreaID)
        {
            string sql = "select * from GoodsSite where  StoreAreaID =" + storeAreaID + " and GoodsSiteRow = " + rowsth + " and GoodsSiteLayer=" + layerth
                 + "and GoodsSiteRunStatus != '" + EnumGSRunStatus.异常.ToString() + "'";

            DataSet ds = DbHelperSQL.Query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }  
        }
		#endregion  ExtensionMethod
	}
}

