/**  版本信息模板在安装目录下，可自行修改。
* Device.cs
*
* 功 能： N/A
* 类 名： Device
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:49:59   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：np　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ECAMSDataAccess
{
	/// <summary>
	/// 设备表
	/// </summary>
	[Serializable]
	public partial class DeviceModel
	{
		public DeviceModel()
		{}
		#region Model
        private string _deviceid;
        private int? _tasktypecode;
        private string _devicetype;
        private string _devicedescribe;
        private string _db1addrstart;
        private int _byteslendb1;
        private string _db2addrstart;
        private int _byteslendb2;
        private string _devicestatus;
        private string _devstatusdescribe;
		/// <summary>
		/// 
		/// </summary>
		public string DeviceID
		{
			set{ _deviceid=value;}
			get{return _deviceid;}
		}
		/// <summary>
		/// 业务类型，比如料框入库，出库分容，空料框出库等
		/// </summary>
		public int? TaskTypeCode
		{
			set{ _tasktypecode=value;}
			get{return _tasktypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeviceType
		{
			set{ _devicetype=value;}
			get{return _devicetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeviceDescribe
		{
			set{ _devicedescribe=value;}
			get{return _devicedescribe;}
		}
        /// <summary>
        /// PLC通信DB1起始地址
        /// </summary>
        public string DB1AddrStart
        {
            set { _db1addrstart = value; }
            get { return _db1addrstart; }
        }
		/// <summary>
		/// DB1数据区字节长度
		/// </summary>
		public int BytesLenDB1
		{
			set{ _byteslendb1=value;}
			get{return _byteslendb1;}
		}
        /// <summary>
        /// PLC通信DB2起始地址
        /// </summary>
        public string DB2AddrStart
        {
            set { _db2addrstart = value; }
            get { return _db2addrstart; }
        }
		/// <summary>
		/// DB2数据区字节长度
		/// </summary>
		public int BytesLenDB2
		{
			set{ _byteslendb2=value;}
			get{return _byteslendb2;}
		}
		/// <summary>
		/// 设备状态，空闲，忙碌，故障
		/// </summary>
		public string DeviceStatus
		{
			set{ _devicestatus=value;}
			get{return _devicestatus;}
		}
		/// <summary>
		/// 设备状态信息详细描述
		/// </summary>
		public string DevStatusDescribe
		{
			set{ _devstatusdescribe=value;}
			get{return _devstatusdescribe;}
		}
		#endregion Model

	}
}

