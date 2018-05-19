using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 用户权限设置
    /// </summary>
    [Serializable]
    public partial class User_LimitModel
    {
        public User_LimitModel()
        { }
        #region Model
        private int _userlimitid;
        private int _roleid;
        private int _functionid;
        /// <summary>
        /// 用户权限设置ID
        /// </summary>
        public int UserLimitID
        {
            set { _userlimitid = value; }
            get { return _userlimitid; }
        }
        /// <summary>
        /// 用户角色ID
        /// </summary>
        public int RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 功能ID
        /// </summary>
        public int FunctionID
        {
            set { _functionid = value; }
            get { return _functionid; }
        }
        #endregion Model

    }
}

