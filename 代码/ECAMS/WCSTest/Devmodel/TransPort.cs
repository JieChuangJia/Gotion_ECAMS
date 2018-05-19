using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;

namespace WCSTest
{
    public enum EnumTransPortStatus
    {
        EMPTY,
        FULL,
        ERROR
    }
    /// <summary>
    /// 站台
    /// </summary>
    public class TransPort : DevBase
    {
        #region 私有数据
        /// <summary>
        /// 是否允许申请新的任务
        /// </summary>
        private bool enableRequireNewTask = true;

        /// <summary>
        /// 站台状态
        /// </summary>
        private EnumTransPortStatus portStatus = EnumTransPortStatus.EMPTY;
        public EnumTransPortStatus PortStatus
        {
            get
            {
                return portStatus;
            }
            private set { }
        }
        #endregion
        public TransPort(ECAMSDataAccess.DeviceModel devModel, IPlcRW plcRW,DeviceBll devBll)
            : base(devModel, plcRW, devBll)
        {

        }
        #region 重写虚函数
        
        #endregion
        #region 私有功能函数

        #endregion
    }
}
