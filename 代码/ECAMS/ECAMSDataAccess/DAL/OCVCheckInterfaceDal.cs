/**  版本信息模板在安装目录下，可自行修改。
* OCVCheckInterface.cs
*
* 功 能： N/A
* 类 名： OCVCheckInterface
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

namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:OCVCheckInterface
	/// </summary>
	public partial class OCVCheckInterfaceDal
	{
		public OCVCheckInterfaceDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CheckInterfaceID", "OCVCheckInterface"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CheckInterfaceID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from OCVCheckInterface");
			strSql.Append(" where CheckInterfaceID=@CheckInterfaceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckInterfaceID", SqlDbType.Int,4)			};
			parameters[0].Value = CheckInterfaceID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ECAMSDataAccess.OCVCheckInterfaceModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into OCVCheckInterface(");
			strSql.Append("CheckInterfaceID,ProductFrameCode,CheckType,CorePosition,HasCore,CoreStatus,ReportTime,HandleStatus,Remarks)");
			strSql.Append(" values (");
			strSql.Append("@CheckInterfaceID,@ProductFrameCode,@CheckType,@CorePosition,@HasCore,@CoreStatus,@ReportTime,@HandleStatus,@Remarks)");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckInterfaceID", SqlDbType.Int,4),
					new SqlParameter("@ProductFrameCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckType", SqlDbType.NVarChar,50),
					new SqlParameter("@CorePosition", SqlDbType.NVarChar,50),
					new SqlParameter("@HasCore", SqlDbType.Int,4),
					new SqlParameter("@CoreStatus", SqlDbType.Int,4),
					new SqlParameter("@ReportTime", SqlDbType.DateTime),
					new SqlParameter("@HandleStatus", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.CheckInterfaceID;
			parameters[1].Value = model.ProductFrameCode;
			parameters[2].Value = model.CheckType;
			parameters[3].Value = model.CorePosition;
			parameters[4].Value = model.HasCore;
			parameters[5].Value = model.CoreStatus;
			parameters[6].Value = model.ReportTime;
			parameters[7].Value = model.HandleStatus;
			parameters[8].Value = model.Remarks;

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
		public bool Update(ECAMSDataAccess.OCVCheckInterfaceModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update OCVCheckInterface set ");
			strSql.Append("ProductFrameCode=@ProductFrameCode,");
			strSql.Append("CheckType=@CheckType,");
			strSql.Append("CorePosition=@CorePosition,");
			strSql.Append("HasCore=@HasCore,");
			strSql.Append("CoreStatus=@CoreStatus,");
			strSql.Append("ReportTime=@ReportTime,");
			strSql.Append("HandleStatus=@HandleStatus,");
			strSql.Append("Remarks=@Remarks");
			strSql.Append(" where CheckInterfaceID=@CheckInterfaceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductFrameCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckType", SqlDbType.NVarChar,50),
					new SqlParameter("@CorePosition", SqlDbType.NVarChar,50),
					new SqlParameter("@HasCore", SqlDbType.Int,4),
					new SqlParameter("@CoreStatus", SqlDbType.Int,4),
					new SqlParameter("@ReportTime", SqlDbType.DateTime),
					new SqlParameter("@HandleStatus", SqlDbType.Int,4),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100),
					new SqlParameter("@CheckInterfaceID", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductFrameCode;
			parameters[1].Value = model.CheckType;
			parameters[2].Value = model.CorePosition;
			parameters[3].Value = model.HasCore;
			parameters[4].Value = model.CoreStatus;
			parameters[5].Value = model.ReportTime;
			parameters[6].Value = model.HandleStatus;
			parameters[7].Value = model.Remarks;
			parameters[8].Value = model.CheckInterfaceID;

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
		public bool Delete(int CheckInterfaceID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OCVCheckInterface ");
			strSql.Append(" where CheckInterfaceID=@CheckInterfaceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckInterfaceID", SqlDbType.Int,4)			};
			parameters[0].Value = CheckInterfaceID;

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
		public bool DeleteList(string CheckInterfaceIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from OCVCheckInterface ");
			strSql.Append(" where CheckInterfaceID in ("+CheckInterfaceIDlist + ")  ");
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
		public ECAMSDataAccess.OCVCheckInterfaceModel GetModel(int CheckInterfaceID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 CheckInterfaceID,ProductFrameCode,CheckType,CorePosition,HasCore,CoreStatus,ReportTime,HandleStatus,Remarks from OCVCheckInterface ");
			strSql.Append(" where CheckInterfaceID=@CheckInterfaceID ");
			SqlParameter[] parameters = {
					new SqlParameter("@CheckInterfaceID", SqlDbType.Int,4)			};
			parameters[0].Value = CheckInterfaceID;

			ECAMSDataAccess.OCVCheckInterfaceModel model=new ECAMSDataAccess.OCVCheckInterfaceModel();
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
		public ECAMSDataAccess.OCVCheckInterfaceModel DataRowToModel(DataRow row)
		{
			ECAMSDataAccess.OCVCheckInterfaceModel model=new ECAMSDataAccess.OCVCheckInterfaceModel();
			if (row != null)
			{
				if(row["CheckInterfaceID"]!=null && row["CheckInterfaceID"].ToString()!="")
				{
					model.CheckInterfaceID=int.Parse(row["CheckInterfaceID"].ToString());
				}
				if(row["ProductFrameCode"]!=null)
				{
					model.ProductFrameCode=row["ProductFrameCode"].ToString();
				}
				if(row["CheckType"]!=null)
				{
					model.CheckType=row["CheckType"].ToString();
				}
				if(row["CorePosition"]!=null)
				{
					model.CorePosition=row["CorePosition"].ToString();
				}
				if(row["HasCore"]!=null && row["HasCore"].ToString()!="")
				{
					model.HasCore=int.Parse(row["HasCore"].ToString());
				}
				if(row["CoreStatus"]!=null && row["CoreStatus"].ToString()!="")
				{
					model.CoreStatus=int.Parse(row["CoreStatus"].ToString());
				}
				if(row["ReportTime"]!=null && row["ReportTime"].ToString()!="")
				{
					model.ReportTime=DateTime.Parse(row["ReportTime"].ToString());
				}
				if(row["HandleStatus"]!=null && row["HandleStatus"].ToString()!="")
				{
					model.HandleStatus=int.Parse(row["HandleStatus"].ToString());
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
			strSql.Append("select CheckInterfaceID,ProductFrameCode,CheckType,CorePosition,HasCore,CoreStatus,ReportTime,HandleStatus,Remarks ");
			strSql.Append(" FROM OCVCheckInterface ");
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
			strSql.Append(" CheckInterfaceID,ProductFrameCode,CheckType,CorePosition,HasCore,CoreStatus,ReportTime,HandleStatus,Remarks ");
			strSql.Append(" FROM OCVCheckInterface ");
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
			strSql.Append("select count(1) FROM OCVCheckInterface ");
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
				strSql.Append("order by T.CheckInterfaceID desc");
			}
			strSql.Append(")AS Row, T.*  from OCVCheckInterface T ");
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
			parameters[0].Value = "OCVCheckInterface";
			parameters[1].Value = "CheckInterfaceID";
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

