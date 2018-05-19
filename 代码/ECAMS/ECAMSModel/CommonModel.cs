using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ECAMSModel
{
    public class LogEventArgs : EventArgs
    {

        public EnumLogCategory LogCate { get; set; }
        public EnumLogType LogType { get; set; }
        public string LogContent { get; set; }
        public DateTime LogTime { get; set; }
       
    }
    /// <summary>
    /// 通信设备状态事件参数
    /// </summary>
    public class CommDeviceEventArgs : EventArgs
    {
        public DataTable DtStatus { get; set; }
    }

    /// <summary>
    /// 通信设备状态事件参数
    /// </summary>
    public class DeviceStatusEventArgs : EventArgs
    {
        public DataTable DtStatus { get; set; }
    }


    public class DeviceMonitorEventArgs : EventArgs
    {
        public DataTable DtTask { get; set; }
        public DataTable DtDB1 { get; set; }
        public DataTable DtDB2 { get; set; }
    }

    /// <summary>
    /// 错误事件参数
    /// </summary>
    public class ECAMSErrorEventArgs : LogEventArgs
    {
        /// <summary>
        /// 是否需要停止自动运行
        /// </summary>
        public bool RequireRunningStop { get; set; }
        public int ErrorCode { get; set; }
        //public string ErrorTitle { get; set; }
        //public string ErrorDescribe { get; set; }
        //public DateTime ErrorTime { get; set; }
    }
}
