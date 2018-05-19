using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// View_StockListDetail:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class View_StockListDetailModel
    {
        public View_StockListDetailModel()
        { }
        #region Model
        private long _stocklistid;
        private long _manataskid;
        private string _storehousename;
        private string _productcode;
        private int _productnum;
        private string _productstatus;
        private string _productframecode;
        private string _productname;
        private string _goodssitename;
        private string _productbatchnum;
        private DateTime _inhousetime;
        private DateTime? _updatetime;
        private string _remarks;
        private long _stockdetailid;
        private string _corecode;
        private int? _corequalitysign;
        private int? _corepositionid;
        private string _trayid;
        private string _goodssitetype;
        private int _goodssitelayer;
        private int _goodssitecolumn;
        private int _goodssiterow;
        private string _deviceid;
        private string _goodssitestorestatus;
        private string _goodssiterunstatus;
        private string _goodssiteinouttype;
        private string _goodssitestoretype;
        private int _logicstoreareaid;
        private int _storeareaid;
        private int _goodssiteid;
        private long _stockid;
        /// <summary>
        /// 
        /// </summary>
        public long StockListID
        {
            set { _stocklistid = value; }
            get { return _stocklistid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ManaTaskID
        {
            set { _manataskid = value; }
            get { return _manataskid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StoreHouseName
        {
            set { _storehousename = value; }
            get { return _storehousename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProductNum
        {
            set { _productnum = value; }
            get { return _productnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStatus
        {
            set { _productstatus = value; }
            get { return _productstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductFrameCode
        {
            set { _productframecode = value; }
            get { return _productframecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsSiteName
        {
            set { _goodssitename = value; }
            get { return _goodssitename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductBatchNum
        {
            set { _productbatchnum = value; }
            get { return _productbatchnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime InHouseTime
        {
            set { _inhousetime = value; }
            get { return _inhousetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long StockDetailID
        {
            set { _stockdetailid = value; }
            get { return _stockdetailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CoreCode
        {
            set { _corecode = value; }
            get { return _corecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CoreQualitySign
        {
            set { _corequalitysign = value; }
            get { return _corequalitysign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CorePositionID
        {
            set { _corepositionid = value; }
            get { return _corepositionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TrayID
        {
            set { _trayid = value; }
            get { return _trayid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsSiteType
        {
            set { _goodssitetype = value; }
            get { return _goodssitetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GoodsSiteLayer
        {
            set { _goodssitelayer = value; }
            get { return _goodssitelayer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GoodsSiteColumn
        {
            set { _goodssitecolumn = value; }
            get { return _goodssitecolumn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GoodsSiteRow
        {
            set { _goodssiterow = value; }
            get { return _goodssiterow; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceID
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsSiteStoreStatus
        {
            set { _goodssitestorestatus = value; }
            get { return _goodssitestorestatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsSiteRunStatus
        {
            set { _goodssiterunstatus = value; }
            get { return _goodssiterunstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsSiteInOutType
        {
            set { _goodssiteinouttype = value; }
            get { return _goodssiteinouttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodsSiteStoreType
        {
            set { _goodssitestoretype = value; }
            get { return _goodssitestoretype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LogicStoreAreaID
        {
            set { _logicstoreareaid = value; }
            get { return _logicstoreareaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int StoreAreaID
        {
            set { _storeareaid = value; }
            get { return _storeareaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GoodsSiteID
        {
            set { _goodssiteid = value; }
            get { return _goodssiteid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long StockID
        {
            set { _stockid = value; }
            get { return _stockid; }
        }
        #endregion Model

    }
}

