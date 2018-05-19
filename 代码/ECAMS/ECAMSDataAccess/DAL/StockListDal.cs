/**  版本信息模板在安装目录下，可自行修改。
* StockList.cs
*
* 功 能： N/A
* 类 名： StockList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:05   N/A    初版
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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;

using System.Linq;
 
namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:StockList
	/// </summary>
	public partial class StockListDal
	{
		public StockListDal()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long StockListID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from StockList");
            strSql.Append(" where StockListID=@StockListID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt)
			};
            parameters[0].Value = StockListID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(StockListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StockList(");
            strSql.Append("ManaTaskID,StoreHouseName,StockID,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,ProductBatchNum,InHouseTime,UpdateTime,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@ManaTaskID,@StoreHouseName,@StockID,@ProductCode,@ProductNum,@ProductStatus,@ProductFrameCode,@ProductName,@GoodsSiteName,@ProductBatchNum,@InHouseTime,@UpdateTime,@Remarks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
					new SqlParameter("@StockID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductNum", SqlDbType.Int,4),
					new SqlParameter("@ProductStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductFrameCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductBatchNum", SqlDbType.NVarChar,50),
					new SqlParameter("@InHouseTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.ManaTaskID;
            parameters[1].Value = model.StoreHouseName;
            parameters[2].Value = model.StockID;
            parameters[3].Value = model.ProductCode;
            parameters[4].Value = model.ProductNum;
            parameters[5].Value = model.ProductStatus;
            parameters[6].Value = model.ProductFrameCode;
            parameters[7].Value = model.ProductName;
            parameters[8].Value = model.GoodsSiteName;
            parameters[9].Value = model.ProductBatchNum;
            parameters[10].Value = model.InHouseTime;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.Remarks;

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
        public bool Update(StockListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StockList set ");
            strSql.Append("ManaTaskID=@ManaTaskID,");
            strSql.Append("StoreHouseName=@StoreHouseName,");
            strSql.Append("StockID=@StockID,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("ProductNum=@ProductNum,");
            strSql.Append("ProductStatus=@ProductStatus,");
            strSql.Append("ProductFrameCode=@ProductFrameCode,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("GoodsSiteName=@GoodsSiteName,");
            strSql.Append("ProductBatchNum=@ProductBatchNum,");
            strSql.Append("InHouseTime=@InHouseTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("Remarks=@Remarks");
            strSql.Append(" where StockListID=@StockListID");
            SqlParameter[] parameters = {
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
					new SqlParameter("@StockID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductNum", SqlDbType.Int,4),
					new SqlParameter("@ProductStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductFrameCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductBatchNum", SqlDbType.NVarChar,50),
					new SqlParameter("@InHouseTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.ManaTaskID;
            parameters[1].Value = model.StoreHouseName;
            parameters[2].Value = model.StockID;
            parameters[3].Value = model.ProductCode;
            parameters[4].Value = model.ProductNum;
            parameters[5].Value = model.ProductStatus;
            parameters[6].Value = model.ProductFrameCode;
            parameters[7].Value = model.ProductName;
            parameters[8].Value = model.GoodsSiteName;
            parameters[9].Value = model.ProductBatchNum;
            parameters[10].Value = model.InHouseTime;
            parameters[11].Value = model.UpdateTime;
            parameters[12].Value = model.Remarks;
            parameters[13].Value = model.StockListID;

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
        public bool Delete(long StockListID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockList ");
            strSql.Append(" where StockListID=@StockListID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt)
			};
            parameters[0].Value = StockListID;

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
        public bool DeleteList(string StockListIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockList ");
            strSql.Append(" where StockListID in (" + StockListIDlist + ")  ");
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
        public StockListModel GetModel(long StockListID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 StockListID,ManaTaskID,StoreHouseName,StockID,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,ProductBatchNum,InHouseTime,UpdateTime,Remarks from StockList ");
            strSql.Append(" where StockListID=@StockListID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt)
			};
            parameters[0].Value = StockListID;

            StockListModel model = new StockListModel();
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
        public StockListModel DataRowToModel(DataRow row)
        {
            StockListModel model = new StockListModel();
            if (row != null)
            {
                if (row["StockListID"] != null && row["StockListID"].ToString() != "")
                {
                    model.StockListID = long.Parse(row["StockListID"].ToString());
                }
                if (row["ManaTaskID"] != null && row["ManaTaskID"].ToString() != "")
                {
                    model.ManaTaskID = long.Parse(row["ManaTaskID"].ToString());
                }
                if (row["StoreHouseName"] != null)
                {
                    model.StoreHouseName = row["StoreHouseName"].ToString();
                }
                if (row["StockID"] != null && row["StockID"].ToString() != "")
                {
                    model.StockID = long.Parse(row["StockID"].ToString());
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["ProductNum"] != null && row["ProductNum"].ToString() != "")
                {
                    model.ProductNum = int.Parse(row["ProductNum"].ToString());
                }
                if (row["ProductStatus"] != null)
                {
                    model.ProductStatus = row["ProductStatus"].ToString();
                }
                if (row["ProductFrameCode"] != null)
                {
                    model.ProductFrameCode = row["ProductFrameCode"].ToString();
                }
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["GoodsSiteName"] != null)
                {
                    model.GoodsSiteName = row["GoodsSiteName"].ToString();
                }
                if (row["ProductBatchNum"] != null)
                {
                    model.ProductBatchNum = row["ProductBatchNum"].ToString();
                }
                if (row["InHouseTime"] != null && row["InHouseTime"].ToString() != "")
                {
                    model.InHouseTime = DateTime.Parse(row["InHouseTime"].ToString());
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
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
            strSql.Append("select StockListID,ManaTaskID,StoreHouseName,StockID,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,ProductBatchNum,InHouseTime,UpdateTime,Remarks ");
            strSql.Append(" FROM StockList ");
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
            strSql.Append(" StockListID,ManaTaskID,StoreHouseName,StockID,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,ProductBatchNum,InHouseTime,UpdateTime,Remarks ");
            strSql.Append(" FROM StockList ");
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
            strSql.Append("select count(1) FROM StockList ");
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
                strSql.Append("order by T.StockListID desc");
            }
            strSql.Append(")AS Row, T.*  from StockList T ");
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
            parameters[0].Value = "StockList";
            parameters[1].Value = "StockListID";
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
        /// 获取删除库存列表hash
        /// </summary>
        /// <param name="stockListID"></param>
        /// <returns></returns>
        public Hashtable GetDeleteModelHs(long stockListID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockList ");
            strSql.Append(" where StockListID=@StockListID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt)
			};
            parameters[0].Value = stockListID;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="completeTime"></param>
        /// <param name="stockListID"></param>
        /// <returns></returns>
        public bool UpdateCompleteTime(DateTime completeTime, long stockListID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StockList set ");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where StockListID=@StockListID");
            SqlParameter[] parameters = {
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8)};
            parameters[0].Value = completeTime;
            parameters[1].Value = stockListID;
            
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

        public Hashtable GetUpdateCompleteTimeHs(DateTime completeTime, long stockListID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StockList set ");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where StockListID=@StockListID");
            SqlParameter[] parameters = {
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8)};
            parameters[0].Value = completeTime;
            parameters[1].Value = stockListID;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }

        public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from StockList");

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
        /// 获得数据列表
        /// </summary>
        public DataSet GetQueryList(string houseName, string rowth, string columnth, string layerth, string productStatus, string workFlowStatus, int workFlowTime)
        {
            string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder strSql = new StringBuilder();
            int rowthTemp = 0;
            int columnTemp = 0;
            int layerTemp = 0;
            if (rowth != "所有")
            {
               rowthTemp = int.Parse(rowth);
            }
           
            if (columnth != "所有")
            { columnTemp = int.Parse(columnth); }
             
            if (layerth != "所有")
            {
                layerTemp = int.Parse(layerth); 
            }
           
            strSql.Append("select StockListID,ManaTaskID,StoreHouseName,StockID,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,GoodsSiteRow,GoodsSiteColumn,GoodsSiteLayer,ProductBatchNum,InHouseTime,UpdateTime,Remarks ");
            strSql.Append("From (select StockListID,ManaTaskID,StoreHouseName,StockID,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,GoodsSiteRow,GoodsSiteColumn,GoodsSiteLayer,ProductBatchNum,InHouseTime,UpdateTime,Remarks ");
            strSql.Append(", case when StoreHouseName = '所有' then 1 else 0 end as flag1");
            strSql.Append(", case when GoodsSiteRow = " + rowthTemp + " then 1 else 0 end as flag2");
            strSql.Append(", case when GoodsSiteColumn = " + columnTemp + " then 1 else 0 end as flag3");
            strSql.Append(", case when GoodsSiteLayer = "+layerTemp+" then 1 else 0 end as flag4");
            strSql.Append(", case when ProductStatus = '所有' then 1 else 0 end as flag5 ");
           
            strSql.Append(" FROM StockList ) as temp");
            strSql.Append(" where  temp.flag1 = 0 and temp.flag2 = 0  and temp.flag2 = 0 and temp.flag3 = 0 and temp.flag4 = 0 and temp.flag5 = 0 ");
            if (workFlowStatus == "已到达")
            {
                strSql.Append("and datediff(hour,UpdateTime,'" + dtNow + "')" + ">=" + workFlowTime);
            }
            else if (workFlowStatus == "未到达")
            {
                strSql.Append("and datediff(hour,UpdateTime,'" + dtNow + "')" + "<" + workFlowTime);
            }
           
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取符合条件自动出库库存列表
        /// </summary>
        /// <param name="productStatus">物料状态</param>
        /// <param name="timeInterval">当前时间减去更新时间的时间差（单位小时）</param>
        /// <returns></returns>
        public DataTable GetTimeArriveModelList(string productStatus, int timeInterval, Dictionary<string, List<string>> outStorageBatchDic)
        {
            if (outStorageBatchDic == null || outStorageBatchDic.Count == 0)
            {
                return null;
            }
            string batchNumSqlA1 = "";
            string batchNumSqlB1 = "";
            for (int i = 0; i < outStorageBatchDic.Keys.Count; i++)
            {
                string houseName = outStorageBatchDic.Keys.ElementAt(i);
                List<string> batchesList = outStorageBatchDic[houseName];
                switch (houseName)
                {
                    case "A1库房":
                        for (int a = 0; a < batchesList.Count; a++)
                        {
                            if (a == 0)
                            {
                                batchNumSqlA1 += "(ProductBatchNum ='" + batchesList[a] + "'";
                            }
                            else
                            {
                                batchNumSqlA1 += "or ProductBatchNum='" + batchesList[a] + "'";
                            }
                        }
                        batchNumSqlA1 += "and StoreHouseName = '" + houseName + "')";
                        break;
                    case "B1库房":
                        for (int b = 0; b < batchesList.Count; b++)
                        {
                            if (b == 0)
                            {
                                batchNumSqlB1 += "(ProductBatchNum ='" + batchesList[b] + "'";
                            }
                            else
                            {
                                batchNumSqlB1 += "or ProductBatchNum='" + batchesList[b] + "'";
                            }

                        }
                        batchNumSqlB1 += "and StoreHouseName = '" + houseName+"')";
                        break;
                }
               
            }
            string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  StockList .*  FROM GoodsSite INNER JOIN Stock ON GoodsSite.GoodsSiteID = Stock.GoodsSiteID INNER JOIN StockList ON Stock.StockID = StockList.StockID ");
            strSql.Append(" where  GoodsSiteStoreStatus='有货' and GoodsSiteRunStatus = '任务完成' and");
            strSql.Append( " ProductStatus = '" + productStatus + "' and datediff(hour,UpdateTime,'" + dtNow + "') >=" + timeInterval
                + " and UpdateTime != '' and (" + batchNumSqlA1 + "or" +batchNumSqlB1+")");

             StockListModel model = new StockListModel();
             DataSet ds = DbHelperSQL.Query(strSql.ToString());
             if (ds.Tables[0].Rows.Count > 0)
             {
                 return  ds.Tables[0] ;
             }
             else
             {
                 return null;
             }
           
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年5月11日
        /// 内容:获取所有不重复的产品批次号
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllProductBatchNums()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct ProductBatchNum from StockList Where ProductBatchNum is not null");
         
            DataSet ds= DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

