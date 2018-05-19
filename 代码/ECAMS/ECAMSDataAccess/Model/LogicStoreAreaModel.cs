/**  版本信息模板在安装目录下，可自行修改。
* LogicStoreArea.cs
*
* 功 能： N/A
* 类 名： LogicStoreArea
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:00   N/A    初版
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
	/// 逻辑区域
	/// </summary>
	[Serializable]
	public partial class LogicStoreAreaModel
	{
		public LogicStoreAreaModel()
		{}
		#region Model
		private int _logicstoreareaid;
		private string _logicstoreareacode;
		private string _logicstoreareaname;
		private string _logicstoreareatype;
		private string _logicstoreareadescribe;
		private int _logicstoreareasign;
		/// <summary>
		/// 逻辑区域ID
		/// </summary>
		public int LogicStoreAreaID
		{
			set{ _logicstoreareaid=value;}
			get{return _logicstoreareaid;}
		}
		/// <summary>
		/// 逻辑区域编码
		/// </summary>
		public string LogicStoreAreaCode
		{
			set{ _logicstoreareacode=value;}
			get{return _logicstoreareacode;}
		}
		/// <summary>
		/// 逻辑区域名称
		/// </summary>
		public string LogicStoreAreaName
		{
			set{ _logicstoreareaname=value;}
			get{return _logicstoreareaname;}
		}
		/// <summary>
		/// 逻辑区域类型
		/// </summary>
		public string LogicStoreAreaType
		{
			set{ _logicstoreareatype=value;}
			get{return _logicstoreareatype;}
		}
		/// <summary>
		/// 逻辑区域描述
		/// </summary>
		public string LogicStoreAreaDescribe
		{
			set{ _logicstoreareadescribe=value;}
			get{return _logicstoreareadescribe;}
		}
		/// <summary>
		/// 逻辑区域标记
		/// </summary>
		public int LogicStoreAreaSign
		{
			set{ _logicstoreareasign=value;}
			get{return _logicstoreareasign;}
		}
		#endregion Model

	}
}

