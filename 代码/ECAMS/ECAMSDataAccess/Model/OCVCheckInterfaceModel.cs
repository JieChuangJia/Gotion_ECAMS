/**  版本信息模板在安装目录下，可自行修改。
* OCVCheckInterface.cs
*
* 功 能： N/A
* 类 名： OCVCheckInterface
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:02   N/A    初版
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
	/// OCV检测
	/// </summary>
	[Serializable]
	public partial class OCVCheckInterfaceModel
	{
		public OCVCheckInterfaceModel()
		{}
		#region Model
		private int _checkinterfaceid;
		private string _productframecode;
		private string _checktype;
		private string _coreposition;
		private int? _hascore;
		private int? _corestatus;
		private DateTime? _reporttime;
		private int? _handlestatus;
		private string _remarks;
		/// <summary>
		/// 检测接口ID
		/// </summary>
		public int CheckInterfaceID
		{
			set{ _checkinterfaceid=value;}
			get{return _checkinterfaceid;}
		}
		/// <summary>
		/// 料框条码
		/// </summary>
		public string ProductFrameCode
		{
			set{ _productframecode=value;}
			get{return _productframecode;}
		}
		/// <summary>
		/// 检测类型
		/// </summary>
		public string CheckType
		{
			set{ _checktype=value;}
			get{return _checktype;}
		}
		/// <summary>
		/// 电芯位置
		/// </summary>
		public string CorePosition
		{
			set{ _coreposition=value;}
			get{return _coreposition;}
		}
		/// <summary>
		/// 是否有电芯标志
		/// </summary>
		public int? HasCore
		{
			set{ _hascore=value;}
			get{return _hascore;}
		}
		/// <summary>
		/// 电芯状态
		/// </summary>
		public int? CoreStatus
		{
			set{ _corestatus=value;}
			get{return _corestatus;}
		}
		/// <summary>
		/// 上报时间
		/// </summary>
		public DateTime? ReportTime
		{
			set{ _reporttime=value;}
			get{return _reporttime;}
		}
		/// <summary>
		/// 处理状态
		/// </summary>
		public int? HandleStatus
		{
			set{ _handlestatus=value;}
			get{return _handlestatus;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		#endregion Model

	}
}

