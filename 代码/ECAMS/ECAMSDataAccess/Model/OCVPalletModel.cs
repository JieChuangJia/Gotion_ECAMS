using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 料框状态，和OCV检测系统通信的中间数据表
    /// </summary>
    [Serializable]
    public partial class OCVPalletModel
    {
        public OCVPalletModel()
        { }
        #region Model
        private string _palletid;
        private string _processstatus;
        private DateTime _loadintime;
        private string _batchid;
        /// <summary>
        /// 
        /// </summary>
        public string palletID
        {
            set { _palletid = value; }
            get { return _palletid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string processStatus
        {
            set { _processstatus = value; }
            get { return _processstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime loadInTime
        {
            set { _loadintime = value; }
            get { return _loadintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string batchID
        {
            set { _batchid = value; }
            get { return _batchid; }
        }
        #endregion Model


    }
}
