using System;
using System.Data;
using System.Collections.Generic;
using ECAMSDataAccess;
namespace ECAMSDataAccess
{
    /// <summary>
    /// PlcDBSim
    /// </summary>
    public partial class PlcDBSimBll
    {
        private readonly PlcDBSimDal dal = new ECAMSDataAccess.PlcDBSimDal();
        public PlcDBSimBll()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string PlcAddr)
        {
            return dal.Exists(PlcAddr);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(PlcDBSimModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ECAMSDataAccess.PlcDBSimModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string PlcAddr)
        {

            return dal.Delete(PlcAddr);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PlcAddrlist)
        {
            return dal.DeleteList(PlcAddrlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ECAMSDataAccess.PlcDBSimModel GetModel(string PlcAddr)
        {

            return dal.GetModel(PlcAddr);
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
        public List<ECAMSDataAccess.PlcDBSimModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ECAMSDataAccess.PlcDBSimModel> DataTableToList(DataTable dt)
        {
            List<ECAMSDataAccess.PlcDBSimModel> modelList = new List<ECAMSDataAccess.PlcDBSimModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ECAMSDataAccess.PlcDBSimModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ECAMSDataAccess.PlcDBSimModel();
                    if (dt.Rows[n]["PlcAddr"] != null && dt.Rows[n]["PlcAddr"].ToString() != "")
                    {
                        model.PlcAddr = dt.Rows[n]["PlcAddr"].ToString();
                    }
                    if (dt.Rows[n]["Val"] != null && dt.Rows[n]["Val"].ToString() != "")
                    {
                        model.Val = int.Parse(dt.Rows[n]["Val"].ToString());
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
    }
}

