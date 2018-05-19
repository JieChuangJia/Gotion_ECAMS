using System;
using System.Data;
using System.Collections.Generic;

namespace ECAMSDataAccess
{
    /// <summary>
    /// TB_Tray_index
    /// </summary>
    public partial class TB_Tray_indexBll
    {
        private readonly TB_Tray_indexDal dal = new TB_Tray_indexDal();
        public TB_Tray_indexBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录,使用中的托盘
        /// </summary>
        public bool Exists(string Tf_TrayId)
        {
            return dal.Exists(Tf_TrayId,1);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TB_Tray_indexModel model)
        {
            model.tf_CheckInTime = (DateTime)DateTime.Parse(model.tf_CheckInTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(TB_Tray_indexModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Tf_TrayId)
        {

            return dal.Delete(Tf_TrayId,1);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Tf_TrayIdlist)
        {
            return dal.DeleteList(Tf_TrayIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TB_Tray_indexModel GetModel(string Tf_TrayId)
        {

            return dal.GetModel(Tf_TrayId);
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
        public List<TB_Tray_indexModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<TB_Tray_indexModel> DataTableToList(DataTable dt)
        {
            List<TB_Tray_indexModel> modelList = new List<TB_Tray_indexModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                TB_Tray_indexModel model;
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

        #endregion  ExtensionMethod
    }
}

