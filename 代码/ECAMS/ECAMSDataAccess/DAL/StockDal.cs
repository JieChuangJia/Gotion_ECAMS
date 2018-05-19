/**  版本信息模板在安装目录下，可自行修改。
* Stock.cs
*
* 功 能： N/A
* 类 名： Stock
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
using System.Collections;
namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:Stock
	/// </summary>
	public partial class StockDal
	{
		public StockDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StockID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Stock");
			strSql.Append(" where StockID=@StockID");
			SqlParameter[] parameters = {
					new SqlParameter("@StockID", SqlDbType.BigInt)
			};
			parameters[0].Value = StockID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public long Add(ECAMSDataAccess.StockModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Stock(");
			strSql.Append("GoodsSiteID,TrayCode,FullTraySign,Remarks)");
			strSql.Append(" values (");
			strSql.Append("@GoodsSiteID,@TrayCode,@FullTraySign,@Remarks)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@TrayCode", SqlDbType.NVarChar,50),
					new SqlParameter("@FullTraySign", SqlDbType.NVarChar,50),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.GoodsSiteID;
			parameters[1].Value = model.TrayCode;
			parameters[2].Value = model.FullTraySign;
			parameters[3].Value = model.Remarks;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(ECAMSDataAccess.StockModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Stock set ");
			strSql.Append("GoodsSiteID=@GoodsSiteID,");
			strSql.Append("TrayCode=@TrayCode,");
			strSql.Append("FullTraySign=@FullTraySign,");
			strSql.Append("Remarks=@Remarks");
			strSql.Append(" where StockID=@StockID");
			SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@TrayCode", SqlDbType.NVarChar,50),
					new SqlParameter("@FullTraySign", SqlDbType.NVarChar,50),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100),
					new SqlParameter("@StockID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.GoodsSiteID;
			parameters[1].Value = model.TrayCode;
			parameters[2].Value = model.FullTraySign;
			parameters[3].Value = model.Remarks;
			parameters[4].Value = model.StockID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(long StockID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Stock ");
			strSql.Append(" where StockID=@StockID");
			SqlParameter[] parameters = {
					new SqlParameter("@StockID", SqlDbType.BigInt)
			};
			parameters[0].Value = StockID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string StockIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Stock ");
			strSql.Append(" where StockID in ("+StockIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public ECAMSDataAccess.StockModel GetModel(long StockID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 StockID,GoodsSiteID,TrayCode,FullTraySign,Remarks from Stock ");
			strSql.Append(" where StockID=@StockID");
			SqlParameter[] parameters = {
					new SqlParameter("@StockID", SqlDbType.BigInt)
			};
			parameters[0].Value = StockID;

			ECAMSDataAccess.StockModel model=new ECAMSDataAccess.StockModel();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public ECAMSDataAccess.StockModel DataRowToModel(DataRow row)
		{
			ECAMSDataAccess.StockModel model=new ECAMSDataAccess.StockModel();
			if (row != null)
			{
				if(row["StockID"]!=null && row["StockID"].ToString()!="")
				{
					model.StockID=long.Parse(row["StockID"].ToString());
				}
				if(row["GoodsSiteID"]!=null && row["GoodsSiteID"].ToString()!="")
				{
					model.GoodsSiteID=int.Parse(row["GoodsSiteID"].ToString());
				}
				if(row["TrayCode"]!=null)
				{
					model.TrayCode=row["TrayCode"].ToString();
				}
				if(row["FullTraySign"]!=null)
				{
					model.FullTraySign=row["FullTraySign"].ToString();
				}
				if(row["Remarks"]!=null)
				{
					model.Remarks=row["Remarks"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select StockID,GoodsSiteID,TrayCode,FullTraySign,Remarks ");
			strSql.Append(" FROM Stock ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" StockID,GoodsSiteID,TrayCode,FullTraySign,Remarks ");
			strSql.Append(" FROM Stock ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Stock ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.StockID desc");
			}
			strSql.Append(")AS Row, T.*  from Stock T ");
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
			parameters[0].Value = "Stock";
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
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public long GetMaxId()
        {
            return DbHelperSQL.GetMaxID("StockID", "Stock");
        }

        /// <summary>
        /// 获取删除库存hash
        /// </summary>
        /// <param name="stockID"></param>
        /// <returns></returns>
        public Hashtable GetDeleteModelHash(long stockID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Stock ");
            strSql.Append(" where StockID=@StockID");
            SqlParameter[] parameters = {
					new SqlParameter("@StockID", SqlDbType.BigInt)
			};
            parameters[0].Value = stockID;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }

        /// <summary>
        /// 获取添加库存hash用于事物
        /// </summary>
        /// <param name="model"></param>
        public Hashtable GetAddModelHash(StockModel model)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Stock(");
			strSql.Append("GoodsSiteID,TrayCode,FullTraySign,Remarks)");
			strSql.Append(" values (");
			strSql.Append("@GoodsSiteID,@TrayCode,@FullTraySign,@Remarks)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int,4),
					new SqlParameter("@TrayCode", SqlDbType.NVarChar,50),
					new SqlParameter("@FullTraySign", SqlDbType.NVarChar,50),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.GoodsSiteID;
			parameters[1].Value = model.TrayCode;
			parameters[2].Value = model.FullTraySign;
			parameters[3].Value = model.Remarks;
            hs.Add(strSql.ToString(),parameters);
            return hs;
        }

        public bool DeleteModelByGsID(int goodssiteID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Stock ");
            strSql.Append(" where GoodsSiteID=@GoodsSiteID");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsSiteID", SqlDbType.Int)
			};
            parameters[0].Value = goodssiteID;

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

        public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Stock");

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
		#endregion  ExtensionMethod
	}
}

