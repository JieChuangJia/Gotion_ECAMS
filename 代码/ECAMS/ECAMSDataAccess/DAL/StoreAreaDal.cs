/**  版本信息模板在安装目录下，可自行修改。
* StoreArea.cs
*
* 功 能： N/A
* 类 名： StoreArea
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
	/// 数据访问类:StoreArea
	/// </summary>
	public partial class StoreAreaDal
	{
		public StoreAreaDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
        public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("StoreAreaID", "StoreArea"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int StoreAreaID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from StoreArea");
			strSql.Append(" where StoreAreaID=@StoreAreaID");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreAreaID", SqlDbType.Int,4)
			};
			parameters[0].Value = StoreAreaID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ECAMSDataAccess.StoreAreaModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into StoreArea(");
			strSql.Append("StoreAreaCode,StoreAreaName,StoreAreaType,StoreAreaDescribe,StoreAreaSign,StoreHouseID)");
			strSql.Append(" values (");
			strSql.Append("@StoreAreaCode,@StoreAreaName,@StoreAreaType,@StoreAreaDescribe,@StoreAreaSign,@StoreHouseID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreAreaCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreAreaType", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreAreaDescribe", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreAreaSign", SqlDbType.Int,4),
					new SqlParameter("@StoreHouseID", SqlDbType.Int,4)};
			parameters[0].Value = model.StoreAreaCode;
			parameters[1].Value = model.StoreAreaName;
			parameters[2].Value = model.StoreAreaType;
			parameters[3].Value = model.StoreAreaDescribe;
			parameters[4].Value = model.StoreAreaSign;
			parameters[5].Value = model.StoreHouseID;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(ECAMSDataAccess.StoreAreaModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update StoreArea set ");
			strSql.Append("StoreAreaCode=@StoreAreaCode,");
			strSql.Append("StoreAreaName=@StoreAreaName,");
			strSql.Append("StoreAreaType=@StoreAreaType,");
			strSql.Append("StoreAreaDescribe=@StoreAreaDescribe,");
			strSql.Append("StoreAreaSign=@StoreAreaSign,");
			strSql.Append("StoreHouseID=@StoreHouseID");
			strSql.Append(" where StoreAreaID=@StoreAreaID");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreAreaCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreAreaType", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreAreaDescribe", SqlDbType.NVarChar,50),
					new SqlParameter("@StoreAreaSign", SqlDbType.Int,4),
					new SqlParameter("@StoreHouseID", SqlDbType.Int,4),
					new SqlParameter("@StoreAreaID", SqlDbType.Int,4)};
			parameters[0].Value = model.StoreAreaCode;
			parameters[1].Value = model.StoreAreaName;
			parameters[2].Value = model.StoreAreaType;
			parameters[3].Value = model.StoreAreaDescribe;
			parameters[4].Value = model.StoreAreaSign;
			parameters[5].Value = model.StoreHouseID;
			parameters[6].Value = model.StoreAreaID;

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
		public bool Delete(int StoreAreaID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from StoreArea ");
			strSql.Append(" where StoreAreaID=@StoreAreaID");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreAreaID", SqlDbType.Int,4)
			};
			parameters[0].Value = StoreAreaID;

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
		public bool DeleteList(string StoreAreaIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from StoreArea ");
			strSql.Append(" where StoreAreaID in ("+StoreAreaIDlist + ")  ");
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
		public ECAMSDataAccess.StoreAreaModel GetModel(int StoreAreaID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 StoreAreaID,StoreAreaCode,StoreAreaName,StoreAreaType,StoreAreaDescribe,StoreAreaSign,StoreHouseID from StoreArea ");
			strSql.Append(" where StoreAreaID=@StoreAreaID");
			SqlParameter[] parameters = {
					new SqlParameter("@StoreAreaID", SqlDbType.Int,4)
			};
			parameters[0].Value = StoreAreaID;

			ECAMSDataAccess.StoreAreaModel model=new ECAMSDataAccess.StoreAreaModel();
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
		public ECAMSDataAccess.StoreAreaModel DataRowToModel(DataRow row)
		{
			ECAMSDataAccess.StoreAreaModel model=new ECAMSDataAccess.StoreAreaModel();
			if (row != null)
			{
				if(row["StoreAreaID"]!=null && row["StoreAreaID"].ToString()!="")
				{
					model.StoreAreaID=int.Parse(row["StoreAreaID"].ToString());
				}
				if(row["StoreAreaCode"]!=null)
				{
					model.StoreAreaCode=row["StoreAreaCode"].ToString();
				}
				if(row["StoreAreaName"]!=null)
				{
					model.StoreAreaName=row["StoreAreaName"].ToString();
				}
				if(row["StoreAreaType"]!=null)
				{
					model.StoreAreaType=row["StoreAreaType"].ToString();
				}
				if(row["StoreAreaDescribe"]!=null)
				{
					model.StoreAreaDescribe=row["StoreAreaDescribe"].ToString();
				}
				if(row["StoreAreaSign"]!=null && row["StoreAreaSign"].ToString()!="")
				{
					model.StoreAreaSign=int.Parse(row["StoreAreaSign"].ToString());
				}
				if(row["StoreHouseID"]!=null && row["StoreHouseID"].ToString()!="")
				{
					model.StoreHouseID=int.Parse(row["StoreHouseID"].ToString());
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
			strSql.Append("select StoreAreaID,StoreAreaCode,StoreAreaName,StoreAreaType,StoreAreaDescribe,StoreAreaSign,StoreHouseID ");
			strSql.Append(" FROM StoreArea ");
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
			strSql.Append(" StoreAreaID,StoreAreaCode,StoreAreaName,StoreAreaType,StoreAreaDescribe,StoreAreaSign,StoreHouseID ");
			strSql.Append(" FROM StoreArea ");
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
			strSql.Append("select count(1) FROM StoreArea ");
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
				strSql.Append("order by T.StoreAreaID desc");
			}
			strSql.Append(")AS Row, T.*  from StoreArea T ");
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
			parameters[0].Value = "StoreArea";
			parameters[1].Value = "StoreAreaID";
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

