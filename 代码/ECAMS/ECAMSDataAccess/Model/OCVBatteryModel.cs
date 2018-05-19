using System;


namespace ECAMSDataAccess
{
    /// <summary>
    /// 电芯数据，包括条码，在料框内的位置，状态
    /// </summary>
    [Serializable]
    public partial class OCVBatteryModel
    {
        public OCVBatteryModel()
        { }
        #region Model
        private string _batteryid;
        private int? _rowindex;
        private int? _columnindex;
        private int _positioncode;
        private string _checkresult;
        private string _palletid;
        private bool _hasbattery;
        /// <summary>
        /// 
        /// </summary>
        public string batteryID
        {
            set { _batteryid = value; }
            get { return _batteryid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? rowIndex
        {
            set { _rowindex = value; }
            get { return _rowindex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? columnIndex
        {
            set { _columnindex = value; }
            get { return _columnindex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int positionCode
        {
            set { _positioncode = value; }
            get { return _positioncode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string checkResult
        {
            set { _checkresult = value; }
            get { return _checkresult; }
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
        /// 
        /// </summary>
        public bool hasBattery
        {
            set { _hasbattery = value; }
            get { return _hasbattery; }
        }
        #endregion Model

    }
}
