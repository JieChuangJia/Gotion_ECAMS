using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:View_QueryStockList
    /// </summary>
    public partial class View_QueryStockListDal
    {
        public View_QueryStockListDal()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GoodsSiteID, string GoodsSiteName, int GoodsSiteRow, int GoodsSiteColumn, int GoodsSiteLayer, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, long StockID, long StockListID, string StoreHouseName, string ProductStatus, string ProductName, DateTime InHouseTime, DateTime UpdateTime, long ManaTaskID, string ProductCode, int ProductNum, string ProductBatchNum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from View_QueryStockList");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID and GoodsSiteName=@GoodsSiteName and GoodsSiteRow=@GoodsSiteRow and GoodsSiteColumn=@GoodsSiteColumn and GoodsSiteLayer=@GoodsSiteLayer and GoodsSiteStoreStatus=@GoodsSiteStoreStatus and GoodsSiteRunStatus=@GoodsSiteRunStatus and GoodsSiteInOutType=@GoodsSiteInOutType and StockID=@StockID and StockListID=@StockListID and StoreHouseName=@StoreHouseName and ProductStatus=@ProductStatus and ProductName=@ProductName and InHouseTime=@InHouseTime and UpdateTime=@UpdateTime and ManaTaskID=@ManaTaskID and ProductCode=@ProductCode and ProductNum=@ProductNum and ProductBatchNum=@ProductBatchNum ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRow", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteColumn", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteLayer", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@StockID", SqlDbType.BigInt,8),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@InHouseTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductNum", SqlDbType.Int,4),
					new SqlParameter("@ProductBatchNum", SqlDbType.NVarChar,50)			};
            parameters[0].Value = GoodsSiteID;
            parameters[1].Value = GoodsSiteName;
            parameters[2].Value = GoodsSiteRow;
            parameters[3].Value = GoodsSiteColumn;
            parameters[4].Value = GoodsSiteLayer;
            parameters[5].Value = GoodsSiteStoreStatus;
            parameters[6].Value = GoodsSiteRunStatus;
            parameters[7].Value = GoodsSiteInOutType;
            parameters[8].Value = StockID;
            parameters[9].Value = StockListID;
            parameters[10].Value = StoreHouseName;
            parameters[11].Value = ProductStatus;
            parameters[12].Value = ProductName;
            parameters[13].Value = InHouseTime;
            parameters[14].Value = UpdateTime;
            parameters[15].Value = ManaTaskID;
            parameters[16].Value = ProductCode;
            parameters[17].Value = ProductNum;
            parameters[18].Value = ProductBatchNum;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(View_QueryStockListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into View_QueryStockList(");
            strSql.Append("GoodsSiteID,GoodsSiteName,GoodsSiteRow,GoodsSiteColumn,GoodsSiteLayer,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,StockID,StockListID,StoreHouseName,ProductStatus,ProductName,InHouseTime,UpdateTime,ManaTaskID,ProductCode,ProductNum,ProductBatchNum)");
            strSql.Append(" values (");
            strSql.Append("@GoodsSiteID,@GoodsSiteName,@GoodsSiteRow,@GoodsSiteColumn,@GoodsSiteLayer,@GoodsSiteStoreStatus,@GoodsSiteRunStatus,@GoodsSiteInOutType,@StockID,@StockListID,@StoreHouseName,@ProductStatus,@ProductName,@InHouseTime,@UpdateTime,@ManaTaskID,@ProductCode,@ProductNum,@ProductBatchNum)");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRow", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteColumn", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteLayer", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@StockID", SqlDbType.BigInt,8),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@InHouseTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductNum", SqlDbType.Int,4),
					new SqlParameter("@ProductBatchNum", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.GoodsSiteID;
            parameters[1].Value = model.GoodsSiteName;
            parameters[2].Value = model.GoodsSiteRow;
            parameters[3].Value = model.GoodsSiteColumn;
            parameters[4].Value = model.GoodsSiteLayer;
            parameters[5].Value = model.GoodsSiteStoreStatus;
            parameters[6].Value = model.GoodsSiteRunStatus;
            parameters[7].Value = model.GoodsSiteInOutType;
            parameters[8].Value = model.StockID;
            parameters[9].Value = model.StockListID;
            parameters[10].Value = model.StoreHouseName;
            parameters[11].Value = model.ProductStatus;
            parameters[12].Value = model.ProductName;
            parameters[13].Value = model.InHouseTime;
            parameters[14].Value = model.UpdateTime;
            parameters[15].Value = model.ManaTaskID;
            parameters[16].Value = model.ProductCode;
            parameters[17].Value = model.ProductNum;
            parameters[18].Value = model.ProductBatchNum;

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
        public bool Update(View_QueryStockListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update View_QueryStockList set ");
            strSql.Append("GoodsSiteID=@GoodsSiteID,");
            strSql.Append("GoodsSiteName=@GoodsSiteName,");
            strSql.Append("GoodsSiteRow=@GoodsSiteRow,");
            strSql.Append("GoodsSiteColumn=@GoodsSiteColumn,");
            strSql.Append("GoodsSiteLayer=@GoodsSiteLayer,");
            strSql.Append("GoodsSiteStoreStatus=@GoodsSiteStoreStatus,");
            strSql.Append("GoodsSiteRunStatus=@GoodsSiteRunStatus,");
            strSql.Append("GoodsSiteInOutType=@GoodsSiteInOutType,");
            strSql.Append("StockID=@StockID,");
            strSql.Append("StockListID=@StockListID,");
            strSql.Append("StoreHouseName=@StoreHouseName,");
            strSql.Append("ProductStatus=@ProductStatus,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("InHouseTime=@InHouseTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("ManaTaskID=@ManaTaskID,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("ProductNum=@ProductNum,");
            strSql.Append("ProductBatchNum=@ProductBatchNum");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID and GoodsSiteName=@GoodsSiteName and GoodsSiteRow=@GoodsSiteRow and GoodsSiteColumn=@GoodsSiteColumn and GoodsSiteLayer=@GoodsSiteLayer and GoodsSiteStoreStatus=@GoodsSiteStoreStatus and GoodsSiteRunStatus=@GoodsSiteRunStatus and GoodsSiteInOutType=@GoodsSiteInOutType and StockID=@StockID and StockListID=@StockListID and StoreHouseName=@StoreHouseName and ProductStatus=@ProductStatus and ProductName=@ProductName and InHouseTime=@InHouseTime and UpdateTime=@UpdateTime and ManaTaskID=@ManaTaskID and ProductCode=@ProductCode and ProductNum=@ProductNum and ProductBatchNum=@ProductBatchNum ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRow", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteColumn", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteLayer", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@StockID", SqlDbType.BigInt,8),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@InHouseTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductNum", SqlDbType.Int,4),
					new SqlParameter("@ProductBatchNum", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.GoodsSiteID;
            parameters[1].Value = model.GoodsSiteName;
            parameters[2].Value = model.GoodsSiteRow;
            parameters[3].Value = model.GoodsSiteColumn;
            parameters[4].Value = model.GoodsSiteLayer;
            parameters[5].Value = model.GoodsSiteStoreStatus;
            parameters[6].Value = model.GoodsSiteRunStatus;
            parameters[7].Value = model.GoodsSiteInOutType;
            parameters[8].Value = model.StockID;
            parameters[9].Value = model.StockListID;
            parameters[10].Value = model.StoreHouseName;
            parameters[11].Value = model.ProductStatus;
            parameters[12].Value = model.ProductName;
            parameters[13].Value = model.InHouseTime;
            parameters[14].Value = model.UpdateTime;
            parameters[15].Value = model.ManaTaskID;
            parameters[16].Value = model.ProductCode;
            parameters[17].Value = model.ProductNum;
            parameters[18].Value = model.ProductBatchNum;

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
        public bool Delete(int GoodsSiteID, string GoodsSiteName, int GoodsSiteRow, int GoodsSiteColumn, int GoodsSiteLayer, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, long StockID, long StockListID, string StoreHouseName, string ProductStatus, string ProductName, DateTime InHouseTime, DateTime UpdateTime, long ManaTaskID, string ProductCode, int ProductNum, string ProductBatchNum)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from View_QueryStockList ");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID and GoodsSiteName=@GoodsSiteName and GoodsSiteRow=@GoodsSiteRow and GoodsSiteColumn=@GoodsSiteColumn and GoodsSiteLayer=@GoodsSiteLayer and GoodsSiteStoreStatus=@GoodsSiteStoreStatus and GoodsSiteRunStatus=@GoodsSiteRunStatus and GoodsSiteInOutType=@GoodsSiteInOutType and StockID=@StockID and StockListID=@StockListID and StoreHouseName=@StoreHouseName and ProductStatus=@ProductStatus and ProductName=@ProductName and InHouseTime=@InHouseTime and UpdateTime=@UpdateTime and ManaTaskID=@ManaTaskID and ProductCode=@ProductCode and ProductNum=@ProductNum and ProductBatchNum=@ProductBatchNum ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRow", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteColumn", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteLayer", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@StockID", SqlDbType.BigInt,8),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@InHouseTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductNum", SqlDbType.Int,4),
					new SqlParameter("@ProductBatchNum", SqlDbType.NVarChar,50)			};
            parameters[0].Value = GoodsSiteID;
            parameters[1].Value = GoodsSiteName;
            parameters[2].Value = GoodsSiteRow;
            parameters[3].Value = GoodsSiteColumn;
            parameters[4].Value = GoodsSiteLayer;
            parameters[5].Value = GoodsSiteStoreStatus;
            parameters[6].Value = GoodsSiteRunStatus;
            parameters[7].Value = GoodsSiteInOutType;
            parameters[8].Value = StockID;
            parameters[9].Value = StockListID;
            parameters[10].Value = StoreHouseName;
            parameters[11].Value = ProductStatus;
            parameters[12].Value = ProductName;
            parameters[13].Value = InHouseTime;
            parameters[14].Value = UpdateTime;
            parameters[15].Value = ManaTaskID;
            parameters[16].Value = ProductCode;
            parameters[17].Value = ProductNum;
            parameters[18].Value = ProductBatchNum;

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
        public View_QueryStockListModel GetModel(int GoodsSiteID, string GoodsSiteName, int GoodsSiteRow, int GoodsSiteColumn, int GoodsSiteLayer, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, long StockID, long StockListID, string StoreHouseName, string ProductStatus, string ProductName, DateTime InHouseTime, DateTime UpdateTime, long ManaTaskID, string ProductCode, int ProductNum, string ProductBatchNum)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GoodsSiteID,GoodsSiteName,GoodsSiteRow,GoodsSiteColumn,GoodsSiteLayer,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,StockID,StockListID,StoreHouseName,ProductStatus,ProductName,InHouseTime,UpdateTime,ManaTaskID,ProductCode,ProductNum,ProductBatchNum from View_QueryStockList ");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID and GoodsSiteName=@GoodsSiteName and GoodsSiteRow=@GoodsSiteRow and GoodsSiteColumn=@GoodsSiteColumn and GoodsSiteLayer=@GoodsSiteLayer and GoodsSiteStoreStatus=@GoodsSiteStoreStatus and GoodsSiteRunStatus=@GoodsSiteRunStatus and GoodsSiteInOutType=@GoodsSiteInOutType and StockID=@StockID and StockListID=@StockListID and StoreHouseName=@StoreHouseName and ProductStatus=@ProductStatus and ProductName=@ProductName and InHouseTime=@InHouseTime and UpdateTime=@UpdateTime and ManaTaskID=@ManaTaskID and ProductCode=@ProductCode and ProductNum=@ProductNum and ProductBatchNum=@ProductBatchNum ");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteName", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRow", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteColumn", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteLayer", SqlDbType.Int,4),
					new SqlParameter("@GoodsSiteStoreStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteRunStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@GoodsSiteInOutType", SqlDbType.NVarChar,50),
					new SqlParameter("@StockID", SqlDbType.BigInt,8),
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
					new SqlParameter("@ProductStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,20),
					new SqlParameter("@InHouseTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductNum", SqlDbType.Int,4),
					new SqlParameter("@ProductBatchNum", SqlDbType.NVarChar,50)			};
            parameters[0].Value = GoodsSiteID;
            parameters[1].Value = GoodsSiteName;
            parameters[2].Value = GoodsSiteRow;
            parameters[3].Value = GoodsSiteColumn;
            parameters[4].Value = GoodsSiteLayer;
            parameters[5].Value = GoodsSiteStoreStatus;
            parameters[6].Value = GoodsSiteRunStatus;
            parameters[7].Value = GoodsSiteInOutType;
            parameters[8].Value = StockID;
            parameters[9].Value = StockListID;
            parameters[10].Value = StoreHouseName;
            parameters[11].Value = ProductStatus;
            parameters[12].Value = ProductName;
            parameters[13].Value = InHouseTime;
            parameters[14].Value = UpdateTime;
            parameters[15].Value = ManaTaskID;
            parameters[16].Value = ProductCode;
            parameters[17].Value = ProductNum;
            parameters[18].Value = ProductBatchNum;

            View_QueryStockListModel model = new View_QueryStockListModel();
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
        public View_QueryStockListModel DataRowToModel(DataRow row)
        {
            View_QueryStockListModel model = new View_QueryStockListModel();
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
                if (row["GoodsSiteRow"] != null && row["GoodsSiteRow"].ToString() != "")
                {
                    model.GoodsSiteRow = int.Parse(row["GoodsSiteRow"].ToString());
                }
                if (row["GoodsSiteColumn"] != null && row["GoodsSiteColumn"].ToString() != "")
                {
                    model.GoodsSiteColumn = int.Parse(row["GoodsSiteColumn"].ToString());
                }
                if (row["GoodsSiteLayer"] != null && row["GoodsSiteLayer"].ToString() != "")
                {
                    model.GoodsSiteLayer = int.Parse(row["GoodsSiteLayer"].ToString());
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
                if (row["StockID"] != null && row["StockID"].ToString() != "")
                {
                    model.StockID = long.Parse(row["StockID"].ToString());
                }
                if (row["StockListID"] != null && row["StockListID"].ToString() != "")
                {
                    model.StockListID = long.Parse(row["StockListID"].ToString());
                }
                if (row["StoreHouseName"] != null)
                {
                    model.StoreHouseName = row["StoreHouseName"].ToString();
                }
                if (row["ProductStatus"] != null)
                {
                    model.ProductStatus = row["ProductStatus"].ToString();
                }
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["InHouseTime"] != null && row["InHouseTime"].ToString() != "")
                {
                    model.InHouseTime = DateTime.Parse(row["InHouseTime"].ToString());
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["ManaTaskID"] != null && row["ManaTaskID"].ToString() != "")
                {
                    model.ManaTaskID = long.Parse(row["ManaTaskID"].ToString());
                }
                if (row["ProductCode"] != null)
                {
                    model.ProductCode = row["ProductCode"].ToString();
                }
                if (row["ProductNum"] != null && row["ProductNum"].ToString() != "")
                {
                    model.ProductNum = int.Parse(row["ProductNum"].ToString());
                }
                if (row["ProductBatchNum"] != null)
                {
                    model.ProductBatchNum = row["ProductBatchNum"].ToString();
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
            strSql.Append("select GoodsSiteID,GoodsSiteName,GoodsSiteRow,GoodsSiteColumn,GoodsSiteLayer,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,StockID,StockListID,StoreHouseName,ProductStatus,ProductName,InHouseTime,UpdateTime,ManaTaskID,ProductCode,ProductNum,ProductBatchNum ");
            strSql.Append(" FROM View_QueryStockList ");
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
            strSql.Append(" GoodsSiteID,GoodsSiteName,GoodsSiteRow,GoodsSiteColumn,GoodsSiteLayer,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,StockID,StockListID,StoreHouseName,ProductStatus,ProductName,InHouseTime,UpdateTime,ManaTaskID,ProductCode,ProductNum,ProductBatchNum ");
            strSql.Append(" FROM View_QueryStockList ");
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
            strSql.Append("select count(1) FROM View_QueryStockList ");
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
                strSql.Append("order by T.ProductBatchNum desc");
            }
            strSql.Append(")AS Row, T.*  from View_QueryStockList T ");
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
            parameters[0].Value = "View_QueryStockList";
            parameters[1].Value = "ProductBatchNum";
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetQueryList(string houseName, string rowth, string columnth, string layerth, string productStatus, string workFlowStatus, int workFlowTime
            , string gsRunStatus, string gsStoreStatus, string gsTaskType, string productBatchNum)
        {
            string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder strSql = new StringBuilder();
             
            int rowthTemp = 0;
            int columnTemp = 0;
            int layerTemp = 0;


            int flag1 = 0;
            int flag2 = 0;
            int flag3 = 0;
            int flag4 = 0;
            int flag5 = 0;
            int flag6 = 0;
            int flag7 = 0;
            int flag8 = 0;
            int flag9 = 0;
            if (houseName != "所有")
            {
                flag1 = 1;
            }
            if (rowth != "所有")
            {
                rowthTemp = int.Parse(rowth);
                flag2 = 1;
            }

            if (columnth != "所有")
            {
                columnTemp = int.Parse(columnth);
                flag3 = 1;
            }

            if (layerth != "所有")
            {
                layerTemp = int.Parse(layerth);
                flag4 = 1;
            }
         
            if (productStatus != "所有")
            {
                flag5 = 1;
            }

            if (gsRunStatus != "所有")
            {
                flag6 = 1;
            }
            if (gsStoreStatus != "所有")
            {
                flag7 = 1;
            }
            if (gsTaskType != "所有")
            {
                flag8 = 1;
            }

            if (productBatchNum != "所有")
            {
                flag9 = 1;
            }
            strSql.Append("select *  ");
            strSql.Append("From (select * ");
            strSql.Append(", case when StoreHouseName ='"+houseName+"' then 1 else 0 end as flag1");
            strSql.Append(", case when GoodsSiteRow = " + rowthTemp + " then 1 else 0 end as flag2");
            strSql.Append(", case when GoodsSiteColumn = " + columnTemp + " then 1 else 0 end as flag3");
            strSql.Append(", case when GoodsSiteLayer = " + layerTemp + " then 1 else 0 end as flag4");
            strSql.Append(", case when ProductStatus = '"+productStatus+"' then 1 else 0 end as flag5 ");
            strSql.Append(", case when GoodsSiteRunStatus = '" + gsRunStatus + "' then 1 else 0 end as flag6 ");
            strSql.Append(", case when GoodsSiteStoreStatus = '" + gsStoreStatus + "' then 1 else 0 end as flag7 ");
            strSql.Append(", case when GoodsSiteInOutType = '" + gsTaskType + "' then 1 else 0 end as flag8 ");
            strSql.Append(", case when ProductBatchNum = '" + productBatchNum + "' then 1 else 0 end as flag9 ");

            strSql.Append(" FROM View_QueryStockList ) as temp");
            strSql.Append(" where  temp.flag1 = "+flag1+" and temp.flag2 = "+flag2+"  and temp.flag3 = "
            + flag3 + " and temp.flag4 = " + flag4 + " and temp.flag5 = " + flag5
            + " and temp.flag6 = " + flag6 + " and temp.flag7 = " + flag7 + " and temp.flag8 = " + flag8 
            +" and temp.flag9 = " + flag9 + " and UpdateTime!='' ");
            if (workFlowStatus == "已达到")
            {
                strSql.Append("and datediff(hour,UpdateTime,'" + dtNow + "')" + ">=" + workFlowTime);
            }
            else if (workFlowStatus == "未到达")
            {
                strSql.Append("and datediff(hour,UpdateTime,'" + dtNow + "')" + "<" + workFlowTime);
            }
            //strSql.Append("and GoodsSiteRunStatus ='任务完成' and (GoodsSiteStoreStatus = '有货' or GoodsSiteStoreStatus = '空料框')");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public View_QueryStockListModel GetModelByGSID(int gsID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select *  from View_QueryStockList");

            strSql.Append(" where GoodsSiteID = " +gsID);
            View_QueryStockListModel model = new View_QueryStockListModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString() );
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public View_QueryStockListModel GetModelByGsName(string storeHouseName, string rowth, string columnth, string layerth)
         {
             StringBuilder strSql = new StringBuilder();
            strSql.Append(" select *  from View_QueryStockList");

            strSql.Append(" where StoreHouseName = '" + storeHouseName + "'");
            strSql.Append(" and GoodsSiteRow = " + rowth);
            strSql.Append(" and GoodsSiteColumn = " + columnth);
            strSql.Append(" and GoodsSiteLayer = " + layerth);
            View_QueryStockListModel model = new View_QueryStockListModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString() );
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
        public View_QueryStockListModel GetOutHouseModel(int GoodsSiteID, string gsStoreStatus, string gsRunStatus)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from View_QueryStockList ");
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
        #endregion  ExtensionMethod
    }
}

