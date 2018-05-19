using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// TB_Tray_index:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_Tray_indexModel
    {
        public TB_Tray_indexModel()
        { }
        #region Model
        private string _tf_trayid;
        private string _tf_batchid;
        private int? _tf_batchtype;
        private int? _tf_cellcount;
        private DateTime _tf_checkintime;
        private int _tf_traystat = 1;
        /// <summary>
        /// 
        /// </summary>
        public string Tf_TrayId
        {
            set { _tf_trayid = value; }
            get { return _tf_trayid; }
        }
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
        public int? Tf_CellCount
        {
            set { _tf_cellcount = value; }
            get { return _tf_cellcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime tf_CheckInTime
        {
            set { _tf_checkintime = value; }
            get { return _tf_checkintime; }
        }
        /// <summary>
        /// 使用状态，0：注销，1：使用中
        /// </summary>
        public int tf_traystat
        {
            set { _tf_traystat = value; }
            get { return _tf_traystat; }
        }
        #endregion Model

    }
}

