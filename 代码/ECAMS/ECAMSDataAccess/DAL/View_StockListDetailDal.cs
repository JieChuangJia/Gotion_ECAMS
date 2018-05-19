using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using ECAMSModel;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:View_StockListDetail
    /// </summary>
    public partial class View_StockListDetailDal
    {
        public View_StockListDetailDal()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long StockListID, long ManaTaskID, string StoreHouseName, string ProductCode, int ProductNum, string ProductStatus, string ProductFrameCode, string ProductName, string GoodsSiteName, string ProductBatchNum, DateTime InHouseTime, DateTime UpdateTime, string Remarks, long StockDetailID, string CoreCode, int CoreQualitySign, int CorePositionID, string TrayID, string GoodsSiteType, int GoodsSiteLayer, int GoodsSiteColumn, int GoodsSiteRow, string DeviceID, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, string GoodsSiteStoreType, int LogicStoreAreaID, int StoreAreaID, int GoodsSiteID, long StockID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from View_StockListDetail");
            strSql.Append(" where StockListID=@StockListID and ManaTaskID=@ManaTaskID and StoreHouseName=@StoreHouseName and ProductCode=@ProductCode and ProductNum=@ProductNum and ProductStatus=@ProductStatus and ProductFrameCode=@ProductFrameCode and ProductName=@ProductName and GoodsSiteName=@GoodsSiteName and ProductBatchNum=@ProductBatchNum and InHouseTime=@InHouseTime and UpdateTime=@UpdateTime and Remarks=@Remarks and StockDetailID=@StockDetailID and CoreCode=@CoreCode and CoreQualitySign=@CoreQualitySign and CorePositionID=@CorePositionID and TrayID=@TrayID and GoodsSiteType=@GoodsSiteType and GoodsSiteLayer=@GoodsSiteLayer and GoodsSiteColumn=@GoodsSiteColumn and GoodsSiteRow=@GoodsSiteRow and DeviceID=@DeviceID and GoodsSiteStoreStatus=@GoodsSiteStoreStatus and GoodsSiteRunStatus=@GoodsSiteRunStatus and GoodsSiteInOutType=@GoodsSiteInOutType and GoodsSiteStoreType=@GoodsSiteStoreType and LogicStoreAreaID=@LogicStoreAreaID and StoreAreaID=@StoreAreaID and GoodsSiteID=@GoodsSiteID and StockID=@StockID ");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
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
					new SqlParameter("@StockDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CoreQualitySign", SqlDbType.Int,4),
					new SqlParameter("@CorePositionID", SqlDbType.Int,4),
					new SqlParameter("@TrayID", SqlDbType.NVarChar,50),
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
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@StockID", SqlDbType.BigInt,8)			};
            parameters[0].Value = StockListID;
            parameters[1].Value = ManaTaskID;
            parameters[2].Value = StoreHouseName;
            parameters[3].Value = ProductCode;
            parameters[4].Value = ProductNum;
            parameters[5].Value = ProductStatus;
            parameters[6].Value = ProductFrameCode;
            parameters[7].Value = ProductName;
            parameters[8].Value = GoodsSiteName;
            parameters[9].Value = ProductBatchNum;
            parameters[10].Value = InHouseTime;
            parameters[11].Value = UpdateTime;
            parameters[12].Value = Remarks;
            parameters[13].Value = StockDetailID;
            parameters[14].Value = CoreCode;
            parameters[15].Value = CoreQualitySign;
            parameters[16].Value = CorePositionID;
            parameters[17].Value = TrayID;
            parameters[18].Value = GoodsSiteType;
            parameters[19].Value = GoodsSiteLayer;
            parameters[20].Value = GoodsSiteColumn;
            parameters[21].Value = GoodsSiteRow;
            parameters[22].Value = DeviceID;
            parameters[23].Value = GoodsSiteStoreStatus;
            parameters[24].Value = GoodsSiteRunStatus;
            parameters[25].Value = GoodsSiteInOutType;
            parameters[26].Value = GoodsSiteStoreType;
            parameters[27].Value = LogicStoreAreaID;
            parameters[28].Value = StoreAreaID;
            parameters[29].Value = GoodsSiteID;
            parameters[30].Value = StockID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(View_StockListDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into View_StockListDetail(");
            strSql.Append("StockListID,ManaTaskID,StoreHouseName,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,ProductBatchNum,InHouseTime,UpdateTime,Remarks,StockDetailID,CoreCode,CoreQualitySign,CorePositionID,TrayID,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID,GoodsSiteID,StockID)");
            strSql.Append(" values (");
            strSql.Append("@StockListID,@ManaTaskID,@StoreHouseName,@ProductCode,@ProductNum,@ProductStatus,@ProductFrameCode,@ProductName,@GoodsSiteName,@ProductBatchNum,@InHouseTime,@UpdateTime,@Remarks,@StockDetailID,@CoreCode,@CoreQualitySign,@CorePositionID,@TrayID,@GoodsSiteType,@GoodsSiteLayer,@GoodsSiteColumn,@GoodsSiteRow,@DeviceID,@GoodsSiteStoreStatus,@GoodsSiteRunStatus,@GoodsSiteInOutType,@GoodsSiteStoreType,@LogicStoreAreaID,@StoreAreaID,@GoodsSiteID,@StockID)");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
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
					new SqlParameter("@StockDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CoreQualitySign", SqlDbType.Int,4),
					new SqlParameter("@CorePositionID", SqlDbType.Int,4),
					new SqlParameter("@TrayID", SqlDbType.NVarChar,50),
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
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@StockID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.StockListID;
            parameters[1].Value = model.ManaTaskID;
            parameters[2].Value = model.StoreHouseName;
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
            parameters[13].Value = model.StockDetailID;
            parameters[14].Value = model.CoreCode;
            parameters[15].Value = model.CoreQualitySign;
            parameters[16].Value = model.CorePositionID;
            parameters[17].Value = model.TrayID;
            parameters[18].Value = model.GoodsSiteType;
            parameters[19].Value = model.GoodsSiteLayer;
            parameters[20].Value = model.GoodsSiteColumn;
            parameters[21].Value = model.GoodsSiteRow;
            parameters[22].Value = model.DeviceID;
            parameters[23].Value = model.GoodsSiteStoreStatus;
            parameters[24].Value = model.GoodsSiteRunStatus;
            parameters[25].Value = model.GoodsSiteInOutType;
            parameters[26].Value = model.GoodsSiteStoreType;
            parameters[27].Value = model.LogicStoreAreaID;
            parameters[28].Value = model.StoreAreaID;
            parameters[29].Value = model.GoodsSiteID;
            parameters[30].Value = model.StockID;

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
        public bool Update(View_StockListDetailModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update View_StockListDetail set ");
            strSql.Append("StockListID=@StockListID,");
            strSql.Append("ManaTaskID=@ManaTaskID,");
            strSql.Append("StoreHouseName=@StoreHouseName,");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("ProductNum=@ProductNum,");
            strSql.Append("ProductStatus=@ProductStatus,");
            strSql.Append("ProductFrameCode=@ProductFrameCode,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("GoodsSiteName=@GoodsSiteName,");
            strSql.Append("ProductBatchNum=@ProductBatchNum,");
            strSql.Append("InHouseTime=@InHouseTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("Remarks=@Remarks,");
            strSql.Append("StockDetailID=@StockDetailID,");
            strSql.Append("CoreCode=@CoreCode,");
            strSql.Append("CoreQualitySign=@CoreQualitySign,");
            strSql.Append("CorePositionID=@CorePositionID,");
            strSql.Append("TrayID=@TrayID,");
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
            strSql.Append("StoreAreaID=@StoreAreaID,");
            strSql.Append("GoodsSiteID=@GoodsSiteID,");
            strSql.Append("StockID=@StockID");
            strSql.Append(" where StockListID=@StockListID and ManaTaskID=@ManaTaskID and StoreHouseName=@StoreHouseName and ProductCode=@ProductCode and ProductNum=@ProductNum and ProductStatus=@ProductStatus and ProductFrameCode=@ProductFrameCode and ProductName=@ProductName and GoodsSiteName=@GoodsSiteName and ProductBatchNum=@ProductBatchNum and InHouseTime=@InHouseTime and UpdateTime=@UpdateTime and Remarks=@Remarks and StockDetailID=@StockDetailID and CoreCode=@CoreCode and CoreQualitySign=@CoreQualitySign and CorePositionID=@CorePositionID and TrayID=@TrayID and GoodsSiteType=@GoodsSiteType and GoodsSiteLayer=@GoodsSiteLayer and GoodsSiteColumn=@GoodsSiteColumn and GoodsSiteRow=@GoodsSiteRow and DeviceID=@DeviceID and GoodsSiteStoreStatus=@GoodsSiteStoreStatus and GoodsSiteRunStatus=@GoodsSiteRunStatus and GoodsSiteInOutType=@GoodsSiteInOutType and GoodsSiteStoreType=@GoodsSiteStoreType and LogicStoreAreaID=@LogicStoreAreaID and StoreAreaID=@StoreAreaID and GoodsSiteID=@GoodsSiteID and StockID=@StockID ");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
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
					new SqlParameter("@StockDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CoreQualitySign", SqlDbType.Int,4),
					new SqlParameter("@CorePositionID", SqlDbType.Int,4),
					new SqlParameter("@TrayID", SqlDbType.NVarChar,50),
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
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@StockID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.StockListID;
            parameters[1].Value = model.ManaTaskID;
            parameters[2].Value = model.StoreHouseName;
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
            parameters[13].Value = model.StockDetailID;
            parameters[14].Value = model.CoreCode;
            parameters[15].Value = model.CoreQualitySign;
            parameters[16].Value = model.CorePositionID;
            parameters[17].Value = model.TrayID;
            parameters[18].Value = model.GoodsSiteType;
            parameters[19].Value = model.GoodsSiteLayer;
            parameters[20].Value = model.GoodsSiteColumn;
            parameters[21].Value = model.GoodsSiteRow;
            parameters[22].Value = model.DeviceID;
            parameters[23].Value = model.GoodsSiteStoreStatus;
            parameters[24].Value = model.GoodsSiteRunStatus;
            parameters[25].Value = model.GoodsSiteInOutType;
            parameters[26].Value = model.GoodsSiteStoreType;
            parameters[27].Value = model.LogicStoreAreaID;
            parameters[28].Value = model.StoreAreaID;
            parameters[29].Value = model.GoodsSiteID;
            parameters[30].Value = model.StockID;

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
        public bool Delete(long StockListID, long ManaTaskID, string StoreHouseName, string ProductCode, int ProductNum, string ProductStatus, string ProductFrameCode, string ProductName, string GoodsSiteName, string ProductBatchNum, DateTime InHouseTime, DateTime UpdateTime, string Remarks, long StockDetailID, string CoreCode, int CoreQualitySign, int CorePositionID, string TrayID, string GoodsSiteType, int GoodsSiteLayer, int GoodsSiteColumn, int GoodsSiteRow, string DeviceID, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, string GoodsSiteStoreType, int LogicStoreAreaID, int StoreAreaID, int GoodsSiteID, long StockID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from View_StockListDetail ");
            strSql.Append(" where StockListID=@StockListID and ManaTaskID=@ManaTaskID and StoreHouseName=@StoreHouseName and ProductCode=@ProductCode and ProductNum=@ProductNum and ProductStatus=@ProductStatus and ProductFrameCode=@ProductFrameCode and ProductName=@ProductName and GoodsSiteName=@GoodsSiteName and ProductBatchNum=@ProductBatchNum and InHouseTime=@InHouseTime and UpdateTime=@UpdateTime and Remarks=@Remarks and StockDetailID=@StockDetailID and CoreCode=@CoreCode and CoreQualitySign=@CoreQualitySign and CorePositionID=@CorePositionID and TrayID=@TrayID and GoodsSiteType=@GoodsSiteType and GoodsSiteLayer=@GoodsSiteLayer and GoodsSiteColumn=@GoodsSiteColumn and GoodsSiteRow=@GoodsSiteRow and DeviceID=@DeviceID and GoodsSiteStoreStatus=@GoodsSiteStoreStatus and GoodsSiteRunStatus=@GoodsSiteRunStatus and GoodsSiteInOutType=@GoodsSiteInOutType and GoodsSiteStoreType=@GoodsSiteStoreType and LogicStoreAreaID=@LogicStoreAreaID and StoreAreaID=@StoreAreaID and GoodsSiteID=@GoodsSiteID and StockID=@StockID ");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
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
					new SqlParameter("@StockDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CoreQualitySign", SqlDbType.Int,4),
					new SqlParameter("@CorePositionID", SqlDbType.Int,4),
					new SqlParameter("@TrayID", SqlDbType.NVarChar,50),
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
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@StockID", SqlDbType.BigInt,8)			};
            parameters[0].Value = StockListID;
            parameters[1].Value = ManaTaskID;
            parameters[2].Value = StoreHouseName;
            parameters[3].Value = ProductCode;
            parameters[4].Value = ProductNum;
            parameters[5].Value = ProductStatus;
            parameters[6].Value = ProductFrameCode;
            parameters[7].Value = ProductName;
            parameters[8].Value = GoodsSiteName;
            parameters[9].Value = ProductBatchNum;
            parameters[10].Value = InHouseTime;
            parameters[11].Value = UpdateTime;
            parameters[12].Value = Remarks;
            parameters[13].Value = StockDetailID;
            parameters[14].Value = CoreCode;
            parameters[15].Value = CoreQualitySign;
            parameters[16].Value = CorePositionID;
            parameters[17].Value = TrayID;
            parameters[18].Value = GoodsSiteType;
            parameters[19].Value = GoodsSiteLayer;
            parameters[20].Value = GoodsSiteColumn;
            parameters[21].Value = GoodsSiteRow;
            parameters[22].Value = DeviceID;
            parameters[23].Value = GoodsSiteStoreStatus;
            parameters[24].Value = GoodsSiteRunStatus;
            parameters[25].Value = GoodsSiteInOutType;
            parameters[26].Value = GoodsSiteStoreType;
            parameters[27].Value = LogicStoreAreaID;
            parameters[28].Value = StoreAreaID;
            parameters[29].Value = GoodsSiteID;
            parameters[30].Value = StockID;

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
        public View_StockListDetailModel GetModel(long StockListID, long ManaTaskID, string StoreHouseName, string ProductCode, int ProductNum, string ProductStatus, string ProductFrameCode, string ProductName, string GoodsSiteName, string ProductBatchNum, DateTime InHouseTime, DateTime UpdateTime, string Remarks, long StockDetailID, string CoreCode, int CoreQualitySign, int CorePositionID, string TrayID, string GoodsSiteType, int GoodsSiteLayer, int GoodsSiteColumn, int GoodsSiteRow, string DeviceID, string GoodsSiteStoreStatus, string GoodsSiteRunStatus, string GoodsSiteInOutType, string GoodsSiteStoreType, int LogicStoreAreaID, int StoreAreaID, int GoodsSiteID, long StockID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 StockListID,ManaTaskID,StoreHouseName,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,ProductBatchNum,InHouseTime,UpdateTime,Remarks,StockDetailID,CoreCode,CoreQualitySign,CorePositionID,TrayID,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID,GoodsSiteID,StockID from View_StockListDetail ");
            strSql.Append(" where StockListID=@StockListID and ManaTaskID=@ManaTaskID and StoreHouseName=@StoreHouseName and ProductCode=@ProductCode and ProductNum=@ProductNum and ProductStatus=@ProductStatus and ProductFrameCode=@ProductFrameCode and ProductName=@ProductName and GoodsSiteName=@GoodsSiteName and ProductBatchNum=@ProductBatchNum and InHouseTime=@InHouseTime and UpdateTime=@UpdateTime and Remarks=@Remarks and StockDetailID=@StockDetailID and CoreCode=@CoreCode and CoreQualitySign=@CoreQualitySign and CorePositionID=@CorePositionID and TrayID=@TrayID and GoodsSiteType=@GoodsSiteType and GoodsSiteLayer=@GoodsSiteLayer and GoodsSiteColumn=@GoodsSiteColumn and GoodsSiteRow=@GoodsSiteRow and DeviceID=@DeviceID and GoodsSiteStoreStatus=@GoodsSiteStoreStatus and GoodsSiteRunStatus=@GoodsSiteRunStatus and GoodsSiteInOutType=@GoodsSiteInOutType and GoodsSiteStoreType=@GoodsSiteStoreType and LogicStoreAreaID=@LogicStoreAreaID and StoreAreaID=@StoreAreaID and GoodsSiteID=@GoodsSiteID and StockID=@StockID ");
            SqlParameter[] parameters = {
					new SqlParameter("@StockListID", SqlDbType.BigInt,8),
					new SqlParameter("@ManaTaskID", SqlDbType.BigInt,8),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,20),
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
					new SqlParameter("@StockDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CoreQualitySign", SqlDbType.Int,4),
					new SqlParameter("@CorePositionID", SqlDbType.Int,4),
					new SqlParameter("@TrayID", SqlDbType.NVarChar,50),
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
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@StockID", SqlDbType.BigInt,8)			};
            parameters[0].Value = StockListID;
            parameters[1].Value = ManaTaskID;
            parameters[2].Value = StoreHouseName;
            parameters[3].Value = ProductCode;
            parameters[4].Value = ProductNum;
            parameters[5].Value = ProductStatus;
            parameters[6].Value = ProductFrameCode;
            parameters[7].Value = ProductName;
            parameters[8].Value = GoodsSiteName;
            parameters[9].Value = ProductBatchNum;
            parameters[10].Value = InHouseTime;
            parameters[11].Value = UpdateTime;
            parameters[12].Value = Remarks;
            parameters[13].Value = StockDetailID;
            parameters[14].Value = CoreCode;
            parameters[15].Value = CoreQualitySign;
            parameters[16].Value = CorePositionID;
            parameters[17].Value = TrayID;
            parameters[18].Value = GoodsSiteType;
            parameters[19].Value = GoodsSiteLayer;
            parameters[20].Value = GoodsSiteColumn;
            parameters[21].Value = GoodsSiteRow;
            parameters[22].Value = DeviceID;
            parameters[23].Value = GoodsSiteStoreStatus;
            parameters[24].Value = GoodsSiteRunStatus;
            parameters[25].Value = GoodsSiteInOutType;
            parameters[26].Value = GoodsSiteStoreType;
            parameters[27].Value = LogicStoreAreaID;
            parameters[28].Value = StoreAreaID;
            parameters[29].Value = GoodsSiteID;
            parameters[30].Value = StockID;

            View_StockListDetailModel model = new View_StockListDetailModel();
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
        public View_StockListDetailModel DataRowToModel(DataRow row)
        {
            View_StockListDetailModel model = new View_StockListDetailModel();
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
                if (row["StockDetailID"] != null && row["StockDetailID"].ToString() != "")
                {
                    model.StockDetailID = long.Parse(row["StockDetailID"].ToString());
                }
                if (row["CoreCode"] != null)
                {
                    model.CoreCode = row["CoreCode"].ToString();
                }
                if (row["CoreQualitySign"] != null && row["CoreQualitySign"].ToString() != "")
                {
                    model.CoreQualitySign = int.Parse(row["CoreQualitySign"].ToString());
                }
                if (row["CorePositionID"] != null && row["CorePositionID"].ToString() != "")
                {
                    model.CorePositionID = int.Parse(row["CorePositionID"].ToString());
                }
                if (row["TrayID"] != null)
                {
                    model.TrayID = row["TrayID"].ToString();
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
                if (row["GoodsSiteID"] != null && row["GoodsSiteID"].ToString() != "")
                {
                    model.GoodsSiteID = int.Parse(row["GoodsSiteID"].ToString());
                }
                if (row["StockID"] != null && row["StockID"].ToString() != "")
                {
                    model.StockID = long.Parse(row["StockID"].ToString());
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
            strSql.Append("select StockListID,ManaTaskID,StoreHouseName,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,ProductBatchNum,InHouseTime,UpdateTime,Remarks,StockDetailID,CoreCode,CoreQualitySign,CorePositionID,TrayID,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID,GoodsSiteID,StockID ");
            strSql.Append(" FROM View_StockListDetail ");
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
            strSql.Append(" StockListID,ManaTaskID,StoreHouseName,ProductCode,ProductNum,ProductStatus,ProductFrameCode,ProductName,GoodsSiteName,ProductBatchNum,InHouseTime,UpdateTime,Remarks,StockDetailID,CoreCode,CoreQualitySign,CorePositionID,TrayID,GoodsSiteType,GoodsSiteLayer,GoodsSiteColumn,GoodsSiteRow,DeviceID,GoodsSiteStoreStatus,GoodsSiteRunStatus,GoodsSiteInOutType,GoodsSiteStoreType,LogicStoreAreaID,StoreAreaID,GoodsSiteID,StockID ");
            strSql.Append(" FROM View_StockListDetail ");
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
            strSql.Append("select count(1) FROM View_StockListDetail ");
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
                strSql.Append("order by T.StockID desc");
            }
            strSql.Append(")AS Row, T.*  from View_StockListDetail T ");
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
            parameters[0].Value = "View_StockListDetail";
            parameters[1].Value = "StockID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataTable GetStockDetailByGsID(int gsID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM View_StockListDetail");
            strSql.Append(" where GoodsSiteID = " + gsID);
           
            DataSet ds= DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
             else
            { return null; }
        }
        /// <summary>
        /// 作者:np
        /// 时间:2014年8月29日
        /// 内容:获取库存托盘列表
        /// </summary>
        /// <param name="HouseName"></param>
        /// <param name="rowth"></param>
        /// <param name="columnth"></param>
        /// <param name="layerth"></param>
        /// <returns></returns>
        public List<string> GetStockTrayList(EnumStoreHouse HouseName, int rowth, int columnth, int layerth)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TrayID FROM View_StockListDetail");
            strSql.Append(" where StoreHouseName = '" + HouseName + "' and GoodsSiteRow = " 
                + rowth + " and GoodsSiteColumn=" + columnth + " and GoodsSiteLayer= " + layerth);

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            List<string> trayList = new List<string>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
               
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    trayList.Add(ds.Tables[0].Rows[i][0].ToString());
                }
                return trayList;
            }
            else
            { return null; }
        }
        #endregion  ExtensionMethod
    }
}

