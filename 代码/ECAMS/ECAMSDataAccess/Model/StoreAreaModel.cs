/**  版本信息模板在安装目录下，可自行修改。
* StoreArea.cs
*
* 功 能： N/A
* 类 名： StoreArea
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:05   N/A    初版
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
	public partial class StoreAreaModel
	{
		public StoreAreaModel()
		{}
		#region Model
		private int _storeareaid;
		private string _storeareacode;
		private string _storeareaname;
		private string _storeareatype;
		private string _storeareadescribe;
		private int _storeareasign;
		private int _storehouseid;
		/// <summary>
		/// 库区ID
		/// </summary>
		public int StoreAreaID
		{
			set{ _storeareaid=value;}
			get{return _storeareaid;}
		}
		/// <summary>
		/// 库区编码
		/// </summary>
		public string StoreAreaCode
		{
			set{ _storeareacode=value;}
			get{return _storeareacode;}
		}
		/// <summary>
		/// 库区名称
		/// </summary>
		public string StoreAreaName
		{
			set{ _storeareaname=value;}
			get{return _storeareaname;}
		}
		/// <summary>
		/// 库区类型 不同功能区域划分
		/// </summary>
		public string StoreAreaType
		{
			set{ _storeareatype=value;}
			get{return _storeareatype;}
		}
		/// <summary>
		/// 库区描述
		/// </summary>
		public string StoreAreaDescribe
		{
			set{ _storeareadescribe=value;}
			get{return _storeareadescribe;}
		}
		/// <summary>
		/// 库区标记
		/// </summary>
		public int StoreAreaSign
		{
			set{ _storeareasign=value;}
			get{return _storeareasign;}
		}
		/// <summary>
		/// 库房ID
		/// </summary>
		public int StoreHouseID
		{
			set{ _storehouseid=value;}
			get{return _storehouseid;}
		}
		#endregion Model

	}
}

