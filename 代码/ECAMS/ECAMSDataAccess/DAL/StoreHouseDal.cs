/**  版本信息模板在安装目录下，可自行修改。
* StoreHouse.cs
*
* 功 能： N/A
* 类 名： StoreHouse
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:06   N/A    初版
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
	/// 数据访问类:StoreHouse
	/// </summary>
	public partial class StoreHouseDal
	{
		public StoreHouseDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("StoreHouseID", "StoreHouse"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int StoreHouseID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from StoreHouse");
			strSql.Append(" where StoreHouseID=@StoreHouseID ");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreHouseID", SqlDbType.Int,4)			};
			parameters[0].Value = StoreHouseID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ECAMSDataAccess.StoreHouseModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into StoreHouse(");
			strSql.Append("StoreHouseID,StoreHouseCode,StoreHouseName,StoreHouseDescribe,StoreHouseSign)");
			strSql.Append(" values (");
			strSql.Append("@StoreHouseID,@StoreHouseCode,@StoreHouseName,@StoreHouseDescribe,@StoreHouseSign)");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreHouseID", SqlDbType.Int,4),
					new SqlParameter("@StoreHouseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreHouseDescribe", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreHouseSign", SqlDbType.Int,4)};
			parameters[0].Value = model.StoreHouseID;
			parameters[1].Value = model.StoreHouseCode;
			parameters[2].Value = model.StoreHouseName;
			parameters[3].Value = model.StoreHouseDescribe;
			parameters[4].Value = model.StoreHouseSign;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(ECAMSDataAccess.StoreHouseModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update StoreHouse set ");
			strSql.Append("StoreHouseCode=@StoreHouseCode,");
			strSql.Append("StoreHouseName=@StoreHouseName,");
			strSql.Append("StoreHouseDescribe=@StoreHouseDescribe,");
			strSql.Append("StoreHouseSign=@StoreHouseSign");
			strSql.Append(" where StoreHouseID=@StoreHouseID ");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreHouseCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreHouseName", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreHouseDescribe", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreHouseSign", SqlDbType.Int,4),
					new SqlParameter("@StoreHouseID", SqlDbType.Int,4)};
			parameters[0].Value = model.StoreHouseCode;
			parameters[1].Value = model.StoreHouseName;
			parameters[2].Value = model.StoreHouseDescribe;
			parameters[3].Value = model.StoreHouseSign;
			parameters[4].Value = model.StoreHouseID;

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
		public bool Delete(int StoreHouseID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from StoreHouse ");
			strSql.Append(" where StoreHouseID=@StoreHouseID ");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreHouseID", SqlDbType.Int,4)			};
			parameters[0].Value = StoreHouseID;

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
		public bool DeleteList(string StoreHouseIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from StoreHouse ");
			strSql.Append(" where StoreHouseID in ("+StoreHouseIDlist + ")  ");
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
		public ECAMSDataAccess.StoreHouseModel GetModel(int StoreHouseID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 StoreHouseID,StoreHouseCode,StoreHouseName,StoreHouseDescribe,StoreHouseSign from StoreHouse ");
			strSql.Append(" where StoreHouseID=@StoreHouseID ");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreHouseID", SqlDbType.Int,4)			};
			parameters[0].Value = StoreHouseID;

			ECAMSDataAccess.StoreHouseModel model=new ECAMSDataAccess.StoreHouseModel();
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
		public ECAMSDataAccess.StoreHouseModel DataRowToModel(DataRow row)
		{
			ECAMSDataAccess.StoreHouseModel model=new ECAMSDataAccess.StoreHouseModel();
			if (row != null)
			{
				if(row["StoreHouseID"]!=null && row["StoreHouseID"].ToString()!="")
				{
					model.StoreHouseID=int.Parse(row["StoreHouseID"].ToString());
				}
				if(row["StoreHouseCode"]!=null)
				{
					model.StoreHouseCode=row["StoreHouseCode"].ToString();
				}
				if(row["StoreHouseName"]!=null)
				{
					model.StoreHouseName=row["StoreHouseName"].ToString();
				}
				if(row["StoreHouseDescribe"]!=null)
				{
					model.StoreHouseDescribe=row["StoreHouseDescribe"].ToString();
				}
				if(row["StoreHouseSign"]!=null && row["StoreHouseSign"].ToString()!="")
				{
					model.StoreHouseSign=int.Parse(row["StoreHouseSign"].ToString());
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
			strSql.Append("select StoreHouseID,StoreHouseCode,StoreHouseName,StoreHouseDescribe,StoreHouseSign ");
			strSql.Append(" FROM StoreHouse ");
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
			strSql.Append(" StoreHouseID,StoreHouseCode,StoreHouseName,StoreHouseDescribe,StoreHouseSign ");
			strSql.Append(" FROM StoreHouse ");
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
			strSql.Append("select count(1) FROM StoreHouse ");
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
				strSql.Append("order by T.StoreHouseID desc");
			}
			strSql.Append(")AS Row, T.*  from StoreHouse T ");
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
			parameters[0].Value = "StoreHouse";
			parameters[1].Value = "StoreHouseID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

