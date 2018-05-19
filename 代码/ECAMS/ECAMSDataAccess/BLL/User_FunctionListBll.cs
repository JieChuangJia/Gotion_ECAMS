using System;
using System.Data;
using System.Collections.Generic;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 系统功能列表
    /// </summary>
    public partial class User_FunctionListBll
    {
        private readonly User_FunctionListDal dal = new User_FunctionListDal();
        public User_FunctionListBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FunctionID)
        {
            return dal.Exists(FunctionID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(User_FunctionListModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(User_FunctionListModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int FunctionID)
        {

            return dal.Delete(FunctionID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string FunctionIDlist)
        {
            return dal.DeleteList(FunctionIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public User_FunctionListModel GetModel(int FunctionID)
        {

            return dal.GetModel(FunctionID);
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
        public List<User_FunctionListModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<User_FunctionListModel> DataTableToList(DataTable dt)
        {
            List<User_FunctionListModel> modelList = new List<User_FunctionListModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                User_FunctionListModel model;
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

