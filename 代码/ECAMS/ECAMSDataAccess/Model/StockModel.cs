/**  版本信息模板在安装目录下，可自行修改。
* Stock.cs
*
* 功 能： N/A
* 类 名： Stock
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:04   N/A    初版
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
	/// 物料信息表
	/// </summary>
	[Serializable]
	public partial class StockModel
	{
		public StockModel()
		{}
		#region Model
		private long _stockid;
		private int _goodssiteid;
		private string _traycode;
		private string _fulltraysign;
		private string _remarks;
		/// <summary>
		/// 库存ID
		/// </summary>
		public long StockID
		{
			set{ _stockid=value;}
			get{return _stockid;}
		}
		/// <summary>
		/// 货位ID
		/// </summary>
		public int GoodsSiteID
		{
			set{ _goodssiteid=value;}
			get{return _goodssiteid;}
		}
		/// <summary>
		/// 托盘容器条码
		/// </summary>
		public string TrayCode
		{
			set{ _traycode=value;}
			get{return _traycode;}
		}
		/// <summary>
		/// 满托盘标记
		/// </summary>
		public string FullTraySign
		{
			set{ _fulltraysign=value;}
			get{return _fulltraysign;}
		}
		/// <summary>
		/// 备注信息
		/// </summary>
		public string Remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		#endregion Model

	}
}

