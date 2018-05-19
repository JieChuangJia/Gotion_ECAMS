using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
using ECAMSDataAccess;
namespace PLCControl
{
    /// <summary>
    /// 设备通信相关的接口
    /// </summary>
    public interface IECAMSDevCom
    {
       
          ECAMSDataAccess.DeviceModel DevModel{get;set;}
          byte[] DB1 { get; set; }
          byte[] DB2 { get; set; }
          string[] AddrDB1{get;set;}
          string[] AddrDB2{get;set;}

        /// <summary>
        /// 读设备的DB2数据区
        /// </summary>
        bool ReadDB2();

        
        bool ExeBusiness();

        
        bool DevCmdCommit();

    }
}
