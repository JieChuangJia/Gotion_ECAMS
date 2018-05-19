using System;
using System.Data;
using System.Collections.Generic;

namespace ECAMSDataAccess
{
    /// <summary>
    /// RfidRdRecord
    /// </summary>
    public partial class RfidRdRecordBll
    {
        private readonly RfidRdRecordDal dal = new RfidRdRecordDal();
        public RfidRdRecordBll()
        { }
        #region Basic Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long readingSerialNo)
        {
            return dal.Exists(readingSerialNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(RfidRdRecordModel model)
        {
            dal.DeleteHistoryLog(30);
            model.readingTime = DateTime.Parse(model.readingTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(RfidRdRecordModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long readingSerialNo)
        {

            return dal.Delete(readingSerialNo);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string readingSerialNolist)
        {
            return dal.DeleteList(readingSerialNolist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RfidRdRecordModel GetModel(long readingSerialNo)
        {

            return dal.GetModel(readingSerialNo);
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
        public List<RfidRdRecordModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<RfidRdRecordModel> DataTableToList(DataTable dt)
        {
            List<RfidRdRecordModel> modelList = new List<RfidRdRecordModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                RfidRdRecordModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new RfidRdRecordModel();
                    if (dt.Rows[n]["rfidReaderID"] != null && dt.Rows[n]["rfidReaderID"].ToString() != "")
                    {
                        model.rfidReaderID = int.Parse(dt.Rows[n]["rfidReaderID"].ToString());
                    }
                    if (dt.Rows[n]["readingContent"] != null && dt.Rows[n]["readingContent"].ToString() != "")
                    {
                        model.readingContent = dt.Rows[n]["readingContent"].ToString();
                    }
                    if (dt.Rows[n]["readingTime"] != null && dt.Rows[n]["readingTime"].ToString() != "")
                    {
                        model.readingTime = DateTime.Parse(dt.Rows[n]["readingTime"].ToString());
                    }
                    if (dt.Rows[n]["readerName"] != null && dt.Rows[n]["readerName"].ToString() != "")
                    {
                        model.readerName = dt.Rows[n]["readerName"].ToString();
                    }
                    if (dt.Rows[n]["readingSerialNo"] != null && dt.Rows[n]["readingSerialNo"].ToString() != "")
                    {
                        model.readingSerialNo = long.Parse(dt.Rows[n]["readingSerialNo"].ToString());
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

        #endregion
    }
}

