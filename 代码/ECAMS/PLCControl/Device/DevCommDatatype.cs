/***
 * 文件名:DevCommDatatype.cs
 * 功能：设备通信功能的类型定义
 * 作者：          张文香
 * 创建日期：      2014-03-10
 * 版权所有：沈阳新松机器人自动化股份有限公司 2014
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECAMSModel;
namespace PLCControl
{
    /// <summary>
    /// 通信实现方式枚举
    /// </summary>
    public enum EnumCommMethod
    {
        PLC_OPC_COMMU = 1, //plc OPC通信
        PLC_MIT_COMMU, //plc 三菱控件通信
    }
    /// <summary>
    /// 通信功能数据类型定义
    /// </summary>
    public class DevCommDatatype
    {
        /// <summary>
        /// 功能ID
        /// </summary>
        public int CommuID { get; set; }
       
        /// <summary>
        /// 功能描述
        /// </summary>
        public string DataDescription { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public EnumCommuDataType DataTypeDef { get; set; }

        /// <summary>
        /// 数据字节数
        /// </summary>
        public int DataByteLen { get; set; }

        /// <summary>
        /// 通信地址描述
        /// </summary>
        public string DataAddr { get; set; }

        /// <summary>
        /// 通信数据值
        /// </summary>
        public object Val { get; set; }

        /// <summary>
        /// 通信实现方式
        /// </summary>
        public EnumCommMethod CommuMethod { get; set; }
    }
}
