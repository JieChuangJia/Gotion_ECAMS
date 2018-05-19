/**  版本信息模板在安装目录下，可自行修改。
* Product.cs
*
* 功 能： N/A
* 类 名： Product
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:03   N/A    初版
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
	/// 数据访问类:Product
	/// </summary>
	public partial class ProductDal
	{
		public ProductDal()
		{}
		#region  BasicMethod
/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ProductID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Product");
			strSql.Append(" where ProductID=@ProductID");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.Int,4)
			};
			parameters[0].Value = ProductID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ProductModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Product(");
			strSql.Append("ProductCode,ProductName,ProductUnit,FullTrayNum,ProductSign)");
			strSql.Append(" values (");
			strSql.Append("@ProductCode,@ProductName,@ProductUnit,@FullTrayNum,@ProductSign)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@FullTrayNum", SqlDbType.Int,4),
					new SqlParameter("@ProductSign", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ProductCode;
			parameters[1].Value = model.ProductName;
			parameters[2].Value = model.ProductUnit;
			parameters[3].Value = model.FullTrayNum;
			parameters[4].Value = model.ProductSign;

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
		public bool Update(ProductModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Product set ");
			strSql.Append("ProductCode=@ProductCode,");
			strSql.Append("ProductName=@ProductName,");
			strSql.Append("ProductUnit=@ProductUnit,");
			strSql.Append("FullTrayNum=@FullTrayNum,");
			strSql.Append("ProductSign=@ProductSign");
			strSql.Append(" where ProductID=@ProductID");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@FullTrayNum", SqlDbType.Int,4),
					new SqlParameter("@ProductSign", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductID", SqlDbType.Int,4)};
			parameters[0].Value = model.ProductCode;
			parameters[1].Value = model.ProductName;
			parameters[2].Value = model.ProductUnit;
			parameters[3].Value = model.FullTrayNum;
			parameters[4].Value = model.ProductSign;
			parameters[5].Value = model.ProductID;

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
		public bool Delete(int ProductID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Product ");
			strSql.Append(" where ProductID=@ProductID");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.Int,4)
			};
			parameters[0].Value = ProductID;

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
		public bool DeleteList(string ProductIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Product ");
			strSql.Append(" where ProductID in ("+ProductIDlist + ")  ");
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
		public ProductModel GetModel(int ProductID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ProductID,ProductCode,ProductName,ProductUnit,FullTrayNum,ProductSign from Product ");
			strSql.Append(" where ProductID=@ProductID");
			SqlParameter[] parameters = {
					new SqlParameter("@ProductID", SqlDbType.Int,4)
			};
			parameters[0].Value = ProductID;

			ProductModel model=new ProductModel();
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
		public ProductModel DataRowToModel(DataRow row)
		{
			ProductModel model=new ProductModel();
			if (row != null)
			{
				if(row["ProductID"]!=null && row["ProductID"].ToString()!="")
				{
					model.ProductID=int.Parse(row["ProductID"].ToString());
				}
				if(row["ProductCode"]!=null)
				{
					model.ProductCode=row["ProductCode"].ToString();
				}
				if(row["ProductName"]!=null)
				{
					model.ProductName=row["ProductName"].ToString();
				}
				if(row["ProductUnit"]!=null)
				{
					model.ProductUnit=row["ProductUnit"].ToString();
				}
				if(row["FullTrayNum"]!=null && row["FullTrayNum"].ToString()!="")
				{
					model.FullTrayNum=int.Parse(row["FullTrayNum"].ToString());
				}
				if(row["ProductSign"]!=null)
				{
					model.ProductSign=row["ProductSign"].ToString();
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
			strSql.Append("select ProductID,ProductCode,ProductName,ProductUnit,FullTrayNum,ProductSign ");
			strSql.Append(" FROM Product ");
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
			strSql.Append(" ProductID,ProductCode,ProductName,ProductUnit,FullTrayNum,ProductSign ");
			strSql.Append(" FROM Product ");
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
			strSql.Append("select count(1) FROM Product ");
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
				strSql.Append("order by T.ProductID desc");
			}
			strSql.Append(")AS Row, T.*  from Product T ");
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
			parameters[0].Value = "Product";
			parameters[1].Value = "ProductID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        public bool UpdateByCode(ProductModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Product set ");
            strSql.Append("ProductCode=@ProductCode,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ProductUnit=@ProductUnit,");
            strSql.Append("FullTrayNum=@FullTrayNum,");
            strSql.Append("ProductSign=@ProductSign");
            strSql.Append(" where ProductCode=@ProductCode");
            SqlParameter[] parameters = {
					new SqlParameter("@ProductCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProductUnit", SqlDbType.NVarChar,50),
					new SqlParameter("@FullTrayNum", SqlDbType.Int,4),
					new SqlParameter("@ProductSign", SqlDbType.NVarChar,50)};
				
            parameters[0].Value = model.ProductCode;
            parameters[1].Value = model.ProductName;
            parameters[2].Value = model.ProductUnit;
            parameters[3].Value = model.FullTrayNum;
            parameters[4].Value = model.ProductSign;
          

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
		#endregion  ExtensionMethod
	}
}

