using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:Man_WarnnigConfig
	/// </summary>
	public partial class Man_WarnnigConfigDal
	{
		public Man_WarnnigConfigDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		    return (int)DbHelperSQL.GetMaxID("WarningCode", "Man_WarnnigConfig"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int WarningCode)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Man_WarnnigConfig");
			strSql.Append(" where WarningCode=@WarningCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@WarningCode", SqlDbType.Int,4)			};
			parameters[0].Value = WarningCode;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Man_WarnnigConfigModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Man_WarnnigConfig(");
			strSql.Append("WarningCode,WarningLayer,WarningCata,WarningExplain,WarningName)");
			strSql.Append(" values (");
			strSql.Append("@WarningCode,@WarningLayer,@WarningCata,@WarningExplain,@WarningName)");
			SqlParameter[] parameters = {
					new SqlParameter("@WarningCode", SqlDbType.Int,4),
					new SqlParameter("@WarningLayer", SqlDbType.NVarChar,50),
					new SqlParameter("@WarningCata", SqlDbType.NVarChar,50),
					new SqlParameter("@WarningExplain", SqlDbType.NVarChar,500),
					new SqlParameter("@WarningName", SqlDbType.NChar,10)};
			parameters[0].Value = model.WarningCode;
			parameters[1].Value = model.WarningLayer;
			parameters[2].Value = model.WarningCata;
			parameters[3].Value = model.WarningExplain;
			parameters[4].Value = model.WarningName;

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
		public bool Update(Man_WarnnigConfigModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Man_WarnnigConfig set ");
			strSql.Append("WarningLayer=@WarningLayer,");
			strSql.Append("WarningCata=@WarningCata,");
			strSql.Append("WarningExplain=@WarningExplain,");
			strSql.Append("WarningName=@WarningName");
			strSql.Append(" where WarningCode=@WarningCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@WarningLayer", SqlDbType.NVarChar,50),
					new SqlParameter("@WarningCata", SqlDbType.NVarChar,50),
					new SqlParameter("@WarningExplain", SqlDbType.NVarChar,500),
					new SqlParameter("@WarningName", SqlDbType.NChar,10),
					new SqlParameter("@WarningCode", SqlDbType.Int,4)};
			parameters[0].Value = model.WarningLayer;
			parameters[1].Value = model.WarningCata;
			parameters[2].Value = model.WarningExplain;
			parameters[3].Value = model.WarningName;
			parameters[4].Value = model.WarningCode;

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
		public bool Delete(int WarningCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Man_WarnnigConfig ");
			strSql.Append(" where WarningCode=@WarningCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@WarningCode", SqlDbType.Int,4)			};
			parameters[0].Value = WarningCode;

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
		public bool DeleteList(string WarningCodelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Man_WarnnigConfig ");
			strSql.Append(" where WarningCode in ("+WarningCodelist + ")  ");
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
		public Man_WarnnigConfigModel GetModel(int WarningCode)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 WarningCode,WarningLayer,WarningCata,WarningExplain,WarningName from Man_WarnnigConfig ");
			strSql.Append(" where WarningCode=@WarningCode ");
			SqlParameter[] parameters = {
					new SqlParameter("@WarningCode", SqlDbType.Int,4)			};
			parameters[0].Value = WarningCode;

			Man_WarnnigConfigModel model=new Man_WarnnigConfigModel();
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
		public Man_WarnnigConfigModel DataRowToModel(DataRow row)
		{
			Man_WarnnigConfigModel model=new Man_WarnnigConfigModel();
			if (row != null)
			{
				if(row["WarningCode"]!=null && row["WarningCode"].ToString()!="")
				{
					model.WarningCode=int.Parse(row["WarningCode"].ToString());
				}
				if(row["WarningLayer"]!=null)
				{
					model.WarningLayer=row["WarningLayer"].ToString();
				}
				if(row["WarningCata"]!=null)
				{
					model.WarningCata=row["WarningCata"].ToString();
				}
				if(row["WarningExplain"]!=null)
				{
					model.WarningExplain=row["WarningExplain"].ToString();
				}
				if(row["WarningName"]!=null)
				{
					model.WarningName=row["WarningName"].ToString();
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
			strSql.Append("select WarningCode,WarningLayer,WarningCata,WarningExplain,WarningName ");
			strSql.Append(" FROM Man_WarnnigConfig ");
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
			strSql.Append(" WarningCode,WarningLayer,WarningCata,WarningExplain,WarningName ");
			strSql.Append(" FROM Man_WarnnigConfig ");
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
			strSql.Append("select count(1) FROM Man_WarnnigConfig ");
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
				strSql.Append("order by T.WarningCode desc");
			}
			strSql.Append(")AS Row, T.*  from Man_WarnnigConfig T ");
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
			parameters[0].Value = "Man_WarnnigConfig";
			parameters[1].Value = "WarningCode";
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