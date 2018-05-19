using System;
using System.Data;
using System.Collections.Generic;
 
namespace ECAMSDataAccess
{
    /// <summary>
    /// TB_After_GradeData
    /// </summary>
    public partial class TB_After_GradeDataBll
    {
        private readonly TB_After_GradeDataDal dal = new TB_After_GradeDataDal();
        public TB_After_GradeDataBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string batchID, string Tf_TrayId, string Tf_CellSn)
        {
            return dal.Exists(batchID,Tf_TrayId, Tf_CellSn);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TB_After_GradeDataModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TB_After_GradeDataModel model,string oldTrayID)
        {
            return dal.Update(model,oldTrayID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string batchID, string Tf_TrayId, string Tf_CellSn)
        {

            return dal.Delete(batchID,Tf_TrayId, Tf_CellSn);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TB_After_GradeDataModel GetModel(string batchID,string Tf_TrayId, string Tf_CellSn)
        {

            return dal.GetModel(batchID,Tf_TrayId, Tf_CellSn);
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
        public List<TB_After_GradeDataModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TB_After_GradeDataModel> DataTableToList(DataTable dt)
        {
            List<TB_After_GradeDataModel> modelList = new List<TB_After_GradeDataModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TB_After_GradeDataModel model;
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
        /// 时间:2014年6月22日
        /// 内容:查询料框详细
        /// </summary>
        /// <param name="trayCode"></param>
        /// <returns></returns>
        public List<TB_After_GradeDataModel> GetListByTrayCode(string trayCode)
        {
            string whereStr = "Tf_TrayId ='" + trayCode+"'";
            return GetModelList(whereStr);
            
        }
        #endregion  ExtensionMethod
    }
}

