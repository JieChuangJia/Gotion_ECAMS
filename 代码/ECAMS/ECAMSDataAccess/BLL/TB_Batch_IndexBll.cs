using System;
using System.Data;
using System.Collections.Generic;

namespace ECAMSDataAccess
{
    /// <summary>
    /// TB_Batch_Index
    /// </summary>
    public partial class TB_Batch_IndexBll
    {
        private readonly TB_Batch_IndexDal dal = new TB_Batch_IndexDal();
        public TB_Batch_IndexBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Tf_BatchID)
        {
            return dal.Exists(Tf_BatchID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TB_Batch_IndexModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TB_Batch_IndexModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Tf_BatchID)
        {

            return dal.Delete(Tf_BatchID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Tf_BatchIDlist)
        {
            return dal.DeleteList(Tf_BatchIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TB_Batch_IndexModel GetModel(string Tf_BatchID)
        {

            return dal.GetModel(Tf_BatchID);
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
        public List<TB_Batch_IndexModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TB_Batch_IndexModel> DataTableToList(DataTable dt)
        {
            List<TB_Batch_IndexModel> modelList = new List<TB_Batch_IndexModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TB_Batch_IndexModel model;
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
        /// 作者:np
        /// 时间:2014年5月19日
        /// 内容:获取所有产品批次号
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllBatches()
        {
            string whereStr = "Tf_BatchID is not null";
            DataSet  ds = GetList(whereStr);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion  ExtensionMethod
    }
}

