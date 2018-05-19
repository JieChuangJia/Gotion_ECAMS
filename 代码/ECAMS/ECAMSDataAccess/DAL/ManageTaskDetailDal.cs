/**  版本信息模板在安装目录下，可自行修改。
* ManageTaskDetail.cs
*
* 功 能： N/A
* 类 名： ManageTaskDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:01   N/A    初版
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
	/// 数据访问类:ManageTaskDetail
	/// </summary>
	public partial class ManageTaskDetailDal
	{
		public ManageTaskDetailDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long TaskDetailID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ManageTaskDetail");
			strSql.Append(" where TaskDetailID=@TaskDetailID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaskDetailID", SqlDbType.BigInt,8)			};
			parameters[0].Value = TaskDetailID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ECAMSDataAccess.ManageTaskDetailModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ManageTaskDetail(");
			strSql.Append("TaskDetailID,TaskDetailListID,CoreCode,Remark)");
			strSql.Append(" values (");
			strSql.Append("@TaskDetailID,@TaskDetailListID,@CoreCode,@Remark)");
			SqlParameter[] parameters = {
					new SqlParameter("@TaskDetailID", SqlDbType.BigInt,8),
					new SqlParameter("@TaskDetailListID", SqlDbType.BigInt,8),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarBinary,100)};
			parameters[0].Value = model.TaskDetailID;
			parameters[1].Value = model.TaskDetailListID;
			parameters[2].Value = model.CoreCode;
			parameters[3].Value = model.Remark;

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
		public bool Update(ECAMSDataAccess.ManageTaskDetailModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ManageTaskDetail set ");
			strSql.Append("TaskDetailListID=@TaskDetailListID,");
			strSql.Append("CoreCode=@CoreCode,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where TaskDetailID=@TaskDetailID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaskDetailListID", SqlDbType.BigInt,8),
					new SqlParameter("@CoreCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarBinary,100),
					new SqlParameter("@TaskDetailID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.TaskDetailListID;
			parameters[1].Value = model.CoreCode;
			parameters[2].Value = model.Remark;
			parameters[3].Value = model.TaskDetailID;

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
		public bool Delete(long TaskDetailID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ManageTaskDetail ");
			strSql.Append(" where TaskDetailID=@TaskDetailID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaskDetailID", SqlDbType.BigInt,8)			};
			parameters[0].Value = TaskDetailID;

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
		public bool DeleteList(string TaskDetailIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ManageTaskDetail ");
			strSql.Append(" where TaskDetailID in ("+TaskDetailIDlist + ")  ");
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
		public ECAMSDataAccess.ManageTaskDetailModel GetModel(long TaskDetailID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 TaskDetailID,TaskDetailListID,CoreCode,Remark from ManageTaskDetail ");
			strSql.Append(" where TaskDetailID=@TaskDetailID ");
			SqlParameter[] parameters = {
					new SqlParameter("@TaskDetailID", SqlDbType.BigInt,8)			};
			parameters[0].Value = TaskDetailID;

			ECAMSDataAccess.ManageTaskDetailModel model=new ECAMSDataAccess.ManageTaskDetailModel();
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
		public ECAMSDataAccess.ManageTaskDetailModel DataRowToModel(DataRow row)
		{
			ECAMSDataAccess.ManageTaskDetailModel model=new ECAMSDataAccess.ManageTaskDetailModel();
			if (row != null)
			{
				if(row["TaskDetailID"]!=null && row["TaskDetailID"].ToString()!="")
				{
					model.TaskDetailID=long.Parse(row["TaskDetailID"].ToString());
				}
				if(row["TaskDetailListID"]!=null && row["TaskDetailListID"].ToString()!="")
				{
					model.TaskDetailListID=long.Parse(row["TaskDetailListID"].ToString());
				}
				if(row["CoreCode"]!=null)
				{
					model.CoreCode=row["CoreCode"].ToString();
				}
				if(row["Remark"]!=null && row["Remark"].ToString()!="")
				{
					model.Remark=(byte[])row["Remark"];
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
			strSql.Append("select TaskDetailID,TaskDetailListID,CoreCode,Remark ");
			strSql.Append(" FROM ManageTaskDetail ");
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
			strSql.Append(" TaskDetailID,TaskDetailListID,CoreCode,Remark ");
			strSql.Append(" FROM ManageTaskDetail ");
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
			strSql.Append("select count(1) FROM ManageTaskDetail ");
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
				strSql.Append("order by T.TaskDetailID desc");
			}
			strSql.Append(")AS Row, T.*  from ManageTaskDetail T ");
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
			parameters[0].Value = "ManageTaskDetail";
			parameters[1].Value = "TaskDetailID";
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

