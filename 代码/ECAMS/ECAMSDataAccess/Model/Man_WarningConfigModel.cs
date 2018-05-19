using System;
namespace ECAMSDataAccess
{
    /// <summary>
    /// 报警（错误）码配置
    /// </summary>
    [Serializable]
    public partial class Man_WarnnigConfigModel
    {
        public Man_WarnnigConfigModel()
        { }
        #region Model
        private int _warningcode;
        private string _warninglayer;
        private string _warningcata;
        private string _warningexplain;
        private string _warningname;
        /// <summary>
        /// 
        /// </summary>
        public int WarningCode
        {
            set { _warningcode = value; }
            get { return _warningcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WarningLayer
        {
            set { _warninglayer = value; }
            get { return _warninglayer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WarningCata
        {
            set { _warningcata = value; }
            get { return _warningcata; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WarningExplain
        {
            set { _warningexplain = value; }
            get { return _warningexplain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WarningName
        {
            set { _warningname = value; }
            get { return _warningname; }
        }
        #endregion Model

    }
}

