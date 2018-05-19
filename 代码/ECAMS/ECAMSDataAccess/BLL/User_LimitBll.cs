using System;
using System.Data;
using System.Collections.Generic;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 用户权限设置
    /// </summary>
    public partial class User_LimitBll
    {
        private readonly User_LimitDal dal = new User_LimitDal();
        public User_LimitBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserLimitID)
        {
            return dal.Exists(UserLimitID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(User_LimitModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(User_LimitModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int UserLimitID)
        {

            return dal.Delete(UserLimitID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string UserLimitIDlist)
        {
            return dal.DeleteList(UserLimitIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public User_LimitModel GetModel(int UserLimitID)
        {

            return dal.GetModel(UserLimitID);
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
        public List<User_LimitModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<User_LimitModel> DataTableToList(DataTable dt)
        {
            List<User_LimitModel> modelList = new List<User_LimitModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                User_LimitModel model;
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
        public List<int> GetFunctionIDList(int roleID)
        {
            List<int> funcList = new List<int>();
            List<User_LimitModel> limitList = GetModelList("RoleID =" + roleID);
            
            for (int i = 0; i < limitList.Count; i++)
            {
                funcList.Add(limitList[i].FunctionID);
            }
            return funcList;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByRoleID(int roleID)
        {
            return dal.DeleteByRoleID(roleID);
        }
        #endregion  ExtensionMethod
    }
}

