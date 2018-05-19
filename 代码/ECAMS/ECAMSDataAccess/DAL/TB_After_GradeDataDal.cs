using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
 
namespace ECAMSDataAccess
{
    /// <summary>
    /// 数据访问类:TB_After_GradeData
    /// </summary>
    public partial class TB_After_GradeDataDal
    {
        public TB_After_GradeDataDal()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string batchID, string Tf_TrayId, string Tf_CellSn)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from TB_After_GradeData");
            strSql.Append(" where Tf_BatchID =@Tf_BatchID and Tf_TrayId=@Tf_TrayId and Tf_CellSn=@Tf_CellSn ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_CellSn", SqlDbType.VarChar,32)			};
            parameters[0].Value = batchID;
            parameters[1].Value = Tf_TrayId;
            parameters[2].Value = Tf_CellSn;

            return DbHelperSQL2.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TB_After_GradeDataModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TB_After_GradeData(");
            strSql.Append("Tf_BatchID,Tf_Batchtype,Tf_TrayId,Tf_ChannelNo,Tf_CellSn,Tf_Pick,Tf_Tag)");
            strSql.Append(" values (");
            strSql.Append("@Tf_BatchID,@Tf_Batchtype,@Tf_TrayId,@Tf_ChannelNo,@Tf_CellSn,@Tf_Pick,@Tf_Tag)");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_Batchtype", SqlDbType.Int,4),
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_ChannelNo", SqlDbType.Int,4),
					new SqlParameter("@Tf_CellSn", SqlDbType.VarChar,32),
                    new SqlParameter("@Tf_Pick",SqlDbType.VarChar,32),
                    new SqlParameter("@Tf_Tag",SqlDbType.Int,4)};
            parameters[0].Value = model.Tf_BatchID;
            parameters[1].Value = model.Tf_Batchtype;
            parameters[2].Value = model.Tf_TrayId;
            parameters[3].Value = model.Tf_ChannelNo;
            parameters[4].Value = model.Tf_CellSn;
            parameters[5].Value = model.Tf_Pick;
            parameters[6].Value = model.Tf_Tag;
            int rows = DbHelperSQL2.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Update(TB_After_GradeDataModel model,string oldTrayID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_After_GradeData set ");
            strSql.Append("Tf_TrayId=@Tf_TrayId,");
            strSql.Append("Tf_BatchID=@Tf_BatchID,");
            strSql.Append("Tf_Batchtype=@Tf_Batchtype,");
            strSql.Append("Tf_ChannelNo=@Tf_ChannelNo,");
            strSql.Append("Tf_CellSn=@Tf_CellSn,");
            strSql.Append("Tf_Pick=@Tf_Pick,");
            strSql.Append("Tf_Tag=@Tf_Tag");
            strSql.Append(" where Tf_TrayId='" + oldTrayID + "' and Tf_CellSn=@Tf_CellSn ");
            SqlParameter[] parameters = {
					new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_Batchtype", SqlDbType.Int,4),
					new SqlParameter("@Tf_ChannelNo", SqlDbType.Int,4),
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_CellSn", SqlDbType.VarChar,32),
                    new SqlParameter("@Tf_Pick",SqlDbType.VarChar,32),
                    new SqlParameter("@Tf_Tag",SqlDbType.Int,4)};
            parameters[0].Value = model.Tf_BatchID;
            parameters[1].Value = model.Tf_Batchtype;
            parameters[2].Value = model.Tf_ChannelNo;
            parameters[3].Value = model.Tf_TrayId;
            parameters[4].Value = model.Tf_CellSn;
            parameters[5].Value = model.Tf_Pick;
            parameters[6].Value = model.Tf_Tag;
            int rows = DbHelperSQL2.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(string batchID, string Tf_TrayId, string Tf_CellSn)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_After_GradeData ");
            strSql.Append(" where Tf_BatchID =@Tf_BatchID and Tf_TrayId=@Tf_TrayId and Tf_CellSn=@Tf_CellSn ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_CellSn", SqlDbType.VarChar,32)			};
            parameters[0].Value = batchID;
            parameters[1].Value = Tf_TrayId;
            parameters[2].Value = Tf_CellSn;

            int rows = DbHelperSQL2.ExecuteSql(strSql.ToString(), parameters);
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
        public TB_After_GradeDataModel GetModel(string batchID,string Tf_TrayId, string Tf_CellSn)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Tf_BatchID,Tf_Batchtype,Tf_TrayId,Tf_ChannelNo,Tf_CellSn,Tf_Pick,Tf_Tag from TB_After_GradeData ");
            strSql.Append(" where Tf_BatchID =@Tf_BatchID and Tf_TrayId=@Tf_TrayId and Tf_CellSn=@Tf_CellSn ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Tf_BatchID", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_TrayId", SqlDbType.VarChar,32),
					new SqlParameter("@Tf_CellSn", SqlDbType.VarChar,32)			};
            parameters[0].Value = batchID;
            parameters[1].Value = Tf_TrayId;
            parameters[2].Value = Tf_CellSn;

            //TB_After_GradeDataModel model = new TB_After_GradeDataModel();
            DataSet ds = DbHelperSQL2.Query(strSql.ToString(), parameters);
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
        public TB_After_GradeDataModel DataRowToModel(DataRow row)
        {
            TB_After_GradeDataModel model = new TB_After_GradeDataModel();
            if (row != null)
            {
                if (row["Tf_BatchID"] != null)
                {
                    model.Tf_BatchID = row["Tf_BatchID"].ToString();
                }
                if (row["Tf_Batchtype"] != null && row["Tf_Batchtype"].ToString() != "")
                {
                    model.Tf_Batchtype = int.Parse(row["Tf_Batchtype"].ToString());
                }
                if (row["Tf_TrayId"] != null)
                {
                    model.Tf_TrayId = row["Tf_TrayId"].ToString();
                }
                if (row["Tf_ChannelNo"] != null && row["Tf_ChannelNo"].ToString() != "")
                {
                    model.Tf_ChannelNo = int.Parse(row["Tf_ChannelNo"].ToString());
                }
                if (row["Tf_CellSn"] != null)
                {
                    model.Tf_CellSn = row["Tf_CellSn"].ToString();
                }
                if (row["Tf_Pick"] != null)
                {
                    model.Tf_Pick = row["Tf_Pick"].ToString();
                }
                if (row["Tf_Tag"] != null && row["Tf_Tag"].ToString() != "")
                {
                    model.Tf_Tag = int.Parse(row["Tf_Tag"].ToString());
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
            strSql.Append("select Tf_BatchID,Tf_Batchtype,Tf_TrayId,Tf_ChannelNo,Tf_CellSn,Tf_Pick,Tf_Tag ");
            strSql.Append(" FROM TB_After_GradeData ");
           
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Tf_ChannelNo ");
            return DbHelperSQL2.Query(strSql.ToString());
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
            strSql.Append(" Tf_BatchID,Tf_Batchtype,Tf_TrayId,Tf_ChannelNo,Tf_CellSn,Tf_Pick,Tf_Tag ");
            strSql.Append(" FROM TB_After_GradeData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL2.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM TB_After_GradeData ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL2.GetSingle(strSql.ToString());
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
                strSql.Append("order by T.Tf_CellSn desc");
            }
            strSql.Append(")AS Row, T.*  from TB_After_GradeData T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL2.Query(strSql.ToString());
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
            parameters[0].Value = "TB_After_GradeData";
            parameters[1].Value = "Tf_CellSn";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL2.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

