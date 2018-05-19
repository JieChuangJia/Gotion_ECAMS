/**  版本信息模板在安装目录下，可自行修改。
* LogicStoreArea.cs
*
* 功 能： N/A
* 类 名： LogicStoreArea
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:00   N/A    初版
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
	/// 数据访问类:LogicStoreArea
	/// </summary>
	public partial class LogicStoreAreaDal
	{
		public LogicStoreAreaDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("LogicStoreAreaID", "LogicStoreArea"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int LogicStoreAreaID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from LogicStoreArea");
			strSql.Append(" where LogicStoreAreaID=@LogicStoreAreaID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogicStoreAreaID", SqlDbType.Int,4)			};
			parameters[0].Value = LogicStoreAreaID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ECAMSDataAccess.LogicStoreAreaModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into LogicStoreArea(");
			strSql.Append("LogicStoreAreaID,LogicStoreAreaCode,LogicStoreAreaName,LogicStoreAreaType,LogicStoreAreaDescribe,LogicStoreAreaSign)");
			strSql.Append(" values (");
			strSql.Append("@LogicStoreAreaID,@LogicStoreAreaCode,@LogicStoreAreaName,@LogicStoreAreaType,@LogicStoreAreaDescribe,@LogicStoreAreaSign)");
			SqlParameter[] parameters = {
					new SqlParameter("@LogicStoreAreaID", SqlDbType.Int,4),
					new SqlParameter("@LogicStoreAreaCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaType", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaDescribe", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaSign", SqlDbType.Int,4)};
			parameters[0].Value = model.LogicStoreAreaID;
			parameters[1].Value = model.LogicStoreAreaCode;
			parameters[2].Value = model.LogicStoreAreaName;
			parameters[3].Value = model.LogicStoreAreaType;
			parameters[4].Value = model.LogicStoreAreaDescribe;
			parameters[5].Value = model.LogicStoreAreaSign;

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
		public bool Update(ECAMSDataAccess.LogicStoreAreaModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update LogicStoreArea set ");
			strSql.Append("LogicStoreAreaCode=@LogicStoreAreaCode,");
			strSql.Append("LogicStoreAreaName=@LogicStoreAreaName,");
			strSql.Append("LogicStoreAreaType=@LogicStoreAreaType,");
			strSql.Append("LogicStoreAreaDescribe=@LogicStoreAreaDescribe,");
			strSql.Append("LogicStoreAreaSign=@LogicStoreAreaSign");
			strSql.Append(" where LogicStoreAreaID=@LogicStoreAreaID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogicStoreAreaCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaType", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaDescribe", SqlDbType.NVarChar,50),
					new SqlParameter("@LogicStoreAreaSign", SqlDbType.Int,4),
					new SqlParameter("@LogicStoreAreaID", SqlDbType.Int,4)};
			parameters[0].Value = model.LogicStoreAreaCode;
			parameters[1].Value = model.LogicStoreAreaName;
			parameters[2].Value = model.LogicStoreAreaType;
			parameters[3].Value = model.LogicStoreAreaDescribe;
			parameters[4].Value = model.LogicStoreAreaSign;
			parameters[5].Value = model.LogicStoreAreaID;

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
		public bool Delete(int LogicStoreAreaID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogicStoreArea ");
			strSql.Append(" where LogicStoreAreaID=@LogicStoreAreaID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogicStoreAreaID", SqlDbType.Int,4)			};
			parameters[0].Value = LogicStoreAreaID;

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
		public bool DeleteList(string LogicStoreAreaIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from LogicStoreArea ");
			strSql.Append(" where LogicStoreAreaID in ("+LogicStoreAreaIDlist + ")  ");
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
		public ECAMSDataAccess.LogicStoreAreaModel GetModel(int LogicStoreAreaID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LogicStoreAreaID,LogicStoreAreaCode,LogicStoreAreaName,LogicStoreAreaType,LogicStoreAreaDescribe,LogicStoreAreaSign from LogicStoreArea ");
			strSql.Append(" where LogicStoreAreaID=@LogicStoreAreaID ");
			SqlParameter[] parameters = {
					new SqlParameter("@LogicStoreAreaID", SqlDbType.Int,4)			};
			parameters[0].Value = LogicStoreAreaID;

			ECAMSDataAccess.LogicStoreAreaModel model=new ECAMSDataAccess.LogicStoreAreaModel();
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
		public ECAMSDataAccess.LogicStoreAreaModel DataRowToModel(DataRow row)
		{
			ECAMSDataAccess.LogicStoreAreaModel model=new ECAMSDataAccess.LogicStoreAreaModel();
			if (row != null)
			{
				if(row["LogicStoreAreaID"]!=null && row["LogicStoreAreaID"].ToString()!="")
				{
					model.LogicStoreAreaID=int.Parse(row["LogicStoreAreaID"].ToString());
				}
				if(row["LogicStoreAreaCode"]!=null)
				{
					model.LogicStoreAreaCode=row["LogicStoreAreaCode"].ToString();
				}
				if(row["LogicStoreAreaName"]!=null)
				{
					model.LogicStoreAreaName=row["LogicStoreAreaName"].ToString();
				}
				if(row["LogicStoreAreaType"]!=null)
				{
					model.LogicStoreAreaType=row["LogicStoreAreaType"].ToString();
				}
				if(row["LogicStoreAreaDescribe"]!=null)
				{
					model.LogicStoreAreaDescribe=row["LogicStoreAreaDescribe"].ToString();
				}
				if(row["LogicStoreAreaSign"]!=null && row["LogicStoreAreaSign"].ToString()!="")
				{
					model.LogicStoreAreaSign=int.Parse(row["LogicStoreAreaSign"].ToString());
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
			strSql.Append("select LogicStoreAreaID,LogicStoreAreaCode,LogicStoreAreaName,LogicStoreAreaType,LogicStoreAreaDescribe,LogicStoreAreaSign ");
			strSql.Append(" FROM LogicStoreArea ");
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
			strSql.Append(" LogicStoreAreaID,LogicStoreAreaCode,LogicStoreAreaName,LogicStoreAreaType,LogicStoreAreaDescribe,LogicStoreAreaSign ");
			strSql.Append(" FROM LogicStoreArea ");
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
			strSql.Append("select count(1) FROM LogicStoreArea ");
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
				strSql.Append("order by T.LogicStoreAreaID desc");
			}
			strSql.Append(")AS Row, T.*  from LogicStoreArea T ");
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
			parameters[0].Value = "LogicStoreArea";
			parameters[1].Value = "LogicStoreAreaID";
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

