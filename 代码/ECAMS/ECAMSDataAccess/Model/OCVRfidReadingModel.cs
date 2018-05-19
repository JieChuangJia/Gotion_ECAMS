using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// OCVRfidReadingModel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OCVRfidReadingModel
    {
        public OCVRfidReadingModel()
        { }
        #region Model
        private int _readerid;
        private string _rfidvalue;
        private bool _readrequire = false;
        private bool _readcomplete = false;
        /// <summary>
        /// 读卡器ID,从1开始编号，两个ocv检测机分别对应2和4
        /// </summary>
        public int readerID
        {
            set { _readerid = value; }
            get { return _readerid; }
        }
        /// <summary>
        /// 读卡结果
        /// </summary>
        public string rfidValue
        {
            set { _rfidvalue = value; }
            get { return _rfidvalue; }
        }
        /// <summary>
        /// 读卡请求，由OCV检测系统置位和复位
        /// </summary>
        public bool readRequire
        {
            set { _readrequire = value; }
            get { return _readrequire; }
        }
        /// <summary>
        /// 读卡完成，由调度系统置位和复位
        /// </summary>
        public bool readComplete
        {
            set { _readcomplete = value; }
            get { return _readcomplete; }
        }
        #endregion Model

    }
}

