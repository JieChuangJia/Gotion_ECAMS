using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECAMSDataAccess
{

    /// <summary>
    /// PlcDBSim:plc db数据区模拟
    /// </summary>
    [Serializable]
    public partial class PlcDBSimModel
    {
        public PlcDBSimModel()
        { }
        #region Model
        private string _plcaddr;
        private int _val;
        /// <summary>
        /// 
        /// </summary>
        public string PlcAddr
        {
            set { _plcaddr = value; }
            get { return _plcaddr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Val
        {
            set { _val = value; }
            get { return _val; }
        }
        #endregion Model

    }
    
}
