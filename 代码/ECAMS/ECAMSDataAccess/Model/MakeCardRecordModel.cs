using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECAMSDataAccess
{
    public class MakeCardRecordModel
    {
        #region Model
        private string _cardid;
        private DateTime? _makedtime;
        private long _serialno;
        private string _reserve1;
        private string _reserve2;
        /// <summary>
        /// 发卡ID号
        /// </summary>
        public string cardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }
        /// <summary>
        /// 发卡时间
        /// </summary>
        public DateTime? makedTime
        {
            set { _makedtime = value; }
            get { return _makedtime; }
        }
        /// <summary>
        /// 流水号
        /// </summary>
        public long serialNo
        {
            set { _serialno = value; }
            get { return _serialno; }
        }
        /// <summary>
        /// 备用1
        /// </summary>
        public string reserve1
        {
            set { _reserve1 = value; }
            get { return _reserve1; }
        }
        /// <summary>
        /// 备用2
        /// </summary>
        public string reserve2
        {
            set { _reserve2 = value; }
            get { return _reserve2; }
        }
        #endregion Model
    }
}
