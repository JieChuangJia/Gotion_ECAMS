using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ECAMSDataAccess
{
    /// <summary>
    /// PalletHistoryRecord
    /// </summary>
    public partial class PalletHistoryRecordBll
    {
        private readonly PalletHistoryRecordDal dal = new PalletHistoryRecordDal();
        public PalletHistoryRecordBll()
        { }
        #region  Basic Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long serialNo)
        {
            return dal.Exists(serialNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(PalletHistoryRecordModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PalletHistoryRecordModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long serialNo)
        {

            return dal.Delete(serialNo);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string serialNolist)
        {
            return dal.DeleteList(serialNolist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PalletHistoryRecordModel GetModel(long serialNo)
        {

            return dal.GetModel(serialNo);
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
        public List<PalletHistoryRecordModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PalletHistoryRecordModel> DataTableToList(DataTable dt)
        {
            List<PalletHistoryRecordModel> modelList = new List<PalletHistoryRecordModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PalletHistoryRecordModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new PalletHistoryRecordModel();
                    if (dt.Rows[n]["serialNo"] != null && dt.Rows[n]["serialNo"].ToString() != "")
                    {
                        model.serialNo = long.Parse(dt.Rows[n]["serialNo"].ToString());
                    }
                    if (dt.Rows[n]["palletID"] != null && dt.Rows[n]["palletID"].ToString() != "")
                    {
                        model.palletID = dt.Rows[n]["palletID"].ToString();
                    }
                    if (dt.Rows[n]["hisEventTime"] != null && dt.Rows[n]["hisEventTime"].ToString() != "")
                    {
                        model.hisEventTime = DateTime.Parse(dt.Rows[n]["hisEventTime"].ToString());
                    }
                    if (dt.Rows[n]["processStatus"] != null && dt.Rows[n]["processStatus"].ToString() != "")
                    {
                        model.processStatus = dt.Rows[n]["processStatus"].ToString();
                    }
                    if (dt.Rows[n]["hisEventDetail"] != null && dt.Rows[n]["hisEventDetail"].ToString() != "")
                    {
                        model.hisEventDetail = dt.Rows[n]["hisEventDetail"].ToString();
                    }
                    if (dt.Rows[n]["currentUser"] != null && dt.Rows[n]["currentUser"].ToString() != "")
                    {
                        model.currentUser = dt.Rows[n]["currentUser"].ToString();
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
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
        #region 扩展方法

        /// <summary>
        /// 增加历史事件
        /// </summary>
        /// <param name="palletID"></param>
        /// <param name="processStatus"></param>
        /// <param name="eventDetail"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool AddHistoryEvent(string palletID, string processStatus, string eventDetail, string userName)
        {
            DeleteHistoryLog(60);//两个月
            PalletHistoryRecordModel hisModel = new PalletHistoryRecordModel();
            hisModel.currentUser = userName;
            hisModel.hisEventTime = System.DateTime.Now;
            hisModel.palletID = palletID;
            hisModel.hisEventDetail = eventDetail;
            hisModel.processStatus = processStatus;
            if (dal.Add(hisModel) <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据托盘号查询所有的历史事件
        /// </summary>
        /// <param name="palletID"></param>
        /// <param name="timeAsc">是否按时间升序</param>
        /// <returns></returns>
        public List<PalletHistoryRecordModel> GetEventList(string palletID,bool timeAsc)
        {
            string strWhere = "palletID='" + palletID + "' ";
            return dal.GetList(strWhere, timeAsc);
        }
        /// <summary>
        /// 删除多个月以前的数据
        /// </summary>
        /// <param name="monthes">月数</param>
        /// <returns></returns>
        public bool DeleteHistoryLog(int days)
        {
            return dal.DeleteHistoryLog(days);
        }
        #endregion
    }
}
