using System;
using System.Data;
using System.Collections.Generic;

namespace ECAMSDataAccess
{
    /// <summary>
    /// OCVRfidReading
    /// </summary>
    public partial class OCVRfidReadingBll
    {
        private readonly OCVRfidReadingDal dal = new OCVRfidReadingDal();
        public OCVRfidReadingBll()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int readerID)
        {
            return dal.Exists(readerID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(OCVRfidReadingModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(OCVRfidReadingModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int readerID)
        {

            return dal.Delete(readerID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string readerIDlist)
        {
            return dal.DeleteList(readerIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OCVRfidReadingModel GetModel(int readerID)
        {

            return dal.GetModel(readerID);
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
        public List<OCVRfidReadingModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<OCVRfidReadingModel> DataTableToList(DataTable dt)
        {
            List<OCVRfidReadingModel> modelList = new List<OCVRfidReadingModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                OCVRfidReadingModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new OCVRfidReadingModel();
                    if (dt.Rows[n]["readerID"] != null && dt.Rows[n]["readerID"].ToString() != "")
                    {
                        model.readerID = int.Parse(dt.Rows[n]["readerID"].ToString());
                    }
                    if (dt.Rows[n]["rfidValue"] != null && dt.Rows[n]["rfidValue"].ToString() != "")
                    {
                        model.rfidValue = dt.Rows[n]["rfidValue"].ToString();
                    }
                    if (dt.Rows[n]["readRequire"] != null && dt.Rows[n]["readRequire"].ToString() != "")
                    {
                        if ((dt.Rows[n]["readRequire"].ToString() == "1") || (dt.Rows[n]["readRequire"].ToString().ToLower() == "true"))
                        {
                            model.readRequire = true;
                        }
                        else
                        {
                            model.readRequire = false;
                        }
                    }
                    if (dt.Rows[n]["readComplete"] != null && dt.Rows[n]["readComplete"].ToString() != "")
                    {
                        if ((dt.Rows[n]["readComplete"].ToString() == "1") || (dt.Rows[n]["readComplete"].ToString().ToLower() == "true"))
                        {
                            model.readComplete = true;
                        }
                        else
                        {
                            model.readComplete = false;
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
    }
}


