/**  版本信息模板在安装目录下，可自行修改。
* GoodsSite.cs
*
* 功 能： N/A
* 类 名： GoodsSite
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
	/// 货位信息表
	/// </summary>
	[Serializable]
	public partial class GoodsSiteModel
	{
		public GoodsSiteModel()
		{}
		#region Model
		private int _goodssiteid;
		private string _goodssitename;
		private string _goodssitetype;
		private int _goodssitelayer;
		private int _goodssitecolumn;
		private int _goodssiterow;
		private string _deviceid;
		private string _goodssitestorestatus;
		private string _goodssiterunstatus;
		private string _goodssiteinouttype;
		private string _goodssitestoretype;
		private int _logicstoreareaid;
		private int _storeareaid;
		/// <summary>
		/// 货位ID
		/// </summary>
		public int GoodsSiteID
		{
			set{ _goodssiteid=value;}
			get{return _goodssiteid;}
		}
		/// <summary>
		/// 货位名称
		/// </summary>
		public string GoodsSiteName
		{
			set{ _goodssitename=value;}
			get{return _goodssitename;}
		}
		/// <summary>
		/// 货位类型
		/// </summary>
		public string GoodsSiteType
		{
			set{ _goodssitetype=value;}
			get{return _goodssitetype;}
		}
		/// <summary>
		/// 货位层
		/// </summary>
		public int GoodsSiteLayer
		{
			set{ _goodssitelayer=value;}
			get{return _goodssitelayer;}
		}
		/// <summary>
		/// 货位列
		/// </summary>
		public int GoodsSiteColumn
		{
			set{ _goodssitecolumn=value;}
			get{return _goodssitecolumn;}
		}
		/// <summary>
		/// 货位排
		/// </summary>
		public int GoodsSiteRow
		{
			set{ _goodssiterow=value;}
			get{return _goodssiterow;}
		}
		/// <summary>
		/// 设备ID
		/// </summary>
		public string DeviceID
		{
			set{ _deviceid=value;}
			get{return _deviceid;}
		}
		/// <summary>
		/// 货位存储状态：有货、空货位、空料框
		/// </summary>
		public string GoodsSiteStoreStatus
		{
			set{ _goodssitestorestatus=value;}
			get{return _goodssitestorestatus;}
		}
		/// <summary>
		/// 货位任务完成状态：可用、任务锁定、任务完成、异常
		/// </summary>
		public string GoodsSiteRunStatus
		{
			set{ _goodssiterunstatus=value;}
			get{return _goodssiterunstatus;}
		}
		/// <summary>
		/// 货位出入类型：出库、入库、出入库
		/// </summary>
		public string GoodsSiteInOutType
		{
			set{ _goodssiteinouttype=value;}
			get{return _goodssiteinouttype;}
		}
		/// <summary>
		/// 货位存储类型：单托盘、多托盘
		/// </summary>
		public string GoodsSiteStoreType
		{
			set{ _goodssitestoretype=value;}
			get{return _goodssitestoretype;}
		}
		/// <summary>
		/// 逻辑存储区域ID
		/// </summary>
		public int LogicStoreAreaID
		{
			set{ _logicstoreareaid=value;}
			get{return _logicstoreareaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int StoreAreaID
		{
			set{ _storeareaid=value;}
			get{return _storeareaid;}
		}
		#endregion Model

	}
}

