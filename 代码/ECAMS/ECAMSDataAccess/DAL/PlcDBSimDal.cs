using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:PlcDBSim
	/// </summary>
	public partial class PlcDBSimDal
	{
		public PlcDBSimDal()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string PlcAddr)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from PlcDBSim");
			strSql.Append(" where PlcAddr=@PlcAddr ");
			SqlParameter[] parameters = {
					new SqlParameter("@PlcAddr", SqlDbType.NChar,10)			};
			parameters[0].Value = PlcAddr;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ECAMSDataAccess.PlcDBSimModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into PlcDBSim(");
			strSql.Append("PlcAddr,Val)");
			strSql.Append(" values (");
			strSql.Append("@PlcAddr,@Val)");
			SqlParameter[] parameters = {
					new SqlParameter("@PlcAddr", SqlDbType.NChar,10),
					new SqlParameter("@Val", SqlDbType.Int,4)};
			parameters[0].Value = model.PlcAddr;
			parameters[1].Value = model.Val;

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
		public bool Update(ECAMSDataAccess.PlcDBSimModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update PlcDBSim set ");
			strSql.Append("Val=@Val");
			strSql.Append(" where PlcAddr=@PlcAddr ");
			SqlParameter[] parameters = {
					new SqlParameter("@Val", SqlDbType.Int,4),
					new SqlParameter("@PlcAddr", SqlDbType.NChar,10)};
			parameters[0].Value = model.Val;
			parameters[1].Value = model.PlcAddr;

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
		public bool Delete(string PlcAddr)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PlcDBSim ");
			strSql.Append(" where PlcAddr=@PlcAddr ");
			SqlParameter[] parameters = {
					new SqlParameter("@PlcAddr", SqlDbType.NChar,10)			};
			parameters[0].Value = PlcAddr;

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
		public bool DeleteList(string PlcAddrlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from PlcDBSim ");
			strSql.Append(" where PlcAddr in ("+PlcAddrlist + ")  ");
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
		public ECAMSDataAccess.PlcDBSimModel GetModel(string PlcAddr)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 PlcAddr,Val from PlcDBSim ");
			strSql.Append(" where PlcAddr=@PlcAddr ");
			SqlParameter[] parameters = {
					new SqlParameter("@PlcAddr", SqlDbType.NChar,10)			};
			parameters[0].Value = PlcAddr;

			ECAMSDataAccess.PlcDBSimModel model=new ECAMSDataAccess.PlcDBSimModel();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["PlcAddr"]!=null && ds.Tables[0].Rows[0]["PlcAddr"].ToString()!="")
				{
					model.PlcAddr=ds.Tables[0].Rows[0]["PlcAddr"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Val"]!=null && ds.Tables[0].Rows[0]["Val"].ToString()!="")
				{
					model.Val=int.Parse(ds.Tables[0].Rows[0]["Val"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PlcAddr,Val ");
			strSql.Append(" FROM PlcDBSim ");
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
			strSql.Append(" PlcAddr,Val ");
			strSql.Append(" FROM PlcDBSim ");
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
			strSql.Append("select count(1) FROM PlcDBSim ");
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
				strSql.Append("order by T.PlcAddr desc");
			}
			strSql.Append(")AS Row, T.*  from PlcDBSim T ");
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
			parameters[0].Value = "PlcDBSim";
			parameters[1].Value = "PlcAddr";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

