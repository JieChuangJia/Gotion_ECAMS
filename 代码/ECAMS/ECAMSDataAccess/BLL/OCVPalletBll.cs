using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using ECAMSDataAccess;
using ECAMSModel;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 料框状态
    /// </summary>
    public partial class OCVPalletBll
    {
        private readonly OCVPalletDal dal = new OCVPalletDal();
        private readonly OCVBatteryBll batteryBll = new OCVBatteryBll();
        public OCVPalletBll()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string palletID)
        {
            return dal.Exists(palletID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(OCVPalletModel model)
        {

            model.loadInTime = DateTime.Parse(model.loadInTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(OCVPalletModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string palletID)
        {

            return dal.Delete(palletID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string palletIDlist)
        {
            return dal.DeleteList(palletIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OCVPalletModel GetModel(string palletID)
        {

            return dal.GetModel(palletID);
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
        public List<OCVPalletModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<OCVPalletModel> DataTableToList(DataTable dt)
        {
            List<OCVPalletModel> modelList = new List<OCVPalletModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                OCVPalletModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new OCVPalletModel();
                    if (dt.Rows[n]["palletID"] != null && dt.Rows[n]["palletID"].ToString() != "")
                    {
                        model.palletID = dt.Rows[n]["palletID"].ToString();
                    }
                    if (dt.Rows[n]["processStatus"] != null && dt.Rows[n]["processStatus"].ToString() != "")
                    {
                        model.processStatus = dt.Rows[n]["processStatus"].ToString();
                    }
                    if (dt.Rows[n]["loadInTime"] != null && dt.Rows[n]["loadInTime"].ToString() != "")
                    {
                        model.loadInTime = DateTime.Parse(dt.Rows[n]["loadInTime"].ToString());
                    }
                    if (dt.Rows[n]["batchID"] != null && dt.Rows[n]["batchID"].ToString() != "")
                    {
                        model.batchID = dt.Rows[n]["batchID"].ToString();
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
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        #endregion  BasicMethod
        #region Extension Method

        /// <summary>
        /// OCV检测结果
        /// </summary>
        /// <param name="palletRfid">料框RFID</param>
        /// <param name="processStatus">检测过程状态</param>
        /// <returns></returns>
        public byte[] GetOcvCheckResult(string palletRfid, string processStatus)
        {
            byte[] checkResult = new byte[48];
            for (int i = 0; i < 48; i++)
            {
                checkResult[i] = 3;
            }
            //默认料框为空
          
            OCVPalletModel palletModel = dal.GetModel(palletRfid);
            if (palletModel == null || palletModel.processStatus.Trim() != processStatus)
            {
                return null;
            }
            IList<OCVBatteryModel> batteryList = batteryBll.GetModelList(" palletID='" + palletRfid + "'");
            if (batteryList == null)
            {
                return null;
            }
            //if (batteryList != null && batteryList.Count >= 48)
            {
                for(int i =0;i<Math.Min(batteryList.Count,48);i++)
                {
                    OCVBatteryModel battery = batteryList[i];
                    if(battery == null)
                    {
                        continue;
                    }
                    if (battery.positionCode < 1 || battery.positionCode > 48)
                    {
                        continue;
                    }
                    int batteryIndex = battery.positionCode - 1;
                    if (!battery.hasBattery)
                    {
                        checkResult[batteryIndex] = 3;
                    }
                    else
                    {
                        if (battery.checkResult.Trim() == EnumOCVCheckResult.良品.ToString())
                        {
                            checkResult[batteryIndex] = 1;

                        }
                        else
                        {
                            checkResult[batteryIndex] = 2;
                        }
                    }
                   
                  
                }
               
            }
            return checkResult;
        }

        public int GetBatteryCountAfterOCV3(string palletID)
        {
            OCVPalletModel palletModel = dal.GetModel(palletID);
            if (palletModel == null || palletModel.processStatus.Trim() != EnumOCVProcessStatus.一次OCV检测完成.ToString())
            {
                return -1;
            }
            string strWhere = string.Format("palletID = '{0}' and hasBattery=1 and checkResult='{1}' ", palletID, EnumOCVCheckResult.良品.ToString());
            IList<OCVBatteryModel> batteryList = batteryBll.GetModelList(strWhere);
            if (batteryList == null)
            {
                return -1;
            }
            return batteryList.Count;
        }
        
        public bool ClearPallet()
        {
            return dal.ClearPallet();
        }
        #endregion Extension Method
    }
}