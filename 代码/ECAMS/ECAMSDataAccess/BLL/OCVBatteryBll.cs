using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using ECAMSDataAccess;
using ECAMSModel;

namespace ECAMSDataAccess
{
    
    /// 电芯数据
    /// </summary>
    public partial class OCVBatteryBll
    {
        private readonly OCVBatteryDal dal = new OCVBatteryDal();
        public OCVBatteryBll()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string batteryID, string palletID)
        {
            return dal.Exists(batteryID, palletID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(OCVBatteryModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(OCVBatteryModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string batteryID, string palletID)
        {

            return dal.Delete(batteryID, palletID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OCVBatteryModel GetModel(string batteryID, string palletID)
        {

            return dal.GetModel(batteryID, palletID);
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
        public List<OCVBatteryModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<OCVBatteryModel> DataTableToList(DataTable dt)
        {
            List<OCVBatteryModel> modelList = new List<OCVBatteryModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                OCVBatteryModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new OCVBatteryModel();
                    if (dt.Rows[n]["batteryID"] != null && dt.Rows[n]["batteryID"].ToString() != "")
                    {
                        model.batteryID = dt.Rows[n]["batteryID"].ToString();
                    }
                    if (dt.Rows[n]["rowIndex"] != null && dt.Rows[n]["rowIndex"].ToString() != "")
                    {
                        model.rowIndex = int.Parse(dt.Rows[n]["rowIndex"].ToString());
                    }
                    if (dt.Rows[n]["columnIndex"] != null && dt.Rows[n]["columnIndex"].ToString() != "")
                    {
                        model.columnIndex = int.Parse(dt.Rows[n]["columnIndex"].ToString());
                    }
                    if (dt.Rows[n]["positionCode"] != null && dt.Rows[n]["positionCode"].ToString() != "")
                    {
                        model.positionCode = int.Parse(dt.Rows[n]["positionCode"].ToString());
                    }
                    if (dt.Rows[n]["checkResult"] != null && dt.Rows[n]["checkResult"].ToString() != "")
                    {
                        model.checkResult = dt.Rows[n]["checkResult"].ToString();
                    }
                    if (dt.Rows[n]["palletID"] != null && dt.Rows[n]["palletID"].ToString() != "")
                    {
                        model.palletID = dt.Rows[n]["palletID"].ToString();
                    }
                    if (dt.Rows[n]["hasBattery"] != null && dt.Rows[n]["hasBattery"].ToString() != "")
                    {
                        if ((dt.Rows[n]["hasBattery"].ToString() == "1") || (dt.Rows[n]["hasBattery"].ToString().ToLower() == "true"))
                        {
                            model.hasBattery = true;
                        }
                        else
                        {
                            model.hasBattery = false;
                        }
                    }
                    modelList.Add(model);
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
        #endregion  Method
        #region 扩展方法
        public bool ClearBattery()
        {
            return dal.ClearBattery();
        }
        #endregion
    }
}
