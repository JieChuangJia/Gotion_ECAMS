/**  版本信息模板在安装目录下，可自行修改。
* Device.cs
*
* 功 能： N/A
* 类 名： Device
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:49:59   N/A    初版
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
	/// 数据访问类:Device
	/// </summary>
	public partial class DeviceDal
	{
		public DeviceDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DeviceID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Device");
			strSql.Append(" where DeviceID=@DeviceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = DeviceID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ECAMSDataAccess.DeviceModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Device(");
			strSql.Append("DeviceID,TaskTypeCode,DeviceType,DeviceDescribe,DB1AddrStart,BytesLenDB1,DB2AddrStart,BytesLenDB2,DeviceStatus,DevStatusDescribe)");
			strSql.Append(" values (");
			strSql.Append("@DeviceID,@TaskTypeCode,@DeviceType,@DeviceDescribe,@DB1AddrStart,@BytesLenDB1,@DB2AddrStart,@BytesLenDB2,@DeviceStatus,@DevStatusDescribe)");
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4),
					new SqlParameter("@DeviceType", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceDescribe", SqlDbType.NVarChar,50),
					new SqlParameter("@DB1AddrStart", SqlDbType.NVarChar,50),
					new SqlParameter("@BytesLenDB1", SqlDbType.Int,4),
					new SqlParameter("@DB2AddrStart", SqlDbType.NVarChar,50),
					new SqlParameter("@BytesLenDB2", SqlDbType.Int,4),
					new SqlParameter("@DeviceStatus", SqlDbType.NVarChar,20),
					new SqlParameter("@DevStatusDescribe", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.DeviceID;
			parameters[1].Value = model.TaskTypeCode;
			parameters[2].Value = model.DeviceType;
			parameters[3].Value = model.DeviceDescribe;
			parameters[4].Value = model.DB1AddrStart;
			parameters[5].Value = model.BytesLenDB1;
			parameters[6].Value = model.DB2AddrStart;
			parameters[7].Value = model.BytesLenDB2;
			parameters[8].Value = model.DeviceStatus;
			parameters[9].Value = model.DevStatusDescribe;

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
		public bool Update(ECAMSDataAccess.DeviceModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Device set ");
			strSql.Append("TaskTypeCode=@TaskTypeCode,");
			strSql.Append("DeviceType=@DeviceType,");
			strSql.Append("DeviceDescribe=@DeviceDescribe,");
			strSql.Append("DB1AddrStart=@DB1AddrStart,");
			strSql.Append("BytesLenDB1=@BytesLenDB1,");
			strSql.Append("DB2AddrStart=@DB2AddrStart,");
			strSql.Append("BytesLenDB2=@BytesLenDB2,");
			strSql.Append("DeviceStatus=@DeviceStatus,");
			strSql.Append("DevStatusDescribe=@DevStatusDescribe");
			strSql.Append(" where DeviceID=@DeviceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4),
					new SqlParameter("@DeviceType", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceDescribe", SqlDbType.NVarChar,50),
					new SqlParameter("@DB1AddrStart", SqlDbType.NVarChar,50),
					new SqlParameter("@BytesLenDB1", SqlDbType.Int,4),
					new SqlParameter("@DB2AddrStart", SqlDbType.NVarChar,50),
					new SqlParameter("@BytesLenDB2", SqlDbType.Int,4),
					new SqlParameter("@DeviceStatus", SqlDbType.NVarChar,20),
					new SqlParameter("@DevStatusDescribe", SqlDbType.NVarChar,100),
					new SqlParameter("@DeviceID", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.TaskTypeCode;
			parameters[1].Value = model.DeviceType;
			parameters[2].Value = model.DeviceDescribe;
			parameters[3].Value = model.DB1AddrStart;
			parameters[4].Value = model.BytesLenDB1;
			parameters[5].Value = model.DB2AddrStart;
			parameters[6].Value = model.BytesLenDB2;
			parameters[7].Value = model.DeviceStatus;
			parameters[8].Value = model.DevStatusDescribe;
			parameters[9].Value = model.DeviceID;

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
		public bool Delete(string DeviceID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Device ");
			strSql.Append(" where DeviceID=@DeviceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = DeviceID;

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
		public bool DeleteList(string DviceIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Device ");
			strSql.Append(" where DeviceID in ("+DviceIDlist + ")  ");
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
		public ECAMSDataAccess.DeviceModel GetModel(string DeviceID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 DeviceID,TaskTypeCode,DeviceType,DeviceDescribe,DB1AddrStart,BytesLenDB1,DB2AddrStart,BytesLenDB2,DeviceStatus,DevStatusDescribe from Device ");
			strSql.Append(" where DeviceID=@DeviceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@DeviceID", SqlDbType.NVarChar,50)			};
			parameters[0].Value = DeviceID;

			ECAMSDataAccess.DeviceModel model=new ECAMSDataAccess.DeviceModel();
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
		public ECAMSDataAccess.DeviceModel DataRowToModel(DataRow row)
		{
			ECAMSDataAccess.DeviceModel model=new ECAMSDataAccess.DeviceModel();
			if (row != null)
			{
				if(row["DeviceID"]!=null)
				{
					model.DeviceID=row["DeviceID"].ToString();
				}
				if(row["TaskTypeCode"]!=null && row["TaskTypeCode"].ToString()!="")
				{
					model.TaskTypeCode=int.Parse(row["TaskTypeCode"].ToString());
				}
				if(row["DeviceType"]!=null)
				{
					model.DeviceType=row["DeviceType"].ToString();
				}
				if(row["DeviceDescribe"]!=null)
				{
					model.DeviceDescribe=row["DeviceDescribe"].ToString();
				}
				if(row["DB1AddrStart"]!=null)
				{
					model.DB1AddrStart=row["DB1AddrStart"].ToString();
				}
				if(row["BytesLenDB1"]!=null && row["BytesLenDB1"].ToString()!="")
				{
					model.BytesLenDB1=int.Parse(row["BytesLenDB1"].ToString());
				}
				if(row["DB2AddrStart"]!=null)
				{
					model.DB2AddrStart=row["DB2AddrStart"].ToString();
				}
				if(row["BytesLenDB2"]!=null && row["BytesLenDB2"].ToString()!="")
				{
					model.BytesLenDB2=int.Parse(row["BytesLenDB2"].ToString());
				}
				if(row["DeviceStatus"]!=null)
				{
					model.DeviceStatus=row["DeviceStatus"].ToString();
				}
				if(row["DevStatusDescribe"]!=null)
				{
					model.DevStatusDescribe=row["DevStatusDescribe"].ToString();
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
			strSql.Append("select DeviceID,TaskTypeCode,DeviceType,DeviceDescribe,DB1AddrStart,BytesLenDB1,DB2AddrStart,BytesLenDB2,DeviceStatus,DevStatusDescribe ");
			strSql.Append(" FROM Device ");
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
			strSql.Append(" DeviceID,TaskTypeCode,DeviceType,DeviceDescribe,DB1AddrStart,BytesLenDB1,DB2AddrStart,BytesLenDB2,DeviceStatus,DevStatusDescribe ");
			strSql.Append(" FROM Device ");
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
			strSql.Append("select count(1) FROM Device ");
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
				strSql.Append("order by T.DeviceID desc");
			}
			strSql.Append(")AS Row, T.*  from Device T ");
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
			parameters[0].Value = "Device";
			parameters[1].Value = "DeviceID";
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

