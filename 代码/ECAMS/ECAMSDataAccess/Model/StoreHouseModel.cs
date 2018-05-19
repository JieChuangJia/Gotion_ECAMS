/**  版本信息模板在安装目录下，可自行修改。
* StoreHouse.cs
*
* 功 能： N/A
* 类 名： StoreHouse
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:06   N/A    初版
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
	public partial class StoreHouseModel
	{
		public StoreHouseModel()
		{}
		#region Model
		private int _storehouseid;
		private string _storehousecode;
		private string _storehousename;
		private string _storehousedescribe;
		private int _storehousesign;
		/// <summary>
		/// 库房ID
		/// </summary>
		public int StoreHouseID
		{
			set{ _storehouseid=value;}
			get{return _storehouseid;}
		}
		/// <summary>
		/// 库房编码
		/// </summary>
		public string StoreHouseCode
		{
			set{ _storehousecode=value;}
			get{return _storehousecode;}
		}
		/// <summary>
		/// 库房名称
		/// </summary>
		public string StoreHouseName
		{
			set{ _storehousename=value;}
			get{return _storehousename;}
		}
		/// <summary>
		/// 库房描述
		/// </summary>
		public string StoreHouseDescribe
		{
			set{ _storehousedescribe=value;}
			get{return _storehousedescribe;}
		}
		/// <summary>
		/// 库房标记
		/// </summary>
		public int StoreHouseSign
		{
			set{ _storehousesign=value;}
			get{return _storehousesign;}
		}
		#endregion Model

	}
}

