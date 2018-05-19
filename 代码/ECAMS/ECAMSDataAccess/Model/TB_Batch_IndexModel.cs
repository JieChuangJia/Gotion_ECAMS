using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// TB_Batch_Index:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_Batch_IndexModel
    {
        public TB_Batch_IndexModel()
        { }
        #region Model
        private string _tf_batchid;
        private int? _tf_batchtype;
        private int? _tf_traycount;
        private int? _tf_cellcount;
        /// <summary>
        /// 
        /// </summary>
        public string Tf_BatchID
        {
            set { _tf_batchid = value; }
            get { return _tf_batchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Tf_Batchtype
        {
            set { _tf_batchtype = value; }
            get { return _tf_batchtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Tf_TrayCount
        {
            set { _tf_traycount = value; }
            get { return _tf_traycount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Tf_CellCount
        {
            set { _tf_cellcount = value; }
            get { return _tf_cellcount; }
        }
        #endregion Model

    }
}

