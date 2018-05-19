using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECAMSDataAccess
{
    /// <summary>
    /// 生产过程历史追踪
    /// </summary>
    [Serializable]
    public partial class PalletHistoryRecordModel
    {
        public PalletHistoryRecordModel()
        { }
        #region Model
        private long _serialno;
        private string _palletid;
        private DateTime _hiseventtime;
        private string _processstatus;
        private string _hiseventdetail;
        private string _currentuser;
        /// <summary>
        /// 
        /// </summary>
        public long serialNo
        {
            set { _serialno = value; }
            get { return _serialno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string palletID
        {
            set { _palletid = value; }
            get { return _palletid; }
        }
        /// <summary>
        /// 历史事件发生的时间
        /// </summary>
        public DateTime hisEventTime
        {
            set { _hiseventtime = value; }
            get { return _hiseventtime; }
        }
        /// <summary>
        /// 当前所处的工艺段
        /// </summary>
        public string processStatus
        {
            set { _processstatus = value; }
            get { return _processstatus; }
        }
        /// <summary>
        /// 事件描述
        /// </summary>
        public string hisEventDetail
        {
            set { _hiseventdetail = value; }
            get { return _hiseventdetail; }
        }
        /// <summary>
        /// 当前用户
        /// </summary>
        public string currentUser
        {
            set { _currentuser = value; }
            get { return _currentuser; }
        }
        #endregion Model

    }
}
