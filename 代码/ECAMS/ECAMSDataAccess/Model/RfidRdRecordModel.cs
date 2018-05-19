using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// RfidRdRecordModel:读卡记录
    /// </summary>
    [Serializable]
    public partial class RfidRdRecordModel
    {
        public RfidRdRecordModel()
        { }
        #region Model
        private int _rfidreaderid;
        private string _readingcontent;
        private DateTime _readingtime;
        private string _readername;
        private long _readingserialno;
        /// <summary>
        /// 
        /// </summary>
        public int rfidReaderID
        {
            set { _rfidreaderid = value; }
            get { return _rfidreaderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string readingContent
        {
            set { _readingcontent = value; }
            get { return _readingcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime readingTime
        {
            set { _readingtime = value; }
            get { return _readingtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string readerName
        {
            set { _readername = value; }
            get { return _readername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long readingSerialNo
        {
            set { _readingserialno = value; }
            get { return _readingserialno; }
        }
        #endregion Model

    }
}

