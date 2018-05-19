using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// View_QueryStockList:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class View_QueryStockListModel
    {
        public View_QueryStockListModel()
        { }
        #region Model
        private int _goodssiteid;
        private string _goodssitename;
        private int _goodssiterow;
        private int _goodssitecolumn;
        private int _goodssitelayer;
        private string _goodssitestorestatus;
        private string _goodssiterunstatus;
        private string _goodssiteinouttype;
        private long _stockid;
        private long _stocklistid;
        private string _storehousename;
        private string _productstatus;
        private string _productname;
        private DateTime _inhousetime;
        private DateTime? _updatetime;
        private long _manataskid;
        private string _productcode;
        private int _productnum;
        private string _productbatchnum;
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
        public string GoodsSiteName
        {
            set { _goodssitename = value; }
            get { return _goodssitename; }
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
        public int GoodsSiteColumn
        {
            set { _goodssitecolumn = value; }
            get { return _goodssitecolumn; }
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
        public long StockID
        {
            set { _stockid = value; }
            get { return _stockid; }
        }
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
        public string StoreHouseName
        {
            set { _storehousename = value; }
            get { return _storehousename; }
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
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
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
        public long ManaTaskID
        {
            set { _manataskid = value; }
            get { return _manataskid; }
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
        public string ProductBatchNum
        {
            set { _productbatchnum = value; }
            get { return _productbatchnum; }
        }
        #endregion Model

    }
}

