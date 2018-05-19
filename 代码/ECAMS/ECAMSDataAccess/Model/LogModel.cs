using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 日志表
    /// </summary>
    [Serializable]
    public partial class LogModel
    {
        public LogModel()
        { }
        #region Model
        private long _logid;
        private string _logcontent;
        private DateTime _logtime;
        private string _logcategory;
        private string _logtype;
        private int? _warningcode;
        /// <summary>
        /// 
        /// </summary>
        public long logID
        {
            set { _logid = value; }
            get { return _logid; }
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string logContent
        {
            set { _logcontent = value; }
            get { return _logcontent; }
        }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime logTime
        {
            set { _logtime = value; }
            get { return _logtime; }
        }
        /// <summary>
        /// 类型，如控制层、管理层
        /// </summary>
        public string logCategory
        {
            set { _logcategory = value; }
            get { return _logcategory; }
        }
        /// <summary>
        /// 日志类型，每个类别分多个类型
        /// </summary>
        public string logType
        {
            set { _logtype = value; }
            get { return _logtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? warningCode
        {
            set { _warningcode = value; }
            get { return _warningcode; }
        }
        #endregion Model


    }
}

