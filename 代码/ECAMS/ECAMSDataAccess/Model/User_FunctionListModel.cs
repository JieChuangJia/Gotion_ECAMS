using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 系统功能列表
    /// </summary>
    [Serializable]
    public partial class User_FunctionListModel
    {
        public User_FunctionListModel()
        { }
        #region Model
        private int _functionid;
        private string _functionname;
        private string _remarks;
        /// <summary>
        /// 功能ID
        /// </summary>
        public int FunctionID
        {
            set { _functionid = value; }
            get { return _functionid; }
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName
        {
            set { _functionname = value; }
            get { return _functionname; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model

    }
}

