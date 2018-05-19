/**  版本信息模板在安装目录下，可自行修改。
* ControlTask.cs
*
* 功 能： N/A
* 类 名： ControlTask
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
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using ECAMSModel;
namespace ECAMSDataAccess
{
	/// <summary>
	/// 数据访问类:ControlTask
	/// </summary>
	public partial class ControlTaskDal
	{
		public ControlTaskDal()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ControlTaskID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ControlTask");
            strSql.Append(" where ControlTaskID=@ControlTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@ControlTaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = ControlTaskID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ControlTaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ControlTask(");
            strSql.Append("TaskID,TaskTypeName,TaskTypeCode,TaskType,ControlCode,StartArea,StartDevice,TargetArea,TargetDevice,TaskParameter,TaskStatus,CreateMode,TaskPhase,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@TaskID,@TaskTypeName,@TaskTypeCode,@TaskType,@ControlCode,@StartArea,@StartDevice,@TargetArea,@TargetDevice,@TaskParameter,@TaskStatus,@CreateMode,@TaskPhase,@CreateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt,8),
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4),
					new SqlParameter("@TaskType", SqlDbType.NVarChar,20),
					new SqlParameter("@ControlCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StartArea", SqlDbType.NVarChar,50),
					new SqlParameter("@StartDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateMode", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskPhase", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.TaskID;
            parameters[1].Value = model.TaskTypeName;
            parameters[2].Value = model.TaskTypeCode;
            parameters[3].Value = model.TaskType;
            parameters[4].Value = model.ControlCode;
            parameters[5].Value = model.StartArea;
            parameters[6].Value = model.StartDevice;
            parameters[7].Value = model.TargetArea;
            parameters[8].Value = model.TargetDevice;
            parameters[9].Value = model.TaskParameter;
            parameters[10].Value = model.TaskStatus;
            parameters[11].Value = model.CreateMode;
            parameters[12].Value = model.TaskPhase;
            parameters[13].Value = model.CreateTime;

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
        public bool Update(ControlTaskModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ControlTask set ");
            strSql.Append("TaskID=@TaskID,");
            strSql.Append("TaskTypeName=@TaskTypeName,");
            strSql.Append("TaskTypeCode=@TaskTypeCode,");
            strSql.Append("TaskType=@TaskType,");
            strSql.Append("ControlCode=@ControlCode,");
            strSql.Append("StartArea=@StartArea,");
            strSql.Append("StartDevice=@StartDevice,");
            strSql.Append("TargetArea=@TargetArea,");
            strSql.Append("TargetDevice=@TargetDevice,");
            strSql.Append("TaskParameter=@TaskParameter,");
            strSql.Append("TaskStatus=@TaskStatus,");
            strSql.Append("CreateMode=@CreateMode,");
            strSql.Append("TaskPhase=@TaskPhase,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where ControlTaskID=@ControlTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt,8),
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4),
					new SqlParameter("@TaskType", SqlDbType.NVarChar,20),
					new SqlParameter("@ControlCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StartArea", SqlDbType.NVarChar,50),
					new SqlParameter("@StartDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateMode", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskPhase", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ControlTaskID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.TaskID;
            parameters[1].Value = model.TaskTypeName;
            parameters[2].Value = model.TaskTypeCode;
            parameters[3].Value = model.TaskType;
            parameters[4].Value = model.ControlCode;
            parameters[5].Value = model.StartArea;
            parameters[6].Value = model.StartDevice;
            parameters[7].Value = model.TargetArea;
            parameters[8].Value = model.TargetDevice;
            parameters[9].Value = model.TaskParameter;
            parameters[10].Value = model.TaskStatus;
            parameters[11].Value = model.CreateMode;
            parameters[12].Value = model.TaskPhase;
            parameters[13].Value = model.CreateTime;
            parameters[14].Value = model.ControlTaskID;

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
        public bool Delete(long ControlTaskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlTask ");
            strSql.Append(" where ControlTaskID=@ControlTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@ControlTaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = ControlTaskID;

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
        public bool DeleteList(string ControlTaskIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlTask ");
            strSql.Append(" where ControlTaskID in (" + ControlTaskIDlist + ")  ");
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
        public ControlTaskModel GetModel(long ControlTaskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ControlTaskID,TaskID,TaskTypeName,TaskTypeCode,TaskType,ControlCode,StartArea,StartDevice,TargetArea,TargetDevice,TaskParameter,TaskStatus,CreateMode,TaskPhase,CreateTime from ControlTask ");
            strSql.Append(" where ControlTaskID=@ControlTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@ControlTaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = ControlTaskID;

            ControlTaskModel model = new ControlTaskModel();
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
        public ControlTaskModel DataRowToModel(DataRow row)
        {
            ControlTaskModel model = new ControlTaskModel();
            if (row != null)
            {
              
                if (row["ControlTaskID"] != null && row["ControlTaskID"].ToString() != "")
                {
                    model.ControlTaskID = long.Parse(row["ControlTaskID"].ToString());
                }
                if (row["TaskID"] != null && row["TaskID"].ToString() != "")
                {
                    model.TaskID = long.Parse(row["TaskID"].ToString());
                }
                if (row["TaskTypeName"] != null)
                {
                    model.TaskTypeName = row["TaskTypeName"].ToString();
                }
                if (row["TaskTypeCode"] != null && row["TaskTypeCode"].ToString() != "")
                {
                    model.TaskTypeCode = int.Parse(row["TaskTypeCode"].ToString());
                }
                if (row["TaskType"] != null)
                {
                    model.TaskType = row["TaskType"].ToString();
                }
                if (row["ControlCode"] != null)
                {
                    model.ControlCode = row["ControlCode"].ToString();
                }
                if (row["StartArea"] != null)
                {
                    model.StartArea = row["StartArea"].ToString();
                }
                if (row["StartDevice"] != null)
                {
                    model.StartDevice = row["StartDevice"].ToString();
                }
                if (row["TargetArea"] != null)
                {
                    model.TargetArea = row["TargetArea"].ToString();
                }
                if (row["TargetDevice"] != null)
                {
                    model.TargetDevice = row["TargetDevice"].ToString();
                }
                if (row["TaskParameter"] != null)
                {
                    model.TaskParameter = row["TaskParameter"].ToString();
                }
                if (row["TaskStatus"] != null)
                {
                    model.TaskStatus = row["TaskStatus"].ToString();
                }
                if (row["CreateMode"] != null)
                {
                    model.CreateMode = row["CreateMode"].ToString();
                }
                if (row["TaskPhase"] != null)
                {
                    model.TaskPhase = row["TaskPhase"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
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
            strSql.Append("select ControlTaskID,TaskID,TaskTypeName,TaskTypeCode,TaskType,ControlCode,StartArea,StartDevice,TargetArea,TargetDevice,TaskParameter,TaskStatus,CreateMode,TaskPhase,CreateTime ");
            strSql.Append(" FROM ControlTask ");
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
            strSql.Append(" ControlTaskID,TaskID,TaskTypeName,TaskTypeCode,TaskType,ControlCode,StartArea,StartDevice,TargetArea,TargetDevice,TaskParameter,TaskStatus,CreateMode,TaskPhase,CreateTime ");
            strSql.Append(" FROM ControlTask ");
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
            strSql.Append("select count(1) FROM ControlTask ");
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
                strSql.Append("order by T.ControlTaskID desc");
            }
            strSql.Append(")AS Row, T.*  from ControlTask T ");
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
            parameters[0].Value = "ControlTask";
            parameters[1].Value = "ControlTaskID";
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
        /// 获取插入控制任务hash
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Hashtable GetAddModelHash(ControlTaskModel model)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ControlTask(");
            strSql.Append("TaskID,TaskTypeName,TaskTypeCode,TaskType,ControlCode,StartArea,StartDevice,TargetArea,TargetDevice,TaskParameter,TaskStatus,CreateMode,CreateTime,TaskPhase)");
            strSql.Append(" values (");
            strSql.Append("@TaskID,@TaskTypeName,@TaskTypeCode,@TaskType,@ControlCode,@StartArea,@StartDevice,@TargetArea,@TargetDevice,@TaskParameter,@TaskStatus,@CreateMode,@CreateTime,@TaskPhase)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TaskID", SqlDbType.BigInt,8),
					new SqlParameter("@TaskTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskTypeCode", SqlDbType.Int,4),
					new SqlParameter("@TaskType", SqlDbType.NVarChar,20),
					new SqlParameter("@ControlCode", SqlDbType.NVarChar,50),
					new SqlParameter("@StartArea", SqlDbType.NVarChar,50),
					new SqlParameter("@StartDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetArea", SqlDbType.NVarChar,50),
					new SqlParameter("@TargetDevice", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskParameter", SqlDbType.NVarChar,50),
					new SqlParameter("@TaskStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateMode", SqlDbType.NVarChar,50),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@TaskPhase", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.TaskID;
            parameters[1].Value = model.TaskTypeName;
            parameters[2].Value = model.TaskTypeCode;
            parameters[3].Value = model.TaskType;
            parameters[4].Value = model.ControlCode;
            parameters[5].Value = model.StartArea;
            parameters[6].Value = model.StartDevice;
            parameters[7].Value = model.TargetArea;
            parameters[8].Value = model.TargetDevice;
            parameters[9].Value = model.TaskParameter;
            parameters[10].Value = model.TaskStatus;
            parameters[11].Value = model.CreateMode;
            parameters[12].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
			parameters[13].Value = model.TaskPhase;
            hs.Add(strSql.ToString(), parameters);
            return hs;
        }

        public Hashtable GetDeleteModelHash(long controlTaskID)
        {
            Hashtable hs = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlTask ");
            strSql.Append(" where ControlTaskID=@ControlTaskID");
            SqlParameter[] parameters = {
					new SqlParameter("@ControlTaskID", SqlDbType.BigInt)
			};
            parameters[0].Value = controlTaskID;
            hs.Add(strSql.ToString(), parameters);
            return hs;

        }

        /// <summary>
        /// 获取所有控制任务
        /// </summary>
        /// <returns></returns>
        public DataSet GetControlList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  TaskID as 管理任务索引,TaskTypeName as 任务流程名称,ControlTaskID as 控制任务ID,"
            + "TaskParameter as 任务参数,TaskType as 任务类型 ,ControlCode as 控制条码 ,StartArea as 开始区域,StartDevice as 开始设备"
            + ",TargetArea as 目标区域,TargetDevice as 目标设备,TaskStatus as 任务状态,CreateMode as 任务创建模式,CreateTime as 创建时间 from ControlTask ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetCtrlModelList(string taskName,string taskStatus,string storeHouseName,string createMode,string taskType)
        {
            int flag1 = 0;
            int flag2 = 0;
            int flag3 = 0;
            int flag4 = 0;
            int flag5 = 0;
            if (taskName != "所有")
            {
                flag1 = 1;
            }
            if (taskStatus != "所有")
            { 
                flag2 = 1;
            }
            if (storeHouseName != "所有")
            {
                flag3 = 1;
            }
            if (createMode != "所有")
            {
                flag4 = 1;
            }
            if (taskType != "所有")
            {
                flag5 = 1;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  TaskID as 管理任务索引,TaskTypeName as 任务流程名称,ControlTaskID as 控制任务ID,"
            + "TaskParameter as 任务参数,TaskType as 任务类型 ,ControlCode as 控制条码 ,StartArea as 开始区域,StartDevice as 开始设备"
            + ",CreateMode as 任务创建模式,TargetArea as 目标区域,TargetDevice as 目标设备,TaskStatus as 任务状态,CreateTime as 创建时间  ");
            strSql.Append("From (select ControlTaskID,TaskID,TaskTypeName,TaskTypeCode,TaskType,ControlCode,StartArea,StartDevice,TargetArea,TargetDevice,TaskParameter,TaskStatus,CreateMode,CreateTime,TaskPhase  ");
            strSql.Append(", case when TaskTypeName ='" + taskName + "' then 1 else 0 end as flag1");
            strSql.Append(", case when TaskStatus = '" + taskStatus + "' then 1 else 0 end as flag2");

            if (storeHouseName == EnumStoreHouse.A1库房.ToString())
            {
                strSql.Append(", case when TaskTypeName = '电芯出库_A1' or TaskTypeName ='电芯入库_A1' or  TaskTypeName ='分容出库_A1' or  TaskTypeName ='分容入库_A1' then 1 else 0 end as flag3");
            }
            else if (storeHouseName == EnumStoreHouse.B1库房.ToString())
            {
                strSql.Append(", case when TaskTypeName = '电芯出库_B1' or TaskTypeName ='电芯入库_B1' or  TaskTypeName ='空料框出库' or  TaskTypeName ='空料框入库' then 1 else 0 end as flag3");
            }
           
            strSql.Append(", case when CreateMode = '" + createMode + "' then 1 else 0 end as flag4");
            strSql.Append(", case when TaskType = '" + taskType + "' then 1 else 0 end as flag5");

            strSql.Append(" FROM ControlTask ) as temp");
            if (storeHouseName == "所有")
            {
                strSql.Append(" where  temp.flag1 = " + flag1 + " and temp.flag2 = " + flag2  + " and temp.flag4 = " + flag4 + " and temp.flag5 = " + flag5);
            }
            else
            {
                strSql.Append(" where  temp.flag1 = " + flag1 + " and temp.flag2 = " + flag2 + " and temp.flag3 = "
                    + flag3 + " and temp.flag4 = " + flag4 + " and temp.flag5 = " + flag5);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
		#endregion  ExtensionMethod
        #region 控制层接口，zwx
		 public bool Clear()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ControlTask");

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
        /// 在给定任务类型列表里，取第一个待执行的任务
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
         public ControlTaskModel GetFirstTaskToRun(string taskType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.* from ControlTask a");
            strSql.Append(" inner join TaskType b on a.TaskTypeCode = b.TaskTypeCode ");
            strSql.Append(" where b.TaskTypeName='" + taskType + "'" + "and a.TaskStatus='" + EnumTaskStatus.超时.ToString() + "'");
            strSql.Append(" order by ControlTaskID ASC ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToModel(ds.Tables[0].Rows[0]);
                }
            }

            strSql = new StringBuilder();
            strSql.Append("select a.* from ControlTask a");
            strSql.Append(" inner join TaskType b on a.TaskTypeCode = b.TaskTypeCode ");
            strSql.Append(" where b.TaskTypeName='"+taskType+"'"+ "and a.TaskStatus='"+EnumTaskStatus.待执行.ToString()+"'");
            strSql.Append(" order by ControlTaskID ASC ");
            ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToModel(ds.Tables[0].Rows[0]);
                }
            }
            return null;
        }

        /// <summary>
        /// 根据任务类型，获取正在执行的控制任务，只返回符合条件的第一个
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public ControlTaskModel GetRunningTask(string taskType)
        {
            //先判断是否有超时任务，若有则先返回超时任务
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.* from ControlTask a");
            strSql.Append(" inner join TaskType b on a.TaskTypeCode = b.TaskTypeCode ");
            strSql.Append(" where b.TaskTypeName='" + taskType + "'" + "and a.TaskStatus='" + EnumTaskStatus.超时.ToString() + "'");
            strSql.Append(" order by ControlTaskID ASC ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToModel(ds.Tables[0].Rows[0]);
                }
            }

            //再判断是否有正常执行中的任务
            strSql.Clear();
            strSql.Append("select a.* from ControlTask a");
            strSql.Append(" inner join TaskType b on a.TaskTypeCode = b.TaskTypeCode ");
            strSql.Append(" where b.TaskTypeName='" + taskType + "'" + "and a.TaskStatus='" + EnumTaskStatus.执行中.ToString() + "'");
            strSql.Append(" order by ControlTaskID ASC ");
            ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToModel(ds.Tables[0].Rows[0]);
                }
            }
            return null;
        }

        public IList<ControlTaskModel> GetConditionedModel(string strWhere)
        {
            IList<ControlTaskModel> modelList = new List<ControlTaskModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ControlTask ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ControlTaskModel m = DataRowToModel(dr);
                    modelList.Add(m);
                }

            }

            return modelList;
        }
        #endregion
    }
}

