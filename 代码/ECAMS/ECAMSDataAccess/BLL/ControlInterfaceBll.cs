/**  版本信息模板在安装目录下，可自行修改。
* ControlInterface.cs
*
* 功 能： N/A
* 类 名： ControlInterface
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
using System.Collections.Generic;
using System.Collections;
using ECAMSDataAccess;
using ECAMSModel;
namespace ECAMSDataAccess
{
	/// <summary>
	/// 物料信息表
	/// </summary>
	public partial class ControlInterfaceBll
	{
        
		private readonly ECAMSDataAccess.ControlInterfaceDal dal=new ECAMSDataAccess.ControlInterfaceDal();
		public ControlInterfaceBll()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ControlInterfaceID)
        {
            return dal.Exists(ControlInterfaceID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(ControlInterfaceModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ControlInterfaceModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ControlInterfaceID)
        {

            return dal.Delete(ControlInterfaceID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ControlInterfaceIDlist)
        {
            return dal.DeleteList(ControlInterfaceIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ControlInterfaceModel GetModel(long ControlInterfaceID)
        {

            return dal.GetModel(ControlInterfaceID);
        }

       
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ControlInterfaceModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ControlInterfaceModel> DataTableToList(DataTable dt)
        {
            List<ControlInterfaceModel> modelList = new List<ControlInterfaceModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ControlInterfaceModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

		#region  ExtensionMethod
        /// <summary>
        /// 放到数据库Bll层，可以在任务申请时调用此方法（往任务接口表插入数据时）
        ///限制日期在创建触发器的sql语句中修改
        /// </summary>
        public void CreateLimitTrigger(DateTime limitDate)
        {
            string existTriggerSql = "SELECT * FROM master.sys.server_triggers WHERE parent_class_desc = 'SERVER' AND name = N'safe_hk' ";
            DataSet ds = DbHelperSQL.Query(existTriggerSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string deleteTriggerSql = "drop trigger safe_hk ON all server";
                DbHelperSQL.ExecuteSql(deleteTriggerSql);
            }

            string createTriggerSql = "CREATE TRIGGER safe_hk ON ALL SERVER FOR LOGON AS BEGIN"
            + " IF ORIGINAL_LOGIN()= 'sa' and  DATEDIFF(hour,getdate(),'"+limitDate.ToString("yyyy-MM-dd HH:00:00")+"')<=0 ROLLBACK END";
            DbHelperSQL.ExecuteSql(createTriggerSql);
        }
        /// <summary>
        /// 从控制接口中获取没有生成任务的选项
        /// </summary>
        /// <returns></returns>
        public List<ControlInterfaceModel> GetControlApplyList()
        {
            List<ControlInterfaceModel> controlTasksList = new List<ControlInterfaceModel>();
            string whereStr = "InterfaceStatus ='未生成'";
            controlTasksList = GetModelList(whereStr);
         
            return controlTasksList;
        }

        /// <summary>
        /// 获取更新控制任务接口hash用于事物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Hashtable GetUpdateHash(ControlInterfaceModel model)
        {
            return dal.GetUpdateHash(model);
        }

        public bool DeleteByControlCode(string controlCode)
        {
            return dal.DeleteByControlCode(controlCode);
        }

        /// <summary>
        /// 获取删除控制接口hash通过控制条码
        /// </summary>
        /// <param name="controlCode"></param>
        /// <returns></returns>
        public Hashtable GetDeleteHash(string controlCode)
        {
            return dal.GetDeleteHash(controlCode);
		}
		#endregion  ExtensionMethod
        #region 控制层接口，zwx
		
		 /// <summary>
        /// 得到最大ID
        /// </summary>
        public long GetMaxTaskCode()
        {
            long maxTaskCode = 0;
            long controlTaskID = dal.GetMaxTaskCode();
            if (controlTaskID != 1)
            {
                ControlInterfaceModel interfaceModel = GetModel(controlTaskID);
                if (interfaceModel != null)
                {
                    maxTaskCode = long.Parse(interfaceModel.TaskCode);
                }
            }
            return maxTaskCode;
        }
		
		 /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllListByTime(DateTime dtStart,DateTime dtEnd)
        {
            return GetList("CreateTime between  '" + dtStart.ToString("yyyy-MM-dd 00:00:00") + "' and '"
                + dtEnd.ToString("yyyy-MM-dd 23:59:59") + "'");
        }
        /// <summary>
        /// 获取一个新的任务码流水号
        /// </summary>
        /// <returns></returns>
        public string GetNewTaskCode()
        {
            return dal.GetTaskCodeMax();
        }

        public bool Clear()
        {
            return dal.Clear();
        }


        /// <summary>
        /// 根据控制任务码，检查是否存在重复的
        /// </summary>
        /// <param name="taskCode"></param>
        /// <returns></returns>
        public bool ExistsTask(string taskCode)
        {
            string strWhere = " TaskCode='"+taskCode+"' ";
            IList<ControlInterfaceModel> modelList = dal.GetConditionedModel(strWhere);
            if (modelList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}

