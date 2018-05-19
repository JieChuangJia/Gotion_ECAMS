/**  版本信息模板在安装目录下，可自行修改。
* Product.cs
*
* 功 能： N/A
* 类 名： Product
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013-12-1 12:50:03   N/A    初版
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
	public partial class ProductModel
	{
		public ProductModel()
		{}
        #region Model
	    private int _productid;
		private string _productcode;
		private string _productname;
		private string _productunit;
		private int? _fulltraynum;
		private string _productsign;
		/// <summary>
		/// 物料ID
		/// </summary>
		public int ProductID
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 物料编码
		/// </summary>
		public string ProductCode
		{
			set{ _productcode=value;}
			get{return _productcode;}
		}
		/// <summary>
		/// 物料描述
		/// </summary>
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 物料单位
		/// </summary>
		public string ProductUnit
		{
			set{ _productunit=value;}
			get{return _productunit;}
		}
		/// <summary>
		/// 满托盘数量
		/// </summary>
		public int? FullTrayNum
		{
			set{ _fulltraynum=value;}
			get{return _fulltraynum;}
		}
		/// <summary>
		/// 物料标记
		/// </summary>
		public string ProductSign
		{
			set{ _productsign=value;}
			get{return _productsign;}
		}
		#endregion Model

	}
}

