/**  版本信息模板在安装目录下，可自行修改。
* ControlInterface.cs
*
* 功 能： N/A
* 类 名： ControlInterface
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:49:58   N/A    初版
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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:ControlInterface
	/// </summary>
	public partial class ControlInterfaceDal
	{
		public ControlInterfaceDal()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ControlInterfaceID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ControlInterface");
            strSql.Append(" where ControlInterfaceID=@ControlInterfaceID");
            SqlParameter[] parameters = {
					new SqlParameter("@ControlInterfaceID", SqlDbType.BigInt)
			};
            parameters[0].Value = ControlInterfaceID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ControlInterfaceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ControlInterface(");
            strSql.Append("InterfaceType,DeviceCode,TaskCode,InterfaceStatus,CreateTime,InterfaceParameter,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@InterfaceType,@DeviceCode,@TaskCode,@InterfaceStatus,@CreateTime,@InterfaceParameter,@Remarks)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@InterfaceType", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCode", SqlDbType.NVarChar,6),
					new SqlParameter("@InterfaceStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@InterfaceParameter", SqlDbType.NVarChar,200),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100)};
            parameters[0].Value = model.InterfaceType;
            parameters[1].Value = model.DeviceCode;
            parameters[2].Value = model.TaskCode;
            parameters[3].Value = model.InterfaceStatus;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.InterfaceParameter;
            parameters[6].Value = model.Remarks;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(ControlInterfaceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ControlInterface set ");
            strSql.Append("InterfaceType=@InterfaceType,");
            strSql.Append("DeviceCode=@DeviceCode,");
            strSql.Append("TaskCode=@TaskCode,");
            strSql.Append("InterfaceStatus=@InterfaceStatus,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("InterfaceParameter=@InterfaceParameter,");
            strSql.Append("Remarks=@Remarks");
            strSql.Append(" where ControlInterfaceID=@ControlInterfaceID");
            SqlParameter[] parameters = {
					new SqlParameter("@InterfaceType", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCode", SqlDbType.NVarChar,6),
					new SqlParameter("@InterfaceStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@InterfaceParameter", SqlDbType.NVarChar,200),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlInterfaceID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.InterfaceType;
            parameters[1].Value = model.DeviceCode;
            parameters[2].Value = model.TaskCode;
            parameters[3].Value = model.InterfaceStatus;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.InterfaceParameter;
            parameters[6].Value = model.Remarks;
            parameters[7].Value = model.ControlInterfaceID;

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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ControlInterfaceID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlInterface ");
            strSql.Append(" where ControlInterfaceID=@ControlInterfaceID");
            SqlParameter[] parameters = {
					new SqlParameter("@ControlInterfaceID", SqlDbType.BigInt)
			};
            parameters[0].Value = ControlInterfaceID;

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
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string ControlInterfaceIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlInterface ");
            strSql.Append(" where ControlInterfaceID in (" + ControlInterfaceIDlist + ")  ");
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


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ControlInterfaceModel GetModel(long ControlInterfaceID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ControlInterfaceID,InterfaceType,DeviceCode,TaskCode,InterfaceStatus,CreateTime,InterfaceParameter,Remarks from ControlInterface ");
            strSql.Append(" where ControlInterfaceID=@ControlInterfaceID");
            SqlParameter[] parameters = {
					new SqlParameter("@ControlInterfaceID", SqlDbType.BigInt)
			};
            parameters[0].Value = ControlInterfaceID;

            ControlInterfaceModel model = new ControlInterfaceModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public ControlInterfaceModel DataRowToModel(DataRow row)
        {
            ControlInterfaceModel model = new ControlInterfaceModel();
            if (row != null)
            {
                if (row["ControlInterfaceID"] != null && row["ControlInterfaceID"].ToString() != "")
                {
                    model.ControlInterfaceID = long.Parse(row["ControlInterfaceID"].ToString());
                }
                if (row["InterfaceType"] != null)
                {
                    model.InterfaceType = row["InterfaceType"].ToString();
                }
                if (row["DeviceCode"] != null)
                {
                    model.DeviceCode = row["DeviceCode"].ToString();
                }
                if (row["TaskCode"] != null)
                {
                    model.TaskCode = row["TaskCode"].ToString();
                }
                if (row["InterfaceStatus"] != null)
                {
                    model.InterfaceStatus = row["InterfaceStatus"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["InterfaceParameter"] != null)
                {
                    model.InterfaceParameter = row["InterfaceParameter"].ToString();
                }
                if (row["Remarks"] != null)
                {
                    model.Remarks = row["Remarks"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ControlInterfaceID,InterfaceType,DeviceCode,TaskCode,InterfaceStatus,CreateTime,InterfaceParameter,Remarks ");
            strSql.Append(" FROM ControlInterface ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ControlInterfaceID,InterfaceType,DeviceCode,TaskCode,InterfaceStatus,CreateTime,InterfaceParameter,Remarks ");
            strSql.Append(" FROM ControlInterface ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM ControlInterface ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ControlInterfaceID desc");
            }
            strSql.Append(")AS Row, T.*  from ControlInterface T ");
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
            parameters[0].Value = "ControlInterface";
            parameters[1].Value = "ControlInterfaceID";
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
        /// 获取更新控制任务接口hash用于事物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Hashtable GetUpdateHash(ControlInterfaceModel model)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ControlInterface set ");
            strSql.Append("InterfaceType=@InterfaceType,");
            strSql.Append("DeviceCode=@DeviceCode,");
            strSql.Append("TaskCode=@TaskCode,");
            strSql.Append("InterfaceStatus=@InterfaceStatus,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("InterfaceParameter=@InterfaceParameter,");
            strSql.Append("Remarks=@Remarks");
            strSql.Append(" where ControlInterfaceID=@ControlInterfaceID");
            SqlParameter[] parameters = {
					new SqlParameter("@InterfaceType", SqlDbType.NVarChar,50),
					new SqlParameter("@DeviceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskCode", SqlDbType.NVarChar,50),
					new SqlParameter("@InterfaceStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@InterfaceParameter", SqlDbType.NVarChar,50),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,100),
					new SqlParameter("@ControlInterfaceID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.InterfaceType;
            parameters[1].Value = model.DeviceCode;
            parameters[2].Value = model.TaskCode;
            parameters[3].Value = model.InterfaceStatus;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.InterfaceParameter;
            parameters[6].Value = model.Remarks;
            parameters[7].Value = model.ControlInterfaceID;


            hs.Add(strSql.ToString(), parameters);
            return hs;
        }
		 public bool DeleteByControlCode(string controlCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlInterface ");
            strSql.Append(" where TaskCode=@TaskCode");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskCode", SqlDbType.NVarChar)
			};
            parameters[0].Value = controlCode;

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
		public Hashtable GetDeleteHash(string controlCode)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlInterface ");
            strSql.Append(" where TaskCode=@TaskCode");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskCode", SqlDbType.NVarChar)
			};
            parameters[0].Value = controlCode;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }
		#endregion  ExtensionMethod
        #region 控制层，zwx
		 /// <summary>
        /// 得到最大ID
        /// </summary>
        public long GetMaxTaskCode()
        {
            return DbHelperSQL.GetMaxID("ControlInterfaceID", "ControlInterface");
        }
		 public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlInterface");

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
		
        /// <summary>
        /// 根据条件得到一条任务申请记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public IList<ControlInterfaceModel> GetConditionedModel(string strWhere)
        {
            IList<ControlInterfaceModel> modelList = new List<ControlInterfaceModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ControlInterface ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ControlInterfaceModel m = DataRowToModel(dr);
                    modelList.Add(m);
                }
               
            }
           
            return modelList;
        }

        /// <summary>
        /// 获得当前最大任务号
        /// </summary>
        /// <returns></returns>
        public string GetTaskCodeMax()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  IDENT_CURRENT('dbo.ControlInterface')+IDENT_INCR('dbo.ControlInterface') ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }
        #endregion
    }
}

