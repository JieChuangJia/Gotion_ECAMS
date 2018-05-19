/**  版本信息模板在安装目录下，可自行修改。
* ControlInterface.cs
*
* 功 能： N/A
* 类 名： ControlInterface
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:49:58   N/A    初版
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
	/// ControlInterface:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ControlInterfaceModel
	{
		public ControlInterfaceModel()
		{}
        #region Model
        private long _controlinterfaceid;
        private string _interfacetype;
        private string _devicecode;
        private string _taskcode;
        private string _interfacestatus;
        private DateTime _createtime;
        private string _interfaceparameter;
        private string _remarks;
        /// <summary>
        /// 控制接口ID
        /// </summary>
        public long ControlInterfaceID
        {
            set { _controlinterfaceid = value; }
            get { return _controlinterfaceid; }
        }
        /// <summary>
        /// 接口类型
        /// </summary>
        public string InterfaceType
        {
            set { _interfacetype = value; }
            get { return _interfacetype; }
        }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceCode
        {
            set { _devicecode = value; }
            get { return _devicecode; }
        }
        /// <summary>
        /// 任务编码
        /// </summary>
        public string TaskCode
        {
            set { _taskcode = value; }
            get { return _taskcode; }
        }
        /// <summary>
        /// 接口状态
        /// </summary>
        public string InterfaceStatus
        {
            set { _interfacestatus = value; }
            get { return _interfacestatus; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 接口参数
        /// </summary>
        public string InterfaceParameter
        {
            set { _interfaceparameter = value; }
            get { return _interfaceparameter; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model

	}
}

