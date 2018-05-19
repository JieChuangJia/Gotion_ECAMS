using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// TB_After_GradeData:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_After_GradeDataModel
    {
        public TB_After_GradeDataModel()
        { }
        #region Model
        private string _tf_batchid;
        private int? _tf_batchtype;
        private string _tf_trayid;
        private int? _tf_channelno;
        private string _tf_cellsn;
        private string _tf_pick;
        private int? _tf_tag;
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
        public string Tf_TrayId
        {
            set { _tf_trayid = value; }
            get { return _tf_trayid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Tf_ChannelNo
        {
            set { _tf_channelno = value; }
            get { return _tf_channelno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tf_CellSn
        {
            set { _tf_cellsn = value; }
            get { return _tf_cellsn; }
        }

        public string Tf_Pick
        {
            get { return _tf_pick; }
            set { _tf_pick = value; }
        }

        public int? Tf_Tag
        {
            get { return _tf_tag; }
            set { _tf_tag = value; }
        }
        #endregion Model

    }
}

